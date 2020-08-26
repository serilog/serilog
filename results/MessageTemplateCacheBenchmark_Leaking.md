``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]          : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT

IterationCount=3  LaunchCount=1  WarmupCount=3  

```
|     Method |             Job |       Jit |       Runtime | Items | OverflowCount | MaxDegreeOfParallelism |       Mean |       Error |     StdDev |  Ratio | RatioSD |
|----------- |---------------- |---------- |-------------- |------ |-------------- |----------------------- |-----------:|------------:|-----------:|-------:|--------:|
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |             **1** |                     **-1** |   **1.463 ms** |   **0.4685 ms** |  **0.0257 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                     -1 |   2.110 ms |   1.5533 ms |  0.0851 ms |   1.44 |    0.08 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                     -1 |  48.095 ms | 115.5490 ms |  6.3336 ms |  32.93 |    4.85 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                     -1 |   3.243 ms |   2.2544 ms |  0.1236 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                     -1 |  21.398 ms |  40.7942 ms |  2.2361 ms |   6.62 |    0.94 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                     -1 |  50.282 ms |  39.8598 ms |  2.1848 ms |  15.52 |    0.83 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                     -1 |   3.131 ms |   2.7307 ms |  0.1497 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                     -1 |  20.833 ms |   2.6454 ms |  0.1450 ms |   6.66 |    0.35 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                     -1 |  53.445 ms |  98.6814 ms |  5.4091 ms |  17.15 |    2.60 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |             **1** |                      **1** |   **1.112 ms** |   **0.4159 ms** |  **0.0228 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                      1 |   2.170 ms |   0.6506 ms |  0.0357 ms |   1.95 |    0.05 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                      1 | 240.501 ms |  50.5928 ms |  2.7732 ms | 216.40 |    3.72 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.847 ms |   0.5291 ms |  0.0290 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.829 ms |   0.6003 ms |  0.0329 ms |   0.99 |    0.02 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                      1 | 255.848 ms |  64.3664 ms |  3.5281 ms | 138.53 |    0.58 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.824 ms |   0.5485 ms |  0.0301 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.843 ms |   0.3605 ms |  0.0198 ms |   1.01 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                      1 | 254.529 ms |  60.1403 ms |  3.2965 ms | 139.58 |    2.23 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |            **10** |                     **-1** |   **1.568 ms** |   **0.3065 ms** |  **0.0168 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                     -1 |   2.258 ms |   5.7274 ms |  0.3139 ms |   1.44 |    0.21 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                     -1 |  56.334 ms |  61.4121 ms |  3.3662 ms |  35.95 |    2.51 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                     -1 |   3.377 ms |   1.4999 ms |  0.0822 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                     -1 |  21.178 ms |  29.3723 ms |  1.6100 ms |   6.28 |    0.63 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                     -1 |  54.263 ms |  93.2080 ms |  5.1090 ms |  16.10 |    1.88 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                     -1 |   3.475 ms |   3.2938 ms |  0.1805 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                     -1 |  21.961 ms |   5.6659 ms |  0.3106 ms |   6.33 |    0.27 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                     -1 |  52.076 ms |  66.9542 ms |  3.6700 ms |  15.02 |    1.40 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |            **10** |                      **1** |   **1.165 ms** |   **0.3685 ms** |  **0.0202 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                      1 |   2.162 ms |   0.4414 ms |  0.0242 ms |   1.86 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                      1 | 231.566 ms |  78.8083 ms |  4.3197 ms | 198.82 |    1.55 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.856 ms |   0.5102 ms |  0.0280 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.825 ms |   0.4145 ms |  0.0227 ms |   0.98 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                      1 | 249.868 ms |  76.5724 ms |  4.1972 ms | 134.70 |    4.00 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.839 ms |   0.4044 ms |  0.0222 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.839 ms |   0.4511 ms |  0.0247 ms |   1.00 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                      1 | 254.202 ms |  57.9878 ms |  3.1785 ms | 138.21 |    2.35 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |           **100** |                     **-1** |   **1.623 ms** |   **0.7143 ms** |  **0.0392 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                     -1 |   2.611 ms |   5.5770 ms |  0.3057 ms |   1.61 |    0.22 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                     -1 | 112.223 ms | 138.0212 ms |  7.5654 ms |  69.14 |    4.42 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                     -1 |   3.398 ms |   1.6054 ms |  0.0880 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                     -1 |  22.352 ms |  41.1873 ms |  2.2576 ms |   6.57 |    0.49 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                     -1 |  69.464 ms | 271.6554 ms | 14.8904 ms |  20.48 |    4.63 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                     -1 |   3.318 ms |   1.5494 ms |  0.0849 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                     -1 |  20.214 ms |  18.0810 ms |  0.9911 ms |   6.09 |    0.14 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                     -1 |  66.379 ms | 260.6408 ms | 14.2866 ms |  19.95 |    3.81 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |           **100** |                      **1** |   **1.126 ms** |   **0.2930 ms** |  **0.0161 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                      1 |   2.157 ms |   0.8639 ms |  0.0474 ms |   1.91 |    0.04 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                      1 | 239.529 ms |  79.8068 ms |  4.3745 ms | 212.66 |    1.17 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.832 ms |   0.3191 ms |  0.0175 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.817 ms |   0.2720 ms |  0.0149 ms |   0.99 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                      1 | 255.955 ms |  80.8527 ms |  4.4318 ms | 139.68 |    1.32 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.820 ms |   0.2715 ms |  0.0149 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.844 ms |   0.5912 ms |  0.0324 ms |   1.01 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                      1 | 249.907 ms | 107.6085 ms |  5.8984 ms | 137.35 |    4.26 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |          **1000** |                     **-1** |   **1.768 ms** |   **0.4008 ms** |  **0.0220 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                     -1 |   3.612 ms |   1.2427 ms |  0.0681 ms |   2.04 |    0.05 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                     -1 | 154.520 ms | 432.9984 ms | 23.7341 ms |  87.36 |   12.63 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                     -1 |   3.620 ms |   0.6637 ms |  0.0364 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                     -1 |  17.879 ms |   5.7551 ms |  0.3155 ms |   4.94 |    0.12 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                     -1 | 139.363 ms | 346.3177 ms | 18.9828 ms |  38.48 |    5.03 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                     -1 |   3.467 ms |   3.2412 ms |  0.1777 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                     -1 |  17.131 ms |  33.1527 ms |  1.8172 ms |   4.97 |    0.77 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                     -1 | 137.983 ms | 710.0006 ms | 38.9175 ms |  40.22 |   13.42 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |          **1000** |                      **1** |   **1.109 ms** |   **0.1006 ms** |  **0.0055 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                      1 |   2.161 ms |   0.5440 ms |  0.0298 ms |   1.95 |    0.03 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                      1 | 235.545 ms |  28.9245 ms |  1.5855 ms | 212.43 |    2.12 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.848 ms |   0.7257 ms |  0.0398 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.863 ms |   1.3759 ms |  0.0754 ms |   1.01 |    0.05 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                      1 | 246.787 ms |  29.3380 ms |  1.6081 ms | 133.60 |    3.51 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.830 ms |   0.2794 ms |  0.0153 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.823 ms |   0.7139 ms |  0.0391 ms |   1.00 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                      1 | 249.491 ms |  66.0864 ms |  3.6224 ms | 136.34 |    2.94 |
