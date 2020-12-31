``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.101
  [Host]           : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
  core22 RyuJit    : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  core31 RyuJit    : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net462 LegacyJit : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net462 RyuJit    : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net472 LegacyJit : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net472 RyuJit    : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net48 LegacyJit  : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net48 RyuJit     : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net50 RyuJit     : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT

IterationCount=3  LaunchCount=1  WarmupCount=3  

```
|                     Method |              Job |       Jit |       Runtime |        Mean |        Error |      StdDev |  Ratio | RatioSD |     Gen 0 |    Gen 1 | Gen 2 |   Allocated |
|--------------------------- |----------------- |---------- |-------------- |------------:|-------------:|------------:|-------:|--------:|----------:|---------:|------:|------------:|
| SimulateAAppWithoutSerilog |    core22 RyuJit |    RyuJit | .NET Core 2.2 |    141.5 μs |     20.06 μs |     1.10 μs |   1.00 |    0.00 |    6.3477 |   0.4883 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |    core22 RyuJit |    RyuJit | .NET Core 2.2 |  1,435.2 μs |    490.51 μs |    26.89 μs |  10.14 |    0.27 |  439.4531 |  48.8281 |     - |  2702.13 KB |
|  SimulateAAppWithSerilogOn |    core22 RyuJit |    RyuJit | .NET Core 2.2 | 61,263.4 μs | 28,468.46 μs | 1,560.45 μs | 433.03 |   14.41 | 9777.7778 | 111.1111 |     - | 60190.09 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |    core31 RyuJit |    RyuJit | .NET Core 3.1 |    141.2 μs |     94.53 μs |     5.18 μs |   1.00 |    0.00 |    6.3477 |   0.7324 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |    core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,503.5 μs |    545.35 μs |    29.89 μs |  10.66 |    0.60 |  439.4531 |  54.6875 |     - |  2702.13 KB |
|  SimulateAAppWithSerilogOn |    core31 RyuJit |    RyuJit | .NET Core 3.1 | 57,439.2 μs | 10,655.92 μs |   584.09 μs | 407.15 |   19.23 | 9666.6667 | 111.1111 |     - | 59720.67 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog | net462 LegacyJit | LegacyJit |    .NET 4.6.2 |    193.6 μs |     48.10 μs |     2.64 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff | net462 LegacyJit | LegacyJit |    .NET 4.6.2 |  1,416.8 μs |    795.72 μs |    43.62 μs |   7.32 |    0.26 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn | net462 LegacyJit | LegacyJit |    .NET 4.6.2 | 59,711.8 μs | 29,855.94 μs | 1,636.51 μs | 308.48 |    7.43 | 9625.0000 | 250.0000 |     - | 59673.56 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |    net462 RyuJit |    RyuJit |    .NET 4.6.2 |    193.6 μs |     67.90 μs |     3.72 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |    net462 RyuJit |    RyuJit |    .NET 4.6.2 |  1,466.7 μs |    842.49 μs |    46.18 μs |   7.57 |    0.16 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |    net462 RyuJit |    RyuJit |    .NET 4.6.2 | 59,607.3 μs | 33,653.67 μs | 1,844.67 μs | 307.97 |   14.07 | 9666.6667 | 222.2222 |     - | 59673.66 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog | net472 LegacyJit | LegacyJit |    .NET 4.7.2 |    192.7 μs |     39.58 μs |     2.17 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff | net472 LegacyJit | LegacyJit |    .NET 4.7.2 |  1,400.2 μs |    186.97 μs |    10.25 μs |   7.27 |    0.12 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn | net472 LegacyJit | LegacyJit |    .NET 4.7.2 | 60,239.3 μs | 48,578.43 μs | 2,662.75 μs | 312.75 |   16.83 | 9666.6667 | 222.2222 |     - |  59673.7 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |    net472 RyuJit |    RyuJit |    .NET 4.7.2 |    193.7 μs |     81.91 μs |     4.49 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |    net472 RyuJit |    RyuJit |    .NET 4.7.2 |  1,454.5 μs |    707.39 μs |    38.77 μs |   7.51 |    0.28 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |    net472 RyuJit |    RyuJit |    .NET 4.7.2 | 59,329.3 μs | 28,412.88 μs | 1,557.41 μs | 306.32 |    7.74 | 9666.6667 | 222.2222 |     - |  59673.7 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |  net48 LegacyJit | LegacyJit |      .NET 4.8 |    194.7 μs |     88.93 μs |     4.87 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |  net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,458.0 μs |    433.96 μs |    23.79 μs |   7.49 |    0.22 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |  net48 LegacyJit | LegacyJit |      .NET 4.8 | 59,755.1 μs | 20,658.86 μs | 1,132.38 μs | 307.06 |   13.70 | 9666.6667 | 222.2222 |     - | 59673.78 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |     net48 RyuJit |    RyuJit |      .NET 4.8 |    195.2 μs |     29.22 μs |     1.60 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |     net48 RyuJit |    RyuJit |      .NET 4.8 |  1,406.1 μs |    412.20 μs |    22.59 μs |   7.20 |    0.17 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |     net48 RyuJit |    RyuJit |      .NET 4.8 | 59,259.4 μs | 43,782.79 μs | 2,399.88 μs | 303.69 |   14.50 | 9666.6667 | 222.2222 |     - | 59673.57 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |     net50 RyuJit |    RyuJit | .NET Core 5.0 |    143.0 μs |     77.52 μs |     4.25 μs |   1.00 |    0.00 |    6.3477 |   0.7324 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |     net50 RyuJit |    RyuJit | .NET Core 5.0 |  1,433.4 μs |    499.99 μs |    27.41 μs |  10.03 |    0.44 |  439.4531 |  54.6875 |     - |  2702.13 KB |
|  SimulateAAppWithSerilogOn |     net50 RyuJit |    RyuJit | .NET Core 5.0 | 44,301.4 μs | 19,075.64 μs | 1,045.60 μs | 309.98 |   10.01 | 9750.0000 | 166.6667 |     - | 60189.92 KB |
