namespace Driftsmonitor.Core;

public static class Extensions
{
    extension(RingBuffer<SensorReading> window)
    {
        public double MovingAverage()
        {
            if (window.Count == 0) return 0;

            return window.Average(reading => reading.Value);
        }
    }

    extension(RingBuffer<RequestLog> durations)
    {
        public int MoreThan_FiveHundred()
        {
            return durations.Count(log => log.DurationMs > 500);
        }

        public RequestLog SlowestCall()
        {
            return durations.MaxBy(log => log.DurationMs)
            ?? throw new InvalidOperationException("Bufferen er tom");
        }
    }
}