``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=3  LaunchCount=1  WarmupCount=3  

```
|     Method |             Job |       Jit |       Runtime | Items | OverflowCount | MaxDegreeOfParallelism |       Mean |         Error |     StdDev |  Ratio | RatioSD |
|----------- |---------------- |---------- |-------------- |------ |-------------- |----------------------- |-----------:|--------------:|-----------:|-------:|--------:|
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |             **1** |                     **-1** |   **1.435 ms** |     **0.5002 ms** |  **0.0274 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                     -1 |   2.126 ms |     8.0870 ms |  0.4433 ms |   1.48 |    0.33 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                     -1 |  32.696 ms |    90.0073 ms |  4.9336 ms |  22.83 |    3.84 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                     -1 |   3.005 ms |     2.6478 ms |  0.1451 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                     -1 |  22.153 ms |     7.1640 ms |  0.3927 ms |   7.38 |    0.24 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                     -1 |  49.804 ms |    68.1100 ms |  3.7333 ms |  16.56 |    0.47 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                     -1 |   3.194 ms |     2.1329 ms |  0.1169 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                     -1 |  22.646 ms |    27.9775 ms |  1.5335 ms |   7.10 |    0.52 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                     -1 |  47.713 ms |    31.6235 ms |  1.7334 ms |  14.97 |    1.05 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |             **1** |                      **1** |   **1.062 ms** |     **0.5942 ms** |  **0.0326 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                      1 |   2.110 ms |     1.0955 ms |  0.0601 ms |   1.99 |    0.10 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                      1 | 229.458 ms |   119.8941 ms |  6.5718 ms | 216.10 |    3.00 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.787 ms |     0.3719 ms |  0.0204 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.818 ms |     0.2261 ms |  0.0124 ms |   1.02 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                      1 | 237.573 ms |   119.1271 ms |  6.5298 ms | 132.96 |    3.77 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.792 ms |     0.3977 ms |  0.0218 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.792 ms |     0.4227 ms |  0.0232 ms |   1.00 |    0.02 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                      1 | 239.940 ms |   125.9049 ms |  6.9013 ms | 133.97 |    5.32 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |            **10** |                     **-1** |   **1.511 ms** |     **0.1623 ms** |  **0.0089 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                     -1 |   2.460 ms |     0.8055 ms |  0.0442 ms |   1.63 |    0.04 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                     -1 |  60.554 ms |    31.4853 ms |  1.7258 ms |  40.08 |    1.31 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                     -1 |   3.088 ms |     0.5569 ms |  0.0305 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                     -1 |  23.290 ms |    16.8103 ms |  0.9214 ms |   7.54 |    0.34 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                     -1 |  50.814 ms |    60.2994 ms |  3.3052 ms |  16.45 |    0.90 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                     -1 |   3.191 ms |     1.8327 ms |  0.1005 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                     -1 |  21.333 ms |    17.3551 ms |  0.9513 ms |   6.69 |    0.36 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                     -1 |  57.290 ms |    27.3505 ms |  1.4992 ms |  17.97 |    0.80 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |            **10** |                      **1** |   **1.069 ms** |     **0.6112 ms** |  **0.0335 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                      1 |   2.085 ms |     0.8677 ms |  0.0476 ms |   1.95 |    0.10 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                      1 | 226.871 ms |   103.2032 ms |  5.6569 ms | 212.34 |    1.48 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.787 ms |     0.5467 ms |  0.0300 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.838 ms |     0.5268 ms |  0.0289 ms |   1.03 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                      1 | 242.255 ms |   107.1004 ms |  5.8705 ms | 135.59 |    4.22 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.799 ms |     0.0476 ms |  0.0026 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.788 ms |     0.6365 ms |  0.0349 ms |   0.99 |    0.02 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                      1 | 233.397 ms |    90.7787 ms |  4.9759 ms | 129.73 |    2.63 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |           **100** |                     **-1** |   **1.601 ms** |     **0.4539 ms** |  **0.0249 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                     -1 |   2.837 ms |     6.1656 ms |  0.3380 ms |   1.77 |    0.23 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                     -1 |  72.295 ms |   249.2304 ms | 13.6612 ms |  45.23 |    9.17 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                     -1 |   3.254 ms |     1.5661 ms |  0.0858 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                     -1 |  23.557 ms |    27.1188 ms |  1.4865 ms |   7.25 |    0.62 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                     -1 |  73.811 ms |   179.7426 ms |  9.8523 ms |  22.72 |    3.32 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                     -1 |   3.196 ms |     2.2103 ms |  0.1212 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                     -1 |  22.162 ms |    30.4180 ms |  1.6673 ms |   6.93 |    0.31 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                     -1 |  65.921 ms |   121.1654 ms |  6.6415 ms |  20.69 |    2.79 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |           **100** |                      **1** |   **1.052 ms** |     **0.6239 ms** |  **0.0342 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                      1 |   2.090 ms |     0.4102 ms |  0.0225 ms |   1.99 |    0.04 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                      1 | 228.287 ms |    70.6778 ms |  3.8741 ms | 217.01 |    4.84 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.824 ms |     0.7528 ms |  0.0413 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.830 ms |     0.3567 ms |  0.0195 ms |   1.00 |    0.03 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                      1 | 239.027 ms |   118.8848 ms |  6.5165 ms | 131.06 |    3.65 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.783 ms |     0.4413 ms |  0.0242 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.851 ms |     0.1908 ms |  0.0105 ms |   1.04 |    0.02 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                      1 | 238.463 ms |    48.8092 ms |  2.6754 ms | 133.76 |    3.19 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |          **1000** |                     **-1** |   **1.771 ms** |     **0.5063 ms** |  **0.0278 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                     -1 |   3.366 ms |     1.2610 ms |  0.0691 ms |   1.90 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                     -1 | 192.768 ms | 1,320.9009 ms | 72.4030 ms | 108.85 |   40.73 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                     -1 |   3.329 ms |     1.4432 ms |  0.0791 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                     -1 |  18.409 ms |    30.5945 ms |  1.6770 ms |   5.54 |    0.63 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                     -1 | 151.955 ms | 1,187.2686 ms | 65.0782 ms |  45.35 |   18.37 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                     -1 |   3.304 ms |     1.0378 ms |  0.0569 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                     -1 |  18.750 ms |    17.7334 ms |  0.9720 ms |   5.68 |    0.27 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                     -1 | 137.043 ms |   252.3630 ms | 13.8329 ms |  41.53 |    4.82 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |          **1000** |                      **1** |   **1.067 ms** |     **0.3684 ms** |  **0.0202 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                      1 |   2.112 ms |     0.9776 ms |  0.0536 ms |   1.98 |    0.05 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                      1 | 229.826 ms |   105.1561 ms |  5.7640 ms | 215.53 |    9.34 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.792 ms |     0.4526 ms |  0.0248 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.797 ms |     0.4866 ms |  0.0267 ms |   1.00 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                      1 | 244.933 ms |   142.7759 ms |  7.8260 ms | 136.74 |    4.94 |
|            |                 |           |               |       |               |                        |            |               |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.795 ms |     0.4335 ms |  0.0238 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.796 ms |     0.5875 ms |  0.0322 ms |   1.00 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                      1 | 240.767 ms |   113.0706 ms |  6.1978 ms | 134.09 |    2.46 |
