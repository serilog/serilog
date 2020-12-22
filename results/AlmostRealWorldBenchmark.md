``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.404
  [Host]          : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT

IterationCount=3  LaunchCount=1  WarmupCount=3  

```
|                     Method |             Job |       Jit |       Runtime |        Mean |        Error |    StdDev |  Ratio | RatioSD |     Gen 0 |    Gen 1 | Gen 2 |   Allocated |
|--------------------------- |---------------- |---------- |-------------- |------------:|-------------:|----------:|-------:|--------:|----------:|---------:|------:|------------:|
| SimulateAAppWithoutSerilog |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    149.9 μs |     14.59 μs |   0.80 μs |   1.00 |    0.00 |    6.3477 |   0.7324 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,557.7 μs |     47.58 μs |   2.61 μs |  10.39 |    0.05 |  439.4531 |  54.6875 |     - |  2702.13 KB |
|  SimulateAAppWithSerilogOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 60,929.9 μs |  2,158.87 μs | 118.34 μs | 406.58 |    1.95 | 9666.6667 | 111.1111 |     - | 59720.67 KB |
|                            |                 |           |               |             |              |           |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog | net48 LegacyJit | LegacyJit |      .NET 4.8 |    206.6 μs |      5.93 μs |   0.32 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,518.8 μs |     67.75 μs |   3.71 μs |   7.35 |    0.02 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 62,946.8 μs | 13,204.56 μs | 723.79 μs | 304.62 |    3.52 | 9666.6667 | 222.2222 |     - | 59673.67 KB |
|                            |                 |           |               |             |              |           |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |    net48 RyuJit |    RyuJit |      .NET 4.8 |    205.9 μs |     19.33 μs |   1.06 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,509.3 μs |    108.02 μs |   5.92 μs |   7.33 |    0.04 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 62,631.4 μs |  5,433.74 μs | 297.84 μs | 304.13 |    2.27 | 9625.0000 | 250.0000 |     - | 59673.54 KB |
