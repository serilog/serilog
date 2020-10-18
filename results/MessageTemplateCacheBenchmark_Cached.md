``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.403
  [Host]          : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=3  LaunchCount=1  WarmupCount=3  

```
|     Method |             Job |       Jit |       Runtime | Items | MaxDegreeOfParallelism |        Mean |      Error |    StdDev | Ratio | RatioSD |
|----------- |---------------- |---------- |-------------- |------ |----------------------- |------------:|-----------:|----------:|------:|--------:|
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                     **-1** |   **216.92 μs** | **157.654 μs** |  **8.642 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    22.88 μs |   6.518 μs |  0.357 μs |  0.11 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    24.85 μs |  10.382 μs |  0.569 μs |  0.11 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |   518.65 μs | 203.318 μs | 11.145 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    33.33 μs |  18.269 μs |  1.001 μs |  0.06 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    35.75 μs |   4.709 μs |  0.258 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |   515.66 μs |  26.406 μs |  1.447 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    34.82 μs |   8.843 μs |  0.485 μs |  0.07 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    35.09 μs |   6.360 μs |  0.349 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                      **1** |    **60.95 μs** |  **11.960 μs** |  **0.656 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |    97.66 μs |   7.339 μs |  0.402 μs |  1.60 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |    95.98 μs |   1.906 μs |  0.104 μs |  1.57 |    0.02 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |   102.01 μs |  18.221 μs |  0.999 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    86.08 μs |   8.299 μs |  0.455 μs |  0.84 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    82.50 μs |   7.694 μs |  0.422 μs |  0.81 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    99.33 μs |   5.651 μs |  0.310 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    85.21 μs |  13.222 μs |  0.725 μs |  0.86 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    81.13 μs |  11.285 μs |  0.619 μs |  0.82 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                     **-1** |   **299.56 μs** | **111.964 μs** |  **6.137 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    37.99 μs |  32.955 μs |  1.806 μs |  0.13 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    40.89 μs |   7.487 μs |  0.410 μs |  0.14 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |   750.12 μs | 323.789 μs | 17.748 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    51.67 μs |   3.408 μs |  0.187 μs |  0.07 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    52.74 μs |   7.254 μs |  0.398 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |   724.18 μs |  92.485 μs |  5.069 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    50.26 μs |  21.404 μs |  1.173 μs |  0.07 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    51.14 μs |  17.984 μs |  0.986 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                      **1** |   **119.94 μs** |   **8.083 μs** |  **0.443 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   194.04 μs |  17.982 μs |  0.986 μs |  1.62 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   194.92 μs |   8.658 μs |  0.475 μs |  1.63 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   198.79 μs |  19.043 μs |  1.044 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   173.11 μs |   5.542 μs |  0.304 μs |  0.87 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   162.37 μs |  21.751 μs |  1.192 μs |  0.82 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   199.98 μs |  23.678 μs |  1.298 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   169.06 μs |  14.145 μs |  0.775 μs |  0.85 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   166.84 μs |   7.815 μs |  0.428 μs |  0.83 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                     **-1** |   **569.74 μs** |  **24.558 μs** |  **1.346 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |    80.29 μs |  13.157 μs |  0.721 μs |  0.14 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |    88.79 μs |   7.756 μs |  0.425 μs |  0.16 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 | 1,163.63 μs |  89.632 μs |  4.913 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |    96.28 μs |  10.117 μs |  0.555 μs |  0.08 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |    99.70 μs |  19.787 μs |  1.085 μs |  0.09 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 | 1,195.37 μs | 239.529 μs | 13.129 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   102.45 μs |   2.498 μs |  0.137 μs |  0.09 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   100.60 μs |  41.758 μs |  2.289 μs |  0.08 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                      **1** |   **296.90 μs** |  **27.738 μs** |  **1.520 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   519.90 μs |  37.777 μs |  2.071 μs |  1.75 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   484.05 μs |   3.990 μs |  0.219 μs |  1.63 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   509.69 μs |  83.348 μs |  4.569 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   430.49 μs |  23.762 μs |  1.302 μs |  0.84 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   419.07 μs |  17.858 μs |  0.979 μs |  0.82 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   496.00 μs |  28.006 μs |  1.535 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   436.15 μs |  42.549 μs |  2.332 μs |  0.88 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   412.88 μs |  25.818 μs |  1.415 μs |  0.83 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                     **-1** |   **989.68 μs** | **111.823 μs** |  **6.129 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   167.10 μs |  18.172 μs |  0.996 μs |  0.17 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   166.12 μs |  40.770 μs |  2.235 μs |  0.17 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 | 2,031.24 μs |  49.946 μs |  2.738 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   179.86 μs |  15.980 μs |  0.876 μs |  0.09 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   181.91 μs |  51.266 μs |  2.810 μs |  0.09 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 | 1,930.28 μs | 158.442 μs |  8.685 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   177.23 μs |  57.194 μs |  3.135 μs |  0.09 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   183.91 μs |   9.235 μs |  0.506 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                      **1** |   **603.24 μs** |  **58.196 μs** |  **3.190 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 | 1,018.33 μs |  74.884 μs |  4.105 μs |  1.69 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 |   970.88 μs | 125.406 μs |  6.874 μs |  1.61 |    0.02 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   965.25 μs |  38.650 μs |  2.119 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   861.15 μs |  30.349 μs |  1.664 μs |  0.89 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   843.98 μs |  92.572 μs |  5.074 μs |  0.87 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   993.96 μs |  93.283 μs |  5.113 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   827.04 μs |  54.457 μs |  2.985 μs |  0.83 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   842.59 μs |  51.018 μs |  2.796 μs |  0.85 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                     **-1** | **1,073.07 μs** |  **74.763 μs** |  **4.098 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   152.73 μs |  38.645 μs |  2.118 μs |  0.14 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   172.04 μs |   5.659 μs |  0.310 μs |  0.16 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 | 1,932.94 μs |  67.675 μs |  3.709 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   179.96 μs |  52.050 μs |  2.853 μs |  0.09 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   187.51 μs |  15.519 μs |  0.851 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 | 1,937.70 μs | 122.077 μs |  6.691 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   183.77 μs |  15.316 μs |  0.840 μs |  0.09 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   185.14 μs |  23.709 μs |  1.300 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                      **1** |   **623.68 μs** | **106.695 μs** |  **5.848 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 | 1,031.79 μs |  55.691 μs |  3.053 μs |  1.65 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 | 1,000.10 μs |  65.053 μs |  3.566 μs |  1.60 |    0.02 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   988.73 μs |  55.236 μs |  3.028 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   853.85 μs |  22.543 μs |  1.236 μs |  0.86 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   879.18 μs |  10.917 μs |  0.598 μs |  0.89 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   996.26 μs | 244.629 μs | 13.409 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   871.92 μs | 147.900 μs |  8.107 μs |  0.88 |    0.02 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   854.26 μs |  96.197 μs |  5.273 μs |  0.86 |    0.01 |
