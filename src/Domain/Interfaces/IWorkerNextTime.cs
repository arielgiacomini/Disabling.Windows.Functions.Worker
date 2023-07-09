using Domain.Configuration;

namespace Domain.Interfaces
{
    public interface IWorkerNextTime
    {
        TimeSpan GetWaitingTime(WorkerOptions workerOptions);
    }
}