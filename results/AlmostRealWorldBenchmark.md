``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=3  LaunchCount=1  WarmupCount=3  

```
|                     Method |             Job |       Jit |       Runtime |        Mean |        Error |      StdDev |  Ratio | RatioSD |     Gen 0 |    Gen 1 | Gen 2 |   Allocated |
|--------------------------- |---------------- |---------- |-------------- |------------:|-------------:|------------:|-------:|--------:|----------:|---------:|------:|------------:|
| SimulateAAppWithoutSerilog |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    153.7 μs |     69.22 μs |     3.79 μs |   1.00 |    0.00 |    6.3477 |   0.7324 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,536.6 μs |    491.17 μs |    26.92 μs |  10.00 |    0.33 |  439.4531 |  54.6875 |     - |  2702.13 KB |
|  SimulateAAppWithSerilogOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 58,859.0 μs | 46,973.77 μs | 2,574.79 μs | 383.27 |   26.06 | 9666.6667 | 111.1111 |     - | 59720.67 KB |
|                            |                 |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog | net48 LegacyJit | LegacyJit |      .NET 4.8 |    195.8 μs |     82.52 μs |     4.52 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,450.0 μs |    508.78 μs |    27.89 μs |   7.41 |    0.31 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 60,147.5 μs | 36,026.42 μs | 1,974.73 μs | 307.48 |   17.23 | 9666.6667 | 222.2222 |     - | 59673.59 KB |
|                            |                 |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |    net48 RyuJit |    RyuJit |      .NET 4.8 |    196.2 μs |     48.71 μs |     2.67 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,432.6 μs |    614.75 μs |    33.70 μs |   7.30 |    0.16 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 60,489.8 μs | 18,188.54 μs |   996.98 μs | 308.37 |    5.35 | 9666.6667 | 222.2222 |     - | 59673.39 KB |
