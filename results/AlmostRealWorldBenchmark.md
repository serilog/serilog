``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.403
  [Host]          : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=3  LaunchCount=1  WarmupCount=3  

```
|                     Method |             Job |       Jit |       Runtime |        Mean |        Error |    StdDev |  Ratio | RatioSD |     Gen 0 |    Gen 1 | Gen 2 |   Allocated |
|--------------------------- |---------------- |---------- |-------------- |------------:|-------------:|----------:|-------:|--------:|----------:|---------:|------:|------------:|
| SimulateAAppWithoutSerilog |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    140.3 μs |     29.39 μs |   1.61 μs |   1.00 |    0.00 |    6.3477 |   0.7324 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,485.7 μs |    137.96 μs |   7.56 μs |  10.59 |    0.11 |  439.4531 |  54.6875 |     - |  2702.13 KB |
|  SimulateAAppWithSerilogOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 58,592.2 μs | 11,050.04 μs | 605.69 μs | 417.61 |    8.59 | 9666.6667 | 111.1111 |     - | 59720.65 KB |
|                            |                 |           |               |             |              |           |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog | net48 LegacyJit | LegacyJit |      .NET 4.8 |    194.2 μs |     22.84 μs |   1.25 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,398.2 μs |    199.31 μs |  10.92 μs |   7.20 |    0.03 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 58,961.1 μs | 11,493.86 μs | 630.02 μs | 303.59 |    4.27 | 9666.6667 | 222.2222 |     - | 59673.75 KB |
|                            |                 |           |               |             |              |           |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |    net48 RyuJit |    RyuJit |      .NET 4.8 |    193.0 μs |     14.08 μs |   0.77 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,372.8 μs |     28.40 μs |   1.56 μs |   7.11 |    0.02 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 59,297.3 μs |  9,635.45 μs | 528.15 μs | 307.18 |    3.71 | 9666.6667 | 222.2222 |     - | 59673.65 KB |
