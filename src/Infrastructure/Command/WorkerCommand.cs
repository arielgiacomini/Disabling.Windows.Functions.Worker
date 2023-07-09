using Domain.Configuration;
using Domain.Interfaces;
using Domain.Output;

namespace Infrastructure.Command
{
    public class WorkerCommand : IWorkerCommand
    {
        private readonly Serilog.ILogger _logger;

        public WorkerCommand(
            Serilog.ILogger logger)
        {
            _logger = logger;
        }

        public PurgeOutput SetThread(WorkerOptions purgeSprecificBySystem)
        {
            try
            {
                PurgeOutput output = new();

                return output;
            }
            catch (Exception ex)
            {
                _logger.Error("[{0}] - Erro ao efetuar a chamada - Error: {1}", nameof(SetThread), ex);
                throw;
            }
        }
    }
}