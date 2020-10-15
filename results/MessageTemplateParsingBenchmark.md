``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
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
|                  EmptyTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   145.4 ns |  1.08 ns |  1.52 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   215.0 ns |  2.78 ns |  3.98 ns |  1.48 |    0.04 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   268.8 ns |  1.28 ns |  1.83 ns |  1.85 |    0.02 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   522.0 ns |  3.91 ns |  5.61 ns |  3.59 |    0.03 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   494.6 ns |  3.34 ns |  5.00 ns |  3.40 |    0.04 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,025.1 ns |  5.22 ns |  7.81 ns |  7.05 |    0.06 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 1,315.7 ns | 36.37 ns | 54.44 ns |  9.04 |    0.46 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 3,615.4 ns | 17.91 ns | 25.10 ns | 24.86 |    0.20 | 0.9956 | 0.0229 |     - |    6264 B |
|                                |                 |           |               |            |          |          |       |         |        |        |       |           |
|                  EmptyTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   126.8 ns |  0.40 ns |  0.60 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   184.2 ns |  1.67 ns |  2.50 ns |  1.45 |    0.02 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   297.0 ns |  1.16 ns |  1.69 ns |  2.34 |    0.02 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   463.4 ns |  0.99 ns |  1.42 ns |  3.65 |    0.02 | 0.1502 | 0.0005 |     - |     947 B |
|      ManyPropertyTokenTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   606.7 ns |  3.25 ns |  4.66 ns |  4.78 |    0.04 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,201.3 ns |  5.68 ns |  8.50 ns |  9.47 |    0.07 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,611.8 ns |  6.16 ns |  8.84 ns | 12.71 |    0.09 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 4,618.5 ns | 26.35 ns | 38.62 ns | 36.42 |    0.36 | 1.0529 | 0.0229 |     - |    6652 B |
|                                |                 |           |               |            |          |          |       |         |        |        |       |           |
|                  EmptyTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   127.2 ns |  0.60 ns |  0.89 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   178.6 ns |  0.84 ns |  1.25 ns |  1.40 |    0.01 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   295.7 ns |  1.08 ns |  1.54 ns |  2.32 |    0.02 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   464.3 ns |  1.34 ns |  1.97 ns |  3.65 |    0.03 | 0.1502 | 0.0005 |     - |     947 B |
|      ManyPropertyTokenTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   602.3 ns |  2.16 ns |  3.10 ns |  4.74 |    0.04 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,202.7 ns |  7.65 ns | 11.21 ns |  9.45 |    0.11 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,610.3 ns |  4.91 ns |  7.34 ns | 12.66 |    0.10 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 4,593.3 ns | 19.54 ns | 28.64 ns | 36.10 |    0.31 | 1.0529 | 0.0229 |     - |    6652 B |
