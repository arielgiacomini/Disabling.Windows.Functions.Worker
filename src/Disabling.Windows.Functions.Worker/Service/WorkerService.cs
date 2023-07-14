using Domain.Configuration;
using Domain.Interfaces;
using Domain.Interfaces.Services;

namespace Disabling.Windows.Functions.Worker.Service
{
    public class WorkerService : IWorkerService
    {
        private readonly Serilog.ILogger _logger;
        private readonly IWorkerCommand _workerCommand;

        public WorkerService(
            Serilog.ILogger logger,
            IWorkerCommand workerCommand)
        {
            _logger = logger;
            _workerCommand = workerCommand;
        }

        public void Execute(WorkerOptions workerOptions)
        {
            try
            {
                _workerCommand.SetThread(workerOptions);
            }
            catch (Exception ex)
            {
                _logger.Error("[{0}] - Erro ao efetuar chamada no Command. Error: {1} ", nameof(Execute), ex);
                throw;
            }
        }
    }
}