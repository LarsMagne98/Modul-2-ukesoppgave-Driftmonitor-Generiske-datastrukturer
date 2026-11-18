namespace Driftsmonitor.Core;

public static class DataGenerator
{
    private static readonly string[] LogLevels = ["INFO", "WARNING", "ERROR"];
    private static readonly string[] LogMessages =
    [
        "Tilkobling etablert",
        "Høy CPU-last oppdaget",
        "Timeout mot database",
        "Bakgrunnsjobb fullført"
    ];

    private static readonly string[] SensorId = ["TEMP-01", "TEMP-02", "HUM-01", "PRESS-04"];
    private static readonly string[] Endpoints = ["/api/health", "/api/sensors", "/api/logs", "/api/auth"];
    private static readonly int[] StatusCodes = [200, 200, 200, 400, 404, 500];

    public static IEnumerable<LogEntry> GenerateLogs(int count)
    {
        var startTime = DateTime.Now.AddMinutes(-count);

        for (int i = 0; i < count; i++)
        {
            string level = LogLevels[Random.Shared.Next(LogLevels.Length)];
            string message = LogMessages[Random.Shared.Next(LogMessages.Length)];

            yield return new LogEntry(startTime.AddMinutes(i), level, message);
        }
    }

    public static IEnumerable<SensorReading> GenerateReadings(int count)
    {
        var startTime = DateTime.Now.AddSeconds(-count);

        for (int i = 0; i < count; i++)
        {
            string sensor = SensorId[Random.Shared.Next(SensorId.Length)];
            double value = Math.Round(18.0 + Random.Shared.NextDouble() * 10, 2);

            yield return new SensorReading(sensor, value, startTime.AddSeconds(i * 5));
        }
    }

    public static IEnumerable<RequestLog> GenerateRequests(int count)
    {
        int seed = 1;
        Random rnd = new Random(seed);
        for (int i = 0; i < count; i++)
        {
            string endpoint = Endpoints[rnd.Next(Endpoints.Length)];
            int durationMs = rnd.Next(50, 901);
            int statusCode = StatusCodes[rnd.Next(StatusCodes.Length)];

            yield return new RequestLog(endpoint, durationMs, statusCode);
        }
    }

}
