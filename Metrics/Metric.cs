namespace MiniMetricsCollector.Metrics;

public class Metric(string name, double value, string unit)
{
    public string Name { get; set; } = name;
    public double Value { get; set; } = value;
    public string Unit { get; set; } = unit;
}
