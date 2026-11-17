namespace Driftsmonitor.Core;

public class WriteErrorReports
{
    public static void WriteLogErrorReports(RingBuffer<LogEntry> log, string message)
    {
        Console.WriteLine($"FEIL: {message}");
        Console.WriteLine($"---- de {log.Count} siste hendelsene før feilen----");
        foreach (var entry in log)
        {
            Console.WriteLine($"{entry.Time:HH:mm:ss} [{entry.Level}] {entry.Message}");
        }
    }
}