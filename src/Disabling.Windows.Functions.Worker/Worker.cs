using Domain.Configuration;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace Purge.Worker
{
    public class Worker : BackgroundService
    {
        private readonly Serilog.ILogger _logger;
        private readonly IScheduleTask _scheduleTask;
        private readonly IWorkerNextTime _purgeNextTime;
        private readonly IWorkerService _purgeService;
        private readonly WorkerOptions _purgeConfig;

        public Worker(
            Serilog.ILogger logger,
            IScheduleTask scheduleTask,
            IWorkerNextTime purgeNextTime,
            IWorkerService purgeService,
            IOptions<WorkerOptions> purgeConfig)
        {
            _logger = logger;
            _scheduleTask = scheduleTask;
            _purgeNextTime = purgeNextTime;
            _purgeService = purgeService;
            _purgeConfig = purgeConfig.Value;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (IsPurgeTaskSchedulerEnabled())
            {
                _ = Task.Run(() =>
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        _scheduleTask.ExecuteTaskOnTime(_purgeNextTime.GetWaitingTime(new WorkerOptions()), () => _purgeService.Execute(new WorkerOptions()));
                    }
                }, stoppingToken);
            }
            else
            {
                _logger.Information("[{0}] - Processo de Expurgo está desligado na flag principal.", nameof(ExecuteAsync));
            }

            return Task.CompletedTask;
        }

        public bool IsPurgeTaskSchedulerEnabled()
        {
            try
            {
                if (_purgeConfig.Enable.HasValue && _purgeConfig.Enable.Value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("[{0}] - Erro ao tentar buscar a config de expurgo. Error: {1}", nameof(IsPurgeTaskSchedulerEnabled), ex);
                return false;
            }
        }
    }
}