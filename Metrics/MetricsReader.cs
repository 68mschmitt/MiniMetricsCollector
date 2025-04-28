
namespace MiniMetricsCollector.Metrics;

public class MetricsReader(int pid) : IMetricsReader
{
    private readonly int _pid = pid;

    public async Task<List<Metric>> ReadMetricsAsync()
    {
        await Task.Yield();

        var dummmyMetrics = new List<Metric>
        {
            new("CPU Usage", new Random().NextDouble() * 100, "%"),
            new("Memory Usage", new Random().NextDouble() * 512, "MB"),
            new("GC Colletions (Gen 0)", new Random().Next(0, 100) * 100, "count"),
        };

        return dummmyMetrics;
    }
}

