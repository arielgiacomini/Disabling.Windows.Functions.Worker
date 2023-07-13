namespace Domain.Configuration
{
    public class WorkerOptions
    {
        public bool? Enable { get; set; }
        public TimeSpan StartTime { get; set; }
        public bool AddDay { get; set; }
        public int MinutesPerRound { get; set; }
    }
}