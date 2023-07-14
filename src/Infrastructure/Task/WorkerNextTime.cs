using Domain.Configuration;
using Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace Infrastructure.Task
{
    public class WorkerNextTime : IWorkerNextTime
    {
        private readonly Serilog.ILogger _logger;
        private readonly WorkerOptions _workerOptionsConfig;

        public WorkerNextTime(Serilog.ILogger logger,
            IOptions<WorkerOptions> workerConfig)
        {
            _logger = logger;
            _workerOptionsConfig = workerConfig.Value;
        }

        public TimeSpan GetWaitingTime(WorkerOptions workerOptions)
        {
            try
            {
                DateTime dataDeExecucao = GetNextExecutionDate(
                    workerOptions.StartTime.Hours,
                    workerOptions.StartTime.Minutes,
                    workerOptions.AddDay,
                    workerOptions.MinutesPerRound);

                _logger.Information("Data programada para próximas execução Data/Hora: [{0}]", dataDeExecucao);

                return dataDeExecucao.Subtract(DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.Error("[{0}] - Erro ao buscar a próxima data de execução. Error: {1}", nameof(GetWaitingTime), ex);

                throw;
            }
        }

        public static DateTime GetNextExecutionDate(int hour, int minute, bool addDay = true, int? minuteToAdd = null)
        {
            var dataDeExecucao = new DateTime(
                year: DateTime.Now.Year,
                month: DateTime.Now.Month,
                day: DateTime.Now.Day,
                hour: hour,
                minute: minute,
                second: 0);

            if (addDay)
            {
                if (DateTime.Now > dataDeExecucao)
                    dataDeExecucao = dataDeExecucao.AddDays(1);
            }

            if (minuteToAdd.HasValue)
            {
                dataDeExecucao = DateTime.Now.AddMinutes(minuteToAdd.Value);
            }

            return dataDeExecucao;
        }
    }
}