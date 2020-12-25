``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.101
  [Host] : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
  core31 : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48  : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net50  : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT

Jit=RyuJit  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|     Method |    Job |       Runtime | Items | OverflowCount | MaxDegreeOfParallelism |       Mean |       Error |     StdDev |  Ratio | RatioSD |
|----------- |------- |-------------- |------ |-------------- |----------------------- |-----------:|------------:|-----------:|-------:|--------:|
| **Dictionary** | **core31** | **.NET Core 3.1** | **10000** |             **1** |                     **-1** |   **1.452 ms** |   **0.2306 ms** |  **0.0126 ms** |   **1.00** |    **0.00** |
|  Hashtable | core31 | .NET Core 3.1 | 10000 |             1 |                     -1 |   1.508 ms |   0.8424 ms |  0.0462 ms |   1.04 |    0.03 |
| Concurrent | core31 | .NET Core 3.1 | 10000 |             1 |                     -1 |  37.083 ms |  94.2792 ms |  5.1678 ms |  25.52 |    3.38 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net48 |      .NET 4.8 | 10000 |             1 |                     -1 |   2.974 ms |   1.6401 ms |  0.0899 ms |   1.00 |    0.00 |
|  Hashtable |  net48 |      .NET 4.8 | 10000 |             1 |                     -1 |  23.026 ms |  29.1268 ms |  1.5965 ms |   7.74 |    0.30 |
| Concurrent |  net48 |      .NET 4.8 | 10000 |             1 |                     -1 |  47.304 ms |  17.8661 ms |  0.9793 ms |  15.91 |    0.37 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net50 | .NET Core 5.0 | 10000 |             1 |                     -1 |   1.370 ms |   0.1610 ms |  0.0088 ms |   1.00 |    0.00 |
|  Hashtable |  net50 | .NET Core 5.0 | 10000 |             1 |                     -1 |   1.220 ms |   0.2395 ms |  0.0131 ms |   0.89 |    0.01 |
| Concurrent |  net50 | .NET Core 5.0 | 10000 |             1 |                     -1 |  31.537 ms | 103.5237 ms |  5.6745 ms |  23.02 |    4.08 |
|            |        |               |       |               |                        |            |             |            |        |         |
| **Dictionary** | **core31** | **.NET Core 3.1** | **10000** |             **1** |                      **1** |   **1.087 ms** |   **0.0401 ms** |  **0.0022 ms** |   **1.00** |    **0.00** |
|  Hashtable | core31 | .NET Core 3.1 | 10000 |             1 |                      1 |   2.174 ms |   0.3309 ms |  0.0181 ms |   2.00 |    0.02 |
| Concurrent | core31 | .NET Core 3.1 | 10000 |             1 |                      1 | 238.445 ms |  26.8114 ms |  1.4696 ms | 219.30 |    0.93 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net48 |      .NET 4.8 | 10000 |             1 |                      1 |   1.847 ms |   0.2860 ms |  0.0157 ms |   1.00 |    0.00 |
|  Hashtable |  net48 |      .NET 4.8 | 10000 |             1 |                      1 |   1.881 ms |   0.1404 ms |  0.0077 ms |   1.02 |    0.00 |
| Concurrent |  net48 |      .NET 4.8 | 10000 |             1 |                      1 | 245.086 ms |  42.1467 ms |  2.3102 ms | 132.69 |    0.15 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net50 | .NET Core 5.0 | 10000 |             1 |                      1 |   1.028 ms |   0.2358 ms |  0.0129 ms |   1.00 |    0.00 |
|  Hashtable |  net50 | .NET Core 5.0 | 10000 |             1 |                      1 |   2.144 ms |   0.3452 ms |  0.0189 ms |   2.09 |    0.04 |
| Concurrent |  net50 | .NET Core 5.0 | 10000 |             1 |                      1 | 224.040 ms |  89.0228 ms |  4.8796 ms | 217.94 |    3.34 |
|            |        |               |       |               |                        |            |             |            |        |         |
| **Dictionary** | **core31** | **.NET Core 3.1** | **10000** |            **10** |                     **-1** |   **1.464 ms** |   **0.1410 ms** |  **0.0077 ms** |   **1.00** |    **0.00** |
|  Hashtable | core31 | .NET Core 3.1 | 10000 |            10 |                     -1 |   1.593 ms |   2.4095 ms |  0.1321 ms |   1.09 |    0.10 |
| Concurrent | core31 | .NET Core 3.1 | 10000 |            10 |                     -1 |  58.175 ms | 180.0531 ms |  9.8693 ms |  39.71 |    6.55 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net48 |      .NET 4.8 | 10000 |            10 |                     -1 |   3.175 ms |   1.6878 ms |  0.0925 ms |   1.00 |    0.00 |
|  Hashtable |  net48 |      .NET 4.8 | 10000 |            10 |                     -1 |  21.238 ms |  14.5999 ms |  0.8003 ms |   6.69 |    0.24 |
| Concurrent |  net48 |      .NET 4.8 | 10000 |            10 |                     -1 |  56.714 ms |  21.8033 ms |  1.1951 ms |  17.88 |    0.82 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net50 | .NET Core 5.0 | 10000 |            10 |                     -1 |   1.437 ms |   0.7115 ms |  0.0390 ms |   1.00 |    0.00 |
|  Hashtable |  net50 | .NET Core 5.0 | 10000 |            10 |                     -1 |   1.982 ms |  10.9582 ms |  0.6007 ms |   1.38 |    0.40 |
| Concurrent |  net50 | .NET Core 5.0 | 10000 |            10 |                     -1 |  53.559 ms |  63.7196 ms |  3.4927 ms |  37.26 |    1.73 |
|            |        |               |       |               |                        |            |             |            |        |         |
| **Dictionary** | **core31** | **.NET Core 3.1** | **10000** |            **10** |                      **1** |   **1.108 ms** |   **0.4928 ms** |  **0.0270 ms** |   **1.00** |    **0.00** |
|  Hashtable | core31 | .NET Core 3.1 | 10000 |            10 |                      1 |   2.149 ms |   0.5003 ms |  0.0274 ms |   1.94 |    0.06 |
| Concurrent | core31 | .NET Core 3.1 | 10000 |            10 |                      1 | 236.919 ms |  86.0397 ms |  4.7161 ms | 213.88 |    4.34 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net48 |      .NET 4.8 | 10000 |            10 |                      1 |   1.843 ms |   0.2567 ms |  0.0141 ms |   1.00 |    0.00 |
|  Hashtable |  net48 |      .NET 4.8 | 10000 |            10 |                      1 |   1.861 ms |   0.1861 ms |  0.0102 ms |   1.01 |    0.00 |
| Concurrent |  net48 |      .NET 4.8 | 10000 |            10 |                      1 | 242.013 ms |  42.8306 ms |  2.3477 ms | 131.31 |    0.32 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net50 | .NET Core 5.0 | 10000 |            10 |                      1 |   1.039 ms |   0.2188 ms |  0.0120 ms |   1.00 |    0.00 |
|  Hashtable |  net50 | .NET Core 5.0 | 10000 |            10 |                      1 |   2.128 ms |   0.5418 ms |  0.0297 ms |   2.05 |    0.05 |
| Concurrent |  net50 | .NET Core 5.0 | 10000 |            10 |                      1 | 227.973 ms | 197.1654 ms | 10.8073 ms | 219.47 |   10.92 |
|            |        |               |       |               |                        |            |             |            |        |         |
| **Dictionary** | **core31** | **.NET Core 3.1** | **10000** |           **100** |                     **-1** |   **1.612 ms** |   **0.9043 ms** |  **0.0496 ms** |   **1.00** |    **0.00** |
|  Hashtable | core31 | .NET Core 3.1 | 10000 |           100 |                     -1 |   2.891 ms |  13.2612 ms |  0.7269 ms |   1.80 |    0.51 |
| Concurrent | core31 | .NET Core 3.1 | 10000 |           100 |                     -1 | 103.397 ms | 200.3006 ms | 10.9792 ms |  64.27 |    8.00 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net48 |      .NET 4.8 | 10000 |           100 |                     -1 |   3.350 ms |   1.4499 ms |  0.0795 ms |   1.00 |    0.00 |
|  Hashtable |  net48 |      .NET 4.8 | 10000 |           100 |                     -1 |  21.659 ms |  23.3431 ms |  1.2795 ms |   6.46 |    0.24 |
| Concurrent |  net48 |      .NET 4.8 | 10000 |           100 |                     -1 |  75.633 ms | 183.7762 ms | 10.0734 ms |  22.58 |    3.00 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net50 | .NET Core 5.0 | 10000 |           100 |                     -1 |   1.464 ms |   0.2646 ms |  0.0145 ms |   1.00 |    0.00 |
|  Hashtable |  net50 | .NET Core 5.0 | 10000 |           100 |                     -1 |   2.865 ms |   5.7780 ms |  0.3167 ms |   1.96 |    0.23 |
| Concurrent |  net50 | .NET Core 5.0 | 10000 |           100 |                     -1 |  79.553 ms | 113.6922 ms |  6.2319 ms |  54.35 |    4.65 |
|            |        |               |       |               |                        |            |             |            |        |         |
| **Dictionary** | **core31** | **.NET Core 3.1** | **10000** |           **100** |                      **1** |   **1.109 ms** |   **0.3446 ms** |  **0.0189 ms** |   **1.00** |    **0.00** |
|  Hashtable | core31 | .NET Core 3.1 | 10000 |           100 |                      1 |   2.129 ms |   0.4091 ms |  0.0224 ms |   1.92 |    0.05 |
| Concurrent | core31 | .NET Core 3.1 | 10000 |           100 |                      1 | 237.859 ms |  63.7970 ms |  3.4969 ms | 214.48 |    4.13 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net48 |      .NET 4.8 | 10000 |           100 |                      1 |   1.844 ms |   0.1171 ms |  0.0064 ms |   1.00 |    0.00 |
|  Hashtable |  net48 |      .NET 4.8 | 10000 |           100 |                      1 |   1.879 ms |   0.2234 ms |  0.0122 ms |   1.02 |    0.00 |
| Concurrent |  net48 |      .NET 4.8 | 10000 |           100 |                      1 | 244.536 ms |  71.1950 ms |  3.9024 ms | 132.61 |    1.77 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net50 | .NET Core 5.0 | 10000 |           100 |                      1 |   1.035 ms |   0.2618 ms |  0.0143 ms |   1.00 |    0.00 |
|  Hashtable |  net50 | .NET Core 5.0 | 10000 |           100 |                      1 |   2.132 ms |   0.2919 ms |  0.0160 ms |   2.06 |    0.04 |
| Concurrent |  net50 | .NET Core 5.0 | 10000 |           100 |                      1 | 219.405 ms |  43.6321 ms |  2.3916 ms | 212.02 |    3.40 |
|            |        |               |       |               |                        |            |             |            |        |         |
| **Dictionary** | **core31** | **.NET Core 3.1** | **10000** |          **1000** |                     **-1** |   **1.755 ms** |   **0.4891 ms** |  **0.0268 ms** |   **1.00** |    **0.00** |
|  Hashtable | core31 | .NET Core 3.1 | 10000 |          1000 |                     -1 |   3.546 ms |   3.5191 ms |  0.1929 ms |   2.02 |    0.10 |
| Concurrent | core31 | .NET Core 3.1 | 10000 |          1000 |                     -1 | 185.893 ms | 482.6151 ms | 26.4538 ms | 105.98 |   15.45 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net48 |      .NET 4.8 | 10000 |          1000 |                     -1 |   3.170 ms |   1.8270 ms |  0.1001 ms |   1.00 |    0.00 |
|  Hashtable |  net48 |      .NET 4.8 | 10000 |          1000 |                     -1 |  18.510 ms |  16.3129 ms |  0.8942 ms |   5.85 |    0.45 |
| Concurrent |  net48 |      .NET 4.8 | 10000 |          1000 |                     -1 | 166.868 ms | 166.3770 ms |  9.1197 ms |  52.72 |    4.25 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net50 | .NET Core 5.0 | 10000 |          1000 |                     -1 |   1.660 ms |   0.6623 ms |  0.0363 ms |   1.00 |    0.00 |
|  Hashtable |  net50 | .NET Core 5.0 | 10000 |          1000 |                     -1 |   3.534 ms |   1.7028 ms |  0.0933 ms |   2.13 |    0.10 |
| Concurrent |  net50 | .NET Core 5.0 | 10000 |          1000 |                     -1 | 145.543 ms | 632.2747 ms | 34.6571 ms |  87.67 |   20.65 |
|            |        |               |       |               |                        |            |             |            |        |         |
| **Dictionary** | **core31** | **.NET Core 3.1** | **10000** |          **1000** |                      **1** |   **1.111 ms** |   **0.2604 ms** |  **0.0143 ms** |   **1.00** |    **0.00** |
|  Hashtable | core31 | .NET Core 3.1 | 10000 |          1000 |                      1 |   2.134 ms |   0.8945 ms |  0.0490 ms |   1.92 |    0.07 |
| Concurrent | core31 | .NET Core 3.1 | 10000 |          1000 |                      1 | 242.335 ms |  28.8866 ms |  1.5834 ms | 218.17 |    1.79 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net48 |      .NET 4.8 | 10000 |          1000 |                      1 |   1.854 ms |   0.3557 ms |  0.0195 ms |   1.00 |    0.00 |
|  Hashtable |  net48 |      .NET 4.8 | 10000 |          1000 |                      1 |   1.834 ms |   0.2259 ms |  0.0124 ms |   0.99 |    0.00 |
| Concurrent |  net48 |      .NET 4.8 | 10000 |          1000 |                      1 | 251.545 ms |  42.8993 ms |  2.3515 ms | 135.70 |    0.61 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net50 | .NET Core 5.0 | 10000 |          1000 |                      1 |   1.028 ms |   0.1171 ms |  0.0064 ms |   1.00 |    0.00 |
|  Hashtable |  net50 | .NET Core 5.0 | 10000 |          1000 |                      1 |   2.144 ms |   0.6826 ms |  0.0374 ms |   2.09 |    0.05 |
| Concurrent |  net50 | .NET Core 5.0 | 10000 |          1000 |                      1 | 216.620 ms |  41.6006 ms |  2.2803 ms | 210.71 |    3.40 |
