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
|           NoMessage | core31 | .NET Core 3.1 |     4.288 ns |  0.0470 ns |  0.0704 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | core31 | .NET Core 3.1 |     3.663 ns |  0.4111 ns |  0.6153 ns |   0.85 |    0.14 |      - |     - |     - |         - |
| OneSimpleProperties | core31 | .NET Core 3.1 |    48.444 ns |  1.3418 ns |  1.9667 ns |  11.30 |    0.50 |      - |     - |     - |         - |
|    VariedProperties | core31 | .NET Core 3.1 |   283.053 ns |  3.1449 ns |  4.7072 ns |  66.03 |    1.41 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | core31 | .NET Core 3.1 | 1,250.948 ns | 11.9920 ns | 17.9491 ns | 291.82 |    6.89 | 0.1259 |     - |     - |     800 B |
|                     |        |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |  net48 |      .NET 4.8 |     5.178 ns |  0.0432 ns |  0.0646 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net48 |      .NET 4.8 |     5.175 ns |  0.0450 ns |  0.0674 ns |   1.00 |    0.02 |      - |     - |     - |         - |
| OneSimpleProperties |  net48 |      .NET 4.8 |    84.281 ns |  0.9514 ns |  1.4240 ns |  16.28 |    0.27 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |  net48 |      .NET 4.8 |   362.543 ns |  3.5300 ns |  5.2835 ns |  70.03 |    1.43 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net48 |      .NET 4.8 | 1,825.228 ns |  7.5125 ns | 11.2443 ns | 352.59 |    5.36 | 0.1698 |     - |     - |    1075 B |
|                     |        |               |              |            |            |        |         |        |       |       |           |
|           NoMessage |  net50 | .NET Core 5.0 |     3.680 ns |  0.0436 ns |  0.0653 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net50 | .NET Core 5.0 |     3.901 ns |  0.0439 ns |  0.0657 ns |   1.06 |    0.03 |      - |     - |     - |         - |
| OneSimpleProperties |  net50 | .NET Core 5.0 |    37.505 ns |  0.3135 ns |  0.4693 ns |  10.20 |    0.22 |      - |     - |     - |         - |
|    VariedProperties |  net50 | .NET Core 5.0 |   240.199 ns |  3.5774 ns |  5.3545 ns |  65.30 |    1.79 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net50 | .NET Core 5.0 | 1,114.616 ns | 21.0169 ns | 30.8064 ns | 302.97 |   11.59 | 0.1259 |     - |     - |     800 B |
