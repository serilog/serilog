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
|              Method |    Job |       Runtime |         Mean |     Error |     StdDev |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |------- |-------------- |-------------:|----------:|-----------:|-------:|--------:|-------:|------:|------:|----------:|
|           NoMessage | core31 | .NET Core 3.1 |     4.090 ns | 0.0211 ns |  0.0309 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | core31 | .NET Core 3.1 |     4.073 ns | 0.0389 ns |  0.0582 ns |   1.00 |    0.02 |      - |     - |     - |         - |
| OneSimpleProperties | core31 | .NET Core 3.1 |    47.446 ns | 0.5110 ns |  0.7164 ns |  11.60 |    0.21 |      - |     - |     - |         - |
|    VariedProperties | core31 | .NET Core 3.1 |   268.911 ns | 1.7419 ns |  2.5533 ns |  65.74 |    0.65 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | core31 | .NET Core 3.1 | 1,168.642 ns | 5.9362 ns |  8.7012 ns | 285.73 |    3.57 | 0.1259 |     - |     - |     800 B |
|                     |        |               |              |           |            |        |         |        |       |       |           |
|           NoMessage |  net48 |      .NET 4.8 |     3.298 ns | 0.0246 ns |  0.0368 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net48 |      .NET 4.8 |     3.295 ns | 0.0178 ns |  0.0266 ns |   1.00 |    0.01 |      - |     - |     - |         - |
| OneSimpleProperties |  net48 |      .NET 4.8 |    72.444 ns | 0.3675 ns |  0.5501 ns |  21.97 |    0.30 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |  net48 |      .NET 4.8 |   323.514 ns | 1.6035 ns |  2.3503 ns |  98.10 |    1.31 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net48 |      .NET 4.8 | 1,664.769 ns | 7.1387 ns | 10.2380 ns | 504.85 |    4.77 | 0.1698 |     - |     - |    1075 B |
|                     |        |               |              |           |            |        |         |        |       |       |           |
|           NoMessage |  net50 | .NET Core 5.0 |     3.815 ns | 0.0384 ns |  0.0551 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net50 | .NET Core 5.0 |     3.578 ns | 0.0187 ns |  0.0269 ns |   0.94 |    0.02 |      - |     - |     - |         - |
| OneSimpleProperties |  net50 | .NET Core 5.0 |    35.232 ns | 0.1744 ns |  0.2501 ns |   9.24 |    0.14 |      - |     - |     - |         - |
|    VariedProperties |  net50 | .NET Core 5.0 |   228.538 ns | 0.7990 ns |  1.1711 ns |  59.91 |    0.88 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net50 | .NET Core 5.0 | 1,017.959 ns | 4.9366 ns |  7.3889 ns | 266.60 |    4.11 | 0.1259 |     - |     - |     800 B |
