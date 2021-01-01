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
|             LogEmpty | core31 | .NET Core 3.1 |  2.378 ns | 0.1910 ns | 0.2858 ns |  2.339 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |  2.361 ns | 0.1751 ns | 0.2620 ns |  2.314 ns |  0.99 |    0.04 |      - |     - |     - |         - |
|               LogMsg | core31 | .NET Core 3.1 |  3.587 ns | 0.0612 ns | 0.0916 ns |  3.591 ns |  1.53 |    0.19 |      - |     - |     - |         - |
|         LogMsgWithEx | core31 | .NET Core 3.1 |  4.067 ns | 0.0632 ns | 0.0946 ns |  4.079 ns |  1.73 |    0.22 |      - |     - |     - |         - |
|           LogScalar1 | core31 | .NET Core 3.1 |  6.309 ns | 0.1272 ns | 0.1904 ns |  6.292 ns |  2.69 |    0.36 |      - |     - |     - |         - |
|           LogScalar2 | core31 | .NET Core 3.1 | 11.091 ns | 0.1190 ns | 0.1782 ns | 11.054 ns |  4.73 |    0.59 |      - |     - |     - |         - |
|           LogScalar3 | core31 | .NET Core 3.1 | 15.000 ns | 0.5410 ns | 0.8098 ns | 15.031 ns |  6.39 |    0.75 |      - |     - |     - |         - |
|        LogScalarMany | core31 | .NET Core 3.1 | 18.955 ns | 0.3198 ns | 0.4787 ns | 18.953 ns |  8.10 |    1.09 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |  5.066 ns | 0.0613 ns | 0.0917 ns |  5.097 ns |  2.16 |    0.26 |      - |     - |     - |         - |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |  5.428 ns | 0.0548 ns | 0.0803 ns |  5.431 ns |  2.32 |    0.27 |      - |     - |     - |         - |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |  8.614 ns | 0.2481 ns | 0.3714 ns |  8.492 ns |  3.66 |    0.34 |      - |     - |     - |         - |
|  LogScalarStructMany | core31 | .NET Core 3.1 | 27.444 ns | 0.2853 ns | 0.4270 ns | 27.456 ns | 11.70 |    1.40 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 | 20.585 ns | 0.4006 ns | 0.5996 ns | 20.648 ns |  8.75 |    0.84 |      - |     - |     - |         - |
|        LogDictionary | core31 | .NET Core 3.1 |  9.272 ns | 0.1348 ns | 0.2018 ns |  9.307 ns |  3.96 |    0.50 | 0.0051 |     - |     - |      32 B |
|          LogSequence | core31 | .NET Core 3.1 |  9.156 ns | 0.1260 ns | 0.1885 ns |  9.170 ns |  3.91 |    0.48 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  9.193 ns | 0.1093 ns | 0.1636 ns |  9.196 ns |  3.92 |    0.49 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | core31 | .NET Core 3.1 | 10.974 ns | 0.1729 ns | 0.2589 ns | 10.939 ns |  4.68 |    0.55 |      - |     - |     - |         - |
|              LogMix3 | core31 | .NET Core 3.1 | 14.764 ns | 0.4892 ns | 0.7322 ns | 14.581 ns |  6.27 |    0.54 |      - |     - |     - |         - |
|              LogMix4 | core31 | .NET Core 3.1 | 26.050 ns | 0.7920 ns | 1.1854 ns | 25.891 ns | 11.06 |    0.92 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | core31 | .NET Core 3.1 | 30.650 ns | 0.5048 ns | 0.7555 ns | 30.704 ns | 13.07 |    1.61 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | core31 | .NET Core 3.1 | 57.606 ns | 0.4314 ns | 0.6456 ns | 57.591 ns | 24.57 |    2.97 | 0.0446 |     - |     - |     280 B |
|                      |        |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |  2.342 ns | 0.0508 ns | 0.0760 ns |  2.337 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |  2.351 ns | 0.0655 ns | 0.0981 ns |  2.367 ns |  1.00 |    0.04 |      - |     - |     - |         - |
|               LogMsg |  net48 |      .NET 4.8 |  3.923 ns | 0.0768 ns | 0.1125 ns |  3.947 ns |  1.68 |    0.08 |      - |     - |     - |         - |
|         LogMsgWithEx |  net48 |      .NET 4.8 |  3.941 ns | 0.0717 ns | 0.1074 ns |  3.930 ns |  1.68 |    0.05 |      - |     - |     - |         - |
|           LogScalar1 |  net48 |      .NET 4.8 |  7.135 ns | 0.1505 ns | 0.2253 ns |  7.095 ns |  3.05 |    0.14 |      - |     - |     - |         - |
|           LogScalar2 |  net48 |      .NET 4.8 | 12.798 ns | 0.2409 ns | 0.3606 ns | 12.688 ns |  5.47 |    0.22 |      - |     - |     - |         - |
|           LogScalar3 |  net48 |      .NET 4.8 | 16.686 ns | 0.3093 ns | 0.4629 ns | 16.700 ns |  7.13 |    0.27 |      - |     - |     - |         - |
|        LogScalarMany |  net48 |      .NET 4.8 | 17.397 ns | 0.2194 ns | 0.3284 ns | 17.399 ns |  7.43 |    0.25 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |  6.912 ns | 0.1488 ns | 0.2227 ns |  6.923 ns |  2.95 |    0.14 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |  7.195 ns | 0.2115 ns | 0.3165 ns |  7.219 ns |  3.07 |    0.15 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net48 |      .NET 4.8 | 10.746 ns | 0.1363 ns | 0.2041 ns | 10.768 ns |  4.59 |    0.18 |      - |     - |     - |         - |
|  LogScalarStructMany |  net48 |      .NET 4.8 | 24.752 ns | 0.2556 ns | 0.3826 ns | 24.743 ns | 10.58 |    0.35 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 | 21.987 ns | 0.2972 ns | 0.4448 ns | 22.013 ns |  9.40 |    0.38 |      - |     - |     - |         - |
|        LogDictionary |  net48 |      .NET 4.8 |  9.408 ns | 0.1491 ns | 0.2231 ns |  9.462 ns |  4.02 |    0.17 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net48 |      .NET 4.8 |  9.489 ns | 0.1370 ns | 0.2051 ns |  9.519 ns |  4.06 |    0.19 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  9.483 ns | 0.1609 ns | 0.2408 ns |  9.468 ns |  4.05 |    0.18 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net48 |      .NET 4.8 | 13.707 ns | 0.2085 ns | 0.3121 ns | 13.648 ns |  5.86 |    0.22 |      - |     - |     - |         - |
|              LogMix3 |  net48 |      .NET 4.8 | 17.725 ns | 0.7044 ns | 1.0543 ns | 17.997 ns |  7.57 |    0.47 |      - |     - |     - |         - |
|              LogMix4 |  net48 |      .NET 4.8 | 23.654 ns | 0.2844 ns | 0.4256 ns | 23.799 ns | 10.11 |    0.37 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net48 |      .NET 4.8 | 29.094 ns | 0.5969 ns | 0.8934 ns | 28.870 ns | 12.44 |    0.68 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net48 |      .NET 4.8 | 53.390 ns | 0.5823 ns | 0.8716 ns | 53.603 ns | 22.82 |    0.84 | 0.0446 |     - |     - |     281 B |
|                      |        |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |  2.407 ns | 0.1341 ns | 0.2007 ns |  2.361 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |  2.605 ns | 0.0663 ns | 0.0993 ns |  2.597 ns |  1.09 |    0.09 |      - |     - |     - |         - |
|               LogMsg |  net50 | .NET Core 5.0 |  3.284 ns | 0.0410 ns | 0.0613 ns |  3.276 ns |  1.37 |    0.11 |      - |     - |     - |         - |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |  3.330 ns | 0.0836 ns | 0.1252 ns |  3.313 ns |  1.40 |    0.15 |      - |     - |     - |         - |
|           LogScalar1 |  net50 | .NET Core 5.0 |  6.373 ns | 0.2037 ns | 0.3049 ns |  6.399 ns |  2.67 |    0.32 |      - |     - |     - |         - |
|           LogScalar2 |  net50 | .NET Core 5.0 |  6.666 ns | 0.1507 ns | 0.2256 ns |  6.664 ns |  2.78 |    0.15 |      - |     - |     - |         - |
|           LogScalar3 |  net50 | .NET Core 5.0 | 11.717 ns | 0.1062 ns | 0.1590 ns | 11.763 ns |  4.90 |    0.39 |      - |     - |     - |         - |
|        LogScalarMany |  net50 | .NET Core 5.0 | 12.944 ns | 0.2188 ns | 0.3275 ns | 12.890 ns |  5.42 |    0.52 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |  5.423 ns | 0.2410 ns | 0.3608 ns |  5.400 ns |  2.26 |    0.09 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |  5.793 ns | 0.1375 ns | 0.2058 ns |  5.744 ns |  2.42 |    0.14 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |  9.041 ns | 0.1687 ns | 0.2525 ns |  9.026 ns |  3.77 |    0.23 |      - |     - |     - |         - |
|  LogScalarStructMany |  net50 | .NET Core 5.0 | 20.984 ns | 0.2846 ns | 0.4260 ns | 21.069 ns |  8.78 |    0.72 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |  8.164 ns | 0.2796 ns | 0.4185 ns |  8.207 ns |  3.43 |    0.43 |      - |     - |     - |         - |
|        LogDictionary |  net50 | .NET Core 5.0 |  7.757 ns | 0.1500 ns | 0.2245 ns |  7.778 ns |  3.24 |    0.28 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net50 | .NET Core 5.0 |  7.665 ns | 0.1655 ns | 0.2477 ns |  7.796 ns |  3.21 |    0.28 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  7.798 ns | 0.1632 ns | 0.2443 ns |  7.854 ns |  3.26 |    0.27 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net50 | .NET Core 5.0 |  6.405 ns | 0.1326 ns | 0.1985 ns |  6.331 ns |  2.68 |    0.24 |      - |     - |     - |         - |
|              LogMix3 |  net50 | .NET Core 5.0 | 11.776 ns | 0.2474 ns | 0.3703 ns | 11.726 ns |  4.94 |    0.53 |      - |     - |     - |         - |
|              LogMix4 |  net50 | .NET Core 5.0 | 19.396 ns | 0.2943 ns | 0.4405 ns | 19.527 ns |  8.11 |    0.70 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net50 | .NET Core 5.0 | 23.265 ns | 0.3411 ns | 0.5106 ns | 23.265 ns |  9.72 |    0.68 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net50 | .NET Core 5.0 | 46.629 ns | 0.6731 ns | 1.0075 ns | 46.634 ns | 19.52 |    1.85 | 0.0446 |     - |     - |     280 B |
