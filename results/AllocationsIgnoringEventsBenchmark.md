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
|             LogEmpty | core31 | .NET Core 3.1 |  1.572 ns | 0.0623 ns | 0.0913 ns |  1.563 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |  1.475 ns | 0.0343 ns | 0.0514 ns |  1.473 ns |  0.94 |    0.06 |      - |     - |     - |         - |
|               LogMsg | core31 | .NET Core 3.1 |  2.090 ns | 0.0287 ns | 0.0429 ns |  2.094 ns |  1.34 |    0.08 |      - |     - |     - |         - |
|         LogMsgWithEx | core31 | .NET Core 3.1 |  2.303 ns | 0.0561 ns | 0.0840 ns |  2.321 ns |  1.47 |    0.11 |      - |     - |     - |         - |
|           LogScalar1 | core31 | .NET Core 3.1 |  6.103 ns | 0.1057 ns | 0.1582 ns |  6.093 ns |  3.90 |    0.26 |      - |     - |     - |         - |
|           LogScalar2 | core31 | .NET Core 3.1 | 11.861 ns | 0.2969 ns | 0.4443 ns | 11.780 ns |  7.56 |    0.40 |      - |     - |     - |         - |
|           LogScalar3 | core31 | .NET Core 3.1 | 12.262 ns | 0.1421 ns | 0.2126 ns | 12.277 ns |  7.83 |    0.48 |      - |     - |     - |         - |
|        LogScalarMany | core31 | .NET Core 3.1 | 16.643 ns | 0.2342 ns | 0.3506 ns | 16.745 ns | 10.62 |    0.56 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |  5.626 ns | 0.2887 ns | 0.4232 ns |  5.324 ns |  3.60 |    0.45 |      - |     - |     - |         - |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |  5.515 ns | 0.0676 ns | 0.0991 ns |  5.500 ns |  3.52 |    0.23 |      - |     - |     - |         - |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |  6.009 ns | 0.1474 ns | 0.2207 ns |  5.910 ns |  3.84 |    0.33 |      - |     - |     - |         - |
|  LogScalarStructMany | core31 | .NET Core 3.1 | 26.247 ns | 0.2525 ns | 0.3779 ns | 26.287 ns | 16.77 |    1.08 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 | 20.546 ns | 0.3561 ns | 0.5330 ns | 20.542 ns | 13.12 |    0.86 |      - |     - |     - |         - |
|        LogDictionary | core31 | .NET Core 3.1 |  7.126 ns | 0.1250 ns | 0.1870 ns |  7.116 ns |  4.55 |    0.30 | 0.0051 |     - |     - |      32 B |
|          LogSequence | core31 | .NET Core 3.1 |  7.093 ns | 0.1292 ns | 0.1934 ns |  7.085 ns |  4.53 |    0.30 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  6.949 ns | 0.1366 ns | 0.2044 ns |  6.928 ns |  4.44 |    0.32 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | core31 | .NET Core 3.1 | 11.579 ns | 0.1481 ns | 0.2217 ns | 11.630 ns |  7.39 |    0.45 |      - |     - |     - |         - |
|              LogMix3 | core31 | .NET Core 3.1 | 11.865 ns | 0.2727 ns | 0.4082 ns | 11.839 ns |  7.59 |    0.64 |      - |     - |     - |         - |
|              LogMix4 | core31 | .NET Core 3.1 | 24.828 ns | 0.2597 ns | 0.3887 ns | 24.914 ns | 15.86 |    0.96 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | core31 | .NET Core 3.1 | 29.504 ns | 0.5316 ns | 0.7957 ns | 29.155 ns | 18.81 |    1.20 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | core31 | .NET Core 3.1 | 56.553 ns | 0.4434 ns | 0.6499 ns | 56.735 ns | 36.10 |    2.19 | 0.0446 |     - |     - |     280 B |
|                      |        |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |  1.693 ns | 0.0261 ns | 0.0391 ns |  1.693 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |  1.692 ns | 0.0358 ns | 0.0535 ns |  1.698 ns |  1.00 |    0.03 |      - |     - |     - |         - |
|               LogMsg |  net48 |      .NET 4.8 |  1.664 ns | 0.0302 ns | 0.0452 ns |  1.672 ns |  0.98 |    0.03 |      - |     - |     - |         - |
|         LogMsgWithEx |  net48 |      .NET 4.8 |  2.091 ns | 0.0258 ns | 0.0386 ns |  2.094 ns |  1.24 |    0.04 |      - |     - |     - |         - |
|           LogScalar1 |  net48 |      .NET 4.8 |  8.193 ns | 0.4800 ns | 0.7184 ns |  8.165 ns |  4.84 |    0.41 |      - |     - |     - |         - |
|           LogScalar2 |  net48 |      .NET 4.8 | 13.149 ns | 0.2965 ns | 0.4346 ns | 13.007 ns |  7.77 |    0.33 |      - |     - |     - |         - |
|           LogScalar3 |  net48 |      .NET 4.8 | 14.468 ns | 0.5106 ns | 0.7643 ns | 14.409 ns |  8.55 |    0.46 |      - |     - |     - |         - |
|        LogScalarMany |  net48 |      .NET 4.8 | 15.318 ns | 0.3018 ns | 0.4517 ns | 15.284 ns |  9.05 |    0.35 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |  7.370 ns | 0.3467 ns | 0.5082 ns |  7.295 ns |  4.36 |    0.34 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |  7.180 ns | 0.1402 ns | 0.2098 ns |  7.190 ns |  4.24 |    0.15 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net48 |      .NET 4.8 |  8.158 ns | 0.3313 ns | 0.4958 ns |  8.172 ns |  4.82 |    0.30 |      - |     - |     - |         - |
|  LogScalarStructMany |  net48 |      .NET 4.8 | 23.582 ns | 0.2724 ns | 0.4077 ns | 23.608 ns | 13.94 |    0.34 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 | 21.993 ns | 0.2182 ns | 0.3267 ns | 22.004 ns | 13.00 |    0.26 |      - |     - |     - |         - |
|        LogDictionary |  net48 |      .NET 4.8 |  7.398 ns | 0.1453 ns | 0.2174 ns |  7.419 ns |  4.37 |    0.16 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net48 |      .NET 4.8 |  7.421 ns | 0.1423 ns | 0.2131 ns |  7.449 ns |  4.39 |    0.16 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  7.347 ns | 0.1392 ns | 0.2083 ns |  7.330 ns |  4.34 |    0.16 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net48 |      .NET 4.8 | 13.262 ns | 0.3118 ns | 0.4667 ns | 13.163 ns |  7.84 |    0.32 |      - |     - |     - |         - |
|              LogMix3 |  net48 |      .NET 4.8 | 13.620 ns | 0.2383 ns | 0.3567 ns | 13.507 ns |  8.05 |    0.25 |      - |     - |     - |         - |
|              LogMix4 |  net48 |      .NET 4.8 | 22.407 ns | 0.2868 ns | 0.4293 ns | 22.557 ns | 13.24 |    0.37 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net48 |      .NET 4.8 | 27.446 ns | 0.3347 ns | 0.5009 ns | 27.416 ns | 16.22 |    0.49 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net48 |      .NET 4.8 | 52.424 ns | 0.6773 ns | 1.0138 ns | 52.222 ns | 30.99 |    0.85 | 0.0446 |     - |     - |     281 B |
|                      |        |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |  1.557 ns | 0.0677 ns | 0.1013 ns |  1.559 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |  1.480 ns | 0.0330 ns | 0.0494 ns |  1.490 ns |  0.95 |    0.06 |      - |     - |     - |         - |
|               LogMsg |  net50 | .NET Core 5.0 |  2.095 ns | 0.0249 ns | 0.0373 ns |  2.103 ns |  1.35 |    0.09 |      - |     - |     - |         - |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |  2.234 ns | 0.0341 ns | 0.0500 ns |  2.241 ns |  1.44 |    0.10 |      - |     - |     - |         - |
|           LogScalar1 |  net50 | .NET Core 5.0 |  6.382 ns | 0.1807 ns | 0.2705 ns |  6.337 ns |  4.11 |    0.17 |      - |     - |     - |         - |
|           LogScalar2 |  net50 | .NET Core 5.0 |  6.909 ns | 0.1106 ns | 0.1656 ns |  6.863 ns |  4.45 |    0.31 |      - |     - |     - |         - |
|           LogScalar3 |  net50 | .NET Core 5.0 |  7.547 ns | 0.1888 ns | 0.2826 ns |  7.546 ns |  4.87 |    0.46 |      - |     - |     - |         - |
|        LogScalarMany |  net50 | .NET Core 5.0 | 10.541 ns | 0.1483 ns | 0.2220 ns | 10.579 ns |  6.80 |    0.45 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |  5.330 ns | 0.0559 ns | 0.0837 ns |  5.325 ns |  3.44 |    0.21 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |  5.734 ns | 0.1101 ns | 0.1647 ns |  5.688 ns |  3.70 |    0.23 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |  5.778 ns | 0.0535 ns | 0.0801 ns |  5.789 ns |  3.73 |    0.25 |      - |     - |     - |         - |
|  LogScalarStructMany |  net50 | .NET Core 5.0 | 19.822 ns | 0.3039 ns | 0.4549 ns | 19.862 ns | 12.79 |    1.00 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |  9.110 ns | 0.1618 ns | 0.2422 ns |  9.119 ns |  5.88 |    0.47 |      - |     - |     - |         - |
|        LogDictionary |  net50 | .NET Core 5.0 |  5.870 ns | 0.0605 ns | 0.0906 ns |  5.862 ns |  3.79 |    0.29 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net50 | .NET Core 5.0 |  5.771 ns | 0.1446 ns | 0.2165 ns |  5.710 ns |  3.72 |    0.18 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  5.935 ns | 0.0730 ns | 0.1093 ns |  5.951 ns |  3.83 |    0.26 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net50 | .NET Core 5.0 |  6.677 ns | 0.1409 ns | 0.2109 ns |  6.645 ns |  4.31 |    0.31 |      - |     - |     - |         - |
|              LogMix3 |  net50 | .NET Core 5.0 |  6.984 ns | 0.1220 ns | 0.1826 ns |  6.967 ns |  4.50 |    0.32 |      - |     - |     - |         - |
|              LogMix4 |  net50 | .NET Core 5.0 | 17.334 ns | 0.2530 ns | 0.3787 ns | 17.321 ns | 11.18 |    0.84 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net50 | .NET Core 5.0 | 20.738 ns | 0.2742 ns | 0.4104 ns | 20.821 ns | 13.37 |    0.94 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net50 | .NET Core 5.0 | 43.945 ns | 0.7476 ns | 1.1189 ns | 43.869 ns | 28.30 |    1.43 | 0.0446 |     - |     - |     280 B |
