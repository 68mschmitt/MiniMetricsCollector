using MiniMetricsCollector.Metrics;

namespace MiniMetricsCollector.Output;

public interface IOutput
{
    public Task WriteMetricsAsync(List<Metric> metrics);
}

