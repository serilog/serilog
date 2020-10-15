``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=3  LaunchCount=1  WarmupCount=3  

```
|     Method |             Job |       Jit |       Runtime | Items | MaxDegreeOfParallelism |        Mean |      Error |    StdDev | Ratio | RatioSD |
|----------- |---------------- |---------- |-------------- |------ |----------------------- |------------:|-----------:|----------:|------:|--------:|
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                     **-1** |   **217.53 μs** |  **53.400 μs** |  **2.927 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    22.80 μs |   4.841 μs |  0.265 μs |  0.10 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    23.37 μs |   7.810 μs |  0.428 μs |  0.11 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |   521.79 μs | 174.958 μs |  9.590 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    34.10 μs |  15.212 μs |  0.834 μs |  0.07 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    35.83 μs |  18.074 μs |  0.991 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |   491.38 μs | 147.904 μs |  8.107 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    34.53 μs |   6.437 μs |  0.353 μs |  0.07 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    35.28 μs |   4.444 μs |  0.244 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                      **1** |    **62.18 μs** |   **2.896 μs** |  **0.159 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |    94.82 μs |  11.159 μs |  0.612 μs |  1.52 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |    95.63 μs |  12.208 μs |  0.669 μs |  1.54 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    99.22 μs |  15.902 μs |  0.872 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    81.64 μs |   5.136 μs |  0.282 μs |  0.82 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    86.03 μs |  42.353 μs |  2.322 μs |  0.87 |    0.02 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |   107.16 μs |  11.447 μs |  0.627 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    81.53 μs |   5.209 μs |  0.285 μs |  0.76 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    80.62 μs |  13.793 μs |  0.756 μs |  0.75 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                     **-1** |   **305.12 μs** | **210.199 μs** | **11.522 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    37.84 μs |  10.225 μs |  0.560 μs |  0.12 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    41.98 μs |   4.668 μs |  0.256 μs |  0.14 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |   741.31 μs | 553.798 μs | 30.356 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    50.95 μs |   0.791 μs |  0.043 μs |  0.07 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    52.20 μs |   5.695 μs |  0.312 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |   768.94 μs | 301.141 μs | 16.507 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    52.22 μs |  12.872 μs |  0.706 μs |  0.07 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    51.13 μs |  17.849 μs |  0.978 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                      **1** |   **128.33 μs** |  **15.260 μs** |  **0.836 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   195.93 μs |  13.569 μs |  0.744 μs |  1.53 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   197.73 μs |  30.341 μs |  1.663 μs |  1.54 |    0.02 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   200.85 μs |   9.179 μs |  0.503 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   170.91 μs |  30.232 μs |  1.657 μs |  0.85 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   164.02 μs |  10.721 μs |  0.588 μs |  0.82 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   202.22 μs |   6.982 μs |  0.383 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   168.36 μs |  14.798 μs |  0.811 μs |  0.83 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   166.65 μs |  23.724 μs |  1.300 μs |  0.82 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                     **-1** |   **592.34 μs** | **206.711 μs** | **11.331 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |    89.16 μs |  19.382 μs |  1.062 μs |  0.15 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |    79.97 μs |  12.969 μs |  0.711 μs |  0.14 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 | 1,223.31 μs |  90.044 μs |  4.936 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |    96.26 μs |   2.986 μs |  0.164 μs |  0.08 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |   101.31 μs |  13.788 μs |  0.756 μs |  0.08 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 | 1,141.64 μs |  84.129 μs |  4.611 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   100.76 μs |   4.036 μs |  0.221 μs |  0.09 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   102.65 μs |  20.611 μs |  1.130 μs |  0.09 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                      **1** |   **295.75 μs** |  **28.324 μs** |  **1.553 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   508.73 μs |  68.399 μs |  3.749 μs |  1.72 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   480.48 μs |  48.355 μs |  2.650 μs |  1.62 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   497.58 μs |  29.664 μs |  1.626 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   432.51 μs |  74.667 μs |  4.093 μs |  0.87 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   412.02 μs |  34.104 μs |  1.869 μs |  0.83 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   501.88 μs |  31.783 μs |  1.742 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   431.59 μs |  12.091 μs |  0.663 μs |  0.86 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   410.03 μs |  25.489 μs |  1.397 μs |  0.82 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                     **-1** | **1,055.38 μs** | **833.667 μs** | **45.696 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   151.72 μs |  13.946 μs |  0.764 μs |  0.14 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   149.67 μs |  12.638 μs |  0.693 μs |  0.14 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 | 1,896.38 μs | 135.981 μs |  7.454 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   175.47 μs |  61.122 μs |  3.350 μs |  0.09 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   182.45 μs |  42.113 μs |  2.308 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 | 1,874.20 μs |  39.211 μs |  2.149 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   177.65 μs |  11.967 μs |  0.656 μs |  0.09 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   182.71 μs |  23.862 μs |  1.308 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                      **1** |   **597.33 μs** |  **80.164 μs** |  **4.394 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 | 1,005.72 μs | 125.470 μs |  6.877 μs |  1.68 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 |   957.18 μs |  89.364 μs |  4.898 μs |  1.60 |    0.02 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 | 1,010.29 μs |  50.239 μs |  2.754 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   827.47 μs |  57.845 μs |  3.171 μs |  0.82 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   843.19 μs |  41.233 μs |  2.260 μs |  0.83 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   964.99 μs |  33.422 μs |  1.832 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   834.01 μs |  49.878 μs |  2.734 μs |  0.86 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   833.12 μs |  43.642 μs |  2.392 μs |  0.86 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                     **-1** | **1,040.98 μs** |  **84.443 μs** |  **4.629 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   170.67 μs |  50.645 μs |  2.776 μs |  0.16 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   152.27 μs |  26.233 μs |  1.438 μs |  0.15 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 | 1,930.08 μs | 135.487 μs |  7.426 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   183.56 μs |  32.822 μs |  1.799 μs |  0.10 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   183.88 μs |  51.880 μs |  2.844 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 | 1,957.05 μs | 263.636 μs | 14.451 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   184.51 μs |  56.304 μs |  3.086 μs |  0.09 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   192.58 μs |  10.672 μs |  0.585 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                      **1** |   **620.27 μs** |  **51.841 μs** |  **2.842 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 | 1,017.65 μs |  37.275 μs |  2.043 μs |  1.64 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 |   985.84 μs | 100.710 μs |  5.520 μs |  1.59 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   993.65 μs |  99.548 μs |  5.457 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   860.69 μs |  89.529 μs |  4.907 μs |  0.87 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   857.16 μs |  36.232 μs |  1.986 μs |  0.86 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 | 1,009.91 μs |  67.862 μs |  3.720 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   859.42 μs | 202.713 μs | 11.111 μs |  0.85 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   880.25 μs | 109.156 μs |  5.983 μs |  0.87 |    0.01 |
