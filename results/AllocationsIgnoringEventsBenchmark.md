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
|             LogEmpty | core31 | .NET Core 3.1 |  2.182 ns | 0.0174 ns | 0.0255 ns |  2.180 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |  2.365 ns | 0.1306 ns | 0.1955 ns |  2.353 ns |  1.08 |    0.09 |      - |     - |     - |         - |
|               LogMsg | core31 | .NET Core 3.1 |  3.540 ns | 0.0407 ns | 0.0609 ns |  3.531 ns |  1.62 |    0.03 |      - |     - |     - |         - |
|         LogMsgWithEx | core31 | .NET Core 3.1 |  3.429 ns | 0.0494 ns | 0.0739 ns |  3.415 ns |  1.57 |    0.04 |      - |     - |     - |         - |
|           LogScalar1 | core31 | .NET Core 3.1 |  5.598 ns | 0.0300 ns | 0.0439 ns |  5.599 ns |  2.57 |    0.04 |      - |     - |     - |         - |
|           LogScalar2 | core31 | .NET Core 3.1 | 11.137 ns | 0.1768 ns | 0.2591 ns | 11.146 ns |  5.10 |    0.11 |      - |     - |     - |         - |
|           LogScalar3 | core31 | .NET Core 3.1 | 14.793 ns | 0.6055 ns | 0.9063 ns | 14.452 ns |  6.76 |    0.42 |      - |     - |     - |         - |
|        LogScalarMany | core31 | .NET Core 3.1 | 18.743 ns | 0.1813 ns | 0.2714 ns | 18.792 ns |  8.59 |    0.14 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |  5.995 ns | 0.5584 ns | 0.8358 ns |  6.022 ns |  2.76 |    0.37 |      - |     - |     - |         - |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |  5.708 ns | 0.0685 ns | 0.1025 ns |  5.694 ns |  2.62 |    0.05 |      - |     - |     - |         - |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |  8.291 ns | 0.0993 ns | 0.1456 ns |  8.289 ns |  3.80 |    0.08 |      - |     - |     - |         - |
|  LogScalarStructMany | core31 | .NET Core 3.1 | 26.537 ns | 0.2375 ns | 0.3482 ns | 26.516 ns | 12.16 |    0.21 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 | 20.061 ns | 0.1830 ns | 0.2739 ns | 20.077 ns |  9.19 |    0.17 |      - |     - |     - |         - |
|        LogDictionary | core31 | .NET Core 3.1 |  8.995 ns | 0.1241 ns | 0.1857 ns |  8.989 ns |  4.12 |    0.10 | 0.0051 |     - |     - |      32 B |
|          LogSequence | core31 | .NET Core 3.1 |  9.005 ns | 0.1024 ns | 0.1532 ns |  9.007 ns |  4.12 |    0.09 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  8.976 ns | 0.2110 ns | 0.3158 ns |  8.942 ns |  4.11 |    0.16 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | core31 | .NET Core 3.1 | 11.531 ns | 0.3635 ns | 0.5328 ns | 11.395 ns |  5.28 |    0.24 |      - |     - |     - |         - |
|              LogMix3 | core31 | .NET Core 3.1 | 14.837 ns | 0.2456 ns | 0.3677 ns | 14.872 ns |  6.80 |    0.19 |      - |     - |     - |         - |
|              LogMix4 | core31 | .NET Core 3.1 | 26.294 ns | 0.1467 ns | 0.2151 ns | 26.331 ns | 12.05 |    0.15 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | core31 | .NET Core 3.1 | 30.944 ns | 0.3420 ns | 0.5119 ns | 30.932 ns | 14.18 |    0.25 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | core31 | .NET Core 3.1 | 56.222 ns | 0.3363 ns | 0.4929 ns | 56.275 ns | 25.76 |    0.31 | 0.0446 |     - |     - |     280 B |
|                      |        |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |  2.329 ns | 0.0299 ns | 0.0448 ns |  2.325 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |  2.329 ns | 0.0410 ns | 0.0613 ns |  2.312 ns |  1.00 |    0.04 |      - |     - |     - |         - |
|               LogMsg |  net48 |      .NET 4.8 |  3.899 ns | 0.0446 ns | 0.0668 ns |  3.907 ns |  1.67 |    0.04 |      - |     - |     - |         - |
|         LogMsgWithEx |  net48 |      .NET 4.8 |  3.921 ns | 0.0423 ns | 0.0633 ns |  3.936 ns |  1.68 |    0.04 |      - |     - |     - |         - |
|           LogScalar1 |  net48 |      .NET 4.8 |  7.679 ns | 0.3997 ns | 0.5982 ns |  7.641 ns |  3.30 |    0.28 |      - |     - |     - |         - |
|           LogScalar2 |  net48 |      .NET 4.8 | 13.468 ns | 0.6455 ns | 0.9049 ns | 14.176 ns |  5.78 |    0.43 |      - |     - |     - |         - |
|           LogScalar3 |  net48 |      .NET 4.8 | 16.409 ns | 0.1625 ns | 0.2432 ns | 16.433 ns |  7.05 |    0.20 |      - |     - |     - |         - |
|        LogScalarMany |  net48 |      .NET 4.8 | 16.918 ns | 0.1940 ns | 0.2844 ns | 16.946 ns |  7.26 |    0.18 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |  6.790 ns | 0.0764 ns | 0.1144 ns |  6.769 ns |  2.92 |    0.07 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |  7.221 ns | 0.1684 ns | 0.2520 ns |  7.244 ns |  3.10 |    0.11 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net48 |      .NET 4.8 | 10.713 ns | 0.0862 ns | 0.1290 ns | 10.735 ns |  4.60 |    0.11 |      - |     - |     - |         - |
|  LogScalarStructMany |  net48 |      .NET 4.8 | 24.425 ns | 0.1697 ns | 0.2541 ns | 24.470 ns | 10.49 |    0.22 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 | 21.890 ns | 0.1750 ns | 0.2620 ns | 21.923 ns |  9.40 |    0.21 |      - |     - |     - |         - |
|        LogDictionary |  net48 |      .NET 4.8 |  9.096 ns | 0.0838 ns | 0.1254 ns |  9.124 ns |  3.91 |    0.10 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net48 |      .NET 4.8 |  9.096 ns | 0.0792 ns | 0.1185 ns |  9.142 ns |  3.91 |    0.09 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  9.167 ns | 0.1000 ns | 0.1497 ns |  9.170 ns |  3.94 |    0.10 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net48 |      .NET 4.8 | 12.847 ns | 0.1639 ns | 0.2454 ns | 12.824 ns |  5.52 |    0.15 |      - |     - |     - |         - |
|              LogMix3 |  net48 |      .NET 4.8 | 16.086 ns | 0.2070 ns | 0.3098 ns | 16.026 ns |  6.91 |    0.17 |      - |     - |     - |         - |
|              LogMix4 |  net48 |      .NET 4.8 | 23.063 ns | 0.1558 ns | 0.2331 ns | 23.117 ns |  9.90 |    0.22 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net48 |      .NET 4.8 | 28.487 ns | 0.2951 ns | 0.4417 ns | 28.386 ns | 12.23 |    0.28 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net48 |      .NET 4.8 | 53.304 ns | 0.3266 ns | 0.4889 ns | 53.402 ns | 22.89 |    0.49 | 0.0446 |     - |     - |     281 B |
|                      |        |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |  2.348 ns | 0.1261 ns | 0.1887 ns |  2.356 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |  2.182 ns | 0.0205 ns | 0.0306 ns |  2.189 ns |  0.94 |    0.08 |      - |     - |     - |         - |
|               LogMsg |  net50 | .NET Core 5.0 |  3.066 ns | 0.0508 ns | 0.0760 ns |  3.074 ns |  1.31 |    0.11 |      - |     - |     - |         - |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |  3.234 ns | 0.0442 ns | 0.0662 ns |  3.242 ns |  1.39 |    0.11 |      - |     - |     - |         - |
|           LogScalar1 |  net50 | .NET Core 5.0 |  6.301 ns | 0.1566 ns | 0.2344 ns |  6.305 ns |  2.69 |    0.14 |      - |     - |     - |         - |
|           LogScalar2 |  net50 | .NET Core 5.0 |  7.051 ns | 0.1781 ns | 0.2665 ns |  7.050 ns |  3.01 |    0.16 |      - |     - |     - |         - |
|           LogScalar3 |  net50 | .NET Core 5.0 | 11.697 ns | 0.1060 ns | 0.1486 ns | 11.714 ns |  4.98 |    0.44 |      - |     - |     - |         - |
|        LogScalarMany |  net50 | .NET Core 5.0 | 13.332 ns | 0.2590 ns | 0.3877 ns | 13.423 ns |  5.71 |    0.45 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |  5.114 ns | 0.0580 ns | 0.0868 ns |  5.113 ns |  2.19 |    0.15 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |  5.684 ns | 0.0360 ns | 0.0539 ns |  5.695 ns |  2.44 |    0.20 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |  8.662 ns | 0.1384 ns | 0.2072 ns |  8.684 ns |  3.71 |    0.23 |      - |     - |     - |         - |
|  LogScalarStructMany |  net50 | .NET Core 5.0 | 21.006 ns | 0.2318 ns | 0.3469 ns | 20.979 ns |  9.00 |    0.62 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |  8.172 ns | 0.2497 ns | 0.3738 ns |  8.135 ns |  3.49 |    0.15 |      - |     - |     - |         - |
|        LogDictionary |  net50 | .NET Core 5.0 |  7.660 ns | 0.1571 ns | 0.2351 ns |  7.647 ns |  3.29 |    0.35 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net50 | .NET Core 5.0 |  7.786 ns | 0.2145 ns | 0.3210 ns |  7.789 ns |  3.33 |    0.17 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  8.030 ns | 0.1146 ns | 0.1680 ns |  8.086 ns |  3.43 |    0.27 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net50 | .NET Core 5.0 |  6.646 ns | 0.1303 ns | 0.1950 ns |  6.625 ns |  2.85 |    0.29 |      - |     - |     - |         - |
|              LogMix3 |  net50 | .NET Core 5.0 | 11.762 ns | 0.2330 ns | 0.3488 ns | 11.777 ns |  5.03 |    0.27 |      - |     - |     - |         - |
|              LogMix4 |  net50 | .NET Core 5.0 | 19.161 ns | 0.2149 ns | 0.3216 ns | 19.179 ns |  8.22 |    0.72 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net50 | .NET Core 5.0 | 22.633 ns | 0.2702 ns | 0.4045 ns | 22.618 ns |  9.71 |    0.91 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net50 | .NET Core 5.0 | 45.967 ns | 0.5677 ns | 0.8497 ns | 45.873 ns | 19.72 |    1.86 | 0.0446 |     - |     - |     280 B |
