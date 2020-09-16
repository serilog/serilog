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
|                         Method |             Job |       Jit |       Runtime |       Mean |    Error |   StdDev |     Median | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |---------------- |---------- |-------------- |-----------:|---------:|---------:|-----------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   172.8 ns |  8.79 ns | 12.89 ns |   167.6 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   244.9 ns |  3.77 ns |  5.64 ns |   245.0 ns |  1.42 |    0.10 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   311.3 ns |  4.18 ns |  6.26 ns |   310.6 ns |  1.81 |    0.16 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   574.2 ns |  3.81 ns |  5.58 ns |   574.8 ns |  3.34 |    0.24 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   565.8 ns |  4.90 ns |  7.34 ns |   565.2 ns |  3.29 |    0.25 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,155.6 ns | 49.64 ns | 72.76 ns | 1,201.6 ns |  6.70 |    0.38 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,313.6 ns |  4.04 ns |  6.04 ns | 1,313.3 ns |  7.64 |    0.58 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 3,668.6 ns | 13.98 ns | 18.66 ns | 3,663.8 ns | 21.11 |    1.51 | 0.9956 | 0.0229 |     - |    6264 B |
|                                |                 |           |               |            |          |          |            |       |         |        |        |       |           |
|                  EmptyTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   128.8 ns |  0.52 ns |  0.78 ns |   128.8 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   179.9 ns |  0.43 ns |  0.64 ns |   179.9 ns |  1.40 |    0.01 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   300.9 ns |  1.36 ns |  2.00 ns |   301.2 ns |  2.34 |    0.01 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   476.0 ns |  5.46 ns |  7.84 ns |   473.6 ns |  3.69 |    0.05 | 0.1502 | 0.0005 |     - |     947 B |
|      ManyPropertyTokenTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   613.0 ns |  2.79 ns |  4.18 ns |   611.9 ns |  4.76 |    0.04 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,217.2 ns |  2.39 ns |  3.42 ns | 1,217.6 ns |  9.44 |    0.06 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,631.7 ns |  3.40 ns |  4.88 ns | 1,632.1 ns | 12.66 |    0.09 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 4,693.2 ns | 11.55 ns | 16.20 ns | 4,691.3 ns | 36.40 |    0.25 | 1.0529 | 0.0229 |     - |    6652 B |
|                                |                 |           |               |            |          |          |            |       |         |        |        |       |           |
|                  EmptyTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   128.9 ns |  0.37 ns |  0.54 ns |   128.8 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   179.8 ns |  0.40 ns |  0.59 ns |   179.8 ns |  1.39 |    0.01 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   301.2 ns |  0.75 ns |  1.08 ns |   301.5 ns |  2.34 |    0.02 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   473.0 ns |  1.67 ns |  2.45 ns |   472.8 ns |  3.67 |    0.02 | 0.1502 | 0.0005 |     - |     947 B |
|      ManyPropertyTokenTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   611.3 ns |  1.33 ns |  1.91 ns |   611.1 ns |  4.74 |    0.03 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,215.3 ns |  3.59 ns |  5.37 ns | 1,215.0 ns |  9.43 |    0.06 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,634.8 ns |  4.90 ns |  7.34 ns | 1,634.3 ns | 12.68 |    0.08 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 4,685.7 ns | 12.03 ns | 17.26 ns | 4,685.5 ns | 36.34 |    0.23 | 1.0529 | 0.0229 |     - |    6652 B |
