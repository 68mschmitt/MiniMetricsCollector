# MiniMetricsCollector

**MiniMetricsCollector** is a lightweight C# tool for monitoring performance metrics of running .NET processes.  
It reads real-time data like CPU usage, memory usage, GC collections, and thread pool statistics directly from live applications.

Designed to be small, fast, and easy to extend — ideal for development, debugging, and learning performance tuning.

---

## ✨ Features

- 🖥️ Monitor live CPU, memory, GC heap size, and thread metrics
- ⚡ Very lightweight — uses native .NET diagnostics
- 📊 Output metrics to a clean, human-readable console display
- 🔧 Configurable refresh intervals
- 📈 Extensible architecture (designed for future HTTP exports, Prometheus metrics, and alerts)

---

## 📦 Tech Stack

- .NET 9 / .NET 8 compatible (targeting .NET Standard APIs)
- `Microsoft.Diagnostics.NETCore.Client` for process diagnostics
- Lightweight async programming model
- Clean modular architecture (Reader ➔ Processor ➔ Output)

---

## 🚀 How to Use

### 1. Install Dependencies

```bash
dotnet restore
```

### 2. Build the Project

```bash
dotnet build
```

### 3. Run the Collector

```bash
dotnet run -- --pid <process_id> --interval <seconds>
```

| Argument | Description |
|:---|:---|
| `--pid` | The PID (Process ID) of the target .NET application |
| `--interval` | Refresh interval in seconds (default = 5) |

**Example:**

```bash
dotnet run -- --pid 12345 --interval 3
```

This attaches to process `12345`, collects metrics, and refreshes the output every 3 seconds.

---

## 📋 Example Output

```
Mini Metrics Collector - Output:
----------------------------------
cpu-usage:            23.5 %
working-set:         512.4 MB
gc-heap-size:        280.1 MB
gen-0-gc-count:         12 count
gen-1-gc-count:          4 count
gen-2-gc-count:          1 count
threadpool-thread-count: 45 threads
```

---

## 🛣️ Roadmap

| Feature | Status |
|:---|:---|
| Attach to running .NET process via PID | ✅ Done |
| Collect EventCounters (CPU, Memory, GC) | ✅ Done |
| Console output with live updates | ✅ Done |
| Refined unit detection (%, MB, threads, etc.) | ✅ Done |
| Continuous metrics streaming | 🚧 Planned |
| HTTP server output for remote monitoring | 🚧 Planned |
| Export Prometheus-compatible metrics | 🚧 Planned |
| Threshold-based alerts (e.g., "CPU > 90%!") | 🚧 Planned |
| Process name finder (attach by name, not PID) | 🚧 Planned |
| Basic CLI argument validation and help | 🚧 Planned |
| Output ASCII charts in console | 🚧 Planned |

---

## 🧰 Project Structure

```plaintext
MiniMetricsCollector/
├── Program.cs          (entry point)
├── Metrics/
│   ├── MetricsReader.cs    (read metrics from process)
│   ├── Metric.cs           (data model)
│   └── MetricType.cs       (optional enum: CPU, Memory, etc.)
├── Processing/
│   └── MetricsProcessor.cs (future: smoothing, aggregation)
├── Output/
│   ├── ConsoleOutput.cs    (writes to terminal)
│   ├── IOutput.cs          (interface for output backends)
│   └── HttpOutput.cs       (future feature)
└── Utils/
    └── PIDFinder.cs        (future: find processes by name)
    ```

    ---

## 🤝 Contributing

Contributions are welcome!  
If you have ideas for features, bug fixes, or improvements:
1. Fork the repository
2. Create a feature branch
3. Submit a pull request with a clear description

*Please keep code modular, and prefer small, atomic PRs.*

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

## 👨‍💻 Author

Built with ❤️ by [Mike Schmitt]  
Contact: [68mschmitt@gmail.com]

---
