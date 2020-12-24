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
|           NoMessage | core31 | .NET Core 3.1 |     3.355 ns |  0.0435 ns |  0.0651 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | core31 | .NET Core 3.1 |     3.359 ns |  0.0384 ns |  0.0575 ns |   1.00 |    0.01 |      - |     - |     - |         - |
| OneSimpleProperties | core31 | .NET Core 3.1 |    50.590 ns |  0.2377 ns |  0.3557 ns |  15.08 |    0.29 |      - |     - |     - |         - |
|    VariedProperties | core31 | .NET Core 3.1 |   298.099 ns |  2.2720 ns |  3.4007 ns |  88.87 |    1.33 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | core31 | .NET Core 3.1 | 1,315.308 ns | 10.0015 ns | 14.9698 ns | 392.18 |    8.51 | 0.1259 |     - |     - |     800 B |
|                     |        |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |  net48 |      .NET 4.8 |     3.599 ns |  0.0889 ns |  0.1331 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net48 |      .NET 4.8 |     3.623 ns |  0.1094 ns |  0.1638 ns |   1.01 |    0.01 |      - |     - |     - |         - |
| OneSimpleProperties |  net48 |      .NET 4.8 |    87.642 ns |  0.5760 ns |  0.8621 ns |  24.38 |    0.90 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |  net48 |      .NET 4.8 |   376.811 ns |  2.6361 ns |  3.9456 ns | 104.84 |    4.05 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net48 |      .NET 4.8 | 1,925.484 ns | 15.0413 ns | 22.0474 ns | 536.25 |   20.67 | 0.1678 |     - |     - |    1075 B |
|                     |        |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |  net50 | .NET Core 5.0 |     3.855 ns |  0.0313 ns |  0.0469 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net50 | .NET Core 5.0 |     4.395 ns |  0.4816 ns |  0.7209 ns |   1.14 |    0.19 |      - |     - |     - |         - |
| OneSimpleProperties |  net50 | .NET Core 5.0 |    39.970 ns |  0.6034 ns |  0.9031 ns |  10.37 |    0.31 |      - |     - |     - |         - |
|    VariedProperties |  net50 | .NET Core 5.0 |   252.978 ns |  3.2115 ns |  4.7073 ns |  65.59 |    1.33 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net50 | .NET Core 5.0 | 1,174.328 ns |  9.4945 ns | 13.6167 ns | 304.51 |    5.03 | 0.1259 |     - |     - |     800 B |
