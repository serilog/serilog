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
|     Method |             Job |       Jit |       Runtime | Items | MaxDegreeOfParallelism |        Mean |        Error |    StdDev | Ratio | RatioSD |
|----------- |---------------- |---------- |-------------- |------ |----------------------- |------------:|-------------:|----------:|------:|--------:|
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                     **-1** |   **226.64 μs** |    **64.916 μs** |  **3.558 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    25.28 μs |     4.940 μs |  0.271 μs |  0.11 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    26.39 μs |     8.857 μs |  0.485 μs |  0.12 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |   536.49 μs |   187.828 μs | 10.296 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    34.30 μs |    15.736 μs |  0.863 μs |  0.06 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    34.94 μs |    15.713 μs |  0.861 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |   514.39 μs |   168.085 μs |  9.213 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    34.14 μs |    12.338 μs |  0.676 μs |  0.07 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    36.95 μs |    25.169 μs |  1.380 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                      **1** |    **61.49 μs** |    **39.537 μs** |  **2.167 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |   104.32 μs |    29.804 μs |  1.634 μs |  1.70 |    0.08 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |    98.58 μs |    31.496 μs |  1.726 μs |  1.60 |    0.03 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |   103.66 μs |    15.551 μs |  0.852 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    88.23 μs |     9.766 μs |  0.535 μs |  0.85 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    84.08 μs |    35.669 μs |  1.955 μs |  0.81 |    0.02 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |   102.74 μs |    25.793 μs |  1.414 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    83.25 μs |     8.279 μs |  0.454 μs |  0.81 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    84.63 μs |    23.533 μs |  1.290 μs |  0.82 |    0.02 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                     **-1** |   **310.52 μs** |   **285.502 μs** | **15.649 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    39.16 μs |    18.034 μs |  0.988 μs |  0.13 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    39.21 μs |     7.203 μs |  0.395 μs |  0.13 |    0.01 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |   726.56 μs |   322.464 μs | 17.675 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    52.21 μs |    21.316 μs |  1.168 μs |  0.07 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    53.90 μs |    30.292 μs |  1.660 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |   732.09 μs |   595.377 μs | 32.635 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    52.26 μs |    38.242 μs |  2.096 μs |  0.07 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    54.36 μs |    21.160 μs |  1.160 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                      **1** |   **129.94 μs** |    **72.565 μs** |  **3.978 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   228.28 μs |    75.707 μs |  4.150 μs |  1.76 |    0.07 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   198.13 μs |    76.641 μs |  4.201 μs |  1.53 |    0.07 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   205.59 μs |    33.910 μs |  1.859 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   160.29 μs |    45.515 μs |  2.495 μs |  0.78 |    0.02 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   169.70 μs |    41.992 μs |  2.302 μs |  0.83 |    0.02 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   209.16 μs |    15.713 μs |  0.861 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   185.46 μs |    24.266 μs |  1.330 μs |  0.89 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   166.33 μs |    43.557 μs |  2.388 μs |  0.80 |    0.01 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                     **-1** |   **588.65 μs** |   **193.232 μs** | **10.592 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |    94.85 μs |    45.345 μs |  2.486 μs |  0.16 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |    94.85 μs |     8.642 μs |  0.474 μs |  0.16 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 | 1,181.20 μs |   312.752 μs | 17.143 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |   102.90 μs |    19.756 μs |  1.083 μs |  0.09 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |   105.35 μs |     9.979 μs |  0.547 μs |  0.09 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 | 1,190.72 μs |   600.239 μs | 32.901 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   103.77 μs |    19.939 μs |  1.093 μs |  0.09 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   102.35 μs |    50.668 μs |  2.777 μs |  0.09 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                      **1** |   **308.11 μs** |   **184.230 μs** | **10.098 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   512.26 μs |   230.312 μs | 12.624 μs |  1.66 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   507.41 μs |   298.522 μs | 16.363 μs |  1.65 |    0.11 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   511.47 μs |   296.873 μs | 16.273 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   432.31 μs |   134.270 μs |  7.360 μs |  0.85 |    0.02 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   417.82 μs |   103.234 μs |  5.659 μs |  0.82 |    0.02 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   512.22 μs |   102.541 μs |  5.621 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   434.67 μs |   118.888 μs |  6.517 μs |  0.85 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   425.94 μs |    71.139 μs |  3.899 μs |  0.83 |    0.02 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                     **-1** | **1,050.26 μs** |   **847.452 μs** | **46.452 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   172.87 μs |    32.351 μs |  1.773 μs |  0.16 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   181.94 μs |    66.305 μs |  3.634 μs |  0.17 |    0.01 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 | 1,918.35 μs | 1,072.740 μs | 58.800 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   194.41 μs |    24.684 μs |  1.353 μs |  0.10 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   189.57 μs |    88.936 μs |  4.875 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 | 1,921.79 μs |   341.651 μs | 18.727 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   187.52 μs |    60.402 μs |  3.311 μs |  0.10 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   186.25 μs |   137.175 μs |  7.519 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                      **1** |   **618.12 μs** |   **309.685 μs** | **16.975 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 | 1,041.73 μs |   475.927 μs | 26.087 μs |  1.69 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 |   994.36 μs |   595.490 μs | 32.641 μs |  1.61 |    0.10 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 | 1,017.43 μs |   477.398 μs | 26.168 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   854.54 μs |   230.342 μs | 12.626 μs |  0.84 |    0.03 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   859.30 μs |   168.591 μs |  9.241 μs |  0.84 |    0.02 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 | 1,016.47 μs |   145.547 μs |  7.978 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   874.73 μs |   134.364 μs |  7.365 μs |  0.86 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   867.80 μs |   123.630 μs |  6.777 μs |  0.85 |    0.01 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                     **-1** | **1,075.68 μs** |   **286.010 μs** | **15.677 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   173.47 μs |   147.791 μs |  8.101 μs |  0.16 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   160.87 μs |    84.645 μs |  4.640 μs |  0.15 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 | 1,972.02 μs | 1,239.326 μs | 67.932 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   192.07 μs |   108.922 μs |  5.970 μs |  0.10 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   189.97 μs |    41.620 μs |  2.281 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 | 1,999.58 μs |   234.880 μs | 12.875 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   192.56 μs |    79.828 μs |  4.376 μs |  0.10 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   195.35 μs |    28.943 μs |  1.586 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                      **1** |   **637.18 μs** |   **323.760 μs** | **17.746 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 | 1,063.79 μs |   753.809 μs | 41.319 μs |  1.67 |    0.03 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 | 1,022.22 μs |   553.398 μs | 30.334 μs |  1.61 |    0.09 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 | 1,022.85 μs |   526.335 μs | 28.850 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   876.93 μs |   124.179 μs |  6.807 μs |  0.86 |    0.02 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   893.33 μs |   184.961 μs | 10.138 μs |  0.87 |    0.03 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 | 1,014.17 μs |   369.891 μs | 20.275 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   867.75 μs |   122.256 μs |  6.701 μs |  0.86 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   871.46 μs |   260.685 μs | 14.289 μs |  0.86 |    0.02 |
