``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.404
  [Host]          : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|              Method |             Job |       Jit |       Runtime |         Mean |     Error |     StdDev |       Median |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |---------------- |---------- |-------------- |-------------:|----------:|-----------:|-------------:|-------:|--------:|-------:|------:|------:|----------:|
|           NoMessage |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     4.825 ns | 0.0114 ns |  0.0157 ns |     4.827 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     4.830 ns | 0.0112 ns |  0.0160 ns |     4.834 ns |   1.00 |    0.00 |      - |     - |     - |         - |
| OneSimpleProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    53.207 ns | 0.3761 ns |  0.5513 ns |    52.922 ns |  11.04 |    0.11 |      - |     - |     - |         - |
|    VariedProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   304.545 ns | 1.6629 ns |  2.4889 ns |   304.499 ns |  63.05 |    0.58 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,334.629 ns | 3.1191 ns |  4.4734 ns | 1,335.395 ns | 276.55 |    1.50 | 0.1259 |     - |     - |     800 B |
|                     |                 |           |               |              |           |            |              |        |         |        |       |       |           |
|           NoMessage | net48 LegacyJit | LegacyJit |      .NET 4.8 |     3.516 ns | 0.0088 ns |  0.0132 ns |     3.513 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |     3.520 ns | 0.0118 ns |  0.0169 ns |     3.516 ns |   1.00 |    0.01 |      - |     - |     - |         - |
| OneSimpleProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |    89.348 ns | 0.1131 ns |  0.1549 ns |    89.337 ns |  25.40 |    0.12 | 0.0050 |     - |     - |      32 B |
|    VariedProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 |   381.277 ns | 1.2195 ns |  1.7875 ns |   381.228 ns | 108.42 |    0.67 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,952.274 ns | 7.8031 ns | 10.9389 ns | 1,951.413 ns | 555.14 |    4.14 | 0.1678 |     - |     - |    1075 B |
|                     |                 |           |               |              |           |            |              |        |         |        |       |       |           |
|           NoMessage |    net48 RyuJit |    RyuJit |      .NET 4.8 |     3.519 ns | 0.0109 ns |  0.0159 ns |     3.517 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |     3.513 ns | 0.0075 ns |  0.0100 ns |     3.512 ns |   1.00 |    0.00 |      - |     - |     - |         - |
| OneSimpleProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |    89.311 ns | 0.2995 ns |  0.4295 ns |    89.230 ns |  25.38 |    0.15 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 |   381.040 ns | 0.9442 ns |  1.3541 ns |   381.253 ns | 108.28 |    0.69 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,961.625 ns | 6.8862 ns | 10.3070 ns | 1,960.005 ns | 557.51 |    3.19 | 0.1678 |     - |     - |    1075 B |
