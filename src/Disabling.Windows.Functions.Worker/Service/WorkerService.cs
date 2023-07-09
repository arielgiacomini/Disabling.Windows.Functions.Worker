using Domain.Configuration;
using Domain.Interfaces;
using Domain.Interfaces.Services;

namespace Purge.Worker.Service
{
    public class WorkerService : IWorkerService
    {
        private readonly Serilog.ILogger _logger;
        private readonly IWorkerCommand _purgeCommand;

        public WorkerService(
            Serilog.ILogger logger,
            IWorkerCommand purgeCommand)
        {
            _logger = logger;
            _purgeCommand = purgeCommand;
        }

        public void Execute(WorkerOptions workerOptions)
        {
            try
            {
                _purgeCommand.SetThread(workerOptions);
            }
            catch (Exception ex)
            {
                _logger.Error("[{0}] - Erro ao efetuar chamada no Command. Error: {1} ", nameof(Execute), ex);
                throw;
            }
        }
    }
}