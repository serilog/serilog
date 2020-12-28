``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.101
  [Host] : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
  core31 : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48  : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net50  : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT

Jit=RyuJit  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
|              Method |    Job |       Runtime |         Mean |      Error |     StdDev |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |------- |-------------- |-------------:|-----------:|-----------:|-------:|--------:|-------:|------:|------:|----------:|
|           NoMessage | core31 | .NET Core 3.1 |     4.135 ns |  0.2594 ns |  0.3802 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | core31 | .NET Core 3.1 |     3.623 ns |  0.0453 ns |  0.0664 ns |   0.88 |    0.09 |      - |     - |     - |         - |
| OneSimpleProperties | core31 | .NET Core 3.1 |    50.138 ns |  0.9207 ns |  1.3780 ns |  12.23 |    1.01 |      - |     - |     - |         - |
|    VariedProperties | core31 | .NET Core 3.1 |   289.697 ns |  3.2555 ns |  4.7719 ns |  70.66 |    6.82 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | core31 | .NET Core 3.1 | 1,289.510 ns | 13.0482 ns | 19.1258 ns | 314.61 |   31.31 | 0.1259 |     - |     - |     800 B |
|                     |        |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |  net48 |      .NET 4.8 |     5.317 ns |  0.0426 ns |  0.0624 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net48 |      .NET 4.8 |     5.291 ns |  0.0461 ns |  0.0689 ns |   0.99 |    0.01 |      - |     - |     - |         - |
| OneSimpleProperties |  net48 |      .NET 4.8 |    86.367 ns |  0.7661 ns |  1.1467 ns |  16.25 |    0.26 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |  net48 |      .NET 4.8 |   371.944 ns |  3.4638 ns |  5.1845 ns |  70.00 |    1.47 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net48 |      .NET 4.8 | 1,877.933 ns | 11.4659 ns | 16.4441 ns | 353.30 |    5.16 | 0.1698 |     - |     - |    1075 B |
|                     |        |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |  net50 | .NET Core 5.0 |     3.801 ns |  0.0429 ns |  0.0642 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net50 | .NET Core 5.0 |     4.001 ns |  0.0498 ns |  0.0745 ns |   1.05 |    0.03 |      - |     - |     - |         - |
| OneSimpleProperties |  net50 | .NET Core 5.0 |    38.273 ns |  0.3187 ns |  0.4770 ns |  10.07 |    0.24 |      - |     - |     - |         - |
|    VariedProperties |  net50 | .NET Core 5.0 |   247.739 ns |  3.5262 ns |  5.2779 ns |  65.18 |    1.53 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net50 | .NET Core 5.0 | 1,126.786 ns | 12.8592 ns | 19.2470 ns | 296.49 |    6.93 | 0.1259 |     - |     - |     800 B |
