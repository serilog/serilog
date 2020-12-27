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
|                         Method |    Job |       Runtime |       Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |------- |-------------- |-----------:|----------:|----------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate | core31 | .NET Core 3.1 |   137.9 ns |   1.22 ns |   1.82 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate | core31 | .NET Core 3.1 |   204.4 ns |   2.57 ns |   3.84 ns |  1.48 |    0.03 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate | core31 | .NET Core 3.1 |   271.0 ns |   2.23 ns |   3.33 ns |  1.97 |    0.04 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate | core31 | .NET Core 3.1 |   519.8 ns |   5.91 ns |   8.85 ns |  3.77 |    0.08 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate | core31 | .NET Core 3.1 |   482.3 ns |   5.24 ns |   7.84 ns |  3.50 |    0.07 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate | core31 | .NET Core 3.1 |   989.3 ns |  10.30 ns |  15.42 ns |  7.17 |    0.16 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate | core31 | .NET Core 3.1 | 1,290.0 ns |   9.67 ns |  14.18 ns |  9.36 |    0.19 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate | core31 | .NET Core 3.1 | 3,553.5 ns |  18.91 ns |  28.30 ns | 25.77 |    0.37 | 0.9956 | 0.0229 |     - |    6264 B |
|                                |        |               |            |           |           |       |         |        |        |       |           |
|                  EmptyTemplate |  net48 |      .NET 4.8 |   115.9 ns |   1.09 ns |   1.64 ns |  1.00 |    0.00 | 0.0421 |      - |     - |     265 B |
|             SimpleTextTemplate |  net48 |      .NET 4.8 |   173.6 ns |   1.98 ns |   2.97 ns |  1.50 |    0.04 | 0.0675 |      - |     - |     425 B |
|    SinglePropertyTokenTemplate |  net48 |      .NET 4.8 |   292.9 ns |   2.60 ns |   3.81 ns |  2.53 |    0.04 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate |  net48 |      .NET 4.8 |   526.3 ns |   5.15 ns |   7.70 ns |  4.54 |    0.11 | 0.1497 |      - |     - |     947 B |
|      ManyPropertyTokenTemplate |  net48 |      .NET 4.8 |   632.6 ns |  27.92 ns |  41.79 ns |  5.46 |    0.35 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate |  net48 |      .NET 4.8 | 1,317.6 ns |  10.19 ns |  15.25 ns | 11.37 |    0.21 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate |  net48 |      .NET 4.8 | 1,680.0 ns |  40.29 ns |  59.05 ns | 14.49 |    0.49 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate |  net48 |      .NET 4.8 | 4,799.2 ns | 169.74 ns | 254.06 ns | 41.41 |    2.02 | 1.0529 | 0.0229 |     - |    6652 B |
|                                |        |               |            |           |           |       |         |        |        |       |           |
|                  EmptyTemplate |  net50 | .NET Core 5.0 |   125.0 ns |   1.64 ns |   2.45 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |  net50 | .NET Core 5.0 |   187.6 ns |   1.07 ns |   1.53 ns |  1.50 |    0.03 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |  net50 | .NET Core 5.0 |   251.9 ns |   2.97 ns |   4.44 ns |  2.01 |    0.04 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |  net50 | .NET Core 5.0 |   493.0 ns |   5.39 ns |   8.07 ns |  3.94 |    0.11 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |  net50 | .NET Core 5.0 |   463.8 ns |   3.61 ns |   5.18 ns |  3.71 |    0.08 | 0.1655 | 0.0005 |     - |    1040 B |
|         MultipleTokensTemplate |  net50 | .NET Core 5.0 |   919.5 ns |   3.20 ns |   4.70 ns |  7.36 |    0.16 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate |  net50 | .NET Core 5.0 | 1,242.8 ns |   9.99 ns |  14.95 ns |  9.94 |    0.22 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate |  net50 | .NET Core 5.0 | 3,322.3 ns |  21.19 ns |  31.71 ns | 26.58 |    0.64 | 0.9956 | 0.0267 |     - |    6264 B |
