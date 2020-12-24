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
| **Dictionary** | **core31** | **.NET Core 3.1** | **10000** |             **1** |                     **-1** |   **1.427 ms** |   **0.2923 ms** |  **0.0160 ms** |   **1.00** |    **0.00** |
|  Hashtable | core31 | .NET Core 3.1 | 10000 |             1 |                     -1 |   1.278 ms |   0.7576 ms |  0.0415 ms |   0.90 |    0.03 |
| Concurrent | core31 | .NET Core 3.1 | 10000 |             1 |                     -1 |  35.987 ms | 105.1066 ms |  5.7612 ms |  25.25 |    4.31 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net48 |      .NET 4.8 | 10000 |             1 |                     -1 |   3.042 ms |   1.7672 ms |  0.0969 ms |   1.00 |    0.00 |
|  Hashtable |  net48 |      .NET 4.8 | 10000 |             1 |                     -1 |  22.641 ms |   8.4922 ms |  0.4655 ms |   7.45 |    0.25 |
| Concurrent |  net48 |      .NET 4.8 | 10000 |             1 |                     -1 |  50.035 ms |  69.9564 ms |  3.8345 ms |  16.44 |    1.01 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net50 | .NET Core 5.0 | 10000 |             1 |                     -1 |   1.394 ms |   0.2889 ms |  0.0158 ms |   1.00 |    0.00 |
|  Hashtable |  net50 | .NET Core 5.0 | 10000 |             1 |                     -1 |   2.318 ms |   5.8258 ms |  0.3193 ms |   1.66 |    0.25 |
| Concurrent |  net50 | .NET Core 5.0 | 10000 |             1 |                     -1 |  26.578 ms |  54.8944 ms |  3.0089 ms |  19.07 |    2.26 |
|            |        |               |       |               |                        |            |             |            |        |         |
| **Dictionary** | **core31** | **.NET Core 3.1** | **10000** |             **1** |                      **1** |   **1.114 ms** |   **0.2084 ms** |  **0.0114 ms** |   **1.00** |    **0.00** |
|  Hashtable | core31 | .NET Core 3.1 | 10000 |             1 |                      1 |   2.147 ms |   0.2478 ms |  0.0136 ms |   1.93 |    0.02 |
| Concurrent | core31 | .NET Core 3.1 | 10000 |             1 |                      1 | 235.664 ms |  82.4663 ms |  4.5203 ms | 211.63 |    6.20 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net48 |      .NET 4.8 | 10000 |             1 |                      1 |   1.865 ms |   0.2926 ms |  0.0160 ms |   1.00 |    0.00 |
|  Hashtable |  net48 |      .NET 4.8 | 10000 |             1 |                      1 |   1.889 ms |   0.3066 ms |  0.0168 ms |   1.01 |    0.00 |
| Concurrent |  net48 |      .NET 4.8 | 10000 |             1 |                      1 | 244.191 ms |  23.6275 ms |  1.2951 ms | 130.95 |    1.74 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net50 | .NET Core 5.0 | 10000 |             1 |                      1 |   1.039 ms |   0.2150 ms |  0.0118 ms |   1.00 |    0.00 |
|  Hashtable |  net50 | .NET Core 5.0 | 10000 |             1 |                      1 |   2.157 ms |   0.4701 ms |  0.0258 ms |   2.08 |    0.04 |
| Concurrent |  net50 | .NET Core 5.0 | 10000 |             1 |                      1 | 216.635 ms |  48.6302 ms |  2.6656 ms | 208.57 |    3.47 |
|            |        |               |       |               |                        |            |             |            |        |         |
| **Dictionary** | **core31** | **.NET Core 3.1** | **10000** |            **10** |                     **-1** |   **1.464 ms** |   **0.1275 ms** |  **0.0070 ms** |   **1.00** |    **0.00** |
|  Hashtable | core31 | .NET Core 3.1 | 10000 |            10 |                     -1 |   1.529 ms |   6.4864 ms |  0.3555 ms |   1.04 |    0.25 |
| Concurrent | core31 | .NET Core 3.1 | 10000 |            10 |                     -1 |  62.473 ms |  77.8192 ms |  4.2655 ms |  42.66 |    2.90 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net48 |      .NET 4.8 | 10000 |            10 |                     -1 |   3.078 ms |   2.4325 ms |  0.1333 ms |   1.00 |    0.00 |
|  Hashtable |  net48 |      .NET 4.8 | 10000 |            10 |                     -1 |  23.808 ms |  42.2198 ms |  2.3142 ms |   7.73 |    0.55 |
| Concurrent |  net48 |      .NET 4.8 | 10000 |            10 |                     -1 |  52.206 ms |  63.1982 ms |  3.4641 ms |  17.00 |    1.62 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net50 | .NET Core 5.0 | 10000 |            10 |                     -1 |   1.428 ms |   0.5674 ms |  0.0311 ms |   1.00 |    0.00 |
|  Hashtable |  net50 | .NET Core 5.0 | 10000 |            10 |                     -1 |   1.847 ms |  10.8219 ms |  0.5932 ms |   1.30 |    0.43 |
| Concurrent |  net50 | .NET Core 5.0 | 10000 |            10 |                     -1 |  49.961 ms |  55.6109 ms |  3.0482 ms |  34.97 |    1.71 |
|            |        |               |       |               |                        |            |             |            |        |         |
| **Dictionary** | **core31** | **.NET Core 3.1** | **10000** |            **10** |                      **1** |   **1.093 ms** |   **0.1118 ms** |  **0.0061 ms** |   **1.00** |    **0.00** |
|  Hashtable | core31 | .NET Core 3.1 | 10000 |            10 |                      1 |   2.136 ms |   0.3149 ms |  0.0173 ms |   1.96 |    0.02 |
| Concurrent | core31 | .NET Core 3.1 | 10000 |            10 |                      1 | 231.644 ms |  42.0648 ms |  2.3057 ms | 212.03 |    1.52 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net48 |      .NET 4.8 | 10000 |            10 |                      1 |   1.857 ms |   0.4685 ms |  0.0257 ms |   1.00 |    0.00 |
|  Hashtable |  net48 |      .NET 4.8 | 10000 |            10 |                      1 |   1.835 ms |   0.4023 ms |  0.0221 ms |   0.99 |    0.01 |
| Concurrent |  net48 |      .NET 4.8 | 10000 |            10 |                      1 | 245.797 ms |  28.4682 ms |  1.5604 ms | 132.39 |    1.31 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net50 | .NET Core 5.0 | 10000 |            10 |                      1 |   1.040 ms |   0.2280 ms |  0.0125 ms |   1.00 |    0.00 |
|  Hashtable |  net50 | .NET Core 5.0 | 10000 |            10 |                      1 |   2.152 ms |   0.5049 ms |  0.0277 ms |   2.07 |    0.05 |
| Concurrent |  net50 | .NET Core 5.0 | 10000 |            10 |                      1 | 213.414 ms |  36.3319 ms |  1.9915 ms | 205.21 |    4.10 |
|            |        |               |       |               |                        |            |             |            |        |         |
| **Dictionary** | **core31** | **.NET Core 3.1** | **10000** |           **100** |                     **-1** |   **1.611 ms** |   **0.1641 ms** |  **0.0090 ms** |   **1.00** |    **0.00** |
|  Hashtable | core31 | .NET Core 3.1 | 10000 |           100 |                     -1 |   3.566 ms |   5.8344 ms |  0.3198 ms |   2.21 |    0.19 |
| Concurrent | core31 | .NET Core 3.1 | 10000 |           100 |                     -1 |  92.803 ms | 253.0665 ms | 13.8714 ms |  57.62 |    8.75 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net48 |      .NET 4.8 | 10000 |           100 |                     -1 |   3.103 ms |   2.6352 ms |  0.1444 ms |   1.00 |    0.00 |
|  Hashtable |  net48 |      .NET 4.8 | 10000 |           100 |                     -1 |  23.892 ms |  26.0582 ms |  1.4283 ms |   7.71 |    0.57 |
| Concurrent |  net48 |      .NET 4.8 | 10000 |           100 |                     -1 |  75.313 ms |  34.5621 ms |  1.8945 ms |  24.30 |    1.04 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net50 | .NET Core 5.0 | 10000 |           100 |                     -1 |   1.509 ms |   0.8963 ms |  0.0491 ms |   1.00 |    0.00 |
|  Hashtable |  net50 | .NET Core 5.0 | 10000 |           100 |                     -1 |   2.699 ms |   3.0563 ms |  0.1675 ms |   1.79 |    0.11 |
| Concurrent |  net50 | .NET Core 5.0 | 10000 |           100 |                     -1 |  86.563 ms |  86.3445 ms |  4.7328 ms |  57.37 |    2.15 |
|            |        |               |       |               |                        |            |             |            |        |         |
| **Dictionary** | **core31** | **.NET Core 3.1** | **10000** |           **100** |                      **1** |   **1.106 ms** |   **0.2796 ms** |  **0.0153 ms** |   **1.00** |    **0.00** |
|  Hashtable | core31 | .NET Core 3.1 | 10000 |           100 |                      1 |   2.154 ms |   0.1773 ms |  0.0097 ms |   1.95 |    0.03 |
| Concurrent | core31 | .NET Core 3.1 | 10000 |           100 |                      1 | 239.580 ms |  48.9055 ms |  2.6807 ms | 216.72 |    1.32 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net48 |      .NET 4.8 | 10000 |           100 |                      1 |   1.849 ms |   0.1499 ms |  0.0082 ms |   1.00 |    0.00 |
|  Hashtable |  net48 |      .NET 4.8 | 10000 |           100 |                      1 |   1.837 ms |   0.4870 ms |  0.0267 ms |   0.99 |    0.01 |
| Concurrent |  net48 |      .NET 4.8 | 10000 |           100 |                      1 | 245.628 ms |  62.3538 ms |  3.4178 ms | 132.82 |    2.36 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net50 | .NET Core 5.0 | 10000 |           100 |                      1 |   1.025 ms |   0.2294 ms |  0.0126 ms |   1.00 |    0.00 |
|  Hashtable |  net50 | .NET Core 5.0 | 10000 |           100 |                      1 |   2.141 ms |   0.4778 ms |  0.0262 ms |   2.09 |    0.05 |
| Concurrent |  net50 | .NET Core 5.0 | 10000 |           100 |                      1 | 221.247 ms |   3.3753 ms |  0.1850 ms | 215.97 |    2.49 |
|            |        |               |       |               |                        |            |             |            |        |         |
| **Dictionary** | **core31** | **.NET Core 3.1** | **10000** |          **1000** |                     **-1** |   **1.743 ms** |   **0.5850 ms** |  **0.0321 ms** |   **1.00** |    **0.00** |
|  Hashtable | core31 | .NET Core 3.1 | 10000 |          1000 |                     -1 |   3.422 ms |   2.7965 ms |  0.1533 ms |   1.96 |    0.11 |
| Concurrent | core31 | .NET Core 3.1 | 10000 |          1000 |                     -1 | 166.429 ms | 974.0106 ms | 53.3888 ms |  95.46 |   30.71 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net48 |      .NET 4.8 | 10000 |          1000 |                     -1 |   3.375 ms |   0.8694 ms |  0.0477 ms |   1.00 |    0.00 |
|  Hashtable |  net48 |      .NET 4.8 | 10000 |          1000 |                     -1 |  17.698 ms |   3.2653 ms |  0.1790 ms |   5.24 |    0.04 |
| Concurrent |  net48 |      .NET 4.8 | 10000 |          1000 |                     -1 | 158.938 ms | 463.8245 ms | 25.4238 ms |  47.14 |    7.94 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net50 | .NET Core 5.0 | 10000 |          1000 |                     -1 |   1.622 ms |   1.0506 ms |  0.0576 ms |   1.00 |    0.00 |
|  Hashtable |  net50 | .NET Core 5.0 | 10000 |          1000 |                     -1 |   3.323 ms |   4.1822 ms |  0.2292 ms |   2.05 |    0.13 |
| Concurrent |  net50 | .NET Core 5.0 | 10000 |          1000 |                     -1 | 158.921 ms | 136.8339 ms |  7.5003 ms |  98.14 |    7.76 |
|            |        |               |       |               |                        |            |             |            |        |         |
| **Dictionary** | **core31** | **.NET Core 3.1** | **10000** |          **1000** |                      **1** |   **1.116 ms** |   **0.0572 ms** |  **0.0031 ms** |   **1.00** |    **0.00** |
|  Hashtable | core31 | .NET Core 3.1 | 10000 |          1000 |                      1 |   2.173 ms |   0.6109 ms |  0.0335 ms |   1.95 |    0.04 |
| Concurrent | core31 | .NET Core 3.1 | 10000 |          1000 |                      1 | 234.214 ms |  48.4308 ms |  2.6547 ms | 209.93 |    1.90 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net48 |      .NET 4.8 | 10000 |          1000 |                      1 |   1.851 ms |   0.2239 ms |  0.0123 ms |   1.00 |    0.00 |
|  Hashtable |  net48 |      .NET 4.8 | 10000 |          1000 |                      1 |   1.849 ms |   0.3203 ms |  0.0176 ms |   1.00 |    0.00 |
| Concurrent |  net48 |      .NET 4.8 | 10000 |          1000 |                      1 | 247.235 ms |  50.6764 ms |  2.7777 ms | 133.59 |    1.27 |
|            |        |               |       |               |                        |            |             |            |        |         |
| Dictionary |  net50 | .NET Core 5.0 | 10000 |          1000 |                      1 |   1.033 ms |   0.2237 ms |  0.0123 ms |   1.00 |    0.00 |
|  Hashtable |  net50 | .NET Core 5.0 | 10000 |          1000 |                      1 |   2.171 ms |   0.9166 ms |  0.0502 ms |   2.10 |    0.05 |
| Concurrent |  net50 | .NET Core 5.0 | 10000 |          1000 |                      1 | 213.748 ms |  40.4521 ms |  2.2173 ms | 206.88 |    1.31 |
