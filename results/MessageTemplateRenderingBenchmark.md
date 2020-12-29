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
|           NoMessage | core31 | .NET Core 3.1 |     4.314 ns |  0.0668 ns |  0.0999 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | core31 | .NET Core 3.1 |     4.310 ns |  0.0691 ns |  0.1034 ns |   1.00 |    0.04 |      - |     - |     - |         - |
| OneSimpleProperties | core31 | .NET Core 3.1 |    50.763 ns |  0.4629 ns |  0.6929 ns |  11.77 |    0.33 |      - |     - |     - |         - |
|    VariedProperties | core31 | .NET Core 3.1 |   288.462 ns |  5.3281 ns |  7.9749 ns |  66.90 |    2.45 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | core31 | .NET Core 3.1 | 1,272.593 ns | 18.3318 ns | 27.4381 ns | 295.18 |   10.72 | 0.1259 |     - |     - |     800 B |
|                     |        |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |  net48 |      .NET 4.8 |     4.161 ns |  0.5542 ns |  0.8295 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net48 |      .NET 4.8 |     3.371 ns |  0.0605 ns |  0.0905 ns |   0.84 |    0.17 |      - |     - |     - |         - |
| OneSimpleProperties |  net48 |      .NET 4.8 |    84.737 ns |  0.9433 ns |  1.4118 ns |  21.18 |    4.23 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |  net48 |      .NET 4.8 |   362.621 ns |  4.0648 ns |  6.0840 ns |  90.61 |   18.01 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net48 |      .NET 4.8 | 1,836.579 ns | 11.4374 ns | 17.1190 ns | 459.19 |   92.43 | 0.1698 |     - |     - |    1075 B |
|                     |        |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |  net50 | .NET Core 5.0 |     3.935 ns |  0.0630 ns |  0.0944 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net50 | .NET Core 5.0 |     3.728 ns |  0.0652 ns |  0.0976 ns |   0.95 |    0.04 |      - |     - |     - |         - |
| OneSimpleProperties |  net50 | .NET Core 5.0 |    38.222 ns |  0.4523 ns |  0.6769 ns |   9.72 |    0.29 |      - |     - |     - |         - |
|    VariedProperties |  net50 | .NET Core 5.0 |   238.851 ns |  3.8489 ns |  5.7609 ns |  60.75 |    2.46 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net50 | .NET Core 5.0 | 1,118.862 ns | 18.8457 ns | 27.6238 ns | 284.51 |    9.56 | 0.1259 |     - |     - |     800 B |
