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
|           NoMessage | core31 | .NET Core 3.1 |     3.376 ns |  0.1166 ns |  0.1745 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | core31 | .NET Core 3.1 |     3.372 ns |  0.1201 ns |  0.1797 ns |   1.00 |    0.10 |      - |     - |     - |         - |
| OneSimpleProperties | core31 | .NET Core 3.1 |    48.629 ns |  0.3847 ns |  0.5758 ns |  14.44 |    0.68 |      - |     - |     - |         - |
|    VariedProperties | core31 | .NET Core 3.1 |   283.656 ns |  3.5762 ns |  5.3526 ns |  84.28 |    5.13 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | core31 | .NET Core 3.1 | 1,260.729 ns | 13.6048 ns | 20.3630 ns | 374.36 |   18.48 | 0.1259 |     - |     - |     800 B |
|                     |        |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |  net48 |      .NET 4.8 |     3.487 ns |  0.1116 ns |  0.1671 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net48 |      .NET 4.8 |     3.333 ns |  0.0444 ns |  0.0665 ns |   0.96 |    0.05 |      - |     - |     - |         - |
| OneSimpleProperties |  net48 |      .NET 4.8 |    84.240 ns |  0.7313 ns |  1.0719 ns |  24.16 |    1.20 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |  net48 |      .NET 4.8 |   360.155 ns |  2.3509 ns |  3.4460 ns | 103.31 |    5.10 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net48 |      .NET 4.8 | 1,841.409 ns | 19.0684 ns | 28.5407 ns | 529.17 |   24.88 | 0.1698 |     - |     - |    1075 B |
|                     |        |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |  net50 | .NET Core 5.0 |     3.803 ns |  0.0761 ns |  0.1116 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net50 | .NET Core 5.0 |     3.731 ns |  0.0768 ns |  0.1150 ns |   0.98 |    0.04 |      - |     - |     - |         - |
| OneSimpleProperties |  net50 | .NET Core 5.0 |    38.489 ns |  0.3308 ns |  0.4951 ns |  10.13 |    0.34 |      - |     - |     - |         - |
|    VariedProperties |  net50 | .NET Core 5.0 |   238.899 ns |  2.5369 ns |  3.6384 ns |  62.78 |    2.14 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net50 | .NET Core 5.0 | 1,106.398 ns | 11.6493 ns | 17.4362 ns | 291.09 |   10.07 | 0.1259 |     - |     - |     800 B |
