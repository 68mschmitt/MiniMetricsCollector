namespace MiniMetricsCollector.Metrics;

public interface IMetricsReader
{
    public Task<List<Metric>> ReadMetricsAsync();
}
