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
|                  EmptyTemplate | core31 | .NET Core 3.1 |   146.6 ns |   1.41 ns |   2.11 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate | core31 | .NET Core 3.1 |   218.0 ns |   1.72 ns |   2.52 ns |  1.49 |    0.03 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate | core31 | .NET Core 3.1 |   273.7 ns |   3.01 ns |   4.50 ns |  1.87 |    0.03 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate | core31 | .NET Core 3.1 |   510.5 ns |   6.42 ns |   9.61 ns |  3.48 |    0.08 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate | core31 | .NET Core 3.1 |   511.1 ns |   7.19 ns |  10.77 ns |  3.49 |    0.10 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate | core31 | .NET Core 3.1 | 1,105.2 ns |  67.35 ns | 100.80 ns |  7.54 |    0.72 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate | core31 | .NET Core 3.1 | 1,313.0 ns |  11.25 ns |  16.84 ns |  8.96 |    0.17 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate | core31 | .NET Core 3.1 | 3,687.7 ns |  32.68 ns |  48.92 ns | 25.15 |    0.42 | 0.9918 | 0.0229 |     - |    6264 B |
|                                |        |               |            |           |           |       |         |        |        |       |           |
|                  EmptyTemplate |  net48 |      .NET 4.8 |   138.1 ns |   6.51 ns |   9.75 ns |  1.00 |    0.00 | 0.0458 |      - |     - |     289 B |
|             SimpleTextTemplate |  net48 |      .NET 4.8 |   182.0 ns |   1.55 ns |   2.22 ns |  1.32 |    0.10 | 0.0713 |      - |     - |     449 B |
|    SinglePropertyTokenTemplate |  net48 |      .NET 4.8 |   322.1 ns |  11.27 ns |  16.88 ns |  2.34 |    0.06 | 0.0901 |      - |     - |     570 B |
| SingleTextWithPropertyTemplate |  net48 |      .NET 4.8 |   477.4 ns |   3.69 ns |   5.40 ns |  3.47 |    0.25 | 0.1497 |      - |     - |     947 B |
|      ManyPropertyTokenTemplate |  net48 |      .NET 4.8 |   615.9 ns |   5.42 ns |   7.94 ns |  4.47 |    0.31 | 0.1707 |      - |     - |    1075 B |
|         MultipleTokensTemplate |  net48 |      .NET 4.8 | 1,288.5 ns |  48.70 ns |  72.89 ns |  9.41 |    1.18 | 0.2918 | 0.0019 |     - |    1845 B |
|   DefaultConsoleOutputTemplate |  net48 |      .NET 4.8 | 1,640.3 ns |   9.25 ns |  13.84 ns | 11.93 |    0.85 | 0.3643 | 0.0019 |     - |    2303 B |
|                    BigTemplate |  net48 |      .NET 4.8 | 4,939.3 ns | 154.40 ns | 231.10 ns | 36.04 |    4.16 | 1.0529 | 0.0229 |     - |    6652 B |
|                                |        |               |            |           |           |       |         |        |        |       |           |
|                  EmptyTemplate |  net50 | .NET Core 5.0 |   137.0 ns |   2.12 ns |   3.17 ns |  1.00 |    0.00 | 0.0408 |      - |     - |     256 B |
|             SimpleTextTemplate |  net50 | .NET Core 5.0 |   197.5 ns |   1.60 ns |   2.40 ns |  1.44 |    0.04 | 0.0648 |      - |     - |     408 B |
|    SinglePropertyTokenTemplate |  net50 | .NET Core 5.0 |   255.5 ns |   3.09 ns |   4.62 ns |  1.87 |    0.05 | 0.0877 |      - |     - |     552 B |
| SingleTextWithPropertyTemplate |  net50 | .NET Core 5.0 |   494.8 ns |   6.58 ns |   9.85 ns |  3.61 |    0.13 | 0.1478 |      - |     - |     928 B |
|      ManyPropertyTokenTemplate |  net50 | .NET Core 5.0 |   475.8 ns |   5.71 ns |   8.54 ns |  3.48 |    0.10 | 0.1650 |      - |     - |    1040 B |
|         MultipleTokensTemplate |  net50 | .NET Core 5.0 |   938.1 ns |  10.10 ns |  15.12 ns |  6.85 |    0.14 | 0.2823 | 0.0019 |     - |    1776 B |
|   DefaultConsoleOutputTemplate |  net50 | .NET Core 5.0 | 1,236.8 ns |  22.65 ns |  33.89 ns |  9.04 |    0.42 | 0.3567 | 0.0019 |     - |    2240 B |
|                    BigTemplate |  net50 | .NET Core 5.0 | 3,388.6 ns |  24.87 ns |  37.22 ns | 24.75 |    0.72 | 0.9956 | 0.0267 |     - |    6264 B |
