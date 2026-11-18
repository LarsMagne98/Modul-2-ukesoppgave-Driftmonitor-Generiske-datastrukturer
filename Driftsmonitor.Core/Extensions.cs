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
}