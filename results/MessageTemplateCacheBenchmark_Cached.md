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
|     Method |             Job |       Jit |       Runtime | Items | MaxDegreeOfParallelism |        Mean |        Error |    StdDev | Ratio | RatioSD |
|----------- |---------------- |---------- |-------------- |------ |----------------------- |------------:|-------------:|----------:|------:|--------:|
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                     **-1** |   **230.44 μs** |    **44.538 μs** |  **2.441 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    24.70 μs |     7.433 μs |  0.407 μs |  0.11 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                     -1 |    26.49 μs |    10.713 μs |  0.587 μs |  0.11 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |   510.30 μs |   193.028 μs | 10.581 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    37.23 μs |    12.443 μs |  0.682 μs |  0.07 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                     -1 |    36.70 μs |     7.575 μs |  0.415 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |   518.05 μs |   338.299 μs | 18.543 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    35.70 μs |    18.752 μs |  1.028 μs |  0.07 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                     -1 |    37.34 μs |    10.088 μs |  0.553 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **10** |                      **1** |    **66.70 μs** |    **10.938 μs** |  **0.600 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |   110.00 μs |    42.144 μs |  2.310 μs |  1.65 |    0.04 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    10 |                      1 |   103.87 μs |    11.280 μs |  0.618 μs |  1.56 |    0.01 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |   103.00 μs |    35.086 μs |  1.923 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    92.83 μs |    10.331 μs |  0.566 μs |  0.90 |    0.02 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    10 |                      1 |    85.20 μs |    15.902 μs |  0.872 μs |  0.83 |    0.01 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |   106.81 μs |     6.541 μs |  0.359 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |    96.44 μs |     5.949 μs |  0.326 μs |  0.90 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    10 |                      1 |   105.40 μs |    21.503 μs |  1.179 μs |  0.99 |    0.01 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                     **-1** |   **312.93 μs** |   **132.835 μs** |  **7.281 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    43.37 μs |     7.740 μs |  0.424 μs |  0.14 |    0.00 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                     -1 |    40.71 μs |    51.466 μs |  2.821 μs |  0.13 |    0.01 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |   777.77 μs |   206.373 μs | 11.312 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    52.65 μs |    30.412 μs |  1.667 μs |  0.07 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                     -1 |    54.70 μs |    12.102 μs |  0.663 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |   751.52 μs |   416.609 μs | 22.836 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    53.38 μs |    23.562 μs |  1.291 μs |  0.07 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                     -1 |    54.93 μs |    10.007 μs |  0.549 μs |  0.07 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **20** |                      **1** |   **126.62 μs** |    **17.226 μs** |  **0.944 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   217.47 μs |    13.835 μs |  0.758 μs |  1.72 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    20 |                      1 |   208.05 μs |    25.572 μs |  1.402 μs |  1.64 |    0.02 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   212.43 μs |    19.865 μs |  1.089 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   164.64 μs |     6.763 μs |  0.371 μs |  0.78 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    20 |                      1 |   167.02 μs |     8.572 μs |  0.470 μs |  0.79 |    0.01 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   207.35 μs |    33.795 μs |  1.852 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   188.88 μs |    37.552 μs |  2.058 μs |  0.91 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    20 |                      1 |   170.65 μs |    21.006 μs |  1.151 μs |  0.82 |    0.01 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                     **-1** |   **601.28 μs** |   **492.367 μs** | **26.988 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |    93.02 μs |    41.545 μs |  2.277 μs |  0.15 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                     -1 |    97.95 μs |     8.118 μs |  0.445 μs |  0.16 |    0.01 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 | 1,195.82 μs |   325.791 μs | 17.858 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |   107.29 μs |    50.853 μs |  2.787 μs |  0.09 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                     -1 |   104.24 μs |    29.436 μs |  1.613 μs |  0.09 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 | 1,182.02 μs |   708.086 μs | 38.813 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   106.61 μs |    54.095 μs |  2.965 μs |  0.09 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                     -1 |   106.45 μs |    25.011 μs |  1.371 μs |  0.09 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |    **50** |                      **1** |   **337.57 μs** |    **61.616 μs** |  **3.377 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   559.02 μs |   111.862 μs |  6.132 μs |  1.66 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    50 |                      1 |   519.97 μs |   132.191 μs |  7.246 μs |  1.54 |    0.04 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   515.75 μs |    93.151 μs |  5.106 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   438.67 μs |    40.198 μs |  2.203 μs |  0.85 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |    50 |                      1 |   437.25 μs |    52.740 μs |  2.891 μs |  0.85 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   516.41 μs |    88.355 μs |  4.843 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   440.56 μs |    61.185 μs |  3.354 μs |  0.85 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |    50 |                      1 |   428.60 μs |    26.832 μs |  1.471 μs |  0.83 |    0.01 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                     **-1** | **1,049.10 μs** |   **477.774 μs** | **26.188 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   155.44 μs |    84.962 μs |  4.657 μs |  0.15 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                     -1 |   181.59 μs |   114.401 μs |  6.271 μs |  0.17 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 | 1,928.08 μs | 1,474.680 μs | 80.832 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   190.42 μs |    57.592 μs |  3.157 μs |  0.10 |    0.00 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                     -1 |   193.55 μs |    44.316 μs |  2.429 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 | 1,956.99 μs |   566.353 μs | 31.044 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   189.13 μs |    28.671 μs |  1.572 μs |  0.10 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                     -1 |   194.01 μs |    67.308 μs |  3.689 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |   **100** |                      **1** |   **641.81 μs** |   **179.515 μs** |  **9.840 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 | 1,083.48 μs |   171.583 μs |  9.405 μs |  1.69 |    0.03 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   100 |                      1 | 1,046.45 μs |   210.754 μs | 11.552 μs |  1.63 |    0.03 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 | 1,023.48 μs |   160.434 μs |  8.794 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   863.13 μs |   104.934 μs |  5.752 μs |  0.84 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |   100 |                      1 |   870.42 μs |   140.841 μs |  7.720 μs |  0.85 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 | 1,043.19 μs |   205.624 μs | 11.271 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   885.25 μs |    79.526 μs |  4.359 μs |  0.85 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |   100 |                      1 |   892.00 μs |   114.552 μs |  6.279 μs |  0.86 |    0.01 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                     **-1** | **1,071.82 μs** |    **69.165 μs** |  **3.791 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   162.73 μs |   114.398 μs |  6.271 μs |  0.15 |    0.01 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                     -1 |   163.93 μs |    38.068 μs |  2.087 μs |  0.15 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 | 2,007.38 μs | 1,401.715 μs | 76.833 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   206.63 μs |    63.613 μs |  3.487 μs |  0.10 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                     -1 |   206.13 μs |    88.541 μs |  4.853 μs |  0.10 |    0.01 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 | 2,085.50 μs | 1,815.687 μs | 99.524 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   197.25 μs |   159.220 μs |  8.727 μs |  0.09 |    0.01 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                     -1 |   200.60 μs |    43.752 μs |  2.398 μs |  0.10 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| **Dictionary** |   **core31 RyuJit** |    **RyuJit** | **.NET Core 3.1** |  **1000** |                      **1** |   **673.91 μs** |    **98.043 μs** |  **5.374 μs** |  **1.00** |    **0.00** |
|  Hashtable |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 | 1,092.44 μs |   253.628 μs | 13.902 μs |  1.62 |    0.02 |
| Concurrent |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1000 |                      1 | 1,069.07 μs |   209.331 μs | 11.474 μs |  1.59 |    0.03 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 | 1,039.79 μs |    70.080 μs |  3.841 μs |  1.00 |    0.00 |
|  Hashtable | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   892.94 μs |    71.230 μs |  3.904 μs |  0.86 |    0.01 |
| Concurrent | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1000 |                      1 |   970.66 μs |    65.059 μs |  3.566 μs |  0.93 |    0.00 |
|            |                 |           |               |       |                        |             |              |           |       |         |
| Dictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 | 1,040.40 μs |    70.331 μs |  3.855 μs |  1.00 |    0.00 |
|  Hashtable |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   894.29 μs |    75.795 μs |  4.155 μs |  0.86 |    0.00 |
| Concurrent |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1000 |                      1 |   882.06 μs |    58.611 μs |  3.213 μs |  0.85 |    0.01 |
