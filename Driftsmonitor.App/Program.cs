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

foreach (var log in DataGenerator.GenerateLogs(60)) // 60 > 50, så de 10 eldste overskrives
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

numbers.Add(10);
numbers.Add(20);
numbers.Add(30);
numbers.Add(40);

for (var i = 0; i < 200; i++)
{
    blackBox.Add(new LogEntry(DateTime.Now, "INFO", $"Behandlet ordre {i}"));

    if (i == 137)
    {
        WriteErrorReports.WriteLogErrorReport(blackBox,
        "Databasen svarte ikke",
        entry => $"{entry.Time:HH:mm:ss} [{entry.Level} {entry.Message}]");
        break;
    }
}

requests.Add(new RequestLog("/api/orders", 850, 500));
if (requests.Newest.DurationMs > 500)
{
    WriteErrorReports.WriteLogErrorReport(
        requests,
        "En forespørsel tok for lang tid",
        request => $"{request.Endpoint}: {request.DurationMs} ms, status {request.StatusCode}");
}
// Console.WriteLine("=== Siste sensormålinger ===");
// foreach (var r in readings)
// {
//     Console.WriteLine(r);
// }

// Console.WriteLine($"\nAntall feilmeldinger i blackBox: {blackBox.Count(l => l.Level == "ERROR")}");
// Console.WriteLine($"Snitt-responstid (ms): {requests.Average(r => r.DurationMs):F1}");
// Console.WriteLine($"Tall i bufferet: {string.Join(", ", numbers)}");



watch.Stop();

// Console.WriteLine($"Antall i buffer: {buffer.Count}");
// Console.WriteLine($"Eldste: {buffer.Oldest}");
// Console.WriteLine($"Nyeste: {buffer.Newest}");
Console.WriteLine($"Tid: {watch.ElapsedMilliseconds} ms");
Console.WriteLine($"Minnebruk: {GC.GetTotalMemory(true) / 1024 / 1024} MB");

