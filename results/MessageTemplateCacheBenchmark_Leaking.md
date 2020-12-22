``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.404
  [Host]          : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT

IterationCount=3  LaunchCount=1  WarmupCount=3  

```
|     Method |             Job |       Jit |       Runtime | Items | OverflowCount | MaxDegreeOfParallelism |       Mean |       Error |     StdDev |  Ratio | RatioSD |
|----------- |---------------- |---------- |-------------- |------ |-------------- |----------------------- |-----------:|------------:|-----------:|-------:|--------:|
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |             **1** |                     **-1** |   **1.448 ms** |   **0.3337 ms** |  **0.0183 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                     -1 |   2.088 ms |  10.4581 ms |  0.5732 ms |   1.44 |    0.39 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                     -1 |  36.287 ms | 121.0587 ms |  6.6356 ms |  25.03 |    4.34 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                     -1 |   3.169 ms |   1.3705 ms |  0.0751 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                     -1 |  25.297 ms |  22.7983 ms |  1.2497 ms |   7.98 |    0.22 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                     -1 |  50.659 ms |  24.5365 ms |  1.3449 ms |  15.99 |    0.71 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                     -1 |   2.925 ms |   1.2222 ms |  0.0670 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                     -1 |  21.692 ms |  25.7048 ms |  1.4090 ms |   7.42 |    0.48 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                     -1 |  50.755 ms |  61.7170 ms |  3.3829 ms |  17.37 |    1.57 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |             **1** |                      **1** |   **1.112 ms** |   **0.3239 ms** |  **0.0178 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                      1 |   2.146 ms |   0.4507 ms |  0.0247 ms |   1.93 |    0.03 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                      1 | 245.013 ms |  22.1912 ms |  1.2164 ms | 220.45 |    3.01 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.848 ms |   0.2794 ms |  0.0153 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.870 ms |   0.2651 ms |  0.0145 ms |   1.01 |    0.02 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                      1 | 253.643 ms |  84.2790 ms |  4.6196 ms | 137.29 |    3.46 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.853 ms |   0.5045 ms |  0.0277 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.833 ms |   0.1204 ms |  0.0066 ms |   0.99 |    0.02 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                      1 | 249.085 ms |  68.2503 ms |  3.7410 ms | 134.42 |    0.40 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |            **10** |                     **-1** |   **1.485 ms** |   **0.4470 ms** |  **0.0245 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                     -1 |   1.330 ms |   0.9341 ms |  0.0512 ms |   0.90 |    0.05 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                     -1 |  50.512 ms | 172.7309 ms |  9.4680 ms |  34.00 |    6.30 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                     -1 |   3.138 ms |   3.5872 ms |  0.1966 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                     -1 |  21.832 ms |  14.8413 ms |  0.8135 ms |   6.97 |    0.25 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                     -1 |  50.977 ms |  80.7760 ms |  4.4276 ms |  16.32 |    2.12 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                     -1 |   3.057 ms |   1.5126 ms |  0.0829 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                     -1 |  22.540 ms |  20.8428 ms |  1.1425 ms |   7.37 |    0.32 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                     -1 |  50.044 ms | 158.6416 ms |  8.6957 ms |  16.41 |    3.18 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |            **10** |                      **1** |   **1.106 ms** |   **0.2648 ms** |  **0.0145 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                      1 |   2.150 ms |   0.4101 ms |  0.0225 ms |   1.94 |    0.04 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                      1 | 239.706 ms |  39.1046 ms |  2.1435 ms | 216.75 |    4.67 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.844 ms |   0.3961 ms |  0.0217 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.839 ms |   0.3208 ms |  0.0176 ms |   1.00 |    0.02 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                      1 | 254.680 ms |  81.5211 ms |  4.4684 ms | 138.15 |    2.89 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.846 ms |   0.1987 ms |  0.0109 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.832 ms |   0.2389 ms |  0.0131 ms |   0.99 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                      1 | 253.384 ms | 100.6085 ms |  5.5147 ms | 137.26 |    3.36 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |           **100** |                     **-1** |   **1.579 ms** |   **0.2376 ms** |  **0.0130 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                     -1 |   2.468 ms |   6.0065 ms |  0.3292 ms |   1.56 |    0.21 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                     -1 |  99.429 ms | 276.7430 ms | 15.1692 ms |  63.03 |   10.12 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                     -1 |   3.151 ms |   3.3547 ms |  0.1839 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                     -1 |  20.733 ms |  19.7465 ms |  1.0824 ms |   6.60 |    0.63 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                     -1 |  65.185 ms |  56.9070 ms |  3.1193 ms |  20.77 |    2.13 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                     -1 |   3.204 ms |   2.0813 ms |  0.1141 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                     -1 |  20.250 ms |  31.3162 ms |  1.7165 ms |   6.32 |    0.49 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                     -1 |  64.327 ms | 171.1890 ms |  9.3834 ms |  20.16 |    3.57 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |           **100** |                      **1** |   **1.116 ms** |   **0.0732 ms** |  **0.0040 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                      1 |   2.169 ms |   0.3516 ms |  0.0193 ms |   1.94 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                      1 | 244.768 ms |  28.3780 ms |  1.5555 ms | 219.35 |    2.00 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.857 ms |   0.4573 ms |  0.0251 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.848 ms |   0.1288 ms |  0.0071 ms |   1.00 |    0.02 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                      1 | 249.702 ms |  91.4514 ms |  5.0128 ms | 134.47 |    2.93 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.857 ms |   0.0945 ms |  0.0052 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.849 ms |   0.3860 ms |  0.0212 ms |   1.00 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                      1 | 247.762 ms |  13.4206 ms |  0.7356 ms | 133.43 |    0.57 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |          **1000** |                     **-1** |   **1.725 ms** |   **0.0777 ms** |  **0.0043 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                     -1 |   3.358 ms |   2.4698 ms |  0.1354 ms |   1.95 |    0.08 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                     -1 | 151.535 ms | 590.1504 ms | 32.3481 ms |  87.82 |   18.60 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                     -1 |   3.253 ms |   3.7488 ms |  0.2055 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                     -1 |  17.795 ms |  17.2861 ms |  0.9475 ms |   5.48 |    0.26 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                     -1 | 131.370 ms | 417.3023 ms | 22.8737 ms |  40.55 |    8.10 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                     -1 |   3.214 ms |   0.3583 ms |  0.0196 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                     -1 |  17.584 ms |  14.7575 ms |  0.8089 ms |   5.47 |    0.27 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                     -1 | 124.972 ms | 497.2309 ms | 27.2549 ms |  38.88 |    8.43 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |          **1000** |                      **1** |   **1.124 ms** |   **0.3920 ms** |  **0.0215 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                      1 |   2.170 ms |   0.4262 ms |  0.0234 ms |   1.93 |    0.04 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                      1 | 238.113 ms |  75.5404 ms |  4.1406 ms | 211.81 |    4.64 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.850 ms |   0.3113 ms |  0.0171 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.845 ms |   0.2817 ms |  0.0154 ms |   1.00 |    0.02 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                      1 | 254.043 ms |  74.1835 ms |  4.0663 ms | 137.36 |    3.26 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.855 ms |   0.2356 ms |  0.0129 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.856 ms |   0.1554 ms |  0.0085 ms |   1.00 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                      1 | 252.227 ms |  71.4905 ms |  3.9186 ms | 135.97 |    2.26 |
