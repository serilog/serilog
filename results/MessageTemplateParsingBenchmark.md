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
|                         Method |    Job |       Runtime |       Mean |    Error |   StdDev |     Median | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |------- |-------------- |-----------:|---------:|---------:|-----------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate | core31 | .NET Core 3.1 |   147.2 ns |  0.90 ns |  1.31 ns |   147.2 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate | core31 | .NET Core 3.1 |   210.3 ns |  1.10 ns |  1.57 ns |   210.4 ns |  1.43 |    0.01 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate | core31 | .NET Core 3.1 |   268.9 ns |  2.51 ns |  3.75 ns |   269.6 ns |  1.83 |    0.02 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate | core31 | .NET Core 3.1 |   503.3 ns |  2.63 ns |  3.94 ns |   503.1 ns |  3.42 |    0.04 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate | core31 | .NET Core 3.1 |   499.0 ns |  5.44 ns |  7.45 ns |   499.1 ns |  3.39 |    0.06 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate | core31 | .NET Core 3.1 | 1,012.2 ns |  5.63 ns |  8.25 ns | 1,010.8 ns |  6.88 |    0.08 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate | core31 | .NET Core 3.1 | 1,283.1 ns |  8.26 ns | 12.36 ns | 1,281.4 ns |  8.72 |    0.09 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate | core31 | .NET Core 3.1 | 3,614.2 ns | 19.23 ns | 28.19 ns | 3,612.6 ns | 24.55 |    0.27 | 0.9956 | 0.0229 |     - |    6264 B |
|                                |        |               |            |          |          |            |       |         |        |        |       |           |
|                  EmptyTemplate |  net48 |      .NET 4.8 |   135.8 ns |  5.91 ns |  8.85 ns |   135.8 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate |  net48 |      .NET 4.8 |   178.2 ns |  0.69 ns |  0.99 ns |   178.0 ns |  1.31 |    0.08 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate |  net48 |      .NET 4.8 |   299.0 ns |  1.57 ns |  2.35 ns |   299.5 ns |  2.21 |    0.14 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate |  net48 |      .NET 4.8 |   488.9 ns | 16.70 ns | 24.48 ns |   475.1 ns |  3.62 |    0.42 | 0.1497 |      - |     - |     947 B |
|      ManyPropertyTokenTemplate |  net48 |      .NET 4.8 |   654.1 ns | 20.68 ns | 30.32 ns |   677.7 ns |  4.84 |    0.53 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate |  net48 |      .NET 4.8 | 1,217.8 ns | 24.20 ns | 35.46 ns | 1,204.0 ns |  8.98 |    0.55 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate |  net48 |      .NET 4.8 | 1,620.4 ns |  7.04 ns |  9.86 ns | 1,620.0 ns | 11.89 |    0.77 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate |  net48 |      .NET 4.8 | 4,637.1 ns | 23.38 ns | 34.27 ns | 4,644.3 ns | 34.21 |    2.19 | 1.0529 | 0.0229 |     - |    6652 B |
|                                |        |               |            |          |          |            |       |         |        |        |       |           |
|                  EmptyTemplate |  net50 | .NET Core 5.0 |   130.4 ns |  0.72 ns |  1.08 ns |   130.6 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |  net50 | .NET Core 5.0 |   193.6 ns |  0.58 ns |  0.81 ns |   193.6 ns |  1.49 |    0.02 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |  net50 | .NET Core 5.0 |   256.1 ns |  3.96 ns |  5.81 ns |   259.4 ns |  1.96 |    0.05 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |  net50 | .NET Core 5.0 |   475.3 ns |  2.76 ns |  4.13 ns |   475.6 ns |  3.65 |    0.05 | 0.1478 | 0.0005 |     - |     928 B |
|      ManyPropertyTokenTemplate |  net50 | .NET Core 5.0 |   464.2 ns |  5.37 ns |  8.04 ns |   463.1 ns |  3.56 |    0.07 | 0.1655 | 0.0005 |     - |    1040 B |
|         MultipleTokensTemplate |  net50 | .NET Core 5.0 |   928.7 ns |  5.79 ns |  8.66 ns |   930.8 ns |  7.12 |    0.09 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate |  net50 | .NET Core 5.0 | 1,215.2 ns | 14.93 ns | 22.35 ns | 1,217.4 ns |  9.32 |    0.20 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate |  net50 | .NET Core 5.0 | 3,311.2 ns | 10.67 ns | 15.97 ns | 3,311.4 ns | 25.40 |    0.28 | 0.9956 | 0.0267 |     - |    6264 B |
