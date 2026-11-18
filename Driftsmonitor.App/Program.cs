using Driftsmonitor.Core;

var watch = System.Diagnostics.Stopwatch.StartNew();

//  ----------- RingBuffer<T> -----------
Console.WriteLine("--- BUFFER ---");

var buffer = new RingBuffer<int>(10_000);

for (var i = 0; i < 10_000_000; i++)
{
    buffer.Add(i);
}

watch.Stop();

Console.WriteLine($"Antall i buffer: {buffer.Count}");
Console.WriteLine($"Eldste: {buffer.Oldest}");
Console.WriteLine($"Nyeste: {buffer.Newest}");
Console.WriteLine($"Tid: {watch.ElapsedMilliseconds} ms");
Console.WriteLine($"Minnebruk: {GC.GetTotalMemory(true) / 1024 / 1024} MB");


// ---------- LogEntry --------------
Console.WriteLine("");
Console.WriteLine("--- BLACKBOX ---");
var blackBox = new RingBuffer<LogEntry>(50);
blackBox.Add(new LogEntry(DateTime.Now, "Info", "Ei melding"));

foreach (var log in DataGenerator.GenerateLogs(60))
{
    blackBox.Add(log);
}

for (var i = 0; i < 200; i++)
{
    blackBox.Add(new LogEntry(DateTime.Now, "INFO", $"Behandlet ordre {i}"));

    if (i == 137)
    {
        WriteErrorReports.WriteErrorReport(blackBox, "Databasen svarte ikke", entry => entry.ToString());
        break;
    }
}




// -------- SensorReading ---------------
Console.WriteLine("");
Console.WriteLine("--- READINGS ---");

var readings = new RingBuffer<SensorReading>(10);
readings.Add(new SensorReading("ein ID", 13.42, DateTime.Now));
foreach (var reading in DataGenerator.GenerateReadings(10))
{
    readings.Add(reading);
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

    if (i == 9 || i == 10 || i == 49 || i == 99)
    {
        Console.WriteLine($"Etter {i + 1} målinger per sensor:");

        foreach (var (sensorId, window) in windows)
        {
            Console.WriteLine(
                $"{sensorId}: snitt {window.MovingAverage():F1} °C, " +
                $"eldste måling {window.Oldest.Value:F1} °C");
        }
    }
}

// ------------ RequestLog ------------------
Console.WriteLine("");
Console.WriteLine("--- REQUESTS ---");

var requests = new RingBuffer<RequestLog>(100);
requests.Add(new RequestLog("Heima", 100, 8888));

foreach (var req in DataGenerator.GenerateRequests(500))
{
    requests.Add(req);
}
var slowest = requests.SlowestCall();
Console.WriteLine($"Requests over 500: {requests.MoreThan_FiveHundred()}");
Console.WriteLine($"Tregeste kall: {slowest.Endpoint}, {slowest.DurationMs} ms");

// ---------------------------------------------------


