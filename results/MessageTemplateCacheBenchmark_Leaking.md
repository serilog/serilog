``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT

IterationCount=3  LaunchCount=1  WarmupCount=3  

```
|     Method |             Job |       Jit |       Runtime | Items | OverflowCount | MaxDegreeOfParallelism |       Mean |       Error |     StdDev |     Median |  Ratio | RatioSD |
|----------- |---------------- |---------- |-------------- |------ |-------------- |----------------------- |-----------:|------------:|-----------:|-----------:|-------:|--------:|
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |             **1** |                     **-1** |   **1.457 ms** |   **0.6512 ms** |  **0.0357 ms** |   **1.453 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                     -1 |   2.367 ms |  20.1677 ms |  1.1055 ms |   1.835 ms |   1.61 |    0.71 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                     -1 |  40.774 ms |  28.5363 ms |  1.5642 ms |  40.081 ms |  27.97 |    0.46 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                     -1 |   3.079 ms |   1.8529 ms |  0.1016 ms |   3.104 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                     -1 |  21.164 ms |  27.0860 ms |  1.4847 ms |  21.876 ms |   6.87 |    0.29 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                     -1 |  46.261 ms |  63.1463 ms |  3.4613 ms |  45.978 ms |  15.04 |    1.31 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                     -1 |   2.967 ms |   2.5385 ms |  0.1391 ms |   2.913 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                     -1 |  21.209 ms |  21.6706 ms |  1.1878 ms |  21.381 ms |   7.16 |    0.44 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                     -1 |  52.292 ms |  13.0502 ms |  0.7153 ms |  52.178 ms |  17.65 |    0.58 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |             **1** |                      **1** |   **1.065 ms** |   **0.1285 ms** |  **0.0070 ms** |   **1.064 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                      1 |   2.089 ms |   0.3525 ms |  0.0193 ms |   2.083 ms |   1.96 |    0.03 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                      1 | 227.580 ms |  41.8990 ms |  2.2966 ms | 228.821 ms | 213.72 |    1.37 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.752 ms |   0.0886 ms |  0.0049 ms |   1.750 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.763 ms |   0.1554 ms |  0.0085 ms |   1.768 ms |   1.01 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                      1 | 237.424 ms |  11.1056 ms |  0.6087 ms | 237.705 ms | 135.55 |    0.72 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.761 ms |   0.1463 ms |  0.0080 ms |   1.765 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.752 ms |   0.0740 ms |  0.0041 ms |   1.753 ms |   0.99 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                      1 | 239.999 ms |   1.7500 ms |  0.0959 ms | 239.971 ms | 136.31 |    0.61 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |            **10** |                     **-1** |   **1.523 ms** |   **0.9564 ms** |  **0.0524 ms** |   **1.528 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                     -1 |   3.048 ms |   3.3553 ms |  0.1839 ms |   3.145 ms |   2.00 |    0.14 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                     -1 |  61.383 ms | 122.7008 ms |  6.7256 ms |  60.021 ms |  40.24 |    3.07 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                     -1 |   3.042 ms |   3.3104 ms |  0.1815 ms |   2.957 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                     -1 |  19.756 ms |  26.3398 ms |  1.4438 ms |  19.464 ms |   6.52 |    0.70 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                     -1 |  51.326 ms |  67.4641 ms |  3.6979 ms |  52.393 ms |  16.91 |    1.50 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                     -1 |   2.898 ms |   0.9969 ms |  0.0546 ms |   2.871 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                     -1 |  21.382 ms |  23.8066 ms |  1.3049 ms |  20.855 ms |   7.38 |    0.51 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                     -1 |  49.026 ms |  98.4624 ms |  5.3971 ms |  49.961 ms |  16.90 |    1.64 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |            **10** |                      **1** |   **1.062 ms** |   **0.1088 ms** |  **0.0060 ms** |   **1.064 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                      1 |   2.062 ms |   0.0266 ms |  0.0015 ms |   2.062 ms |   1.94 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                      1 | 226.759 ms |  27.1737 ms |  1.4895 ms | 226.054 ms | 213.57 |    2.60 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.750 ms |   0.0712 ms |  0.0039 ms |   1.750 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.774 ms |   0.0407 ms |  0.0022 ms |   1.773 ms |   1.01 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                      1 | 240.998 ms |  38.0448 ms |  2.0854 ms | 241.122 ms | 137.68 |    1.05 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.754 ms |   0.0962 ms |  0.0053 ms |   1.754 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.761 ms |   0.1572 ms |  0.0086 ms |   1.757 ms |   1.00 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                      1 | 238.852 ms |  10.7408 ms |  0.5887 ms | 238.599 ms | 136.21 |    0.26 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |           **100** |                     **-1** |   **1.560 ms** |   **0.1824 ms** |  **0.0100 ms** |   **1.558 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                     -1 |   2.969 ms |   2.5679 ms |  0.1408 ms |   2.992 ms |   1.90 |    0.10 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                     -1 |  99.442 ms | 182.4203 ms |  9.9991 ms |  99.540 ms |  63.78 |    6.67 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                     -1 |   3.321 ms |   1.3501 ms |  0.0740 ms |   3.361 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                     -1 |  19.918 ms |   9.5733 ms |  0.5247 ms |  19.961 ms |   6.00 |    0.07 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                     -1 |  70.606 ms |  64.2076 ms |  3.5194 ms |  71.694 ms |  21.28 |    1.45 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                     -1 |   3.050 ms |   0.7419 ms |  0.0407 ms |   3.071 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                     -1 |  19.923 ms |  17.9744 ms |  0.9852 ms |  19.848 ms |   6.53 |    0.33 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                     -1 |  65.520 ms | 163.0035 ms |  8.9348 ms |  61.829 ms |  21.47 |    2.77 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |           **100** |                      **1** |   **1.062 ms** |   **0.1458 ms** |  **0.0080 ms** |   **1.066 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                      1 |   2.066 ms |   0.0278 ms |  0.0015 ms |   2.066 ms |   1.94 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                      1 | 221.667 ms |   9.8855 ms |  0.5419 ms | 221.554 ms | 208.69 |    1.53 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.760 ms |   0.0898 ms |  0.0049 ms |   1.760 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.763 ms |   0.0779 ms |  0.0043 ms |   1.761 ms |   1.00 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                      1 | 235.597 ms |  16.5720 ms |  0.9084 ms | 235.256 ms | 133.85 |    0.61 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.753 ms |   0.0368 ms |  0.0020 ms |   1.754 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.762 ms |   0.0555 ms |  0.0030 ms |   1.761 ms |   1.01 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                      1 | 235.974 ms |  34.0765 ms |  1.8678 ms | 234.908 ms | 134.62 |    0.97 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |          **1000** |                     **-1** |   **1.750 ms** |   **0.5224 ms** |  **0.0286 ms** |   **1.736 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                     -1 |   3.613 ms |   4.9964 ms |  0.2739 ms |   3.596 ms |   2.06 |    0.13 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                     -1 | 180.858 ms | 252.4718 ms | 13.8388 ms | 182.498 ms | 103.29 |    6.70 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                     -1 |   3.395 ms |   2.8198 ms |  0.1546 ms |   3.334 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                     -1 |  16.344 ms |  24.9617 ms |  1.3682 ms |  15.555 ms |   4.81 |    0.19 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                     -1 | 124.900 ms | 280.6305 ms | 15.3823 ms | 130.914 ms |  36.77 |    4.03 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                     -1 |   3.342 ms |   1.6147 ms |  0.0885 ms |   3.329 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                     -1 |  16.681 ms |  40.9359 ms |  2.2438 ms |  16.443 ms |   5.01 |    0.80 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                     -1 | 143.156 ms | 387.4348 ms | 21.2366 ms | 137.651 ms |  42.93 |    7.18 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |          **1000** |                      **1** |   **1.057 ms** |   **0.0562 ms** |  **0.0031 ms** |   **1.058 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                      1 |   2.059 ms |   0.2280 ms |  0.0125 ms |   2.065 ms |   1.95 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                      1 | 227.649 ms |   5.9494 ms |  0.3261 ms | 227.687 ms | 215.44 |    0.44 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.755 ms |   0.1161 ms |  0.0064 ms |   1.756 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.771 ms |   0.1697 ms |  0.0093 ms |   1.770 ms |   1.01 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                      1 | 236.448 ms |  20.6591 ms |  1.1324 ms | 237.091 ms | 134.71 |    1.08 |
|            |                 |           |               |       |               |                        |            |             |            |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.761 ms |   0.1533 ms |  0.0084 ms |   1.758 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.765 ms |   0.1104 ms |  0.0061 ms |   1.766 ms |   1.00 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                      1 | 242.713 ms |  28.8252 ms |  1.5800 ms | 242.183 ms | 137.82 |    0.39 |
