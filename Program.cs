using MiniMetricsCollector.Metrics;
using MiniMetricsCollector.Output;

namespace MiniMetricsCollector;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: MiniMetricsCollector --pid <process_id> [--interval <seconds>]");
            return;
        }

        int pid = 0;
        int intervalSeconds = 5; // Default update every 5s

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--pid" && i + 1 < args.Length)
                pid = int.Parse(args[i + 1]);
            else if (args[i] == "--interval" && i + 1 < args.Length)
                intervalSeconds = int.Parse(args[i + 1]);
        }

        if (pid == 0)
        {
            Console.WriteLine("Invalid or missing PID.");
            return;
        }

        IMetricsReader reader = new MetricsReader(pid);
        IOutput output = new ConsoleOutput();

        Console.WriteLine($"Starting monitoring PID {pid} every {intervalSeconds} seconds...");

        while (true)
        {
            var metrics = await reader.ReadMetricsAsync();
            await output.WriteMetricsAsync(metrics);
            await Task.Delay(intervalSeconds * 1000);
        }
    }
}
