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
|                  EmptyTemplate | core31 | .NET Core 3.1 |   150.1 ns |  2.31 ns |  3.46 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate | core31 | .NET Core 3.1 |   218.8 ns |  2.04 ns |  3.05 ns |  1.46 |    0.04 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate | core31 | .NET Core 3.1 |   276.1 ns |  4.45 ns |  6.66 ns |  1.84 |    0.04 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate | core31 | .NET Core 3.1 |   518.8 ns |  6.57 ns |  9.83 ns |  3.46 |    0.07 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate | core31 | .NET Core 3.1 |   514.4 ns |  7.55 ns | 11.31 ns |  3.43 |    0.10 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate | core31 | .NET Core 3.1 | 1,053.2 ns | 17.49 ns | 26.18 ns |  7.02 |    0.23 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate | core31 | .NET Core 3.1 | 1,317.8 ns | 17.57 ns | 26.30 ns |  8.79 |    0.31 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate | core31 | .NET Core 3.1 | 3,736.5 ns | 55.10 ns | 82.47 ns | 24.91 |    0.73 | 0.9956 | 0.0229 |     - |    6264 B |
|                                |        |               |            |          |          |       |         |        |        |       |           |
|                  EmptyTemplate |  net48 |      .NET 4.8 |   131.7 ns |  2.11 ns |  3.15 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate |  net48 |      .NET 4.8 |   182.9 ns |  2.43 ns |  3.64 ns |  1.39 |    0.04 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate |  net48 |      .NET 4.8 |   321.7 ns | 12.27 ns | 18.36 ns |  2.44 |    0.16 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate |  net48 |      .NET 4.8 |   503.8 ns | 16.99 ns | 25.43 ns |  3.83 |    0.20 | 0.1497 |      - |     - |     947 B |
|      ManyPropertyTokenTemplate |  net48 |      .NET 4.8 |   619.8 ns |  9.21 ns | 13.79 ns |  4.71 |    0.17 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate |  net48 |      .NET 4.8 | 1,302.0 ns | 47.04 ns | 70.40 ns |  9.89 |    0.54 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate |  net48 |      .NET 4.8 | 1,646.1 ns | 16.48 ns | 24.66 ns | 12.50 |    0.37 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate |  net48 |      .NET 4.8 | 4,737.7 ns | 39.31 ns | 58.84 ns | 35.99 |    1.13 | 1.0529 | 0.0229 |     - |    6652 B |
|                                |        |               |            |          |          |       |         |        |        |       |           |
|                  EmptyTemplate |  net50 | .NET Core 5.0 |   137.5 ns |  2.69 ns |  4.02 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |  net50 | .NET Core 5.0 |   199.6 ns |  3.32 ns |  4.97 ns |  1.45 |    0.05 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |  net50 | .NET Core 5.0 |   262.4 ns |  5.98 ns |  8.95 ns |  1.91 |    0.10 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |  net50 | .NET Core 5.0 |   490.7 ns |  8.85 ns | 13.25 ns |  3.57 |    0.13 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |  net50 | .NET Core 5.0 |   474.6 ns |  6.68 ns | 10.00 ns |  3.45 |    0.13 | 0.1655 | 0.0005 |     - |    1040 B |
|         MultipleTokensTemplate |  net50 | .NET Core 5.0 |   957.5 ns | 17.89 ns | 26.77 ns |  6.97 |    0.29 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate |  net50 | .NET Core 5.0 | 1,234.1 ns | 19.96 ns | 29.87 ns |  8.98 |    0.22 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate |  net50 | .NET Core 5.0 | 3,419.6 ns | 38.09 ns | 57.01 ns | 24.89 |    0.90 | 0.9956 | 0.0267 |     - |    6264 B |
