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
|                  EmptyTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   156.9 ns |  0.96 ns |  1.38 ns |   157.0 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   225.2 ns |  0.48 ns |  0.70 ns |   225.1 ns |  1.44 |    0.01 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   288.2 ns |  0.94 ns |  1.29 ns |   288.1 ns |  1.84 |    0.02 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   547.9 ns |  1.58 ns |  2.36 ns |   547.8 ns |  3.49 |    0.03 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   533.9 ns |  2.51 ns |  3.67 ns |   533.8 ns |  3.40 |    0.05 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,093.5 ns |  3.84 ns |  5.51 ns | 1,093.6 ns |  6.97 |    0.04 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,389.4 ns |  5.58 ns |  7.82 ns | 1,390.9 ns |  8.86 |    0.11 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 3,938.9 ns | 28.16 ns | 40.39 ns | 3,943.3 ns | 25.11 |    0.19 | 0.9918 | 0.0229 |     - |    6264 B |
|                                |                 |           |               |            |          |          |            |       |         |        |        |       |           |
|                  EmptyTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   137.1 ns |  1.07 ns |  1.50 ns |   137.3 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   199.6 ns |  5.25 ns |  7.86 ns |   198.9 ns |  1.46 |    0.07 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   337.3 ns | 13.61 ns | 19.08 ns |   323.5 ns |  2.46 |    0.16 | 0.0896 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   503.1 ns |  1.55 ns |  2.27 ns |   503.3 ns |  3.67 |    0.05 | 0.1497 |      - |     - |     947 B |
|      ManyPropertyTokenTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   651.1 ns |  2.16 ns |  3.09 ns |   650.5 ns |  4.75 |    0.05 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,299.7 ns |  4.26 ns |  5.97 ns | 1,301.5 ns |  9.48 |    0.09 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,742.6 ns |  4.07 ns |  5.70 ns | 1,743.4 ns | 12.71 |    0.13 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 5,008.2 ns | 18.71 ns | 25.61 ns | 5,001.9 ns | 36.54 |    0.49 | 1.0529 | 0.0229 |     - |    6652 B |
|                                |                 |           |               |            |          |          |            |       |         |        |        |       |           |
|                  EmptyTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   136.9 ns |  0.95 ns |  1.30 ns |   136.8 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   192.0 ns |  0.53 ns |  0.80 ns |   191.8 ns |  1.40 |    0.02 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   321.0 ns |  1.36 ns |  1.91 ns |   321.7 ns |  2.34 |    0.02 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   504.2 ns |  1.47 ns |  2.10 ns |   504.2 ns |  3.68 |    0.03 | 0.1497 |      - |     - |     947 B |
|      ManyPropertyTokenTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   697.5 ns | 28.57 ns | 40.98 ns |   732.0 ns |  5.12 |    0.26 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,290.2 ns |  4.27 ns |  6.39 ns | 1,291.1 ns |  9.42 |    0.10 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,828.8 ns | 54.96 ns | 82.26 ns | 1,814.5 ns | 13.44 |    0.51 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 5,011.3 ns | 23.56 ns | 33.79 ns | 5,000.4 ns | 36.60 |    0.46 | 1.0529 | 0.0229 |     - |    6652 B |
