``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]          : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT

IterationCount=3  LaunchCount=1  WarmupCount=3  

```
|                     Method |             Job |       Jit |       Runtime |         Mean |        Error |       StdDev |  Ratio | RatioSD |     Gen 0 |    Gen 1 | Gen 2 |   Allocated |
|--------------------------- |---------------- |---------- |-------------- |-------------:|-------------:|-------------:|-------:|--------:|----------:|---------:|------:|------------:|
| SimulateAAppWithoutSerilog |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     268.1 μs |     799.2 μs |     43.81 μs |   1.00 |    0.00 |    6.3477 |   0.7324 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   2,322.1 μs |     628.1 μs |     34.43 μs |   8.80 |    1.31 |  437.5000 |  54.6875 |     - |  2702.13 KB |
|  SimulateAAppWithSerilogOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  96,252.1 μs | 374,141.4 μs | 20,507.95 μs | 357.37 |   20.56 | 9714.2857 | 142.8571 |     - | 59720.68 KB |
|                            |                 |           |               |              |              |              |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog | net48 LegacyJit | LegacyJit |      .NET 4.8 |     339.6 μs |     514.4 μs |     28.19 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff | net48 LegacyJit | LegacyJit |      .NET 4.8 |   2,676.6 μs |   3,108.7 μs |    170.40 μs |   7.92 |    0.89 |  324.2188 |  54.6875 |     - |  2015.73 KB |
|  SimulateAAppWithSerilogOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 102,653.4 μs |  71,593.9 μs |  3,924.30 μs | 303.90 |   31.57 | 9666.6667 | 166.6667 |     - | 59674.55 KB |
|                            |                 |           |               |              |              |              |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |    net48 RyuJit |    RyuJit |      .NET 4.8 |     295.7 μs |     606.9 μs |     33.27 μs |   1.00 |    0.00 |   20.5078 |   2.9297 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |    net48 RyuJit |    RyuJit |      .NET 4.8 |   2,193.6 μs |     782.0 μs |     42.87 μs |   7.48 |    0.86 |  324.2188 |  54.6875 |     - |  2015.73 KB |
|  SimulateAAppWithSerilogOn |    net48 RyuJit |    RyuJit |      .NET 4.8 |  83,636.1 μs |  26,597.7 μs |  1,457.91 μs | 285.15 |   31.62 | 9666.6667 | 166.6667 |     - | 59674.55 KB |
