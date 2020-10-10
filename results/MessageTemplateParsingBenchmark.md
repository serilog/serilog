``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|                         Method |             Job |       Jit |       Runtime |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |---------------- |---------- |-------------- |-----------:|---------:|---------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   149.0 ns |  2.19 ns |  3.27 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   225.0 ns |  5.21 ns |  7.79 ns |  1.51 |    0.07 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   277.6 ns |  4.59 ns |  6.88 ns |  1.86 |    0.06 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   524.5 ns |  9.78 ns | 14.64 ns |  3.52 |    0.13 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   517.1 ns |  9.91 ns | 14.83 ns |  3.47 |    0.11 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,051.2 ns | 18.80 ns | 28.13 ns |  7.06 |    0.24 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,324.4 ns | 20.31 ns | 30.40 ns |  8.89 |    0.30 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 3,769.9 ns | 57.37 ns | 85.88 ns | 25.31 |    0.76 | 0.9956 | 0.0229 |     - |    6264 B |
|                                |                 |           |               |            |          |          |       |         |        |        |       |           |
|                  EmptyTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   132.2 ns |  2.51 ns |  3.76 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   185.1 ns |  2.58 ns |  3.86 ns |  1.40 |    0.06 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   313.4 ns |  8.68 ns | 12.72 ns |  2.38 |    0.14 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   478.3 ns |  5.79 ns |  8.66 ns |  3.62 |    0.14 | 0.1497 |      - |     - |     947 B |
|      ManyPropertyTokenTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   624.0 ns |  9.08 ns | 13.59 ns |  4.72 |    0.19 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,233.2 ns | 17.51 ns | 25.66 ns |  9.34 |    0.24 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,663.8 ns | 16.63 ns | 24.89 ns | 12.59 |    0.38 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 4,732.7 ns | 60.93 ns | 91.19 ns | 35.82 |    1.29 | 1.0529 | 0.0229 |     - |    6652 B |
|                                |                 |           |               |            |          |          |       |         |        |        |       |           |
|                  EmptyTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   131.1 ns |  2.06 ns |  3.08 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   184.3 ns |  2.22 ns |  3.32 ns |  1.41 |    0.04 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   310.5 ns |  4.70 ns |  7.04 ns |  2.37 |    0.09 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   481.6 ns | 10.01 ns | 14.98 ns |  3.67 |    0.14 | 0.1497 |      - |     - |     947 B |
|      ManyPropertyTokenTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   624.6 ns |  9.65 ns | 14.15 ns |  4.76 |    0.16 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,227.9 ns | 14.77 ns | 21.65 ns |  9.36 |    0.29 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,668.5 ns | 16.61 ns | 24.87 ns | 12.73 |    0.35 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 4,742.1 ns | 66.23 ns | 97.08 ns | 36.17 |    1.25 | 1.0529 | 0.0229 |     - |    6652 B |
