``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|                         Method |             Job |       Jit |       Runtime |       Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |---------------- |---------- |-------------- |-----------:|----------:|----------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   147.5 ns |   2.77 ns |   4.14 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   212.9 ns |   1.84 ns |   2.69 ns |  1.44 |    0.04 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   283.8 ns |   2.17 ns |   3.25 ns |  1.92 |    0.06 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   547.2 ns |   4.98 ns |   7.45 ns |  3.71 |    0.11 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   514.6 ns |   6.68 ns |  10.00 ns |  3.49 |    0.10 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,050.3 ns |   8.22 ns |  12.30 ns |  7.12 |    0.21 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,344.0 ns |  11.46 ns |  17.16 ns |  9.12 |    0.31 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 3,739.1 ns |  21.71 ns |  31.82 ns | 25.36 |    0.76 | 0.9956 | 0.0229 |     - |    6264 B |
|                                |                 |           |               |            |           |           |       |         |        |        |       |           |
|                  EmptyTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   123.1 ns |   1.50 ns |   2.24 ns |  1.00 |    0.00 | 0.0420 |      - |     - |     265 B |
|             SimpleTextTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   179.9 ns |   1.60 ns |   2.30 ns |  1.46 |    0.04 | 0.0675 |      - |     - |     425 B |
|    SinglePropertyTokenTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   307.0 ns |   4.07 ns |   6.09 ns |  2.49 |    0.06 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   505.6 ns |   4.73 ns |   7.08 ns |  4.11 |    0.12 | 0.1497 |      - |     - |     947 B |
|      ManyPropertyTokenTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   626.2 ns |   2.83 ns |   4.15 ns |  5.09 |    0.10 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,240.9 ns |   9.68 ns |  14.49 ns | 10.08 |    0.18 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,742.1 ns |  22.64 ns |  33.89 ns | 14.15 |    0.41 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 4,893.2 ns |  76.25 ns | 114.13 ns | 39.75 |    0.85 | 1.0529 | 0.0229 |     - |    6652 B |
|                                |                 |           |               |            |           |           |       |         |        |        |       |           |
|                  EmptyTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   122.0 ns |   1.30 ns |   1.95 ns |  1.00 |    0.00 | 0.0420 |      - |     - |     265 B |
|             SimpleTextTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   180.3 ns |   1.61 ns |   2.36 ns |  1.48 |    0.03 | 0.0675 |      - |     - |     425 B |
|    SinglePropertyTokenTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   306.0 ns |   2.02 ns |   3.02 ns |  2.51 |    0.05 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   517.1 ns |  13.65 ns |  20.42 ns |  4.24 |    0.20 | 0.1497 |      - |     - |     947 B |
|      ManyPropertyTokenTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   624.9 ns |   4.30 ns |   6.44 ns |  5.12 |    0.10 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,251.2 ns |  10.29 ns |  14.76 ns | 10.26 |    0.21 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,725.6 ns |   9.05 ns |  13.26 ns | 14.15 |    0.25 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 4,921.3 ns | 115.48 ns | 172.84 ns | 40.34 |    1.44 | 1.0529 | 0.0229 |     - |    6652 B |
