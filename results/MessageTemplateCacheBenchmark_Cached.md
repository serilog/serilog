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
|     Method |             Job |       Jit |       Runtime | Items | MaxDegreeOfParallelism |        Mean |        Error |     StdDev | Ratio | RatioSD |
|----------- |---------------- |---------- |-------------- |------ |----------------------- |------------:|-------------:|-----------:|------:|--------:|
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                     **-1** |   **224.70 μs** |   **107.282 μs** |   **5.880 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    27.79 μs |    11.119 μs |   0.609 μs |  0.12 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    28.13 μs |     6.636 μs |   0.364 μs |  0.13 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |   502.78 μs |   621.678 μs |  34.076 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    41.12 μs |    15.006 μs |   0.823 μs |  0.08 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    42.06 μs |    14.690 μs |   0.805 μs |  0.08 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |   550.56 μs |   754.600 μs |  41.362 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    37.81 μs |    24.900 μs |   1.365 μs |  0.07 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    42.85 μs |    14.520 μs |   0.796 μs |  0.08 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                      **1** |    **62.17 μs** |    **31.280 μs** |   **1.715 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |   110.27 μs |     4.485 μs |   0.246 μs |  1.77 |    0.05 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |   105.35 μs |    49.564 μs |   2.717 μs |  1.70 |    0.08 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |   110.10 μs |    11.353 μs |   0.622 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    85.29 μs |    19.749 μs |   1.083 μs |  0.77 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    87.64 μs |    38.316 μs |   2.100 μs |  0.80 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |   114.15 μs |   116.878 μs |   6.406 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    92.66 μs |    25.664 μs |   1.407 μs |  0.81 |    0.06 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    87.29 μs |    35.437 μs |   1.942 μs |  0.77 |    0.06 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                     **-1** |   **353.67 μs** |   **170.103 μs** |   **9.324 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    48.94 μs |    13.173 μs |   0.722 μs |  0.14 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    51.70 μs |    30.344 μs |   1.663 μs |  0.15 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |   867.87 μs |   486.200 μs |  26.650 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    63.04 μs |    16.442 μs |   0.901 μs |  0.07 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    65.43 μs |    17.854 μs |   0.979 μs |  0.08 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |   874.07 μs |   598.453 μs |  32.803 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    64.34 μs |    31.346 μs |   1.718 μs |  0.07 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    62.80 μs |    26.934 μs |   1.476 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                      **1** |   **131.40 μs** |    **62.161 μs** |   **3.407 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   225.66 μs |    33.625 μs |   1.843 μs |  1.72 |    0.04 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   208.67 μs |    57.669 μs |   3.161 μs |  1.59 |    0.06 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   207.70 μs |    56.791 μs |   3.113 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   174.43 μs |    28.734 μs |   1.575 μs |  0.84 |    0.02 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   182.39 μs |    35.580 μs |   1.950 μs |  0.88 |    0.02 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   211.03 μs |    55.804 μs |   3.059 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   167.57 μs |    72.481 μs |   3.973 μs |  0.79 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   175.54 μs |    67.335 μs |   3.691 μs |  0.83 |    0.03 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                     **-1** |   **661.23 μs** |   **514.916 μs** |  **28.224 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |    99.58 μs |   105.325 μs |   5.773 μs |  0.15 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |   111.62 μs |    53.765 μs |   2.947 μs |  0.17 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 | 1,337.99 μs |   836.620 μs |  45.858 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |   122.49 μs |   107.123 μs |   5.872 μs |  0.09 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |   124.28 μs |    77.398 μs |   4.242 μs |  0.09 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 | 1,297.23 μs |   845.684 μs |  46.355 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   123.38 μs |    16.878 μs |   0.925 μs |  0.10 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   121.69 μs |   174.509 μs |   9.565 μs |  0.09 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                      **1** |   **323.88 μs** |   **115.914 μs** |   **6.354 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   547.55 μs |   205.973 μs |  11.290 μs |  1.69 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   523.97 μs |   280.763 μs |  15.390 μs |  1.62 |    0.08 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   539.76 μs |   203.623 μs |  11.161 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   447.14 μs |   154.932 μs |   8.492 μs |  0.83 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   463.26 μs |   459.080 μs |  25.164 μs |  0.86 |    0.03 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   527.10 μs |   134.481 μs |   7.371 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   443.86 μs |   106.822 μs |   5.855 μs |  0.84 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   429.80 μs |    24.819 μs |   1.360 μs |  0.82 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                     **-1** | **1,066.33 μs** |   **257.003 μs** |  **14.087 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   177.59 μs |    35.332 μs |   1.937 μs |  0.17 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   190.75 μs |   109.469 μs |   6.000 μs |  0.18 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 | 2,073.46 μs |   547.893 μs |  30.032 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   226.57 μs |    72.018 μs |   3.948 μs |  0.11 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   222.80 μs |   171.066 μs |   9.377 μs |  0.11 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 | 2,069.36 μs | 1,869.586 μs | 102.478 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   232.19 μs |    84.420 μs |   4.627 μs |  0.11 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   225.51 μs |    99.590 μs |   5.459 μs |  0.11 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                      **1** |   **639.99 μs** |   **222.007 μs** |  **12.169 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 | 1,101.87 μs |   545.388 μs |  29.895 μs |  1.72 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 | 1,031.51 μs |   343.059 μs |  18.804 μs |  1.61 |    0.06 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 | 1,201.03 μs | 2,206.394 μs | 120.940 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   844.59 μs |   102.527 μs |   5.620 μs |  0.71 |    0.07 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   860.75 μs |    23.945 μs |   1.313 μs |  0.72 |    0.07 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 | 1,030.72 μs |   300.662 μs |  16.480 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   894.73 μs |   180.168 μs |   9.876 μs |  0.87 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   864.30 μs |    54.774 μs |   3.002 μs |  0.84 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                     **-1** | **1,126.39 μs** |   **270.313 μs** |  **14.817 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   201.20 μs |    87.195 μs |   4.779 μs |  0.18 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   191.30 μs |   189.420 μs |  10.383 μs |  0.17 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 | 2,064.49 μs | 1,712.380 μs |  93.861 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   221.14 μs |    33.711 μs |   1.848 μs |  0.11 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   227.24 μs |   143.657 μs |   7.874 μs |  0.11 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 | 2,118.64 μs | 1,810.779 μs |  99.255 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   240.32 μs |    57.592 μs |   3.157 μs |  0.11 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   237.59 μs |    73.720 μs |   4.041 μs |  0.11 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                      **1** |   **650.32 μs** |    **67.330 μs** |   **3.691 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 | 1,085.35 μs |   304.349 μs |  16.682 μs |  1.67 |    0.03 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 | 1,048.06 μs |   239.624 μs |  13.135 μs |  1.61 |    0.02 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 | 1,043.85 μs |   391.695 μs |  21.470 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   901.06 μs |   146.103 μs |   8.008 μs |  0.86 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   883.97 μs |    24.821 μs |   1.361 μs |  0.85 |    0.02 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 | 1,038.23 μs |   271.075 μs |  14.859 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   883.55 μs |   182.966 μs |  10.029 μs |  0.85 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   890.58 μs |   166.703 μs |   9.138 μs |  0.86 |    0.02 |
