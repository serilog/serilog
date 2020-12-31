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
|                  EmptyTemplate | core31 | .NET Core 3.1 |   148.1 ns |   2.21 ns |   3.30 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate | core31 | .NET Core 3.1 |   219.6 ns |   2.82 ns |   4.04 ns |  1.48 |    0.04 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate | core31 | .NET Core 3.1 |   277.6 ns |   4.49 ns |   6.73 ns |  1.88 |    0.08 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate | core31 | .NET Core 3.1 |   529.0 ns |   9.82 ns |  14.69 ns |  3.57 |    0.12 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate | core31 | .NET Core 3.1 |   514.4 ns |   8.76 ns |  13.11 ns |  3.48 |    0.14 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate | core31 | .NET Core 3.1 | 1,043.3 ns |  15.34 ns |  22.96 ns |  7.05 |    0.23 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate | core31 | .NET Core 3.1 | 1,325.7 ns |  17.87 ns |  26.75 ns |  8.95 |    0.22 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate | core31 | .NET Core 3.1 | 3,768.4 ns |  43.96 ns |  64.43 ns | 25.44 |    0.77 | 0.9918 | 0.0229 |     - |    6264 B |
|                                |        |               |            |           |           |       |         |        |        |       |           |
|                  EmptyTemplate |  net48 |      .NET 4.8 |   131.4 ns |   2.21 ns |   3.30 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate |  net48 |      .NET 4.8 |   191.2 ns |   5.84 ns |   8.75 ns |  1.46 |    0.08 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate |  net48 |      .NET 4.8 |   306.4 ns |   4.05 ns |   6.06 ns |  2.33 |    0.07 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate |  net48 |      .NET 4.8 |   504.0 ns |  18.73 ns |  28.03 ns |  3.84 |    0.24 | 0.1497 |      - |     - |     947 B |
|      ManyPropertyTokenTemplate |  net48 |      .NET 4.8 |   622.5 ns |   8.08 ns |  12.09 ns |  4.74 |    0.13 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate |  net48 |      .NET 4.8 | 1,233.3 ns |  17.51 ns |  26.21 ns |  9.39 |    0.28 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate |  net48 |      .NET 4.8 | 1,720.2 ns |  42.32 ns |  63.34 ns | 13.10 |    0.58 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate |  net48 |      .NET 4.8 | 4,980.3 ns | 168.37 ns | 252.00 ns | 37.92 |    2.05 | 1.0529 | 0.0229 |     - |    6652 B |
|                                |        |               |            |           |           |       |         |        |        |       |           |
|                  EmptyTemplate |  net50 | .NET Core 5.0 |   134.4 ns |   2.02 ns |   3.03 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |  net50 | .NET Core 5.0 |   201.6 ns |   2.26 ns |   3.38 ns |  1.50 |    0.04 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |  net50 | .NET Core 5.0 |   256.4 ns |   4.60 ns |   6.89 ns |  1.91 |    0.08 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |  net50 | .NET Core 5.0 |   492.4 ns |   9.44 ns |  14.14 ns |  3.66 |    0.13 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |  net50 | .NET Core 5.0 |   474.1 ns |   3.46 ns |   5.18 ns |  3.53 |    0.09 | 0.1655 | 0.0005 |     - |    1040 B |
|         MultipleTokensTemplate |  net50 | .NET Core 5.0 |   948.5 ns |  12.39 ns |  18.16 ns |  7.06 |    0.21 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate |  net50 | .NET Core 5.0 | 1,225.5 ns |  19.78 ns |  29.61 ns |  9.12 |    0.17 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate |  net50 | .NET Core 5.0 | 3,384.3 ns |  34.43 ns |  51.54 ns | 25.19 |    0.66 | 0.9956 | 0.0267 |     - |    6264 B |
