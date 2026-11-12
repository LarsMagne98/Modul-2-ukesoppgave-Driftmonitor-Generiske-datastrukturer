Console.WriteLine("Hello, World!");
List<string> events = [];

for (var i = 0; i < 1_000_000; i++)
{
    events.Add($"Behandlet ordre {i}");

}
Console.WriteLine($"Antall: {events.Count}");
Console.WriteLine($"Minnebruk: {GC.GetTotalMemory(true) / 1024 / 1024} MB");