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
|                         Method |    Job |       Runtime |        Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |------- |-------------- |------------:|----------:|----------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate | core31 | .NET Core 3.1 |    33.83 ns |  0.558 ns |  0.835 ns |  1.00 |    0.00 | 0.0127 |      - |     - |      80 B |
|             SimpleTextTemplate | core31 | .NET Core 3.1 |   141.33 ns |  1.439 ns |  2.154 ns |  4.18 |    0.10 | 0.0610 |      - |     - |     384 B |
|    SinglePropertyTokenTemplate | core31 | .NET Core 3.1 |   192.89 ns |  2.379 ns |  3.561 ns |  5.71 |    0.20 | 0.0854 |      - |     - |     536 B |
| SingleTextWithPropertyTemplate | core31 | .NET Core 3.1 |   434.24 ns |  2.681 ns |  4.013 ns | 12.84 |    0.34 | 0.1440 | 0.0005 |     - |     904 B |
|      ManyPropertyTokenTemplate | core31 | .NET Core 3.1 |   385.23 ns |  3.620 ns |  5.418 ns | 11.39 |    0.33 | 0.1326 | 0.0005 |     - |     832 B |
|         MultipleTokensTemplate | core31 | .NET Core 3.1 |   781.94 ns |  4.217 ns |  6.048 ns | 23.16 |    0.61 | 0.2308 | 0.0019 |     - |    1448 B |
|   DefaultConsoleOutputTemplate | core31 | .NET Core 3.1 |   956.98 ns |  9.980 ns | 14.937 ns | 28.31 |    0.91 | 0.2584 | 0.0019 |     - |    1624 B |
|                    BigTemplate | core31 | .NET Core 3.1 | 2,725.95 ns | 22.958 ns | 34.362 ns | 80.63 |    2.24 | 0.7019 | 0.0153 |     - |    4424 B |
|                                |        |               |             |           |           |       |         |        |        |       |           |
|                  EmptyTemplate |  net48 |      .NET 4.8 |    44.89 ns |  0.872 ns |  1.306 ns |  1.00 |    0.00 | 0.0178 |      - |     - |     112 B |
|             SimpleTextTemplate |  net48 |      .NET 4.8 |   165.91 ns |  1.633 ns |  2.444 ns |  3.70 |    0.13 | 0.0687 |      - |     - |     433 B |
|    SinglePropertyTokenTemplate |  net48 |      .NET 4.8 |   256.89 ns |  5.588 ns |  8.364 ns |  5.73 |    0.19 | 0.0825 |      - |     - |     522 B |
| SingleTextWithPropertyTemplate |  net48 |      .NET 4.8 |   483.68 ns |  5.787 ns |  8.662 ns | 10.78 |    0.36 | 0.1478 |      - |     - |     931 B |
|      ManyPropertyTokenTemplate |  net48 |      .NET 4.8 |   506.08 ns |  6.496 ns |  9.722 ns | 11.28 |    0.38 | 0.1383 |      - |     - |     875 B |
|         MultipleTokensTemplate |  net48 |      .NET 4.8 | 1,018.84 ns | 14.628 ns | 21.895 ns | 22.72 |    0.79 | 0.2422 |      - |     - |    1524 B |
|   DefaultConsoleOutputTemplate |  net48 |      .NET 4.8 | 1,358.40 ns | 14.005 ns | 20.962 ns | 30.29 |    0.99 | 0.2670 | 0.0019 |     - |    1685 B |
|                    BigTemplate |  net48 |      .NET 4.8 | 3,924.01 ns | 51.520 ns | 77.112 ns | 87.50 |    3.22 | 0.7477 | 0.0153 |     - |    4742 B |
|                                |        |               |             |           |           |       |         |        |        |       |           |
|                  EmptyTemplate |  net50 | .NET Core 5.0 |    31.40 ns |  0.689 ns |  1.009 ns |  1.00 |    0.00 | 0.0127 |      - |     - |      80 B |
|             SimpleTextTemplate |  net50 | .NET Core 5.0 |   139.84 ns |  1.792 ns |  2.682 ns |  4.46 |    0.13 | 0.0610 |      - |     - |     384 B |
|    SinglePropertyTokenTemplate |  net50 | .NET Core 5.0 |   185.68 ns |  1.686 ns |  2.523 ns |  5.92 |    0.19 | 0.0854 |      - |     - |     536 B |
| SingleTextWithPropertyTemplate |  net50 | .NET Core 5.0 |   416.30 ns |  4.889 ns |  7.318 ns | 13.27 |    0.58 | 0.1440 | 0.0005 |     - |     904 B |
|      ManyPropertyTokenTemplate |  net50 | .NET Core 5.0 |   366.99 ns |  4.555 ns |  6.817 ns | 11.70 |    0.52 | 0.1326 | 0.0005 |     - |     832 B |
|         MultipleTokensTemplate |  net50 | .NET Core 5.0 |   738.07 ns |  6.854 ns | 10.258 ns | 23.52 |    0.88 | 0.2308 | 0.0019 |     - |    1448 B |
|   DefaultConsoleOutputTemplate |  net50 | .NET Core 5.0 |   899.23 ns |  5.892 ns |  8.820 ns | 28.66 |    0.99 | 0.2584 | 0.0019 |     - |    1624 B |
|                    BigTemplate |  net50 | .NET Core 5.0 | 2,582.90 ns | 26.181 ns | 39.186 ns | 82.39 |    3.26 | 0.7019 | 0.0153 |     - |    4424 B |
