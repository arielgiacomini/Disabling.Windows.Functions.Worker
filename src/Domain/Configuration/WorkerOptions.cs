namespace Domain.Configuration
{
    public class WorkerOptions
    {
        public bool? Enable { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}