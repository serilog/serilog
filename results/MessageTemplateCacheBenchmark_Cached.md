``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]          : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT

IterationCount=3  LaunchCount=1  WarmupCount=3  

```
|     Method |             Job |       Jit |       Runtime | Items | MaxDegreeOfParallelism |        Mean |        Error |     StdDev | Ratio | RatioSD |
|----------- |---------------- |---------- |-------------- |------ |----------------------- |------------:|-------------:|-----------:|------:|--------:|
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                     **-1** |   **254.19 μs** |   **199.349 μs** |  **10.927 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    34.00 μs |    38.489 μs |   2.110 μs |  0.13 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    29.64 μs |    14.873 μs |   0.815 μs |  0.12 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |   598.86 μs | 2,557.598 μs | 140.191 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    44.79 μs |     5.260 μs |   0.288 μs |  0.08 |    0.02 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    44.20 μs |    15.678 μs |   0.859 μs |  0.08 |    0.02 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |   604.07 μs |   953.817 μs |  52.282 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    42.66 μs |     3.435 μs |   0.188 μs |  0.07 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    41.85 μs |     3.766 μs |   0.206 μs |  0.07 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                      **1** |    **65.12 μs** |    **10.780 μs** |   **0.591 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |   121.77 μs |    13.498 μs |   0.740 μs |  1.87 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |   114.14 μs |    42.267 μs |   2.317 μs |  1.75 |    0.02 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |   113.66 μs |    13.112 μs |   0.719 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    95.51 μs |    19.043 μs |   1.044 μs |  0.84 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    91.62 μs |    23.148 μs |   1.269 μs |  0.81 |    0.02 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |   113.12 μs |    73.760 μs |   4.043 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |   102.28 μs |   218.881 μs |  11.998 μs |  0.90 |    0.07 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    88.73 μs |    25.313 μs |   1.388 μs |  0.78 |    0.02 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                     **-1** |   **335.30 μs** |   **292.509 μs** |  **16.033 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    49.48 μs |    38.331 μs |   2.101 μs |  0.15 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    45.24 μs |    18.508 μs |   1.014 μs |  0.14 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |   840.24 μs |   914.749 μs |  50.140 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    58.81 μs |    36.303 μs |   1.990 μs |  0.07 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    63.97 μs |    37.130 μs |   2.035 μs |  0.08 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |   837.30 μs |   261.394 μs |  14.328 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    63.08 μs |    19.521 μs |   1.070 μs |  0.08 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    63.85 μs |    60.259 μs |   3.303 μs |  0.08 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                      **1** |   **131.38 μs** |    **34.597 μs** |   **1.896 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   217.37 μs |    23.052 μs |   1.264 μs |  1.65 |    0.03 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   211.06 μs |    28.782 μs |   1.578 μs |  1.61 |    0.02 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   221.00 μs |   100.125 μs |   5.488 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   177.59 μs |    17.466 μs |   0.957 μs |  0.80 |    0.02 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   178.36 μs |    97.906 μs |   5.367 μs |  0.81 |    0.02 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   227.44 μs |    78.921 μs |   4.326 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   183.02 μs |   339.261 μs |  18.596 μs |  0.80 |    0.08 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   199.49 μs |   523.755 μs |  28.709 μs |  0.88 |    0.12 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                     **-1** |   **649.41 μs** |   **683.455 μs** |  **37.462 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |   113.45 μs |    47.998 μs |   2.631 μs |  0.17 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |   120.22 μs |    82.205 μs |   4.506 μs |  0.19 |    0.02 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 | 1,461.61 μs |   890.552 μs |  48.814 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |   110.39 μs |    61.271 μs |   3.358 μs |  0.08 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |   129.98 μs |   116.879 μs |   6.407 μs |  0.09 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 | 1,299.75 μs | 1,264.704 μs |  69.323 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   118.18 μs |    62.588 μs |   3.431 μs |  0.09 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   121.34 μs |   114.139 μs |   6.256 μs |  0.09 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                      **1** |   **328.18 μs** |    **14.852 μs** |   **0.814 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   571.92 μs |   102.203 μs |   5.602 μs |  1.74 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   533.42 μs |    26.676 μs |   1.462 μs |  1.63 |    0.00 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   557.79 μs |   341.105 μs |  18.697 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   475.99 μs |    54.070 μs |   2.964 μs |  0.85 |    0.03 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   438.87 μs |    54.942 μs |   3.012 μs |  0.79 |    0.03 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   540.75 μs |   143.273 μs |   7.853 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   497.73 μs |    44.452 μs |   2.437 μs |  0.92 |    0.02 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   456.88 μs |   104.644 μs |   5.736 μs |  0.85 |    0.02 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                     **-1** | **1,088.29 μs** | **1,385.452 μs** |  **75.941 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   198.31 μs |   219.014 μs |  12.005 μs |  0.18 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   214.05 μs |   176.426 μs |   9.671 μs |  0.20 |    0.02 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 | 2,292.41 μs | 3,624.043 μs | 198.646 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   214.94 μs |   108.289 μs |   5.936 μs |  0.09 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   231.43 μs |   140.144 μs |   7.682 μs |  0.10 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 | 2,237.03 μs | 2,530.087 μs | 138.683 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   231.93 μs |    98.840 μs |   5.418 μs |  0.10 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   229.37 μs |   172.094 μs |   9.433 μs |  0.10 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                      **1** |   **679.92 μs** |   **202.101 μs** |  **11.078 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 | 1,090.10 μs |   465.680 μs |  25.525 μs |  1.60 |    0.05 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 | 1,137.80 μs | 1,037.157 μs |  56.850 μs |  1.67 |    0.06 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 | 1,765.86 μs | 2,281.386 μs | 125.050 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   942.51 μs |   611.675 μs |  33.528 μs |  0.54 |    0.05 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   920.05 μs |   143.182 μs |   7.848 μs |  0.52 |    0.04 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 | 1,108.14 μs | 1,131.133 μs |  62.001 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   929.49 μs |   241.341 μs |  13.229 μs |  0.84 |    0.05 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   889.88 μs |    12.457 μs |   0.683 μs |  0.80 |    0.04 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                     **-1** | **1,106.38 μs** |   **713.129 μs** |  **39.089 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   191.88 μs |   131.799 μs |   7.224 μs |  0.17 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   181.23 μs |   137.514 μs |   7.538 μs |  0.16 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 | 2,131.16 μs | 2,710.708 μs | 148.583 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   208.56 μs |    58.510 μs |   3.207 μs |  0.10 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   211.91 μs |    27.811 μs |   1.524 μs |  0.10 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 | 2,149.06 μs | 3,077.967 μs | 168.714 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   208.62 μs |   131.171 μs |   7.190 μs |  0.10 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   208.62 μs |   134.727 μs |   7.385 μs |  0.10 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                      **1** |   **666.55 μs** |   **205.611 μs** |  **11.270 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 | 1,100.12 μs |   425.167 μs |  23.305 μs |  1.65 |    0.06 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 | 1,074.31 μs |   480.055 μs |  26.313 μs |  1.61 |    0.07 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 | 1,062.16 μs |   173.715 μs |   9.522 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   894.70 μs |   166.868 μs |   9.147 μs |  0.84 |    0.02 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   881.11 μs |   129.092 μs |   7.076 μs |  0.83 |    0.01 |
|            |                 |           |               |       |                        |             |              |            |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 | 1,041.71 μs |   292.333 μs |  16.024 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   883.22 μs |   199.945 μs |  10.960 μs |  0.85 |    0.02 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   889.90 μs |   155.321 μs |   8.514 μs |  0.85 |    0.02 |
