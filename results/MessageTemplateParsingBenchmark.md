``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.403
  [Host]          : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|                         Method |             Job |       Jit |       Runtime |        Mean |     Error |    StdDev |      Median | Ratio | RatioSD |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------- |---------------- |---------- |-------------- |------------:|----------:|----------:|------------:|------:|--------:|-------:|-------:|------:|----------:|
|                  EmptyTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    32.19 ns |  0.153 ns |  0.224 ns |    32.23 ns |  1.00 |    0.00 | 0.0127 |      - |     - |      80 B |
|             SimpleTextTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   140.81 ns |  3.737 ns |  5.239 ns |   138.09 ns |  4.38 |    0.15 | 0.0610 |      - |     - |     384 B |
|    SinglePropertyTokenTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   184.43 ns |  0.953 ns |  1.396 ns |   184.39 ns |  5.73 |    0.06 | 0.0854 |      - |     - |     536 B |
| SingleTextWithPropertyTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   419.54 ns |  2.727 ns |  3.997 ns |   418.29 ns | 13.03 |    0.12 | 0.1440 | 0.0005 |     - |     904 B |
|      ManyPropertyTokenTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   375.12 ns |  1.399 ns |  2.006 ns |   374.79 ns | 11.65 |    0.10 | 0.1326 | 0.0005 |     - |     832 B |
|         MultipleTokensTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   758.07 ns |  4.384 ns |  6.146 ns |   758.64 ns | 23.56 |    0.24 | 0.2308 | 0.0019 |     - |    1448 B |
|   DefaultConsoleOutputTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   916.95 ns |  2.847 ns |  4.172 ns |   918.05 ns | 28.49 |    0.25 | 0.2584 | 0.0019 |     - |    1624 B |
|                    BigTemplate |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 2,644.76 ns | 11.695 ns | 17.504 ns | 2,642.67 ns | 82.16 |    0.68 | 0.7019 | 0.0153 |     - |    4424 B |
|                                |                 |           |               |             |           |           |             |       |         |        |        |       |           |
|                  EmptyTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |    41.13 ns |  0.451 ns |  0.648 ns |    41.10 ns |  1.00 |    0.00 | 0.0178 |      - |     - |     112 B |
|             SimpleTextTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   162.88 ns |  0.830 ns |  1.216 ns |   162.59 ns |  3.96 |    0.06 | 0.0687 |      - |     - |     433 B |
|    SinglePropertyTokenTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   241.39 ns |  2.161 ns |  3.099 ns |   240.13 ns |  5.87 |    0.12 | 0.0825 |      - |     - |     522 B |
| SingleTextWithPropertyTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   457.72 ns |  1.535 ns |  2.202 ns |   457.68 ns | 11.13 |    0.21 | 0.1478 | 0.0005 |     - |     931 B |
|      ManyPropertyTokenTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   489.48 ns |  2.338 ns |  3.427 ns |   489.92 ns | 11.90 |    0.16 | 0.1383 |      - |     - |     875 B |
|         MultipleTokensTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 |   978.71 ns |  5.397 ns |  8.077 ns |   977.62 ns | 23.81 |    0.44 | 0.2422 |      - |     - |    1524 B |
|   DefaultConsoleOutputTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,304.72 ns |  4.167 ns |  6.108 ns | 1,303.48 ns | 31.71 |    0.50 | 0.2670 | 0.0019 |     - |    1685 B |
|                    BigTemplate | net48 LegacyJit | LegacyJit |      .NET 4.8 | 3,721.91 ns |  9.901 ns | 14.200 ns | 3,722.50 ns | 90.50 |    1.39 | 0.7515 | 0.0153 |     - |    4742 B |
|                                |                 |           |               |             |           |           |             |       |         |        |        |       |           |
|                  EmptyTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |    42.08 ns |  0.920 ns |  1.349 ns |    42.92 ns |  1.00 |    0.00 | 0.0178 |      - |     - |     112 B |
|             SimpleTextTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   162.51 ns |  0.769 ns |  1.150 ns |   162.36 ns |  3.87 |    0.14 | 0.0687 |      - |     - |     433 B |
|    SinglePropertyTokenTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   242.13 ns |  1.671 ns |  2.501 ns |   241.33 ns |  5.76 |    0.14 | 0.0825 |      - |     - |     522 B |
| SingleTextWithPropertyTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   458.04 ns |  1.901 ns |  2.786 ns |   457.41 ns | 10.90 |    0.34 | 0.1478 | 0.0005 |     - |     931 B |
|      ManyPropertyTokenTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   486.60 ns |  1.775 ns |  2.656 ns |   486.88 ns | 11.58 |    0.39 | 0.1383 |      - |     - |     875 B |
|         MultipleTokensTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 |   990.66 ns |  8.607 ns | 12.616 ns |   989.45 ns | 23.57 |    1.01 | 0.2422 |      - |     - |    1524 B |
|   DefaultConsoleOutputTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,345.14 ns | 33.906 ns | 47.532 ns | 1,337.94 ns | 31.94 |    1.94 | 0.2670 | 0.0019 |     - |    1685 B |
|                    BigTemplate |    net48 RyuJit |    RyuJit |      .NET 4.8 | 3,781.96 ns | 35.771 ns | 52.432 ns | 3,775.54 ns | 89.94 |    2.38 | 0.7477 | 0.0153 |     - |    4742 B |
