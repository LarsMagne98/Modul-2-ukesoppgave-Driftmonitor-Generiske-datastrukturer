namespace Driftsmonitor.Core;

public class WriteErrorReports
{
    public static void WriteErrorReport<T>(
        RingBuffer<T> entries,
        string message,
        Func<T, string> formatEntry)
    {
        Console.WriteLine($"FEIL: {message}");
        Console.WriteLine($"---- de {entries.Count} siste hendelsene før feilen ----");

        foreach (var entry in entries)
        {
            Console.WriteLine(formatEntry(entry));
        }
    }

}