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
|             LogEmpty | core31 | .NET Core 3.1 |  2.577 ns | 0.0603 ns | 0.0903 ns |  2.586 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |  2.405 ns | 0.1575 ns | 0.2357 ns |  2.397 ns |  0.94 |    0.11 |      - |     - |     - |         - |
|               LogMsg | core31 | .NET Core 3.1 |  3.436 ns | 0.0664 ns | 0.0994 ns |  3.426 ns |  1.34 |    0.07 |      - |     - |     - |         - |
|         LogMsgWithEx | core31 | .NET Core 3.1 |  3.734 ns | 0.2805 ns | 0.4198 ns |  3.721 ns |  1.45 |    0.14 |      - |     - |     - |         - |
|           LogScalar1 | core31 | .NET Core 3.1 |  6.077 ns | 0.1418 ns | 0.2123 ns |  6.003 ns |  2.36 |    0.10 |      - |     - |     - |         - |
|           LogScalar2 | core31 | .NET Core 3.1 | 11.571 ns | 0.2318 ns | 0.3470 ns | 11.565 ns |  4.50 |    0.19 |      - |     - |     - |         - |
|           LogScalar3 | core31 | .NET Core 3.1 | 16.737 ns | 1.3643 ns | 2.0420 ns | 15.770 ns |  6.50 |    0.77 |      - |     - |     - |         - |
|        LogScalarMany | core31 | .NET Core 3.1 | 20.322 ns | 0.8253 ns | 1.2352 ns | 20.091 ns |  7.89 |    0.42 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |  6.028 ns | 0.3714 ns | 0.5559 ns |  5.979 ns |  2.34 |    0.18 |      - |     - |     - |         - |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |  5.452 ns | 0.0504 ns | 0.0754 ns |  5.447 ns |  2.12 |    0.08 |      - |     - |     - |         - |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |  8.666 ns | 0.1727 ns | 0.2585 ns |  8.695 ns |  3.37 |    0.11 |      - |     - |     - |         - |
|  LogScalarStructMany | core31 | .NET Core 3.1 | 28.860 ns | 0.7767 ns | 1.1626 ns | 29.037 ns | 11.21 |    0.48 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 | 20.242 ns | 0.2507 ns | 0.3752 ns | 20.214 ns |  7.87 |    0.31 |      - |     - |     - |         - |
|        LogDictionary | core31 | .NET Core 3.1 |  9.079 ns | 0.1377 ns | 0.2060 ns |  9.094 ns |  3.53 |    0.17 | 0.0051 |     - |     - |      32 B |
|          LogSequence | core31 | .NET Core 3.1 |  8.993 ns | 0.1326 ns | 0.1985 ns |  9.002 ns |  3.50 |    0.17 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  9.112 ns | 0.1397 ns | 0.2091 ns |  9.125 ns |  3.54 |    0.18 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | core31 | .NET Core 3.1 | 11.647 ns | 0.5435 ns | 0.7967 ns | 11.142 ns |  4.51 |    0.26 |      - |     - |     - |         - |
|              LogMix3 | core31 | .NET Core 3.1 | 14.992 ns | 0.4304 ns | 0.6442 ns | 15.072 ns |  5.82 |    0.27 |      - |     - |     - |         - |
|              LogMix4 | core31 | .NET Core 3.1 | 25.226 ns | 0.2848 ns | 0.4262 ns | 25.269 ns |  9.80 |    0.40 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | core31 | .NET Core 3.1 | 30.258 ns | 0.5822 ns | 0.8714 ns | 30.147 ns | 11.76 |    0.48 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | core31 | .NET Core 3.1 | 57.320 ns | 0.4599 ns | 0.6741 ns | 57.362 ns | 22.23 |    0.75 | 0.0446 |     - |     - |     280 B |
|                      |        |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |  2.352 ns | 0.0485 ns | 0.0727 ns |  2.363 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |  2.355 ns | 0.0461 ns | 0.0689 ns |  2.351 ns |  1.00 |    0.04 |      - |     - |     - |         - |
|               LogMsg |  net48 |      .NET 4.8 |  3.925 ns | 0.0652 ns | 0.0976 ns |  3.952 ns |  1.67 |    0.08 |      - |     - |     - |         - |
|         LogMsgWithEx |  net48 |      .NET 4.8 |  3.941 ns | 0.0642 ns | 0.0960 ns |  3.950 ns |  1.68 |    0.07 |      - |     - |     - |         - |
|           LogScalar1 |  net48 |      .NET 4.8 |  7.155 ns | 0.1441 ns | 0.2157 ns |  7.143 ns |  3.04 |    0.13 |      - |     - |     - |         - |
|           LogScalar2 |  net48 |      .NET 4.8 | 14.416 ns | 0.7202 ns | 1.0780 ns | 14.422 ns |  6.14 |    0.52 |      - |     - |     - |         - |
|           LogScalar3 |  net48 |      .NET 4.8 | 16.481 ns | 0.1604 ns | 0.2401 ns | 16.440 ns |  7.01 |    0.24 |      - |     - |     - |         - |
|        LogScalarMany |  net48 |      .NET 4.8 | 17.500 ns | 0.3563 ns | 0.5333 ns | 17.407 ns |  7.45 |    0.28 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |  6.839 ns | 0.1245 ns | 0.1864 ns |  6.822 ns |  2.91 |    0.10 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |  6.932 ns | 0.1233 ns | 0.1845 ns |  6.917 ns |  2.95 |    0.11 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net48 |      .NET 4.8 | 11.175 ns | 0.3102 ns | 0.4642 ns | 11.100 ns |  4.76 |    0.27 |      - |     - |     - |         - |
|  LogScalarStructMany |  net48 |      .NET 4.8 | 24.585 ns | 0.2361 ns | 0.3534 ns | 24.544 ns | 10.46 |    0.37 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 | 22.142 ns | 0.2273 ns | 0.3402 ns | 22.120 ns |  9.42 |    0.33 |      - |     - |     - |         - |
|        LogDictionary |  net48 |      .NET 4.8 |  9.396 ns | 0.1368 ns | 0.2047 ns |  9.416 ns |  4.00 |    0.17 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net48 |      .NET 4.8 |  9.399 ns | 0.1417 ns | 0.2078 ns |  9.417 ns |  4.00 |    0.14 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  9.536 ns | 0.1367 ns | 0.1916 ns |  9.587 ns |  4.06 |    0.11 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net48 |      .NET 4.8 | 13.343 ns | 0.2655 ns | 0.3974 ns | 13.246 ns |  5.68 |    0.22 |      - |     - |     - |         - |
|              LogMix3 |  net48 |      .NET 4.8 | 16.802 ns | 0.4429 ns | 0.6629 ns | 16.870 ns |  7.15 |    0.32 |      - |     - |     - |         - |
|              LogMix4 |  net48 |      .NET 4.8 | 23.737 ns | 0.2188 ns | 0.3276 ns | 23.773 ns | 10.10 |    0.33 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net48 |      .NET 4.8 | 29.212 ns | 0.4917 ns | 0.7360 ns | 29.043 ns | 12.44 |    0.62 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net48 |      .NET 4.8 | 53.638 ns | 0.4414 ns | 0.6607 ns | 53.721 ns | 22.82 |    0.78 | 0.0446 |     - |     - |     281 B |
|                      |        |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |  2.621 ns | 0.0569 ns | 0.0851 ns |  2.604 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |  2.394 ns | 0.1598 ns | 0.2392 ns |  2.360 ns |  0.91 |    0.10 |      - |     - |     - |         - |
|               LogMsg |  net50 | .NET Core 5.0 |  3.864 ns | 0.0591 ns | 0.0884 ns |  3.877 ns |  1.48 |    0.06 |      - |     - |     - |         - |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |  4.075 ns | 0.0622 ns | 0.0930 ns |  4.071 ns |  1.56 |    0.06 |      - |     - |     - |         - |
|           LogScalar1 |  net50 | .NET Core 5.0 |  6.125 ns | 0.1829 ns | 0.2737 ns |  6.035 ns |  2.34 |    0.11 |      - |     - |     - |         - |
|           LogScalar2 |  net50 | .NET Core 5.0 |  7.178 ns | 0.1562 ns | 0.2338 ns |  7.121 ns |  2.74 |    0.12 |      - |     - |     - |         - |
|           LogScalar3 |  net50 | .NET Core 5.0 | 11.711 ns | 0.2597 ns | 0.3888 ns | 11.745 ns |  4.47 |    0.21 |      - |     - |     - |         - |
|        LogScalarMany |  net50 | .NET Core 5.0 | 12.662 ns | 0.2320 ns | 0.3473 ns | 12.634 ns |  4.84 |    0.21 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |  5.027 ns | 0.0628 ns | 0.0940 ns |  5.044 ns |  1.92 |    0.07 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |  5.658 ns | 0.0617 ns | 0.0924 ns |  5.679 ns |  2.16 |    0.08 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |  9.035 ns | 0.2811 ns | 0.4207 ns |  9.055 ns |  3.45 |    0.18 |      - |     - |     - |         - |
|  LogScalarStructMany |  net50 | .NET Core 5.0 | 21.067 ns | 0.2602 ns | 0.3895 ns | 21.101 ns |  8.05 |    0.28 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |  7.863 ns | 0.1465 ns | 0.2147 ns |  7.892 ns |  3.01 |    0.15 |      - |     - |     - |         - |
|        LogDictionary |  net50 | .NET Core 5.0 |  8.017 ns | 0.1261 ns | 0.1887 ns |  8.059 ns |  3.06 |    0.11 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net50 | .NET Core 5.0 |  7.467 ns | 0.1762 ns | 0.2638 ns |  7.585 ns |  2.85 |    0.14 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  7.525 ns | 0.1416 ns | 0.2076 ns |  7.570 ns |  2.88 |    0.10 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net50 | .NET Core 5.0 |  7.159 ns | 0.1382 ns | 0.2068 ns |  7.134 ns |  2.73 |    0.12 |      - |     - |     - |         - |
|              LogMix3 |  net50 | .NET Core 5.0 | 11.921 ns | 0.1174 ns | 0.1757 ns | 11.980 ns |  4.55 |    0.15 |      - |     - |     - |         - |
|              LogMix4 |  net50 | .NET Core 5.0 | 19.292 ns | 0.2922 ns | 0.4373 ns | 19.330 ns |  7.37 |    0.19 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net50 | .NET Core 5.0 | 22.796 ns | 0.3081 ns | 0.4611 ns | 22.967 ns |  8.71 |    0.32 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net50 | .NET Core 5.0 | 45.920 ns | 0.6247 ns | 0.9350 ns | 46.315 ns | 17.54 |    0.67 | 0.0446 |     - |     - |     280 B |
