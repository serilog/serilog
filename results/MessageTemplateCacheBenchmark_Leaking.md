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
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |             **1** |                     **-1** |   **1.543 ms** |   **0.0544 ms** |  **0.0030 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                     -1 |   2.266 ms |   2.3103 ms |  0.1266 ms |   1.47 |    0.08 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                     -1 |  35.327 ms | 299.4386 ms | 16.4132 ms |  22.91 |   10.69 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                     -1 |   3.334 ms |   1.9295 ms |  0.1058 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                     -1 |  20.358 ms |  25.0568 ms |  1.3734 ms |   6.10 |    0.26 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                     -1 |  52.351 ms | 134.7281 ms |  7.3849 ms |  15.72 |    2.38 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                     -1 |   3.491 ms |   2.7220 ms |  0.1492 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                     -1 |  22.308 ms |   1.7438 ms |  0.0956 ms |   6.40 |    0.30 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                     -1 |  54.203 ms |  66.5455 ms |  3.6476 ms |  15.58 |    1.72 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |             **1** |                      **1** |   **1.119 ms** |   **0.6358 ms** |  **0.0348 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                      1 |   2.155 ms |   0.8741 ms |  0.0479 ms |   1.93 |    0.03 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |             1 |                      1 | 239.495 ms |  89.3057 ms |  4.8951 ms | 214.25 |    7.63 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.816 ms |   0.2717 ms |  0.0149 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.890 ms |   0.3872 ms |  0.0212 ms |   1.04 |    0.02 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |             1 |                      1 | 247.537 ms |  77.5413 ms |  4.2503 ms | 136.29 |    2.09 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.835 ms |   0.1204 ms |  0.0066 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                      1 |   1.833 ms |   0.5148 ms |  0.0282 ms |   1.00 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |             1 |                      1 | 243.266 ms |  73.9484 ms |  4.0534 ms | 132.59 |    1.79 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |            **10** |                     **-1** |   **1.572 ms** |   **0.4713 ms** |  **0.0258 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                     -1 |   2.811 ms |   1.2077 ms |  0.0662 ms |   1.79 |    0.03 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                     -1 |  69.259 ms |  18.8962 ms |  1.0358 ms |  44.06 |    0.74 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                     -1 |   3.705 ms |   2.8021 ms |  0.1536 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                     -1 |  20.721 ms |  31.0023 ms |  1.6993 ms |   5.59 |    0.35 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                     -1 |  54.406 ms |  79.6503 ms |  4.3659 ms |  14.67 |    0.59 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                     -1 |   3.466 ms |   1.4852 ms |  0.0814 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                     -1 |  21.521 ms |  24.7104 ms |  1.3545 ms |   6.22 |    0.48 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                     -1 |  55.000 ms |  31.7611 ms |  1.7409 ms |  15.88 |    0.74 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |            **10** |                      **1** |   **1.094 ms** |   **0.2528 ms** |  **0.0139 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                      1 |   2.153 ms |   1.2740 ms |  0.0698 ms |   1.97 |    0.08 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |            10 |                      1 | 237.562 ms | 137.0204 ms |  7.5105 ms | 217.29 |    9.55 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.826 ms |   0.1100 ms |  0.0060 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.846 ms |   0.3162 ms |  0.0173 ms |   1.01 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |            10 |                      1 | 252.322 ms |  33.0128 ms |  1.8095 ms | 138.21 |    0.54 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.835 ms |   0.3583 ms |  0.0196 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                      1 |   1.905 ms |   0.2955 ms |  0.0162 ms |   1.04 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |            10 |                      1 | 254.330 ms |  98.0443 ms |  5.3741 ms | 138.62 |    4.42 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |           **100** |                     **-1** |   **1.691 ms** |   **0.5777 ms** |  **0.0317 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                     -1 |   2.951 ms |   5.6761 ms |  0.3111 ms |   1.75 |    0.18 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                     -1 | 121.133 ms | 536.9462 ms | 29.4318 ms |  71.46 |   15.99 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                     -1 |   3.788 ms |   2.2692 ms |  0.1244 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                     -1 |  19.552 ms |  14.0603 ms |  0.7707 ms |   5.16 |    0.11 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                     -1 |  83.739 ms |  58.9573 ms |  3.2316 ms |  22.12 |    0.99 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                     -1 |   3.793 ms |   3.0114 ms |  0.1651 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                     -1 |  21.316 ms |  49.7330 ms |  2.7260 ms |   5.63 |    0.78 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                     -1 |  74.330 ms | 214.7541 ms | 11.7714 ms |  19.70 |    3.90 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |           **100** |                      **1** |   **1.102 ms** |   **0.3347 ms** |  **0.0183 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                      1 |   2.148 ms |   1.1524 ms |  0.0632 ms |   1.95 |    0.06 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |           100 |                      1 | 242.852 ms |  60.9095 ms |  3.3387 ms | 220.42 |    2.31 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.840 ms |   0.0391 ms |  0.0021 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.849 ms |   0.2829 ms |  0.0155 ms |   1.00 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |           100 |                      1 | 242.233 ms |  83.9313 ms |  4.6006 ms | 131.66 |    2.36 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.850 ms |   0.3153 ms |  0.0173 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                      1 |   1.843 ms |   0.6068 ms |  0.0333 ms |   1.00 |    0.02 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |           100 |                      1 | 249.089 ms | 115.8221 ms |  6.3486 ms | 134.64 |    2.38 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |          **1000** |                     **-1** |   **1.787 ms** |   **0.1381 ms** |  **0.0076 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                     -1 |   3.445 ms |   2.4143 ms |  0.1323 ms |   1.93 |    0.07 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                     -1 | 136.655 ms | 934.4018 ms | 51.2177 ms |  76.40 |   28.43 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                     -1 |   3.672 ms |   0.4399 ms |  0.0241 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                     -1 |  16.640 ms |   1.9084 ms |  0.1046 ms |   4.53 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                     -1 | 164.563 ms | 383.5333 ms | 21.0228 ms |  44.84 |    6.02 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                     -1 |   3.790 ms |   0.6261 ms |  0.0343 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                     -1 |  17.018 ms |  11.3135 ms |  0.6201 ms |   4.49 |    0.15 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                     -1 | 148.381 ms | 480.9255 ms | 26.3611 ms |  39.13 |    6.70 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** | **10000** |          **1000** |                      **1** |   **1.094 ms** |   **0.4422 ms** |  **0.0242 ms** |   **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                      1 |   2.164 ms |   0.9640 ms |  0.0528 ms |   1.98 |    0.08 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10000 |          1000 |                      1 | 244.341 ms |  12.2932 ms |  0.6738 ms | 223.34 |    5.19 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.879 ms |   1.0291 ms |  0.0564 ms |   1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.833 ms |   0.2422 ms |  0.0133 ms |   0.98 |    0.02 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10000 |          1000 |                      1 | 250.442 ms |  58.9846 ms |  3.2331 ms | 133.36 |    3.61 |
|            |                 |           |               |       |               |                        |            |             |            |        |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.843 ms |   0.2616 ms |  0.0143 ms |   1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                      1 |   1.817 ms |   0.2093 ms |  0.0115 ms |   0.99 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10000 |          1000 |                      1 | 245.920 ms |  90.7956 ms |  4.9768 ms | 133.45 |    3.43 |
