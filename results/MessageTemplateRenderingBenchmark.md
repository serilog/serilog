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
|              Method |    Job |       Runtime |         Mean |      Error |     StdDev |       Median |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |------- |-------------- |-------------:|-----------:|-----------:|-------------:|-------:|--------:|-------:|------:|------:|----------:|
|           NoMessage | core31 | .NET Core 3.1 |     3.490 ns |  0.1213 ns |  0.1778 ns |     3.442 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties | core31 | .NET Core 3.1 |     3.351 ns |  0.0443 ns |  0.0664 ns |     3.315 ns |   0.96 |    0.05 |      - |     - |     - |         - |
| OneSimpleProperties | core31 | .NET Core 3.1 |    50.599 ns |  0.2769 ns |  0.4145 ns |    50.573 ns |  14.53 |    0.74 |      - |     - |     - |         - |
|    VariedProperties | core31 | .NET Core 3.1 |   297.585 ns |  2.3620 ns |  3.5353 ns |   296.301 ns |  85.53 |    4.93 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties | core31 | .NET Core 3.1 | 1,309.412 ns | 10.4778 ns | 15.6826 ns | 1,309.984 ns | 375.77 |   17.68 | 0.1259 |     - |     - |     800 B |
|                     |        |               |              |            |            |              |        |         |        |       |       |           |
|           NoMessage |  net48 |      .NET 4.8 |     3.462 ns |  0.0283 ns |  0.0406 ns |     3.452 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net48 |      .NET 4.8 |     3.547 ns |  0.0561 ns |  0.0840 ns |     3.545 ns |   1.02 |    0.03 |      - |     - |     - |         - |
| OneSimpleProperties |  net48 |      .NET 4.8 |    87.628 ns |  0.5439 ns |  0.8140 ns |    87.602 ns |  25.33 |    0.37 | 0.0050 |     - |     - |      32 B |
|    VariedProperties |  net48 |      .NET 4.8 |   376.690 ns |  3.3561 ns |  5.0232 ns |   376.934 ns | 108.78 |    1.95 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net48 |      .NET 4.8 | 1,943.108 ns | 21.3179 ns | 31.9076 ns | 1,937.737 ns | 561.16 |   10.50 | 0.1678 |     - |     - |    1075 B |
|                     |        |               |              |            |            |              |        |         |        |       |       |           |
|           NoMessage |  net50 | .NET Core 5.0 |     3.942 ns |  0.0716 ns |  0.1072 ns |     3.956 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|        NoProperties |  net50 | .NET Core 5.0 |     4.043 ns |  0.0375 ns |  0.0562 ns |     4.045 ns |   1.03 |    0.03 |      - |     - |     - |         - |
| OneSimpleProperties |  net50 | .NET Core 5.0 |    39.362 ns |  0.3061 ns |  0.4582 ns |    39.321 ns |   9.99 |    0.27 |      - |     - |     - |         - |
|    VariedProperties |  net50 | .NET Core 5.0 |   251.173 ns |  3.0992 ns |  4.5428 ns |   251.045 ns |  63.72 |    2.38 | 0.0153 |     - |     - |      96 B |
|   ComplexProperties |  net50 | .NET Core 5.0 | 1,163.074 ns | 14.9216 ns | 22.3339 ns | 1,165.572 ns | 295.23 |    9.31 | 0.1259 |     - |     - |     800 B |
