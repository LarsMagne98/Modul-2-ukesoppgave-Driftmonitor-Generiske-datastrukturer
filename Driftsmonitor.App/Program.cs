using System.Security.Principal;

List<string> window = [];
var watch = System.Diagnostics.Stopwatch.StartNew();

for (var i = 0; i < 10_000_000; i++)
{
    window.Add($"Behandlet ordre {i}");
    if (window.Count > 10_000)
    {
        window.RemoveAt(0);
    }
}

Console.WriteLine($"Antall: {window.Count}");
Console.WriteLine($"Tid: {watch.ElapsedMilliseconds} ms");
Console.WriteLine($"Minnebruk: {GC.GetTotalMemory(true) / 1024 / 1024} MB");