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
|     Method |             Job |       Jit |       Runtime | Items | MaxDegreeOfParallelism |        Mean |      Error |    StdDev | Ratio | RatioSD |
|----------- |---------------- |---------- |-------------- |------ |----------------------- |------------:|-----------:|----------:|------:|--------:|
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                     **-1** |   **227.54 μs** | **144.635 μs** |  **7.928 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    24.84 μs |   9.294 μs |  0.509 μs |  0.11 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    23.85 μs |   8.790 μs |  0.482 μs |  0.10 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |   474.27 μs | 163.746 μs |  8.975 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    36.76 μs |   6.739 μs |  0.369 μs |  0.08 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    35.75 μs |  10.629 μs |  0.583 μs |  0.08 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |   502.56 μs | 224.037 μs | 12.280 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    35.85 μs |  16.232 μs |  0.890 μs |  0.07 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    36.48 μs |  13.867 μs |  0.760 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                      **1** |    **62.67 μs** |  **25.627 μs** |  **1.405 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |    98.36 μs |   3.363 μs |  0.184 μs |  1.57 |    0.03 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |    99.76 μs |  15.857 μs |  0.869 μs |  1.59 |    0.02 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |   110.56 μs |  28.347 μs |  1.554 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    88.39 μs |  19.687 μs |  1.079 μs |  0.80 |    0.02 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    83.29 μs |  24.218 μs |  1.327 μs |  0.75 |    0.02 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |   104.95 μs |  13.549 μs |  0.743 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    90.03 μs |  28.376 μs |  1.555 μs |  0.86 |    0.02 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    81.99 μs |  27.984 μs |  1.534 μs |  0.78 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                     **-1** |   **298.38 μs** | **154.125 μs** |  **8.448 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    38.60 μs |  10.620 μs |  0.582 μs |  0.13 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    43.52 μs |   9.323 μs |  0.511 μs |  0.15 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |   717.04 μs |  77.088 μs |  4.225 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    51.06 μs |  12.396 μs |  0.679 μs |  0.07 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    51.48 μs |  17.215 μs |  0.944 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |   748.01 μs |  81.954 μs |  4.492 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    54.19 μs |  31.495 μs |  1.726 μs |  0.07 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    53.94 μs |  26.352 μs |  1.444 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                      **1** |   **119.02 μs** |   **2.237 μs** |  **0.123 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   204.51 μs |   7.291 μs |  0.400 μs |  1.72 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   202.34 μs |   6.770 μs |  0.371 μs |  1.70 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   213.33 μs |   6.501 μs |  0.356 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   168.48 μs |  32.363 μs |  1.774 μs |  0.79 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   168.83 μs |  55.673 μs |  3.052 μs |  0.79 |    0.02 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   214.65 μs |  21.928 μs |  1.202 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   181.81 μs |  21.633 μs |  1.186 μs |  0.85 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   169.20 μs |   6.710 μs |  0.368 μs |  0.79 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                     **-1** |   **581.51 μs** | **157.020 μs** |  **8.607 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |    93.06 μs |  25.007 μs |  1.371 μs |  0.16 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |    93.98 μs |  35.794 μs |  1.962 μs |  0.16 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 | 1,153.17 μs |  60.091 μs |  3.294 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |   100.34 μs |  21.554 μs |  1.181 μs |  0.09 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |   104.44 μs |  18.310 μs |  1.004 μs |  0.09 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 | 1,186.59 μs | 251.671 μs | 13.795 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   104.33 μs |  44.257 μs |  2.426 μs |  0.09 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   103.30 μs |  48.878 μs |  2.679 μs |  0.09 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                      **1** |   **308.93 μs** |  **28.346 μs** |  **1.554 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   509.41 μs | 142.760 μs |  7.825 μs |  1.65 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   493.37 μs |  49.133 μs |  2.693 μs |  1.60 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   492.50 μs |  16.403 μs |  0.899 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   425.88 μs |  87.945 μs |  4.821 μs |  0.86 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   430.53 μs |  27.270 μs |  1.495 μs |  0.87 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   496.51 μs |  54.172 μs |  2.969 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   437.84 μs | 107.911 μs |  5.915 μs |  0.88 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   415.15 μs |  49.449 μs |  2.710 μs |  0.84 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                     **-1** | **1,019.62 μs** | **595.708 μs** | **32.653 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   171.22 μs |  27.147 μs |  1.488 μs |  0.17 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   180.78 μs |  45.954 μs |  2.519 μs |  0.18 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 | 1,911.35 μs | 231.046 μs | 12.664 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   184.69 μs |  46.417 μs |  2.544 μs |  0.10 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   185.89 μs |  69.796 μs |  3.826 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 | 1,890.54 μs | 221.256 μs | 12.128 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   188.10 μs |  54.697 μs |  2.998 μs |  0.10 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   183.98 μs |  96.310 μs |  5.279 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                      **1** |   **605.42 μs** |  **17.834 μs** |  **0.978 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 | 1,033.80 μs |  93.697 μs |  5.136 μs |  1.71 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 |   999.67 μs | 120.690 μs |  6.615 μs |  1.65 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   972.16 μs |  12.724 μs |  0.697 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   815.08 μs |   4.625 μs |  0.254 μs |  0.84 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   855.43 μs |  24.148 μs |  1.324 μs |  0.88 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   974.20 μs | 100.520 μs |  5.510 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   887.45 μs |  46.183 μs |  2.531 μs |  0.91 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   838.13 μs | 180.562 μs |  9.897 μs |  0.86 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                     **-1** | **1,082.26 μs** | **635.681 μs** | **34.844 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   158.17 μs |  90.055 μs |  4.936 μs |  0.15 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   163.54 μs |  10.976 μs |  0.602 μs |  0.15 |    0.01 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 | 1,899.95 μs | 146.274 μs |  8.018 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   186.75 μs |  74.076 μs |  4.060 μs |  0.10 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   186.43 μs |  54.996 μs |  3.015 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 | 1,912.20 μs | 241.159 μs | 13.219 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   193.96 μs |  42.655 μs |  2.338 μs |  0.10 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   189.81 μs |  58.550 μs |  3.209 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                      **1** |   **638.19 μs** | **103.138 μs** |  **5.653 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 | 1,050.16 μs |  34.515 μs |  1.892 μs |  1.65 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 | 1,009.35 μs |  66.378 μs |  3.638 μs |  1.58 |    0.02 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   994.65 μs |  69.444 μs |  3.806 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   872.69 μs |  64.144 μs |  3.516 μs |  0.88 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   849.38 μs |  19.973 μs |  1.095 μs |  0.85 |    0.00 |
|            |                 |           |               |       |                        |             |            |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 | 1,004.37 μs |  31.530 μs |  1.728 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   871.55 μs | 362.482 μs | 19.869 μs |  0.87 |    0.02 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   866.19 μs | 108.989 μs |  5.974 μs |  0.86 |    0.01 |
