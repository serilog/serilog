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
|                  EmptyTemplate | core31 | .NET Core 3.1 |    32.79 ns |  0.406 ns |  0.607 ns |  1.00 |    0.00 | 0.0127 |      - |     - |      80 B |
|             SimpleTextTemplate | core31 | .NET Core 3.1 |   139.69 ns |  1.703 ns |  2.549 ns |  4.26 |    0.09 | 0.0610 |      - |     - |     384 B |
|    SinglePropertyTokenTemplate | core31 | .NET Core 3.1 |   192.07 ns |  1.607 ns |  2.305 ns |  5.86 |    0.15 | 0.0854 |      - |     - |     536 B |
| SingleTextWithPropertyTemplate | core31 | .NET Core 3.1 |   428.80 ns |  2.452 ns |  3.670 ns | 13.08 |    0.27 | 0.1440 | 0.0005 |     - |     904 B |
|      ManyPropertyTokenTemplate | core31 | .NET Core 3.1 |   382.27 ns |  2.979 ns |  4.459 ns | 11.66 |    0.26 | 0.1326 | 0.0005 |     - |     832 B |
|         MultipleTokensTemplate | core31 | .NET Core 3.1 |   771.59 ns |  6.362 ns |  9.523 ns | 23.54 |    0.42 | 0.2308 | 0.0019 |     - |    1448 B |
|   DefaultConsoleOutputTemplate | core31 | .NET Core 3.1 |   927.86 ns |  7.847 ns | 11.746 ns | 28.31 |    0.70 | 0.2575 | 0.0019 |     - |    1624 B |
|                    BigTemplate | core31 | .NET Core 3.1 | 2,660.52 ns | 27.007 ns | 40.423 ns | 81.16 |    1.92 | 0.7019 | 0.0153 |     - |    4424 B |
|                                |        |               |             |           |           |       |         |        |        |       |           |
|                  EmptyTemplate |  net48 |      .NET 4.8 |    42.59 ns |  0.363 ns |  0.520 ns |  1.00 |    0.00 | 0.0178 |      - |     - |     112 B |
|             SimpleTextTemplate |  net48 |      .NET 4.8 |   162.25 ns |  1.582 ns |  2.367 ns |  3.81 |    0.08 | 0.0687 |      - |     - |     433 B |
|    SinglePropertyTokenTemplate |  net48 |      .NET 4.8 |   248.45 ns |  3.135 ns |  4.692 ns |  5.83 |    0.12 | 0.0825 |      - |     - |     522 B |
| SingleTextWithPropertyTemplate |  net48 |      .NET 4.8 |   472.95 ns |  6.069 ns |  8.895 ns | 11.11 |    0.24 | 0.1478 |      - |     - |     931 B |
|      ManyPropertyTokenTemplate |  net48 |      .NET 4.8 |   493.78 ns |  5.937 ns |  8.886 ns | 11.60 |    0.25 | 0.1383 |      - |     - |     875 B |
|         MultipleTokensTemplate |  net48 |      .NET 4.8 |   994.83 ns | 12.807 ns | 19.169 ns | 23.35 |    0.54 | 0.2422 |      - |     - |    1524 B |
|   DefaultConsoleOutputTemplate |  net48 |      .NET 4.8 | 1,327.12 ns | 12.583 ns | 18.834 ns | 31.16 |    0.49 | 0.2670 | 0.0019 |     - |    1685 B |
|                    BigTemplate |  net48 |      .NET 4.8 | 3,796.51 ns | 17.700 ns | 25.944 ns | 89.13 |    1.16 | 0.7515 | 0.0153 |     - |    4742 B |
|                                |        |               |             |           |           |       |         |        |        |       |           |
|                  EmptyTemplate |  net50 | .NET Core 5.0 |    29.57 ns |  0.501 ns |  0.751 ns |  1.00 |    0.00 | 0.0127 |      - |     - |      80 B |
|             SimpleTextTemplate |  net50 | .NET Core 5.0 |   136.77 ns |  2.122 ns |  3.110 ns |  4.63 |    0.19 | 0.0610 |      - |     - |     384 B |
|    SinglePropertyTokenTemplate |  net50 | .NET Core 5.0 |   179.27 ns |  1.876 ns |  2.808 ns |  6.07 |    0.19 | 0.0854 |      - |     - |     536 B |
| SingleTextWithPropertyTemplate |  net50 | .NET Core 5.0 |   403.27 ns |  6.109 ns |  9.144 ns | 13.65 |    0.47 | 0.1440 | 0.0005 |     - |     904 B |
|      ManyPropertyTokenTemplate |  net50 | .NET Core 5.0 |   355.66 ns |  3.487 ns |  5.219 ns | 12.04 |    0.33 | 0.1326 | 0.0005 |     - |     832 B |
|         MultipleTokensTemplate |  net50 | .NET Core 5.0 |   718.73 ns |  6.594 ns |  9.869 ns | 24.32 |    0.72 | 0.2308 | 0.0019 |     - |    1448 B |
|   DefaultConsoleOutputTemplate |  net50 | .NET Core 5.0 |   885.10 ns |  5.246 ns |  7.689 ns | 29.98 |    0.86 | 0.2584 | 0.0019 |     - |    1624 B |
|                    BigTemplate |  net50 | .NET Core 5.0 | 2,499.51 ns | 27.388 ns | 40.993 ns | 84.59 |    2.60 | 0.7019 | 0.0153 |     - |    4424 B |
