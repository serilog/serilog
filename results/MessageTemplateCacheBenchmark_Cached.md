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
|     Method |             Job |       Jit |       Runtime | Items | MaxDegreeOfParallelism |        Mean |      Error |    StdDev | Ratio | RatioSD |
|----------- |---------------- |---------- |-------------- |------ |----------------------- |------------:|-----------:|----------:|------:|--------:|
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                     **-1** |   **219.68 μs** |  **52.829 μs** |  **2.896 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    24.69 μs |   4.023 μs |  0.221 μs |  0.11 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    24.05 μs |   0.601 μs |  0.033 μs |  0.11 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |   501.99 μs | 241.227 μs | 13.222 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    34.43 μs |   3.732 μs |  0.205 μs |  0.07 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    35.32 μs |  10.640 μs |  0.583 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |   497.18 μs | 140.777 μs |  7.716 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    34.68 μs |   9.196 μs |  0.504 μs |  0.07 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    35.12 μs |  13.981 μs |  0.766 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                      **1** |    **57.82 μs** |   **9.812 μs** |  **0.538 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |    97.48 μs |  26.810 μs |  1.470 μs |  1.69 |    0.03 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |    98.90 μs |  13.340 μs |  0.731 μs |  1.71 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    99.22 μs |   1.879 μs |  0.103 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    77.44 μs |  12.036 μs |  0.660 μs |  0.78 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    80.02 μs |   5.808 μs |  0.318 μs |  0.81 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    99.35 μs |  10.096 μs |  0.553 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    81.25 μs |   7.292 μs |  0.400 μs |  0.82 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    82.17 μs |  14.542 μs |  0.797 μs |  0.83 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                     **-1** |   **305.01 μs** | **145.387 μs** |  **7.969 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    36.47 μs |   5.502 μs |  0.302 μs |  0.12 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    41.77 μs |   3.985 μs |  0.218 μs |  0.14 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |   735.39 μs | 213.582 μs | 11.707 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    49.00 μs |  18.078 μs |  0.991 μs |  0.07 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    52.57 μs |  17.386 μs |  0.953 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |   707.65 μs | 209.886 μs | 11.505 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    50.72 μs |  14.274 μs |  0.782 μs |  0.07 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    52.43 μs |   7.896 μs |  0.433 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                      **1** |   **117.54 μs** |   **7.819 μs** |  **0.429 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   214.30 μs |   6.166 μs |  0.338 μs |  1.82 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   193.34 μs |  27.888 μs |  1.529 μs |  1.64 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   198.50 μs |  21.119 μs |  1.158 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   184.36 μs |  15.175 μs |  0.832 μs |  0.93 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   163.04 μs |   9.197 μs |  0.504 μs |  0.82 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   202.68 μs |   9.115 μs |  0.500 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   169.33 μs |  74.758 μs |  4.098 μs |  0.84 |    0.02 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   165.92 μs |   3.342 μs |  0.183 μs |  0.82 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                     **-1** |   **590.83 μs** | **153.083 μs** |  **8.391 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |    80.00 μs |  17.282 μs |  0.947 μs |  0.14 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |    89.75 μs |   0.881 μs |  0.048 μs |  0.15 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 | 1,195.01 μs | 229.669 μs | 12.589 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |    98.84 μs |  13.404 μs |  0.735 μs |  0.08 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |   102.89 μs |   5.836 μs |  0.320 μs |  0.09 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 | 1,211.81 μs | 154.839 μs |  8.487 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |    99.00 μs |   5.928 μs |  0.325 μs |  0.08 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   102.90 μs |   8.362 μs |  0.458 μs |  0.08 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                      **1** |   **296.95 μs** |  **28.940 μs** |  **1.586 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   496.41 μs |  18.429 μs |  1.010 μs |  1.67 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   484.81 μs |  22.136 μs |  1.213 μs |  1.63 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   492.37 μs |  17.278 μs |  0.947 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   416.20 μs |  10.135 μs |  0.556 μs |  0.85 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   406.05 μs |  36.958 μs |  2.026 μs |  0.82 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   496.25 μs |  63.277 μs |  3.468 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   418.91 μs |  32.664 μs |  1.790 μs |  0.84 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   409.72 μs |   5.026 μs |  0.276 μs |  0.83 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                     **-1** | **1,012.07 μs** |  **85.087 μs** |  **4.664 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   149.25 μs |  12.669 μs |  0.694 μs |  0.15 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   174.80 μs |  25.780 μs |  1.413 μs |  0.17 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 | 1,907.91 μs |  57.218 μs |  3.136 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   181.81 μs |  53.336 μs |  2.924 μs |  0.10 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   198.31 μs |  81.112 μs |  4.446 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 | 1,954.64 μs | 108.611 μs |  5.953 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   180.41 μs |  11.138 μs |  0.611 μs |  0.09 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   184.36 μs |  43.147 μs |  2.365 μs |  0.09 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                      **1** |   **626.90 μs** |  **58.741 μs** |  **3.220 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 | 1,005.91 μs |  48.014 μs |  2.632 μs |  1.60 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 |   969.92 μs | 108.209 μs |  5.931 μs |  1.55 |    0.02 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   990.07 μs | 121.279 μs |  6.648 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   830.79 μs |  61.176 μs |  3.353 μs |  0.84 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   845.12 μs |  28.078 μs |  1.539 μs |  0.85 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 | 1,001.69 μs |  78.158 μs |  4.284 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   831.98 μs |  52.969 μs |  2.903 μs |  0.83 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   827.85 μs |  72.680 μs |  3.984 μs |  0.83 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                     **-1** | **1,074.93 μs** | **627.495 μs** | **34.395 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   171.30 μs |   9.725 μs |  0.533 μs |  0.16 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   178.79 μs |  32.301 μs |  1.771 μs |  0.17 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 | 1,916.05 μs | 121.865 μs |  6.680 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   182.67 μs |  14.986 μs |  0.821 μs |  0.10 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   187.62 μs |  14.246 μs |  0.781 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 | 1,926.36 μs | 106.845 μs |  5.857 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   188.04 μs |  41.295 μs |  2.264 μs |  0.10 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   195.06 μs | 123.343 μs |  6.761 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                      **1** |   **617.68 μs** |  **50.591 μs** |  **2.773 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 | 1,050.53 μs |  80.071 μs |  4.389 μs |  1.70 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 |   988.06 μs |  26.186 μs |  1.435 μs |  1.60 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 | 1,003.18 μs |  84.560 μs |  4.635 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   858.69 μs |  25.491 μs |  1.397 μs |  0.86 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   873.96 μs |  66.226 μs |  3.630 μs |  0.87 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   990.05 μs |  98.387 μs |  5.393 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   842.61 μs |  76.827 μs |  4.211 μs |  0.85 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   933.72 μs |  15.275 μs |  0.837 μs |  0.94 |    0.01 |
