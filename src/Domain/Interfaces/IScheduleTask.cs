namespace Domain.Interfaces
{
    public interface IScheduleTask
    {
        void ExecuteTaskOnTime(TimeSpan timeSpan, Action callback);
    }
}