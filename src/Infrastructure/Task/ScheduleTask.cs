using Domain.Interfaces;

namespace Infrastructure.Task
{
    public class ScheduleTask : IScheduleTask
    {
        private readonly Semaphore semaphore = new(0, int.MaxValue);

        public void ExecuteTaskOnTime(TimeSpan timeSpan, Action callback)
        {
            semaphore.WaitOne(timeSpan);

            callback?.Invoke();
        }
    }
}