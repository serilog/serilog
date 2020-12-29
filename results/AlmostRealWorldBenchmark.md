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
| SimulateAAppWithoutSerilog |    core22 RyuJit |    RyuJit | .NET Core 2.2 |    142.5 μs |     49.63 μs |     2.72 μs |   1.00 |    0.00 |    6.3477 |   0.4883 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |    core22 RyuJit |    RyuJit | .NET Core 2.2 |  1,519.6 μs |    445.00 μs |    24.39 μs |  10.67 |    0.38 |  439.4531 |  48.8281 |     - |  2702.13 KB |
|  SimulateAAppWithSerilogOn |    core22 RyuJit |    RyuJit | .NET Core 2.2 | 60,614.5 μs | 16,971.98 μs |   930.29 μs | 425.46 |   12.96 | 9777.7778 | 111.1111 |     - | 60190.09 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |    core31 RyuJit |    RyuJit | .NET Core 3.1 |    138.2 μs |     77.54 μs |     4.25 μs |   1.00 |    0.00 |    6.3477 |   0.7324 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |    core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,465.4 μs |    347.68 μs |    19.06 μs |  10.61 |    0.24 |  439.4531 |  54.6875 |     - |  2702.13 KB |
|  SimulateAAppWithSerilogOn |    core31 RyuJit |    RyuJit | .NET Core 3.1 | 56,266.1 μs | 15,738.43 μs |   862.68 μs | 407.41 |   18.41 | 9666.6667 | 111.1111 |     - | 59720.67 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog | net462 LegacyJit | LegacyJit |    .NET 4.6.2 |    206.3 μs |     63.76 μs |     3.49 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff | net462 LegacyJit | LegacyJit |    .NET 4.6.2 |  1,438.6 μs |    705.71 μs |    38.68 μs |   6.97 |    0.07 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn | net462 LegacyJit | LegacyJit |    .NET 4.6.2 | 59,499.2 μs | 16,318.47 μs |   894.47 μs | 288.58 |    9.27 | 9666.6667 | 222.2222 |     - | 59673.88 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |    net462 RyuJit |    RyuJit |    .NET 4.6.2 |    200.2 μs |     66.41 μs |     3.64 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |    net462 RyuJit |    RyuJit |    .NET 4.6.2 |  1,442.5 μs |    644.69 μs |    35.34 μs |   7.20 |    0.05 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |    net462 RyuJit |    RyuJit |    .NET 4.6.2 | 60,335.3 μs | 12,350.89 μs |   676.99 μs | 301.36 |    3.82 | 9666.6667 | 222.2222 |     - | 59673.58 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog | net472 LegacyJit | LegacyJit |    .NET 4.7.2 |    196.4 μs |     64.48 μs |     3.53 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff | net472 LegacyJit | LegacyJit |    .NET 4.7.2 |  1,446.0 μs |    702.20 μs |    38.49 μs |   7.36 |    0.15 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn | net472 LegacyJit | LegacyJit |    .NET 4.7.2 | 59,726.3 μs | 35,821.14 μs | 1,963.48 μs | 304.11 |    7.88 | 9666.6667 | 222.2222 |     - | 59673.47 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |    net472 RyuJit |    RyuJit |    .NET 4.7.2 |    196.6 μs |     50.17 μs |     2.75 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |    net472 RyuJit |    RyuJit |    .NET 4.7.2 |  1,428.6 μs |    715.62 μs |    39.23 μs |   7.26 |    0.15 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |    net472 RyuJit |    RyuJit |    .NET 4.7.2 | 61,508.8 μs | 42,449.40 μs | 2,326.79 μs | 312.75 |    8.99 | 9666.6667 | 222.2222 |     - | 59673.57 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |  net48 LegacyJit | LegacyJit |      .NET 4.8 |    195.9 μs |     74.98 μs |     4.11 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |  net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,419.6 μs |    852.29 μs |    46.72 μs |   7.25 |    0.24 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |  net48 LegacyJit | LegacyJit |      .NET 4.8 | 59,954.2 μs | 46,182.15 μs | 2,531.40 μs | 306.03 |    8.95 | 9666.6667 | 222.2222 |     - | 59673.45 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |     net48 RyuJit |    RyuJit |      .NET 4.8 |    195.6 μs |     70.91 μs |     3.89 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |     net48 RyuJit |    RyuJit |      .NET 4.8 |  1,449.2 μs |    694.23 μs |    38.05 μs |   7.41 |    0.06 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |     net48 RyuJit |    RyuJit |      .NET 4.8 | 59,849.0 μs | 34,531.12 μs | 1,892.77 μs | 306.00 |    5.18 | 9666.6667 | 222.2222 |     - | 59673.46 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |     net50 RyuJit |    RyuJit | .NET Core 5.0 |    148.0 μs |     73.13 μs |     4.01 μs |   1.00 |    0.00 |    6.3477 |   0.7324 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |     net50 RyuJit |    RyuJit | .NET Core 5.0 |  1,436.7 μs |    627.16 μs |    34.38 μs |   9.71 |    0.48 |  439.4531 |  54.6875 |     - |  2702.13 KB |
|  SimulateAAppWithSerilogOn |     net50 RyuJit |    RyuJit | .NET Core 5.0 | 44,993.2 μs | 23,055.57 μs | 1,263.75 μs | 303.91 |    2.53 | 9750.0000 | 166.6667 |     - | 60189.92 KB |
