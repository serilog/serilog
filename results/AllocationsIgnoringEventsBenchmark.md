``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.101
  [Host] : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
  core31 : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48  : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net50  : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT

Jit=RyuJit  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
|               Method |    Job |       Runtime |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |------- |-------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty | core31 | .NET Core 3.1 |  2.609 ns | 0.0562 ns | 0.0841 ns |  2.597 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |  2.067 ns | 0.0397 ns | 0.0594 ns |  2.065 ns |  0.79 |    0.04 |      - |     - |     - |         - |
|               LogMsg | core31 | .NET Core 3.1 |  3.641 ns | 0.0676 ns | 0.1011 ns |  3.645 ns |  1.40 |    0.06 |      - |     - |     - |         - |
|         LogMsgWithEx | core31 | .NET Core 3.1 |  3.760 ns | 0.2068 ns | 0.3095 ns |  3.761 ns |  1.44 |    0.12 |      - |     - |     - |         - |
|           LogScalar1 | core31 | .NET Core 3.1 |  6.279 ns | 0.1365 ns | 0.2043 ns |  6.245 ns |  2.41 |    0.10 |      - |     - |     - |         - |
|           LogScalar2 | core31 | .NET Core 3.1 | 11.005 ns | 0.1368 ns | 0.2047 ns | 11.051 ns |  4.22 |    0.17 |      - |     - |     - |         - |
|           LogScalar3 | core31 | .NET Core 3.1 | 14.499 ns | 0.3325 ns | 0.4976 ns | 14.467 ns |  5.56 |    0.18 |      - |     - |     - |         - |
|        LogScalarMany | core31 | .NET Core 3.1 | 18.167 ns | 0.3870 ns | 0.5792 ns | 18.126 ns |  6.97 |    0.33 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |  5.747 ns | 0.0572 ns | 0.0856 ns |  5.756 ns |  2.21 |    0.08 |      - |     - |     - |         - |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |  5.800 ns | 0.1862 ns | 0.2787 ns |  5.816 ns |  2.23 |    0.12 |      - |     - |     - |         - |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |  8.122 ns | 0.1371 ns | 0.2052 ns |  8.136 ns |  3.12 |    0.14 |      - |     - |     - |         - |
|  LogScalarStructMany | core31 | .NET Core 3.1 | 26.842 ns | 0.6517 ns | 0.9755 ns | 26.456 ns | 10.30 |    0.46 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 | 20.141 ns | 0.2557 ns | 0.3668 ns | 20.100 ns |  7.73 |    0.25 |      - |     - |     - |         - |
|        LogDictionary | core31 | .NET Core 3.1 |  8.894 ns | 0.1658 ns | 0.2482 ns |  8.886 ns |  3.41 |    0.13 | 0.0051 |     - |     - |      32 B |
|          LogSequence | core31 | .NET Core 3.1 |  9.058 ns | 0.1860 ns | 0.2785 ns |  8.996 ns |  3.48 |    0.16 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  9.149 ns | 0.1582 ns | 0.2319 ns |  9.148 ns |  3.51 |    0.14 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | core31 | .NET Core 3.1 | 10.902 ns | 0.1478 ns | 0.2166 ns | 10.916 ns |  4.19 |    0.15 |      - |     - |     - |         - |
|              LogMix3 | core31 | .NET Core 3.1 | 14.160 ns | 0.2640 ns | 0.3951 ns | 14.031 ns |  5.43 |    0.23 |      - |     - |     - |         - |
|              LogMix4 | core31 | .NET Core 3.1 | 26.501 ns | 0.1827 ns | 0.2734 ns | 26.521 ns | 10.17 |    0.36 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | core31 | .NET Core 3.1 | 30.667 ns | 0.5741 ns | 0.8593 ns | 30.680 ns | 11.77 |    0.51 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | core31 | .NET Core 3.1 | 57.594 ns | 1.0922 ns | 1.6347 ns | 56.990 ns | 22.10 |    0.92 | 0.0446 |     - |     - |     280 B |
|                      |        |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |  2.358 ns | 0.0414 ns | 0.0620 ns |  2.354 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |  2.357 ns | 0.0591 ns | 0.0884 ns |  2.350 ns |  1.00 |    0.04 |      - |     - |     - |         - |
|               LogMsg |  net48 |      .NET 4.8 |  3.935 ns | 0.0631 ns | 0.0944 ns |  3.945 ns |  1.67 |    0.03 |      - |     - |     - |         - |
|         LogMsgWithEx |  net48 |      .NET 4.8 |  3.957 ns | 0.0620 ns | 0.0928 ns |  3.943 ns |  1.68 |    0.06 |      - |     - |     - |         - |
|           LogScalar1 |  net48 |      .NET 4.8 |  7.331 ns | 0.1811 ns | 0.2711 ns |  7.310 ns |  3.11 |    0.14 |      - |     - |     - |         - |
|           LogScalar2 |  net48 |      .NET 4.8 | 13.802 ns | 0.1394 ns | 0.2044 ns | 13.818 ns |  5.86 |    0.16 |      - |     - |     - |         - |
|           LogScalar3 |  net48 |      .NET 4.8 | 17.212 ns | 0.5258 ns | 0.7870 ns | 17.270 ns |  7.30 |    0.40 |      - |     - |     - |         - |
|        LogScalarMany |  net48 |      .NET 4.8 | 17.451 ns | 0.2792 ns | 0.4178 ns | 17.341 ns |  7.41 |    0.29 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |  6.889 ns | 0.1148 ns | 0.1719 ns |  6.860 ns |  2.92 |    0.09 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |  6.973 ns | 0.1342 ns | 0.2008 ns |  6.948 ns |  2.96 |    0.12 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net48 |      .NET 4.8 | 10.772 ns | 0.1424 ns | 0.2131 ns | 10.859 ns |  4.57 |    0.14 |      - |     - |     - |         - |
|  LogScalarStructMany |  net48 |      .NET 4.8 | 24.731 ns | 0.2642 ns | 0.3873 ns | 24.759 ns | 10.49 |    0.34 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 | 22.605 ns | 0.4675 ns | 0.6997 ns | 22.643 ns |  9.59 |    0.41 |      - |     - |     - |         - |
|        LogDictionary |  net48 |      .NET 4.8 |  9.474 ns | 0.1371 ns | 0.2052 ns |  9.547 ns |  4.02 |    0.12 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net48 |      .NET 4.8 |  9.510 ns | 0.1584 ns | 0.2371 ns |  9.545 ns |  4.04 |    0.18 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  9.558 ns | 0.1914 ns | 0.2865 ns |  9.580 ns |  4.06 |    0.14 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net48 |      .NET 4.8 | 13.025 ns | 0.1618 ns | 0.2422 ns | 13.036 ns |  5.53 |    0.21 |      - |     - |     - |         - |
|              LogMix3 |  net48 |      .NET 4.8 | 17.251 ns | 0.2419 ns | 0.3620 ns | 17.248 ns |  7.32 |    0.23 |      - |     - |     - |         - |
|              LogMix4 |  net48 |      .NET 4.8 | 23.875 ns | 0.2891 ns | 0.4326 ns | 23.827 ns | 10.13 |    0.33 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net48 |      .NET 4.8 | 29.368 ns | 0.5314 ns | 0.7954 ns | 29.255 ns | 12.47 |    0.55 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net48 |      .NET 4.8 | 53.921 ns | 0.4333 ns | 0.6486 ns | 53.963 ns | 22.88 |    0.64 | 0.0446 |     - |     - |     281 B |
|                      |        |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |  2.212 ns | 0.0435 ns | 0.0652 ns |  2.221 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |  2.198 ns | 0.0253 ns | 0.0378 ns |  2.200 ns |  0.99 |    0.03 |      - |     - |     - |         - |
|               LogMsg |  net50 | .NET Core 5.0 |  3.683 ns | 0.0622 ns | 0.0932 ns |  3.689 ns |  1.67 |    0.06 |      - |     - |     - |         - |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |  4.042 ns | 0.0578 ns | 0.0866 ns |  4.050 ns |  1.83 |    0.06 |      - |     - |     - |         - |
|           LogScalar1 |  net50 | .NET Core 5.0 |  5.923 ns | 0.0800 ns | 0.1198 ns |  5.918 ns |  2.68 |    0.09 |      - |     - |     - |         - |
|           LogScalar2 |  net50 | .NET Core 5.0 |  6.629 ns | 0.1304 ns | 0.1951 ns |  6.613 ns |  3.00 |    0.14 |      - |     - |     - |         - |
|           LogScalar3 |  net50 | .NET Core 5.0 | 11.267 ns | 0.1283 ns | 0.1920 ns | 11.280 ns |  5.10 |    0.17 |      - |     - |     - |         - |
|        LogScalarMany |  net50 | .NET Core 5.0 | 12.502 ns | 0.1266 ns | 0.1895 ns | 12.495 ns |  5.66 |    0.19 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |  5.700 ns | 0.4439 ns | 0.6643 ns |  5.693 ns |  2.58 |    0.31 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |  5.408 ns | 0.0570 ns | 0.0836 ns |  5.421 ns |  2.44 |    0.09 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |  8.718 ns | 0.1292 ns | 0.1933 ns |  8.743 ns |  3.94 |    0.13 |      - |     - |     - |         - |
|  LogScalarStructMany |  net50 | .NET Core 5.0 | 21.288 ns | 0.2933 ns | 0.4390 ns | 21.405 ns |  9.63 |    0.27 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |  8.747 ns | 0.6631 ns | 0.9720 ns |  8.166 ns |  3.95 |    0.46 |      - |     - |     - |         - |
|        LogDictionary |  net50 | .NET Core 5.0 |  8.001 ns | 0.1193 ns | 0.1786 ns |  7.987 ns |  3.62 |    0.14 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net50 | .NET Core 5.0 |  8.026 ns | 0.1155 ns | 0.1728 ns |  8.005 ns |  3.63 |    0.12 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  8.055 ns | 0.1592 ns | 0.2383 ns |  8.077 ns |  3.64 |    0.17 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net50 | .NET Core 5.0 |  6.607 ns | 0.0989 ns | 0.1481 ns |  6.591 ns |  2.99 |    0.08 |      - |     - |     - |         - |
|              LogMix3 |  net50 | .NET Core 5.0 | 11.565 ns | 0.1654 ns | 0.2476 ns | 11.565 ns |  5.23 |    0.20 |      - |     - |     - |         - |
|              LogMix4 |  net50 | .NET Core 5.0 | 20.512 ns | 0.2943 ns | 0.4404 ns | 20.600 ns |  9.28 |    0.21 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net50 | .NET Core 5.0 | 23.408 ns | 0.6057 ns | 0.9065 ns | 23.242 ns | 10.59 |    0.56 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net50 | .NET Core 5.0 | 46.645 ns | 0.6072 ns | 0.9088 ns | 46.916 ns | 21.10 |    0.78 | 0.0446 |     - |     - |     280 B |
