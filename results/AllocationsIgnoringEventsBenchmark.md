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
|             LogEmpty | core31 | .NET Core 3.1 |  2.284 ns | 0.0159 ns | 0.0232 ns |  2.286 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |  2.285 ns | 0.0175 ns | 0.0256 ns |  2.286 ns |  1.00 |    0.01 |      - |     - |     - |         - |
|               LogMsg | core31 | .NET Core 3.1 |  3.754 ns | 0.0309 ns | 0.0443 ns |  3.763 ns |  1.64 |    0.03 |      - |     - |     - |         - |
|         LogMsgWithEx | core31 | .NET Core 3.1 |  4.173 ns | 0.0334 ns | 0.0499 ns |  4.178 ns |  1.83 |    0.03 |      - |     - |     - |         - |
|           LogScalar1 | core31 | .NET Core 3.1 |  6.104 ns | 0.0622 ns | 0.0892 ns |  6.110 ns |  2.67 |    0.05 |      - |     - |     - |         - |
|           LogScalar2 | core31 | .NET Core 3.1 | 12.406 ns | 0.2633 ns | 0.3941 ns | 12.412 ns |  5.43 |    0.18 |      - |     - |     - |         - |
|           LogScalar3 | core31 | .NET Core 3.1 | 15.257 ns | 0.3447 ns | 0.5160 ns | 15.324 ns |  6.67 |    0.23 |      - |     - |     - |         - |
|        LogScalarMany | core31 | .NET Core 3.1 | 19.787 ns | 0.2532 ns | 0.3790 ns | 19.750 ns |  8.67 |    0.19 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |  5.280 ns | 0.0596 ns | 0.0892 ns |  5.286 ns |  2.31 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |  5.862 ns | 0.0605 ns | 0.0905 ns |  5.851 ns |  2.57 |    0.05 |      - |     - |     - |         - |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |  8.511 ns | 0.0743 ns | 0.1112 ns |  8.492 ns |  3.73 |    0.06 |      - |     - |     - |         - |
|  LogScalarStructMany | core31 | .NET Core 3.1 | 27.673 ns | 0.1784 ns | 0.2670 ns | 27.689 ns | 12.12 |    0.14 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 | 21.421 ns | 0.3964 ns | 0.5933 ns | 21.381 ns |  9.37 |    0.28 |      - |     - |     - |         - |
|        LogDictionary | core31 | .NET Core 3.1 |  9.409 ns | 0.1269 ns | 0.1899 ns |  9.407 ns |  4.13 |    0.09 | 0.0051 |     - |     - |      32 B |
|          LogSequence | core31 | .NET Core 3.1 |  9.593 ns | 0.1040 ns | 0.1557 ns |  9.607 ns |  4.20 |    0.08 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  9.879 ns | 0.1714 ns | 0.2566 ns |  9.901 ns |  4.33 |    0.13 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | core31 | .NET Core 3.1 | 11.602 ns | 0.2507 ns | 0.3753 ns | 11.627 ns |  5.08 |    0.17 |      - |     - |     - |         - |
|              LogMix3 | core31 | .NET Core 3.1 | 15.086 ns | 0.3264 ns | 0.4785 ns | 15.014 ns |  6.61 |    0.22 |      - |     - |     - |         - |
|              LogMix4 | core31 | .NET Core 3.1 | 27.310 ns | 0.1608 ns | 0.2407 ns | 27.320 ns | 11.96 |    0.18 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | core31 | .NET Core 3.1 | 31.976 ns | 0.3646 ns | 0.5458 ns | 31.832 ns | 14.01 |    0.31 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | core31 | .NET Core 3.1 | 58.781 ns | 0.6055 ns | 0.9062 ns | 58.661 ns | 25.75 |    0.49 | 0.0446 |     - |     - |     280 B |
|                      |        |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |  2.433 ns | 0.0288 ns | 0.0431 ns |  2.436 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |  2.429 ns | 0.0309 ns | 0.0462 ns |  2.415 ns |  1.00 |    0.02 |      - |     - |     - |         - |
|               LogMsg |  net48 |      .NET 4.8 |  4.081 ns | 0.0373 ns | 0.0547 ns |  4.091 ns |  1.68 |    0.04 |      - |     - |     - |         - |
|         LogMsgWithEx |  net48 |      .NET 4.8 |  4.096 ns | 0.0275 ns | 0.0412 ns |  4.099 ns |  1.68 |    0.04 |      - |     - |     - |         - |
|           LogScalar1 |  net48 |      .NET 4.8 |  7.537 ns | 0.1075 ns | 0.1609 ns |  7.565 ns |  3.10 |    0.09 |      - |     - |     - |         - |
|           LogScalar2 |  net48 |      .NET 4.8 | 13.132 ns | 0.1430 ns | 0.2140 ns | 13.080 ns |  5.40 |    0.16 |      - |     - |     - |         - |
|           LogScalar3 |  net48 |      .NET 4.8 | 17.018 ns | 0.1258 ns | 0.1845 ns | 16.998 ns |  6.99 |    0.14 |      - |     - |     - |         - |
|        LogScalarMany |  net48 |      .NET 4.8 | 17.618 ns | 0.2208 ns | 0.3305 ns | 17.438 ns |  7.24 |    0.18 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |  7.159 ns | 0.1033 ns | 0.1546 ns |  7.147 ns |  2.94 |    0.08 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |  7.246 ns | 0.0844 ns | 0.1263 ns |  7.215 ns |  2.98 |    0.07 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net48 |      .NET 4.8 | 11.134 ns | 0.0811 ns | 0.1214 ns | 11.167 ns |  4.58 |    0.10 |      - |     - |     - |         - |
|  LogScalarStructMany |  net48 |      .NET 4.8 | 25.464 ns | 0.1546 ns | 0.2315 ns | 25.543 ns | 10.47 |    0.17 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 | 26.077 ns | 2.3277 ns | 3.4840 ns | 26.162 ns | 10.72 |    1.46 |      - |     - |     - |         - |
|        LogDictionary |  net48 |      .NET 4.8 |  9.476 ns | 0.0861 ns | 0.1288 ns |  9.488 ns |  3.90 |    0.06 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net48 |      .NET 4.8 |  9.586 ns | 0.1172 ns | 0.1754 ns |  9.614 ns |  3.94 |    0.10 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  9.586 ns | 0.0880 ns | 0.1317 ns |  9.604 ns |  3.94 |    0.10 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net48 |      .NET 4.8 | 14.191 ns | 0.4208 ns | 0.6299 ns | 14.130 ns |  5.84 |    0.28 |      - |     - |     - |         - |
|              LogMix3 |  net48 |      .NET 4.8 | 17.734 ns | 0.6176 ns | 0.9243 ns | 17.733 ns |  7.29 |    0.41 |      - |     - |     - |         - |
|              LogMix4 |  net48 |      .NET 4.8 | 24.184 ns | 0.1539 ns | 0.2257 ns | 24.267 ns |  9.94 |    0.20 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net48 |      .NET 4.8 | 29.919 ns | 0.3298 ns | 0.4937 ns | 29.943 ns | 12.31 |    0.39 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net48 |      .NET 4.8 | 55.664 ns | 0.2670 ns | 0.3996 ns | 55.728 ns | 22.89 |    0.42 | 0.0446 |     - |     - |     281 B |
|                      |        |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |  2.278 ns | 0.0237 ns | 0.0355 ns |  2.276 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |  2.279 ns | 0.0240 ns | 0.0359 ns |  2.281 ns |  1.00 |    0.03 |      - |     - |     - |         - |
|               LogMsg |  net50 | .NET Core 5.0 |  3.179 ns | 0.0360 ns | 0.0539 ns |  3.184 ns |  1.40 |    0.03 |      - |     - |     - |         - |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |  3.413 ns | 0.0411 ns | 0.0589 ns |  3.423 ns |  1.50 |    0.04 |      - |     - |     - |         - |
|           LogScalar1 |  net50 | .NET Core 5.0 |  6.858 ns | 0.0890 ns | 0.1304 ns |  6.867 ns |  3.01 |    0.06 |      - |     - |     - |         - |
|           LogScalar2 |  net50 | .NET Core 5.0 |  7.325 ns | 0.1864 ns | 0.2790 ns |  7.340 ns |  3.22 |    0.13 |      - |     - |     - |         - |
|           LogScalar3 |  net50 | .NET Core 5.0 | 12.980 ns | 0.3131 ns | 0.4687 ns | 12.971 ns |  5.70 |    0.22 |      - |     - |     - |         - |
|        LogScalarMany |  net50 | .NET Core 5.0 | 12.967 ns | 0.2523 ns | 0.3777 ns | 12.967 ns |  5.69 |    0.20 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |  5.380 ns | 0.0356 ns | 0.0533 ns |  5.397 ns |  2.36 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |  5.610 ns | 0.0328 ns | 0.0492 ns |  5.613 ns |  2.46 |    0.05 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |  9.099 ns | 0.0864 ns | 0.1293 ns |  9.087 ns |  3.99 |    0.08 |      - |     - |     - |         - |
|  LogScalarStructMany |  net50 | .NET Core 5.0 | 22.068 ns | 0.2200 ns | 0.3225 ns | 22.081 ns |  9.70 |    0.16 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |  8.084 ns | 0.0709 ns | 0.1061 ns |  8.099 ns |  3.55 |    0.07 |      - |     - |     - |         - |
|        LogDictionary |  net50 | .NET Core 5.0 |  7.863 ns | 0.1745 ns | 0.2612 ns |  7.893 ns |  3.45 |    0.12 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net50 | .NET Core 5.0 |  8.199 ns | 0.1219 ns | 0.1825 ns |  8.160 ns |  3.60 |    0.10 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  8.325 ns | 0.0770 ns | 0.1153 ns |  8.336 ns |  3.66 |    0.08 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net50 | .NET Core 5.0 |  7.960 ns | 0.5752 ns | 0.8609 ns |  7.966 ns |  3.49 |    0.38 |      - |     - |     - |         - |
|              LogMix3 |  net50 | .NET Core 5.0 | 12.627 ns | 0.5318 ns | 0.7960 ns | 12.633 ns |  5.54 |    0.36 |      - |     - |     - |         - |
|              LogMix4 |  net50 | .NET Core 5.0 | 20.620 ns | 0.4601 ns | 0.6887 ns | 20.489 ns |  9.05 |    0.36 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net50 | .NET Core 5.0 | 23.377 ns | 0.1655 ns | 0.2476 ns | 23.431 ns | 10.26 |    0.19 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net50 | .NET Core 5.0 | 48.632 ns | 1.1064 ns | 1.6560 ns | 48.630 ns | 21.35 |    0.75 | 0.0446 |     - |     - |     280 B |
