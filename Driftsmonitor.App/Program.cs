using Driftsmonitor.Core;

var watch = System.Diagnostics.Stopwatch.StartNew();


var blackBox = new RingBuffer<LogEntry>(50);
var readings = new RingBuffer<SensorReading>(10);
var requests = new RingBuffer<RequestLog>(100);
var numbers = new RingBuffer<int>(3);

blackBox.Add(new LogEntry(DateTime.Now, "Info", "Ei melding"));
readings.Add(new SensorReading("ein ID", 13.42, DateTime.Now));
requests.Add(new RequestLog("Heima", 100, 8888));
numbers.Add(4);

foreach (var log in DataGenerator.GenerateLogs(60))
{
    blackBox.Add(log);
}

foreach (var reading in DataGenerator.GenerateReadings(10))
{
    readings.Add(reading);
}

foreach (var req in DataGenerator.GenerateRequests(25))
{
    requests.Add(req);
}

var windows = new Dictionary<string, RingBuffer<SensorReading>>();

string[] sensors = ["temp-kjeller", "temp-loft", "temp-ute"];
var random = new Random(42);

for (int i = 0; i < 100; i++)
{
    foreach (var sensorId in sensors)
    {
        var reading = new SensorReading(sensorId, 15 + random.NextDouble() * 10, DateTime.Now);

        if (!windows.TryGetValue(sensorId, out var window))
        {
            window = new RingBuffer<SensorReading>(10);
            windows[sensorId] = window;
        }

        window.Add(reading);
    }
}

foreach (var (sensorId, window) in windows)
{
    Console.WriteLine($"{sensorId}: {window.MovingAverage():F1} °C (siste {window.Count} målinger)");
}



watch.Stop();

// Console.WriteLine($"Antall i buffer: {buffer.Count}");
// Console.WriteLine($"Eldste: {buffer.Oldest}");
// Console.WriteLine($"Nyeste: {buffer.Newest}");
Console.WriteLine($"Tid: {watch.ElapsedMilliseconds} ms");
Console.WriteLine($"Minnebruk: {GC.GetTotalMemory(true) / 1024 / 1024} MB");

