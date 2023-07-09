using Domain.Configuration;
using Domain.Output;

namespace Domain.Interfaces
{
    public interface IWorkerCommand
    {
        PurgeOutput SetThread(WorkerOptions purgeSprecificBySystem);
    }
}