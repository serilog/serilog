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
|                         Method |    Job |       Runtime |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |------- |-------------- |-----------:|---------:|---------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate | core31 | .NET Core 3.1 |   149.1 ns |  2.38 ns |  3.56 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate | core31 | .NET Core 3.1 |   221.1 ns |  2.40 ns |  3.59 ns |  1.48 |    0.05 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate | core31 | .NET Core 3.1 |   273.7 ns |  4.02 ns |  6.01 ns |  1.84 |    0.08 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate | core31 | .NET Core 3.1 |   522.6 ns |  7.82 ns | 11.70 ns |  3.51 |    0.07 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate | core31 | .NET Core 3.1 |   509.8 ns |  8.51 ns | 12.74 ns |  3.42 |    0.09 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate | core31 | .NET Core 3.1 | 1,050.4 ns | 17.39 ns | 26.03 ns |  7.05 |    0.19 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate | core31 | .NET Core 3.1 | 1,320.7 ns | 15.50 ns | 23.20 ns |  8.86 |    0.26 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate | core31 | .NET Core 3.1 | 3,702.3 ns | 53.91 ns | 80.69 ns | 24.85 |    0.80 | 0.9918 | 0.0229 |     - |    6264 B |
|                                |        |               |            |          |          |       |         |        |        |       |           |
|                  EmptyTemplate |  net48 |      .NET 4.8 |   141.8 ns |  5.95 ns |  8.90 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate |  net48 |      .NET 4.8 |   199.0 ns |  2.07 ns |  3.11 ns |  1.41 |    0.08 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate |  net48 |      .NET 4.8 |   304.9 ns |  4.22 ns |  6.31 ns |  2.16 |    0.16 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate |  net48 |      .NET 4.8 |   479.7 ns |  9.07 ns | 13.57 ns |  3.40 |    0.24 | 0.1497 |      - |     - |     947 B |
|      ManyPropertyTokenTemplate |  net48 |      .NET 4.8 |   659.0 ns | 27.96 ns | 41.84 ns |  4.65 |    0.20 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate |  net48 |      .NET 4.8 | 1,299.6 ns | 48.57 ns | 72.70 ns |  9.22 |    1.05 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate |  net48 |      .NET 4.8 | 1,702.7 ns | 45.27 ns | 67.75 ns | 12.07 |    1.18 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate |  net48 |      .NET 4.8 | 4,706.9 ns | 65.52 ns | 98.06 ns | 33.30 |    1.99 | 1.0529 | 0.0229 |     - |    6652 B |
|                                |        |               |            |          |          |       |         |        |        |       |           |
|                  EmptyTemplate |  net50 | .NET Core 5.0 |   134.6 ns |  2.56 ns |  3.83 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |  net50 | .NET Core 5.0 |   200.1 ns |  2.43 ns |  3.63 ns |  1.49 |    0.05 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |  net50 | .NET Core 5.0 |   256.3 ns |  4.44 ns |  6.64 ns |  1.90 |    0.05 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |  net50 | .NET Core 5.0 |   504.0 ns | 11.37 ns | 17.02 ns |  3.74 |    0.14 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |  net50 | .NET Core 5.0 |   487.8 ns |  8.53 ns | 12.50 ns |  3.63 |    0.15 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate |  net50 | .NET Core 5.0 |   968.0 ns | 14.30 ns | 21.40 ns |  7.19 |    0.21 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate |  net50 | .NET Core 5.0 | 1,266.0 ns | 15.93 ns | 23.84 ns |  9.41 |    0.31 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate |  net50 | .NET Core 5.0 | 3,432.2 ns | 30.90 ns | 46.25 ns | 25.51 |    0.79 | 0.9956 | 0.0267 |     - |    6264 B |
