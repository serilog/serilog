``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.404
  [Host]          : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|                         Method |             Job |       Jit |       Runtime |       Mean |    Error |   StdDev |     Median | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |---------------- |---------- |-------------- |-----------:|---------:|---------:|-----------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   155.3 ns |  2.41 ns |  3.61 ns |   155.0 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   229.4 ns |  2.28 ns |  3.42 ns |   229.9 ns |  1.48 |    0.05 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   289.8 ns |  3.78 ns |  5.65 ns |   290.0 ns |  1.87 |    0.06 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   542.7 ns |  5.79 ns |  8.66 ns |   541.0 ns |  3.50 |    0.10 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   538.0 ns |  5.77 ns |  8.63 ns |   537.3 ns |  3.47 |    0.10 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,101.0 ns | 12.69 ns | 18.99 ns | 1,100.7 ns |  7.09 |    0.22 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,382.0 ns | 11.86 ns | 17.76 ns | 1,385.6 ns |  8.90 |    0.22 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 3,930.5 ns | 41.68 ns | 62.39 ns | 3,928.4 ns | 25.32 |    0.66 | 0.9918 | 0.0229 |     - |    6264 B |
|                                |                 |           |               |            |          |          |            |       |         |        |        |       |           |
|                  EmptyTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   135.8 ns |  1.47 ns |  2.16 ns |   135.8 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   198.5 ns |  5.41 ns |  8.10 ns |   198.7 ns |  1.46 |    0.06 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   319.5 ns |  3.04 ns |  4.54 ns |   319.5 ns |  2.35 |    0.05 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   504.1 ns |  6.27 ns |  9.38 ns |   502.8 ns |  3.71 |    0.11 | 0.1497 |      - |     - |     947 B |
|      ManyPropertyTokenTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   650.5 ns |  5.17 ns |  7.74 ns |   649.9 ns |  4.79 |    0.10 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,374.5 ns | 54.11 ns | 80.99 ns | 1,373.8 ns | 10.14 |    0.62 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,743.6 ns | 10.95 ns | 16.39 ns | 1,745.1 ns | 12.84 |    0.22 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 5,001.6 ns | 51.62 ns | 77.27 ns | 5,000.6 ns | 36.83 |    0.94 | 1.0529 | 0.0229 |     - |    6652 B |
|                                |                 |           |               |            |          |          |            |       |         |        |        |       |           |
|                  EmptyTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   135.9 ns |  1.30 ns |  1.95 ns |   135.8 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   200.1 ns |  6.18 ns |  9.06 ns |   195.0 ns |  1.47 |    0.07 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   319.0 ns |  2.70 ns |  4.04 ns |   318.0 ns |  2.35 |    0.05 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   530.7 ns | 21.18 ns | 31.04 ns |   521.3 ns |  3.90 |    0.24 | 0.1497 |      - |     - |     947 B |
|      ManyPropertyTokenTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   650.9 ns |  7.15 ns | 10.71 ns |   648.7 ns |  4.79 |    0.11 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,285.8 ns | 11.34 ns | 16.97 ns | 1,288.7 ns |  9.47 |    0.19 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,757.2 ns | 17.28 ns | 25.86 ns | 1,755.8 ns | 12.94 |    0.27 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 5,011.3 ns | 44.88 ns | 67.17 ns | 5,008.9 ns | 36.89 |    0.72 | 1.0529 | 0.0229 |     - |    6652 B |
