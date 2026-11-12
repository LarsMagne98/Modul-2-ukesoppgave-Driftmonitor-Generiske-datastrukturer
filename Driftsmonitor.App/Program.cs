using Driftsmonitor.Core;

RingBuffer<string> buffer = new RingBuffer<string>(10_000);


var watch = System.Diagnostics.Stopwatch.StartNew();

for (var i = 0; i < 10_000_000; i++)
{
    buffer.Add($"Behandlet ordre {i}");
}

watch.Stop();

Console.WriteLine($"Antall i buffer: {buffer.Count}");
Console.WriteLine($"Eldste: {buffer.Oldest}");
Console.WriteLine($"Nyeste: {buffer.Newest}");
Console.WriteLine($"Tid: {watch.ElapsedMilliseconds} ms");
Console.WriteLine($"Minnebruk: {GC.GetTotalMemory(true) / 1024 / 1024} MB");