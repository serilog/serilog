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
|                  EmptyTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   145.7 ns |  0.71 ns |  1.02 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   215.5 ns |  0.70 ns |  1.02 ns |  1.48 |    0.01 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   269.8 ns |  2.17 ns |  3.18 ns |  1.85 |    0.02 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   521.5 ns |  4.19 ns |  6.27 ns |  3.58 |    0.04 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   497.1 ns |  1.84 ns |  2.75 ns |  3.41 |    0.03 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,018.3 ns |  5.02 ns |  7.20 ns |  6.99 |    0.06 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,281.5 ns |  7.75 ns | 11.12 ns |  8.80 |    0.08 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 3,629.6 ns | 14.31 ns | 20.53 ns | 24.92 |    0.15 | 0.9956 | 0.0229 |     - |    6264 B |
|                                |                 |           |               |            |          |          |       |         |        |        |       |           |
|                  EmptyTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   126.8 ns |  0.54 ns |  0.76 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   178.8 ns |  0.68 ns |  1.00 ns |  1.41 |    0.01 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   297.8 ns |  0.93 ns |  1.36 ns |  2.35 |    0.02 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   466.0 ns |  1.81 ns |  2.60 ns |  3.68 |    0.03 | 0.1502 | 0.0005 |     - |     947 B |
|      ManyPropertyTokenTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   607.4 ns |  2.29 ns |  3.28 ns |  4.79 |    0.04 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,201.9 ns |  7.67 ns | 11.49 ns |  9.49 |    0.12 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,613.1 ns |  5.69 ns |  8.16 ns | 12.72 |    0.09 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 4,613.3 ns | 19.68 ns | 28.85 ns | 36.38 |    0.28 | 1.0529 | 0.0229 |     - |    6652 B |
|                                |                 |           |               |            |          |          |       |         |        |        |       |           |
|                  EmptyTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   128.3 ns |  0.95 ns |  1.42 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   181.0 ns |  4.42 ns |  6.33 ns |  1.41 |    0.05 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   299.2 ns |  1.53 ns |  2.25 ns |  2.33 |    0.02 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   464.3 ns |  1.81 ns |  2.70 ns |  3.62 |    0.04 | 0.1502 | 0.0005 |     - |     947 B |
|      ManyPropertyTokenTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   606.3 ns |  1.53 ns |  2.25 ns |  4.73 |    0.05 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,196.1 ns |  5.14 ns |  7.37 ns |  9.33 |    0.11 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,615.3 ns |  6.47 ns |  9.49 ns | 12.59 |    0.16 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 4,611.8 ns | 15.41 ns | 23.07 ns | 35.94 |    0.42 | 1.0529 | 0.0229 |     - |    6652 B |
