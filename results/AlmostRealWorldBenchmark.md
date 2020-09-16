``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT

IterationCount=3  LaunchCount=1  WarmupCount=3  

```
|                     Method |             Job |       Jit |       Runtime |        Mean |       Error |    StdDev |  Ratio | RatioSD |     Gen 0 |    Gen 1 | Gen 2 |   Allocated |
|--------------------------- |---------------- |---------- |-------------- |------------:|------------:|----------:|-------:|--------:|----------:|---------:|------:|------------:|
| SimulateAAppWithoutSerilog |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    137.3 μs |    10.30 μs |   0.56 μs |   1.00 |    0.00 |    6.3477 |   0.7324 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,468.6 μs |    21.66 μs |   1.19 μs |  10.70 |    0.04 |  439.4531 |  54.6875 |     - |  2702.13 KB |
|  SimulateAAppWithSerilogOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 58,288.6 μs | 7,148.79 μs | 391.85 μs | 424.52 |    4.45 | 9666.6667 | 111.1111 |     - |  59720.6 KB |
|                            |                 |           |               |             |             |           |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog | net48 LegacyJit | LegacyJit |      .NET 4.8 |    193.3 μs |    32.82 μs |   1.80 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,434.1 μs |    22.54 μs |   1.24 μs |   7.42 |    0.07 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 59,789.2 μs |   937.32 μs |  51.38 μs | 309.25 |    3.12 | 9666.6667 | 222.2222 |     - | 59673.58 KB |
|                            |                 |           |               |             |             |           |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |    net48 RyuJit |    RyuJit |      .NET 4.8 |    192.8 μs |     7.80 μs |   0.43 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,460.5 μs |    78.56 μs |   4.31 μs |   7.58 |    0.04 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 59,303.6 μs | 6,323.99 μs | 346.64 μs | 307.62 |    1.58 | 9666.6667 | 222.2222 |     - | 59673.76 KB |
