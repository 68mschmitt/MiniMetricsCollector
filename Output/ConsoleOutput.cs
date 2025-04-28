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
            var result = $"{metric.Value,11:F} {metric.Unit}";
            Console.WriteLine($"{metric.Name,-33}: {result,11}");
        }

        return Task.CompletedTask;
    }
}
