using System.Diagnostics.Tracing;
using Microsoft.Diagnostics.NETCore.Client;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;

namespace MiniMetricsCollector.Metrics;

public class MetricsReader(int pid) : IMetricsReader
{
    private readonly DiagnosticsClient _client = new(pid);
    private EventPipeSession? _session;

    public async Task<List<Metric>> ReadMetricsAsync()
    {
        var metrics = new List<Metric>();

        try
        {
            if (_session == null)
            {
                var providers = new List<EventPipeProvider>
                {
                    new(
                            "System.Runtime",
                            EventLevel.Informational,
                            (long) ClrTraceEventParser.Keywords.None,
                            new Dictionary<string, string>
                            {
                                { "EventCounterIntervalSec", "1" }
                            })
                };

                _session = _client.StartEventPipeSession(providers);
            }

            using var source = new EventPipeEventSource(_session.EventStream);

            source.Dynamic.All += traceEvent =>
            {
                if (traceEvent.EventName.Equals("EventCounters"))
                {
                    var payloadVal = (IDictionary<string, object>)traceEvent.PayloadValue(0);
                    var payloadFields = (IDictionary<string, object>)payloadVal["Payload"];

                    var name = (payloadFields["Name"]?.ToString()) ?? throw new ArgumentNullException("Name is null for metric");
                    var value = Convert.ToDouble(payloadFields["Mean"]);

                    metrics.Add(new Metric(name, value, DetectUnit(name)));
                }
            };

            var task = Task.Run(source.Process);
            await Task.Delay(1000);
            _session.Stop();

            _session.Dispose();
            _session = null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading metrics: {ex.Message}");
        }

        return metrics;
    }

    private static readonly (string Pattern, string Unit)[] MetricsPattern =
    [
        ("cpu", "%"),
        ("working-set", "MB"),
        ("memory", "MB"),
        ("heap-size", "MB"),
        ("gc-count", "count"),
        ("collection-count", "count"),
        ("threadpool", "threads"),
        ("thread-count", "threads"),
        ("lock-contention", "contentions/sec"),
        ("alloc-rate", "bytes/sec"),
        ("exceptions", "exceptions/sec"),
        ("request-duration", "MS"),
        ("duration", "MS"),
        ("connections-established", "connections"),
    ];

    private static string DetectUnit(string metricName)
    {
        if (string.IsNullOrEmpty(metricName))
            return "";

        string lowerName = metricName.ToLowerInvariant();

        foreach (var (pattern, unit) in MetricsPattern)
        {
            if (lowerName.Contains(pattern))
                return unit;
        }

        return "";
    }


}
