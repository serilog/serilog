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
|                  EmptyTemplate | core31 | .NET Core 3.1 |   154.5 ns |  1.49 ns |  2.22 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate | core31 | .NET Core 3.1 |   232.1 ns |  1.56 ns |  2.33 ns |  1.50 |    0.02 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate | core31 | .NET Core 3.1 |   283.7 ns |  2.79 ns |  4.18 ns |  1.84 |    0.04 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate | core31 | .NET Core 3.1 |   536.5 ns |  7.45 ns | 11.16 ns |  3.47 |    0.10 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate | core31 | .NET Core 3.1 |   549.9 ns |  6.20 ns |  9.09 ns |  3.56 |    0.09 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate | core31 | .NET Core 3.1 | 1,084.6 ns |  9.60 ns | 14.07 ns |  7.02 |    0.14 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate | core31 | .NET Core 3.1 | 1,366.1 ns | 11.45 ns | 17.13 ns |  8.85 |    0.14 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate | core31 | .NET Core 3.1 | 3,881.8 ns | 37.89 ns | 56.72 ns | 25.14 |    0.57 | 0.9918 | 0.0229 |     - |    6264 B |
|                                |        |               |            |          |          |       |         |        |        |       |           |
|                  EmptyTemplate |  net48 |      .NET 4.8 |   134.2 ns |  1.06 ns |  1.59 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate |  net48 |      .NET 4.8 |   190.6 ns |  1.43 ns |  2.13 ns |  1.42 |    0.03 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate |  net48 |      .NET 4.8 |   332.0 ns | 11.34 ns | 16.97 ns |  2.47 |    0.13 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate |  net48 |      .NET 4.8 |   520.5 ns | 17.84 ns | 26.71 ns |  3.88 |    0.20 | 0.1497 |      - |     - |     947 B |
|      ManyPropertyTokenTemplate |  net48 |      .NET 4.8 |   682.3 ns | 29.72 ns | 44.48 ns |  5.08 |    0.35 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate |  net48 |      .NET 4.8 | 1,270.6 ns |  9.65 ns | 14.45 ns |  9.47 |    0.16 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate |  net48 |      .NET 4.8 | 1,778.4 ns | 45.52 ns | 68.13 ns | 13.25 |    0.55 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate |  net48 |      .NET 4.8 | 4,955.3 ns | 44.44 ns | 65.14 ns | 36.92 |    0.67 | 1.0529 | 0.0229 |     - |    6652 B |
|                                |        |               |            |          |          |       |         |        |        |       |           |
|                  EmptyTemplate |  net50 | .NET Core 5.0 |   140.1 ns |  1.58 ns |  2.37 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |  net50 | .NET Core 5.0 |   204.6 ns |  2.09 ns |  3.13 ns |  1.46 |    0.04 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |  net50 | .NET Core 5.0 |   272.1 ns |  2.55 ns |  3.82 ns |  1.94 |    0.04 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |  net50 | .NET Core 5.0 |   510.9 ns |  5.83 ns |  8.36 ns |  3.65 |    0.08 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |  net50 | .NET Core 5.0 |   501.2 ns |  4.92 ns |  7.37 ns |  3.58 |    0.08 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate |  net50 | .NET Core 5.0 |   995.1 ns |  9.91 ns | 14.83 ns |  7.10 |    0.16 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate |  net50 | .NET Core 5.0 | 1,304.7 ns | 20.42 ns | 30.56 ns |  9.31 |    0.23 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate |  net50 | .NET Core 5.0 | 3,637.0 ns | 37.66 ns | 56.37 ns | 25.96 |    0.58 | 0.9956 | 0.0267 |     - |    6264 B |
