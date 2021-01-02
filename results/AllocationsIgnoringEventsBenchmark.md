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
|               Method |    Job |       Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |------- |-------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty | core31 | .NET Core 3.1 |  2.085 ns | 0.0194 ns | 0.0284 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |  2.615 ns | 0.0287 ns | 0.0429 ns |  1.26 |    0.03 |      - |     - |     - |         - |
|               LogMsg | core31 | .NET Core 3.1 |  3.558 ns | 0.0459 ns | 0.0673 ns |  1.71 |    0.04 |      - |     - |     - |         - |
|         LogMsgWithEx | core31 | .NET Core 3.1 |  3.753 ns | 0.2127 ns | 0.3184 ns |  1.80 |    0.16 |      - |     - |     - |         - |
|           LogScalar1 | core31 | .NET Core 3.1 |  6.266 ns | 0.0913 ns | 0.1367 ns |  3.01 |    0.08 |      - |     - |     - |         - |
|           LogScalar2 | core31 | .NET Core 3.1 | 11.370 ns | 0.2279 ns | 0.3411 ns |  5.46 |    0.18 |      - |     - |     - |         - |
|           LogScalar3 | core31 | .NET Core 3.1 | 14.935 ns | 0.6196 ns | 0.9273 ns |  7.17 |    0.46 |      - |     - |     - |         - |
|        LogScalarMany | core31 | .NET Core 3.1 | 18.981 ns | 0.1905 ns | 0.2852 ns |  9.10 |    0.19 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |  5.006 ns | 0.0473 ns | 0.0709 ns |  2.40 |    0.05 |      - |     - |     - |         - |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |  5.344 ns | 0.0438 ns | 0.0643 ns |  2.56 |    0.05 |      - |     - |     - |         - |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |  8.836 ns | 0.1459 ns | 0.2184 ns |  4.24 |    0.12 |      - |     - |     - |         - |
|  LogScalarStructMany | core31 | .NET Core 3.1 | 27.011 ns | 0.1446 ns | 0.2164 ns | 12.95 |    0.19 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 | 20.001 ns | 0.1997 ns | 0.2989 ns |  9.59 |    0.19 |      - |     - |     - |         - |
|        LogDictionary | core31 | .NET Core 3.1 |  8.899 ns | 0.0833 ns | 0.1246 ns |  4.27 |    0.09 | 0.0051 |     - |     - |      32 B |
|          LogSequence | core31 | .NET Core 3.1 |  9.031 ns | 0.1448 ns | 0.2167 ns |  4.33 |    0.12 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  9.229 ns | 0.2430 ns | 0.3638 ns |  4.42 |    0.18 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | core31 | .NET Core 3.1 | 11.004 ns | 0.2088 ns | 0.3061 ns |  5.28 |    0.16 |      - |     - |     - |         - |
|              LogMix3 | core31 | .NET Core 3.1 | 14.658 ns | 0.3574 ns | 0.5349 ns |  7.03 |    0.29 |      - |     - |     - |         - |
|              LogMix4 | core31 | .NET Core 3.1 | 24.730 ns | 0.1672 ns | 0.2503 ns | 11.86 |    0.17 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | core31 | .NET Core 3.1 | 30.195 ns | 0.3165 ns | 0.4737 ns | 14.47 |    0.28 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | core31 | .NET Core 3.1 | 57.404 ns | 0.7375 ns | 1.1038 ns | 27.51 |    0.64 | 0.0446 |     - |     - |     280 B |
|                      |        |               |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |  2.696 ns | 0.0458 ns | 0.0686 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |  2.696 ns | 0.0489 ns | 0.0732 ns |  1.00 |    0.02 |      - |     - |     - |         - |
|               LogMsg |  net48 |      .NET 4.8 |  3.660 ns | 0.0444 ns | 0.0665 ns |  1.36 |    0.04 |      - |     - |     - |         - |
|         LogMsgWithEx |  net48 |      .NET 4.8 |  4.134 ns | 0.0444 ns | 0.0664 ns |  1.53 |    0.04 |      - |     - |     - |         - |
|           LogScalar1 |  net48 |      .NET 4.8 |  7.108 ns | 0.1121 ns | 0.1677 ns |  2.64 |    0.06 |      - |     - |     - |         - |
|           LogScalar2 |  net48 |      .NET 4.8 | 12.702 ns | 0.0995 ns | 0.1490 ns |  4.71 |    0.14 |      - |     - |     - |         - |
|           LogScalar3 |  net48 |      .NET 4.8 | 17.268 ns | 0.1906 ns | 0.2853 ns |  6.41 |    0.20 |      - |     - |     - |         - |
|        LogScalarMany |  net48 |      .NET 4.8 | 17.663 ns | 0.2258 ns | 0.3380 ns |  6.56 |    0.18 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |  6.496 ns | 0.0609 ns | 0.0853 ns |  2.41 |    0.07 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |  7.421 ns | 0.0967 ns | 0.1447 ns |  2.75 |    0.09 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net48 |      .NET 4.8 | 10.678 ns | 0.1969 ns | 0.2946 ns |  3.96 |    0.14 |      - |     - |     - |         - |
|  LogScalarStructMany |  net48 |      .NET 4.8 | 25.922 ns | 0.1524 ns | 0.2281 ns |  9.62 |    0.26 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 | 22.366 ns | 0.4310 ns | 0.6451 ns |  8.30 |    0.36 |      - |     - |     - |         - |
|        LogDictionary |  net48 |      .NET 4.8 |  9.215 ns | 0.0955 ns | 0.1429 ns |  3.42 |    0.09 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net48 |      .NET 4.8 |  9.374 ns | 0.1022 ns | 0.1498 ns |  3.48 |    0.10 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  9.402 ns | 0.1063 ns | 0.1592 ns |  3.49 |    0.11 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net48 |      .NET 4.8 | 12.890 ns | 0.1762 ns | 0.2583 ns |  4.78 |    0.16 |      - |     - |     - |         - |
|              LogMix3 |  net48 |      .NET 4.8 | 17.131 ns | 0.1890 ns | 0.2830 ns |  6.36 |    0.19 |      - |     - |     - |         - |
|              LogMix4 |  net48 |      .NET 4.8 | 24.628 ns | 0.1662 ns | 0.2436 ns |  9.13 |    0.24 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net48 |      .NET 4.8 | 29.610 ns | 0.3433 ns | 0.5138 ns | 10.99 |    0.27 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net48 |      .NET 4.8 | 52.885 ns | 0.3376 ns | 0.4733 ns | 19.59 |    0.51 | 0.0446 |     - |     - |     281 B |
|                      |        |               |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |  2.401 ns | 0.1307 ns | 0.1956 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |  2.248 ns | 0.0369 ns | 0.0552 ns |  0.94 |    0.08 |      - |     - |     - |         - |
|               LogMsg |  net50 | .NET Core 5.0 |  3.574 ns | 0.0400 ns | 0.0586 ns |  1.50 |    0.13 |      - |     - |     - |         - |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |  3.468 ns | 0.0474 ns | 0.0710 ns |  1.45 |    0.13 |      - |     - |     - |         - |
|           LogScalar1 |  net50 | .NET Core 5.0 |  6.299 ns | 0.1690 ns | 0.2529 ns |  2.65 |    0.30 |      - |     - |     - |         - |
|           LogScalar2 |  net50 | .NET Core 5.0 |  6.628 ns | 0.0996 ns | 0.1491 ns |  2.78 |    0.23 |      - |     - |     - |         - |
|           LogScalar3 |  net50 | .NET Core 5.0 | 11.884 ns | 0.2147 ns | 0.3214 ns |  4.97 |    0.29 |      - |     - |     - |         - |
|        LogScalarMany |  net50 | .NET Core 5.0 | 12.827 ns | 0.0996 ns | 0.1491 ns |  5.37 |    0.40 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |  5.453 ns | 0.3108 ns | 0.4652 ns |  2.30 |    0.38 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |  5.601 ns | 0.0404 ns | 0.0604 ns |  2.35 |    0.20 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |  8.505 ns | 0.1866 ns | 0.2793 ns |  3.56 |    0.20 |      - |     - |     - |         - |
|  LogScalarStructMany |  net50 | .NET Core 5.0 | 20.694 ns | 0.2064 ns | 0.3090 ns |  8.67 |    0.70 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |  9.064 ns | 0.9095 ns | 1.3613 ns |  3.84 |    0.87 |      - |     - |     - |         - |
|        LogDictionary |  net50 | .NET Core 5.0 |  7.969 ns | 0.1321 ns | 0.1977 ns |  3.34 |    0.32 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net50 | .NET Core 5.0 |  7.847 ns | 0.0988 ns | 0.1449 ns |  3.30 |    0.29 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  7.800 ns | 0.1195 ns | 0.1788 ns |  3.27 |    0.27 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net50 | .NET Core 5.0 |  6.574 ns | 0.0819 ns | 0.1225 ns |  2.76 |    0.23 |      - |     - |     - |         - |
|              LogMix3 |  net50 | .NET Core 5.0 | 12.094 ns | 0.2961 ns | 0.4432 ns |  5.06 |    0.24 |      - |     - |     - |         - |
|              LogMix4 |  net50 | .NET Core 5.0 | 19.682 ns | 0.3483 ns | 0.5213 ns |  8.26 |    0.85 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net50 | .NET Core 5.0 | 23.044 ns | 0.3425 ns | 0.5126 ns |  9.67 |    0.96 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net50 | .NET Core 5.0 | 45.678 ns | 0.3490 ns | 0.5224 ns | 19.15 |    1.58 | 0.0446 |     - |     - |     280 B |
