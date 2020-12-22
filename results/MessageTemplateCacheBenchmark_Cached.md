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
|     Method |             Job |       Jit |       Runtime | Items | MaxDegreeOfParallelism |        Mean |      Error |    StdDev | Ratio | RatioSD |
|----------- |---------------- |---------- |-------------- |------ |----------------------- |------------:|-----------:|----------:|------:|--------:|
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                     **-1** |   **222.24 μs** |  **38.518 μs** |  **2.111 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    29.26 μs |   1.482 μs |  0.081 μs |  0.13 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    29.03 μs |   5.841 μs |  0.320 μs |  0.13 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |   500.63 μs |  84.267 μs |  4.619 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    40.82 μs |  16.247 μs |  0.891 μs |  0.08 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    42.52 μs |  10.716 μs |  0.587 μs |  0.08 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |   487.05 μs | 225.616 μs | 12.367 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    41.91 μs |   1.913 μs |  0.105 μs |  0.09 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    42.90 μs |   6.516 μs |  0.357 μs |  0.09 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                      **1** |    **63.66 μs** |   **5.086 μs** |  **0.279 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |   106.84 μs |   2.361 μs |  0.129 μs |  1.68 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |   103.21 μs |   6.888 μs |  0.378 μs |  1.62 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |   112.56 μs |  28.121 μs |  1.541 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    94.95 μs |  17.583 μs |  0.964 μs |  0.84 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    90.99 μs |  61.740 μs |  3.384 μs |  0.81 |    0.02 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |   102.33 μs |  13.269 μs |  0.727 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    84.36 μs |  14.408 μs |  0.790 μs |  0.82 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    88.07 μs |   7.152 μs |  0.392 μs |  0.86 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                     **-1** |   **348.13 μs** | **225.715 μs** | **12.372 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    53.31 μs |  39.034 μs |  2.140 μs |  0.15 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    51.56 μs |  45.963 μs |  2.519 μs |  0.15 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |   839.79 μs | 169.379 μs |  9.284 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    61.21 μs |   3.666 μs |  0.201 μs |  0.07 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    64.95 μs |  38.865 μs |  2.130 μs |  0.08 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |   810.50 μs |  46.898 μs |  2.571 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    64.06 μs |  12.569 μs |  0.689 μs |  0.08 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    64.06 μs |  17.489 μs |  0.959 μs |  0.08 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                      **1** |   **129.22 μs** |   **7.542 μs** |  **0.413 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   208.15 μs |  15.993 μs |  0.877 μs |  1.61 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   209.91 μs |   8.750 μs |  0.480 μs |  1.62 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   208.04 μs |  21.146 μs |  1.159 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   177.58 μs |  34.527 μs |  1.893 μs |  0.85 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   169.00 μs |  25.188 μs |  1.381 μs |  0.81 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   218.68 μs |  59.165 μs |  3.243 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   177.46 μs |  23.846 μs |  1.307 μs |  0.81 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   173.37 μs |  11.291 μs |  0.619 μs |  0.79 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                     **-1** |   **653.12 μs** |  **44.851 μs** |  **2.458 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |   100.92 μs |  95.716 μs |  5.247 μs |  0.15 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |   117.21 μs | 132.903 μs |  7.285 μs |  0.18 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 | 1,307.14 μs | 201.007 μs | 11.018 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |   119.03 μs |  29.993 μs |  1.644 μs |  0.09 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |   123.48 μs | 130.809 μs |  7.170 μs |  0.09 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 | 1,289.58 μs | 353.816 μs | 19.394 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   119.75 μs |  17.034 μs |  0.934 μs |  0.09 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   125.92 μs |  33.542 μs |  1.839 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                      **1** |   **339.78 μs** |  **61.168 μs** |  **3.353 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   534.91 μs |  61.652 μs |  3.379 μs |  1.57 |    0.03 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   516.82 μs |  52.849 μs |  2.897 μs |  1.52 |    0.02 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   536.02 μs |  94.136 μs |  5.160 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   473.60 μs |  31.534 μs |  1.728 μs |  0.88 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   431.61 μs |  37.180 μs |  2.038 μs |  0.81 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   528.11 μs |  35.933 μs |  1.970 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   446.94 μs |  43.588 μs |  2.389 μs |  0.85 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   451.42 μs |  14.963 μs |  0.820 μs |  0.85 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                     **-1** | **1,080.78 μs** | **506.263 μs** | **27.750 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   196.93 μs | 154.150 μs |  8.449 μs |  0.18 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   214.74 μs |  68.790 μs |  3.771 μs |  0.20 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 | 1,992.26 μs | 101.465 μs |  5.562 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   214.03 μs | 117.188 μs |  6.423 μs |  0.11 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   228.56 μs | 108.061 μs |  5.923 μs |  0.11 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 | 2,017.12 μs | 498.946 μs | 27.349 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   216.09 μs | 199.714 μs | 10.947 μs |  0.11 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   214.68 μs |  65.649 μs |  3.598 μs |  0.11 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                      **1** |   **645.68 μs** |  **29.301 μs** |  **1.606 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 | 1,135.65 μs | 143.512 μs |  7.866 μs |  1.76 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 | 1,042.97 μs | 151.740 μs |  8.317 μs |  1.62 |    0.02 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 | 1,048.80 μs | 322.834 μs | 17.696 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   896.21 μs | 145.395 μs |  7.970 μs |  0.85 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   877.88 μs | 100.810 μs |  5.526 μs |  0.84 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 | 1,042.39 μs | 436.311 μs | 23.916 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   874.28 μs |  21.903 μs |  1.201 μs |  0.84 |    0.02 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   902.70 μs |  80.765 μs |  4.427 μs |  0.87 |    0.02 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                     **-1** | **1,120.42 μs** | **455.342 μs** | **24.959 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   193.29 μs |  40.835 μs |  2.238 μs |  0.17 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   187.97 μs |  80.349 μs |  4.404 μs |  0.17 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 | 2,054.05 μs | 617.507 μs | 33.848 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   233.30 μs |  51.734 μs |  2.836 μs |  0.11 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   226.25 μs |   7.301 μs |  0.400 μs |  0.11 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 | 2,039.64 μs | 245.566 μs | 13.460 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   222.02 μs |  31.817 μs |  1.744 μs |  0.11 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   227.43 μs |  30.039 μs |  1.647 μs |  0.11 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                      **1** |   **655.14 μs** |  **12.516 μs** |  **0.686 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 | 1,101.06 μs |  57.447 μs |  3.149 μs |  1.68 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 | 1,077.37 μs |  32.964 μs |  1.807 μs |  1.64 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 | 1,194.51 μs |  33.018 μs |  1.810 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   911.43 μs | 177.483 μs |  9.728 μs |  0.76 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 | 1,001.77 μs |  88.545 μs |  4.853 μs |  0.84 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 | 1,059.72 μs | 106.451 μs |  5.835 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   897.61 μs |  23.580 μs |  1.292 μs |  0.85 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   922.96 μs |  36.550 μs |  2.003 μs |  0.87 |    0.00 |
