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
|     Method |             Job |       Jit |       Runtime | Items | OverflowCount | MaxDegreeOfParallelism |       Mean |       Error |     StdDev |  Ratio | RatioSD |
|----------- |---------------- |---------- |-------------- |------ |-------------- |----------------------- |-----------:|------------:|-----------:|-------:|--------:|
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |             **1** |                     **-1** |   **1.525 ms** |   **0.9792 ms** |  **0.0537 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                     -1 |   2.245 ms |   4.3679 ms |  0.2394 ms |   1.47 |    0.11 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                     -1 |  38.944 ms | 133.2154 ms |  7.3020 ms |  25.59 |    5.05 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                     -1 |   2.980 ms |   3.3009 ms |  0.1809 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                     -1 |  23.542 ms |  15.7139 ms |  0.8613 ms |   7.93 |    0.78 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                     -1 |  46.896 ms |  24.2822 ms |  1.3310 ms |  15.76 |    0.53 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                     -1 |   2.894 ms |   0.6529 ms |  0.0358 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                     -1 |  22.844 ms |  15.1299 ms |  0.8293 ms |   7.89 |    0.30 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                     -1 |  48.306 ms |  68.6399 ms |  3.7624 ms |  16.68 |    1.10 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |             **1** |                      **1** |   **1.034 ms** |   **0.1903 ms** |  **0.0104 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                      1 |   2.046 ms |   0.3178 ms |  0.0174 ms |   1.98 |    0.03 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                      1 | 241.808 ms | 250.7087 ms | 13.7422 ms | 233.82 |   13.26 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.764 ms |   0.3811 ms |  0.0209 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.781 ms |   0.5270 ms |  0.0289 ms |   1.01 |    0.03 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                      1 | 237.287 ms |  24.6472 ms |  1.3510 ms | 134.52 |    2.33 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.740 ms |   0.0453 ms |  0.0025 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.744 ms |   0.0816 ms |  0.0045 ms |   1.00 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                      1 | 232.920 ms |  37.9992 ms |  2.0829 ms | 133.83 |    1.13 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |            **10** |                     **-1** |   **1.460 ms** |   **0.2139 ms** |  **0.0117 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                     -1 |   3.736 ms |   5.9899 ms |  0.3283 ms |   2.56 |    0.22 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                     -1 |  59.025 ms |  27.6912 ms |  1.5178 ms |  40.42 |    0.85 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                     -1 |   3.027 ms |   1.4659 ms |  0.0803 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                     -1 |  25.128 ms |  10.5339 ms |  0.5774 ms |   8.31 |    0.32 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                     -1 |  52.093 ms |  54.6831 ms |  2.9974 ms |  17.20 |    0.56 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                     -1 |   3.033 ms |   0.7084 ms |  0.0388 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                     -1 |  22.902 ms |   9.7270 ms |  0.5332 ms |   7.55 |    0.27 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                     -1 |  52.894 ms |  28.5392 ms |  1.5643 ms |  17.44 |    0.53 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |            **10** |                      **1** |   **1.039 ms** |   **0.1351 ms** |  **0.0074 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                      1 |   2.056 ms |   0.1011 ms |  0.0055 ms |   1.98 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                      1 | 224.102 ms |   6.2545 ms |  0.3428 ms | 215.67 |    1.81 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.746 ms |   0.1030 ms |  0.0056 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.775 ms |   0.1274 ms |  0.0070 ms |   1.02 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                      1 | 229.246 ms |  17.5970 ms |  0.9645 ms | 131.27 |    0.13 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.751 ms |   0.1390 ms |  0.0076 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.772 ms |   0.0146 ms |  0.0008 ms |   1.01 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                      1 | 238.069 ms |  46.6538 ms |  2.5573 ms | 135.99 |    1.99 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |           **100** |                     **-1** |   **1.597 ms** |   **0.1241 ms** |  **0.0068 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                     -1 |   3.011 ms |   6.1965 ms |  0.3397 ms |   1.88 |    0.21 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                     -1 | 102.661 ms | 440.0325 ms | 24.1197 ms |  64.29 |   15.21 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                     -1 |   3.202 ms |   1.9289 ms |  0.1057 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                     -1 |  22.528 ms |  27.8409 ms |  1.5261 ms |   7.05 |    0.66 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                     -1 |  62.965 ms | 217.9335 ms | 11.9457 ms |  19.76 |    4.34 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                     -1 |   3.209 ms |   2.1169 ms |  0.1160 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                     -1 |  21.257 ms |  40.6352 ms |  2.2274 ms |   6.63 |    0.71 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                     -1 |  67.365 ms | 123.0198 ms |  6.7431 ms |  20.97 |    1.59 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |           **100** |                      **1** |   **1.051 ms** |   **0.0556 ms** |  **0.0030 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                      1 |   2.043 ms |   0.2332 ms |  0.0128 ms |   1.94 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                      1 | 225.946 ms |  11.3185 ms |  0.6204 ms | 214.97 |    0.77 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.743 ms |   0.2173 ms |  0.0119 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.815 ms |   0.2152 ms |  0.0118 ms |   1.04 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                      1 | 226.010 ms |  21.9911 ms |  1.2054 ms | 129.68 |    1.57 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.741 ms |   0.0635 ms |  0.0035 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.737 ms |   0.0783 ms |  0.0043 ms |   1.00 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                      1 | 243.471 ms |  17.7578 ms |  0.9734 ms | 139.83 |    0.72 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |          **1000** |                     **-1** |   **1.695 ms** |   **0.0946 ms** |  **0.0052 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                     -1 |   3.461 ms |   1.7607 ms |  0.0965 ms |   2.04 |    0.06 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                     -1 | 196.140 ms | 834.7708 ms | 45.7566 ms | 115.70 |   26.63 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                     -1 |   3.201 ms |   1.2270 ms |  0.0673 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                     -1 |  17.970 ms |   8.3544 ms |  0.4579 ms |   5.61 |    0.19 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                     -1 | 143.327 ms | 885.3681 ms | 48.5300 ms |  44.79 |   15.03 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                     -1 |   3.365 ms |   3.9397 ms |  0.2159 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                     -1 |  17.969 ms |  17.1938 ms |  0.9425 ms |   5.37 |    0.62 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                     -1 | 124.150 ms | 473.7719 ms | 25.9690 ms |  37.30 |   10.02 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |          **1000** |                      **1** |   **1.052 ms** |   **0.2590 ms** |  **0.0142 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                      1 |   2.017 ms |   0.4044 ms |  0.0222 ms |   1.92 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                      1 | 222.598 ms |  41.8650 ms |  2.2948 ms | 211.67 |    4.16 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.749 ms |   0.1122 ms |  0.0062 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.746 ms |   0.0456 ms |  0.0025 ms |   1.00 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                      1 | 245.743 ms | 298.0422 ms | 16.3367 ms | 140.56 |    9.86 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.745 ms |   0.0518 ms |  0.0028 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.809 ms |   0.0581 ms |  0.0032 ms |   1.04 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                      1 | 235.961 ms |  30.6001 ms |  1.6773 ms | 135.20 |    0.75 |
