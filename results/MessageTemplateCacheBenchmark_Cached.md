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
|     Method |             Job |       Jit |       Runtime | Items | MaxDegreeOfParallelism |        Mean |        Error |     StdDev | Ratio | RatioSD |
|----------- |---------------- |---------- |-------------- |------ |----------------------- |------------:|-------------:|-----------:|------:|--------:|
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                     **-1** |   **220.27 μs** |     **6.308 μs** |   **0.346 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    27.52 μs |     8.681 μs |   0.476 μs |  0.12 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    26.88 μs |    16.241 μs |   0.890 μs |  0.12 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |   505.66 μs |   338.362 μs |  18.547 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    37.88 μs |     5.684 μs |   0.312 μs |  0.07 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    38.34 μs |    23.856 μs |   1.308 μs |  0.08 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |   537.20 μs |   610.282 μs |  33.452 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    36.79 μs |    12.169 μs |   0.667 μs |  0.07 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    39.76 μs |    26.599 μs |   1.458 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                      **1** |    **64.26 μs** |    **16.845 μs** |   **0.923 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |   109.40 μs |    19.838 μs |   1.087 μs |  1.70 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |   105.39 μs |    25.101 μs |   1.376 μs |  1.64 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |   114.79 μs |    32.063 μs |   1.757 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    89.62 μs |    29.721 μs |   1.629 μs |  0.78 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    85.73 μs |     8.382 μs |   0.459 μs |  0.75 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |   107.43 μs |    30.066 μs |   1.648 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    97.67 μs |    46.256 μs |   2.535 μs |  0.91 |    0.04 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    85.29 μs |     6.764 μs |   0.371 μs |  0.79 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                     **-1** |   **339.32 μs** |   **348.220 μs** |  **19.087 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    42.01 μs |    32.140 μs |   1.762 μs |  0.12 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    40.58 μs |    25.921 μs |   1.421 μs |  0.12 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |   799.96 μs |   482.933 μs |  26.471 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    55.12 μs |    13.672 μs |   0.749 μs |  0.07 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    57.85 μs |    10.187 μs |   0.558 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |   775.39 μs |   626.109 μs |  34.319 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    55.58 μs |    12.785 μs |   0.701 μs |  0.07 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    57.86 μs |     4.408 μs |   0.242 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                      **1** |   **131.60 μs** |    **28.536 μs** |   **1.564 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   220.36 μs |    29.676 μs |   1.627 μs |  1.67 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   206.96 μs |    43.318 μs |   2.374 μs |  1.57 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   206.05 μs |    23.313 μs |   1.278 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   173.93 μs |    41.514 μs |   2.276 μs |  0.84 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   173.69 μs |    60.293 μs |   3.305 μs |  0.84 |    0.02 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   217.11 μs |    74.916 μs |   4.106 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   175.13 μs |    10.221 μs |   0.560 μs |  0.81 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   181.04 μs |    23.836 μs |   1.307 μs |  0.83 |    0.02 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                     **-1** |   **598.73 μs** |   **112.252 μs** |   **6.153 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |    86.70 μs |    65.035 μs |   3.565 μs |  0.14 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |    87.05 μs |    47.784 μs |   2.619 μs |  0.15 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 | 1,273.03 μs |   248.258 μs |  13.608 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |   114.82 μs |   104.641 μs |   5.736 μs |  0.09 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |   111.78 μs |    60.544 μs |   3.319 μs |  0.09 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 | 1,284.79 μs | 1,464.271 μs |  80.262 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   112.54 μs |    11.159 μs |   0.612 μs |  0.09 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   108.89 μs |    77.872 μs |   4.268 μs |  0.09 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                      **1** |   **323.83 μs** |    **49.118 μs** |   **2.692 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   558.44 μs |    44.635 μs |   2.447 μs |  1.72 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   520.61 μs |    54.737 μs |   3.000 μs |  1.61 |    0.02 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   538.92 μs |    79.125 μs |   4.337 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   438.62 μs |    30.050 μs |   1.647 μs |  0.81 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   451.31 μs |    27.527 μs |   1.509 μs |  0.84 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   521.49 μs |   114.795 μs |   6.292 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   439.78 μs |    57.573 μs |   3.156 μs |  0.84 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   427.70 μs |    40.315 μs |   2.210 μs |  0.82 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                     **-1** | **1,036.65 μs** |   **419.551 μs** |  **22.997 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   162.79 μs |    74.938 μs |   4.108 μs |  0.16 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   165.49 μs |   147.406 μs |   8.080 μs |  0.16 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 | 2,055.18 μs | 2,094.311 μs | 114.796 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   202.64 μs |    40.445 μs |   2.217 μs |  0.10 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   196.12 μs |   118.348 μs |   6.487 μs |  0.10 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 | 2,089.07 μs | 1,641.001 μs |  89.949 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   194.03 μs |   214.285 μs |  11.746 μs |  0.09 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   196.42 μs |   140.958 μs |   7.726 μs |  0.09 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                      **1** |   **630.17 μs** |    **57.654 μs** |   **3.160 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 | 1,038.46 μs |   303.171 μs |  16.618 μs |  1.65 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 | 1,041.98 μs |   149.563 μs |   8.198 μs |  1.65 |    0.02 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 | 1,046.64 μs |   140.866 μs |   7.721 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   891.91 μs |    75.335 μs |   4.129 μs |  0.85 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   885.20 μs |   114.539 μs |   6.278 μs |  0.85 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 | 1,025.88 μs |   178.807 μs |   9.801 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   892.46 μs |   146.645 μs |   8.038 μs |  0.87 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   917.79 μs |   114.467 μs |   6.274 μs |  0.89 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                     **-1** | **1,079.44 μs** |   **495.145 μs** |  **27.141 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   167.25 μs |   123.547 μs |   6.772 μs |  0.15 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   170.94 μs |   100.157 μs |   5.490 μs |  0.16 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 | 1,978.00 μs | 1,707.188 μs |  93.577 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   203.98 μs |   180.768 μs |   9.908 μs |  0.10 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   214.53 μs |   114.103 μs |   6.254 μs |  0.11 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 | 2,004.92 μs | 2,629.257 μs | 144.118 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   202.77 μs |   180.771 μs |   9.909 μs |  0.10 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   207.51 μs |   135.106 μs |   7.406 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                      **1** |   **664.05 μs** |   **106.825 μs** |   **5.855 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 | 1,086.97 μs |   270.094 μs |  14.805 μs |  1.64 |    0.04 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 | 1,065.18 μs |   315.656 μs |  17.302 μs |  1.60 |    0.04 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 | 1,053.16 μs |   116.922 μs |   6.409 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   906.85 μs |    45.711 μs |   2.506 μs |  0.86 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   917.60 μs |   156.426 μs |   8.574 μs |  0.87 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 | 1,041.81 μs |   240.293 μs |  13.171 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   899.16 μs |    72.418 μs |   3.969 μs |  0.86 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   918.65 μs |    16.649 μs |   0.913 μs |  0.88 |    0.01 |
