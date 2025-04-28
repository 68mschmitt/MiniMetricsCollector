using MiniMetricsCollector.Metrics;

namespace MiniMetricsCollector.Output;

public class ConsoleOutput : IOutput
{
    public Task WriteMetricsAsync(List<Metric> metrics)
    {
        Console.Clear();
        Console.WriteLine("Mini Metrics Collector - Output:");
        Console.WriteLine("--------------------------------");

        foreach (var metric in metrics)
        {
            Console.WriteLine($"{metric.Name}: {metric.Value} {metric.Unit}");
        }

        return Task.CompletedTask;
    }
}
