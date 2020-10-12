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
|                         Method |             Job |       Jit |       Runtime |       Mean |    Error |    StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |---------------- |---------- |-------------- |-----------:|---------:|----------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   143.7 ns |  1.53 ns |   2.19 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   214.4 ns |  1.60 ns |   2.35 ns |  1.49 |    0.03 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   283.2 ns |  4.13 ns |   6.17 ns |  1.97 |    0.06 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   526.9 ns |  8.07 ns |  12.08 ns |  3.66 |    0.09 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   512.2 ns |  7.17 ns |  10.73 ns |  3.57 |    0.08 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,052.7 ns | 12.78 ns |  19.14 ns |  7.32 |    0.15 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,341.3 ns | 13.51 ns |  20.22 ns |  9.34 |    0.17 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 3,721.5 ns | 41.11 ns |  61.52 ns | 25.93 |    0.54 | 0.9918 | 0.0229 |     - |    6264 B |
|                                |                 |           |               |            |          |           |       |         |        |        |       |           |
|                  EmptyTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   121.2 ns |  1.47 ns |   2.20 ns |  1.00 |    0.00 | 0.0420 |      - |     - |     265 B |
|             SimpleTextTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   179.9 ns |  1.96 ns |   2.94 ns |  1.48 |    0.04 | 0.0675 |      - |     - |     425 B |
|    SinglePropertyTokenTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   307.1 ns |  4.27 ns |   6.40 ns |  2.53 |    0.07 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   496.9 ns |  6.96 ns |  10.42 ns |  4.10 |    0.14 | 0.1497 |      - |     - |     947 B |
|      ManyPropertyTokenTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   624.9 ns |  8.52 ns |  12.75 ns |  5.16 |    0.14 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,230.0 ns | 16.19 ns |  24.24 ns | 10.15 |    0.28 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,698.9 ns | 15.34 ns |  22.95 ns | 14.02 |    0.31 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 4,789.8 ns | 72.31 ns | 108.22 ns | 39.52 |    1.10 | 1.0529 | 0.0229 |     - |    6652 B |
|                                |                 |           |               |            |          |           |       |         |        |        |       |           |
|                  EmptyTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   120.8 ns |  1.89 ns |   2.83 ns |  1.00 |    0.00 | 0.0420 |      - |     - |     265 B |
|             SimpleTextTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   180.8 ns |  2.01 ns |   3.01 ns |  1.50 |    0.03 | 0.0675 |      - |     - |     425 B |
|    SinglePropertyTokenTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   306.0 ns |  3.10 ns |   4.63 ns |  2.53 |    0.08 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   497.5 ns |  7.97 ns |  11.93 ns |  4.12 |    0.15 | 0.1497 |      - |     - |     947 B |
|      ManyPropertyTokenTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   621.2 ns |  7.93 ns |  11.88 ns |  5.14 |    0.15 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,229.1 ns | 18.21 ns |  27.26 ns | 10.18 |    0.35 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,717.9 ns | 24.44 ns |  34.27 ns | 14.25 |    0.35 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 4,770.7 ns | 56.17 ns |  80.55 ns | 39.53 |    1.14 | 1.0529 | 0.0229 |     - |    6652 B |
