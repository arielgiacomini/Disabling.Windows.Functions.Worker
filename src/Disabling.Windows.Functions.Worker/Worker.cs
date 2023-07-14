using Domain.Configuration;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace Disabling.Windows.Functions.Worker
{
    public class Worker : BackgroundService
    {
        private readonly Serilog.ILogger _logger;
        private readonly IScheduleTask _scheduleTask;
        private readonly IWorkerNextTime _workerNextTime;
        private readonly IWorkerService _workerService;
        private readonly WorkerOptions _workerConfig;

        public Worker(
            Serilog.ILogger logger,
            IScheduleTask scheduleTask,
            IWorkerNextTime workerNextTime,
            IWorkerService workerService,
            IOptions<WorkerOptions> workerConfig)
        {
            _logger = logger;
            _scheduleTask = scheduleTask;
            _workerNextTime = workerNextTime;
            _workerService = workerService;
            _workerConfig = workerConfig.Value;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (IsWorkerTaskSchedulerEnabled())
            {
                _ = Task.Run(() =>
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        _scheduleTask.ExecuteTaskOnTime(_workerNextTime.GetWaitingTime(_workerConfig), () => _workerService.Execute(_workerConfig));
                    }
                }, stoppingToken);
            }
            else
            {
                _logger.Information("[{0}] - Processo de Expurgo está desligado na flag principal.", nameof(ExecuteAsync));
            }

            return Task.CompletedTask;
        }

        public bool IsWorkerTaskSchedulerEnabled()
        {
            try
            {
                if (_workerConfig.Enable.HasValue && _workerConfig.Enable.Value)
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
                _logger.Error("[{0}] - Erro ao tentar buscar a config de expurgo. Error: {1}", nameof(IsWorkerTaskSchedulerEnabled), ex);
                return false;
            }
        }
    }
}