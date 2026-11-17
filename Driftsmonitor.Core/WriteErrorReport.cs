namespace Driftsmonitor.Core;

public class WriteErrorReports
{
    public static void WriteLogErrorReport<T>(
        RingBuffer<T> enteries,
        string message,
        Func<T, string> formatEntry)
    {
        Console.WriteLine($"FEIL: {message}");
        Console.WriteLine($"---- de {enteries.Count} siste hendelsene før feilen ----");

        foreach (var entry in enteries)
        {
            Console.WriteLine(formatEntry(entry));
        }
    }

}