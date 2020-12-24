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
|                  EmptyTemplate | core31 | .NET Core 3.1 |   153.1 ns |  1.47 ns |  2.15 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate | core31 | .NET Core 3.1 |   231.6 ns |  2.29 ns |  3.36 ns |  1.51 |    0.04 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate | core31 | .NET Core 3.1 |   283.3 ns |  2.16 ns |  3.23 ns |  1.85 |    0.04 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate | core31 | .NET Core 3.1 |   532.1 ns |  6.07 ns |  9.08 ns |  3.48 |    0.07 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate | core31 | .NET Core 3.1 |   531.0 ns |  5.61 ns |  8.39 ns |  3.47 |    0.08 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate | core31 | .NET Core 3.1 | 1,089.4 ns |  9.45 ns | 14.14 ns |  7.12 |    0.13 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate | core31 | .NET Core 3.1 | 1,363.9 ns | 11.06 ns | 16.55 ns |  8.91 |    0.19 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate | core31 | .NET Core 3.1 | 3,899.1 ns | 38.87 ns | 58.18 ns | 25.47 |    0.52 | 0.9918 | 0.0229 |     - |    6264 B |
|                                |        |               |            |          |          |       |         |        |        |       |           |
|                  EmptyTemplate |  net48 |      .NET 4.8 |   144.7 ns |  6.48 ns |  9.70 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate |  net48 |      .NET 4.8 |   206.9 ns |  1.25 ns |  1.86 ns |  1.44 |    0.09 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate |  net48 |      .NET 4.8 |   316.6 ns |  2.57 ns |  3.85 ns |  2.20 |    0.16 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate |  net48 |      .NET 4.8 |   520.9 ns | 16.83 ns | 25.20 ns |  3.63 |    0.41 | 0.1497 |      - |     - |     947 B |
|      ManyPropertyTokenTemplate |  net48 |      .NET 4.8 |   684.0 ns | 32.24 ns | 48.25 ns |  4.72 |    0.08 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate |  net48 |      .NET 4.8 | 1,340.9 ns | 52.96 ns | 79.27 ns |  9.34 |    1.15 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate |  net48 |      .NET 4.8 | 1,715.0 ns |  9.16 ns | 13.70 ns | 11.90 |    0.84 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate |  net48 |      .NET 4.8 | 4,904.9 ns | 25.38 ns | 35.58 ns | 34.30 |    2.27 | 1.0529 | 0.0229 |     - |    6652 B |
|                                |        |               |            |          |          |       |         |        |        |       |           |
|                  EmptyTemplate |  net50 | .NET Core 5.0 |   139.4 ns |  1.47 ns |  2.19 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |  net50 | .NET Core 5.0 |   204.6 ns |  1.09 ns |  1.64 ns |  1.47 |    0.02 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |  net50 | .NET Core 5.0 |   263.7 ns |  2.46 ns |  3.69 ns |  1.89 |    0.03 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |  net50 | .NET Core 5.0 |   510.3 ns |  6.57 ns |  9.84 ns |  3.66 |    0.11 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |  net50 | .NET Core 5.0 |   496.8 ns |  5.34 ns |  7.99 ns |  3.57 |    0.09 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate |  net50 | .NET Core 5.0 |   993.2 ns |  8.90 ns | 13.32 ns |  7.13 |    0.14 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate |  net50 | .NET Core 5.0 | 1,316.6 ns | 23.92 ns | 35.81 ns |  9.45 |    0.34 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate |  net50 | .NET Core 5.0 | 3,669.2 ns | 31.73 ns | 47.49 ns | 26.33 |    0.51 | 0.9956 | 0.0267 |     - |    6264 B |
