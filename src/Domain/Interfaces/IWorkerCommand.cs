using Domain.Configuration;
using Domain.Output;

namespace Domain.Interfaces
{
    public interface IWorkerCommand
    {
        WorkerOutput SetThread(WorkerOptions workerOptions);
    }
}