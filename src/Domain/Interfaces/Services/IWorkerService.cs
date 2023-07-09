using Domain.Configuration;

namespace Domain.Interfaces.Services
{
    public interface IWorkerService
    {
        void Execute(WorkerOptions workerOptions);
    }
}