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
| SimulateAAppWithoutSerilog |    core22 RyuJit |    RyuJit | .NET Core 2.2 |    146.7 μs |     98.74 μs |     5.41 μs |   1.00 |    0.00 |    6.3477 |   0.4883 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |    core22 RyuJit |    RyuJit | .NET Core 2.2 |  1,492.8 μs |    245.78 μs |    13.47 μs |  10.19 |    0.38 |  439.4531 |  48.8281 |     - |  2702.13 KB |
|  SimulateAAppWithSerilogOn |    core22 RyuJit |    RyuJit | .NET Core 2.2 | 60,978.4 μs | 16,938.45 μs |   928.45 μs | 416.22 |   19.19 | 9777.7778 | 111.1111 |     - | 60190.09 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |    core31 RyuJit |    RyuJit | .NET Core 3.1 |    148.4 μs |     85.03 μs |     4.66 μs |   1.00 |    0.00 |    6.3477 |   0.7324 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |    core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,529.4 μs |    351.08 μs |    19.24 μs |  10.31 |    0.43 |  439.4531 |  54.6875 |     - |  2702.13 KB |
|  SimulateAAppWithSerilogOn |    core31 RyuJit |    RyuJit | .NET Core 3.1 | 56,511.4 μs | 20,529.66 μs | 1,125.30 μs | 381.11 |   19.37 | 9666.6667 | 111.1111 |     - | 59720.67 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog | net462 LegacyJit | LegacyJit |    .NET 4.6.2 |    195.6 μs |     49.17 μs |     2.70 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff | net462 LegacyJit | LegacyJit |    .NET 4.6.2 |  1,420.2 μs |    323.59 μs |    17.74 μs |   7.26 |    0.16 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn | net462 LegacyJit | LegacyJit |    .NET 4.6.2 | 57,823.8 μs | 21,166.19 μs | 1,160.19 μs | 295.71 |   10.08 | 9666.6667 | 222.2222 |     - | 59673.48 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |    net462 RyuJit |    RyuJit |    .NET 4.6.2 |    194.3 μs |     47.91 μs |     2.63 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |    net462 RyuJit |    RyuJit |    .NET 4.6.2 |  1,422.2 μs |    306.42 μs |    16.80 μs |   7.32 |    0.17 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |    net462 RyuJit |    RyuJit |    .NET 4.6.2 | 58,433.2 μs |  8,318.88 μs |   455.99 μs | 300.78 |    2.31 | 9666.6667 | 222.2222 |     - | 59673.46 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog | net472 LegacyJit | LegacyJit |    .NET 4.7.2 |    193.9 μs |     36.90 μs |     2.02 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff | net472 LegacyJit | LegacyJit |    .NET 4.7.2 |  1,434.4 μs |    399.92 μs |    21.92 μs |   7.40 |    0.13 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn | net472 LegacyJit | LegacyJit |    .NET 4.7.2 | 59,546.8 μs | 27,431.99 μs | 1,503.64 μs | 307.17 |   10.89 | 9666.6667 | 222.2222 |     - | 59673.59 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |    net472 RyuJit |    RyuJit |    .NET 4.7.2 |    192.5 μs |     51.55 μs |     2.83 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |    net472 RyuJit |    RyuJit |    .NET 4.7.2 |  1,435.2 μs |    189.23 μs |    10.37 μs |   7.46 |    0.07 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |    net472 RyuJit |    RyuJit |    .NET 4.7.2 | 58,715.1 μs | 23,257.76 μs | 1,274.84 μs | 305.03 |    7.34 | 9666.6667 | 222.2222 |     - | 59673.46 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |  net48 LegacyJit | LegacyJit |      .NET 4.8 |    191.8 μs |     45.19 μs |     2.48 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |  net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,438.9 μs |    503.59 μs |    27.60 μs |   7.50 |    0.19 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |  net48 LegacyJit | LegacyJit |      .NET 4.8 | 58,523.8 μs | 28,383.84 μs | 1,555.81 μs | 305.20 |   12.07 | 9666.6667 | 222.2222 |     - | 59673.45 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |     net48 RyuJit |    RyuJit |      .NET 4.8 |    195.9 μs |     41.39 μs |     2.27 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |     net48 RyuJit |    RyuJit |      .NET 4.8 |  1,444.3 μs |    420.47 μs |    23.05 μs |   7.37 |    0.19 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |     net48 RyuJit |    RyuJit |      .NET 4.8 | 58,137.5 μs | 21,657.83 μs | 1,187.14 μs | 296.78 |    5.27 | 9666.6667 | 222.2222 |     - | 59673.42 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |     net50 RyuJit |    RyuJit | .NET Core 5.0 |    148.1 μs |     20.26 μs |     1.11 μs |   1.00 |    0.00 |    6.3477 |   0.7324 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |     net50 RyuJit |    RyuJit | .NET Core 5.0 |  1,438.1 μs |    203.21 μs |    11.14 μs |   9.71 |    0.09 |  439.4531 |  54.6875 |     - |  2702.13 KB |
|  SimulateAAppWithSerilogOn |     net50 RyuJit |    RyuJit | .NET Core 5.0 | 44,217.9 μs |  7,670.14 μs |   420.43 μs | 298.52 |    4.60 | 9818.1818 | 181.8182 |     - | 60190.37 KB |
