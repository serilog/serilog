``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.404
  [Host]          : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|               Method |             Job |       Jit |       Runtime |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |---------------- |---------- |-------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.191 ns | 0.0102 ns | 0.0143 ns |  2.186 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.732 ns | 0.0098 ns | 0.0141 ns |  2.735 ns |  1.25 |    0.01 |      - |     - |     - |         - |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  3.837 ns | 0.0111 ns | 0.0159 ns |  3.835 ns |  1.75 |    0.01 |      - |     - |     - |         - |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  3.659 ns | 0.0136 ns | 0.0190 ns |  3.659 ns |  1.67 |    0.01 |      - |     - |     - |         - |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  6.557 ns | 0.0869 ns | 0.1274 ns |  6.604 ns |  3.00 |    0.05 |      - |     - |     - |         - |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 12.518 ns | 0.4017 ns | 0.6013 ns | 12.087 ns |  5.74 |    0.26 |      - |     - |     - |         - |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 14.812 ns | 0.0504 ns | 0.0706 ns | 14.809 ns |  6.76 |    0.06 |      - |     - |     - |         - |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 19.342 ns | 0.2181 ns | 0.2985 ns | 19.467 ns |  8.83 |    0.17 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.297 ns | 0.0280 ns | 0.0401 ns |  5.305 ns |  2.42 |    0.03 |      - |     - |     - |         - |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  6.011 ns | 0.2122 ns | 0.3043 ns |  5.785 ns |  2.75 |    0.13 |      - |     - |     - |         - |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  8.863 ns | 0.0990 ns | 0.1451 ns |  8.843 ns |  4.05 |    0.06 |      - |     - |     - |         - |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 29.395 ns | 0.2706 ns | 0.4050 ns | 29.313 ns | 13.40 |    0.24 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 21.217 ns | 0.0797 ns | 0.1168 ns | 21.197 ns |  9.68 |    0.08 |      - |     - |     - |         - |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.323 ns | 0.2583 ns | 0.3866 ns |  9.329 ns |  4.24 |    0.19 | 0.0051 |     - |     - |      32 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.630 ns | 0.0434 ns | 0.0623 ns |  9.633 ns |  4.40 |    0.04 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.893 ns | 0.1729 ns | 0.2534 ns |  9.992 ns |  4.52 |    0.11 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 11.798 ns | 0.2735 ns | 0.4009 ns | 11.512 ns |  5.37 |    0.20 |      - |     - |     - |         - |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 15.411 ns | 0.3022 ns | 0.4429 ns | 15.302 ns |  7.04 |    0.22 |      - |     - |     - |         - |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 27.222 ns | 0.6372 ns | 0.9537 ns | 27.205 ns | 12.47 |    0.40 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 32.113 ns | 0.1586 ns | 0.2223 ns | 32.138 ns | 14.66 |    0.16 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 60.482 ns | 0.2956 ns | 0.4332 ns | 60.364 ns | 27.61 |    0.31 | 0.0446 |     - |     - |     280 B |
|                      |                 |           |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.465 ns | 0.0138 ns | 0.0198 ns |  2.461 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.466 ns | 0.0115 ns | 0.0161 ns |  2.466 ns |  1.00 |    0.01 |      - |     - |     - |         - |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |  4.123 ns | 0.0153 ns | 0.0224 ns |  4.118 ns |  1.67 |    0.02 |      - |     - |     - |         - |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |  4.147 ns | 0.0170 ns | 0.0249 ns |  4.142 ns |  1.68 |    0.02 |      - |     - |     - |         - |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.662 ns | 0.0861 ns | 0.1235 ns |  7.647 ns |  3.11 |    0.06 |      - |     - |     - |         - |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 13.359 ns | 0.0432 ns | 0.0633 ns | 13.363 ns |  5.42 |    0.05 |      - |     - |     - |         - |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 17.555 ns | 0.1359 ns | 0.1992 ns | 17.563 ns |  7.13 |    0.10 |      - |     - |     - |         - |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 18.100 ns | 0.0648 ns | 0.0950 ns | 18.105 ns |  7.35 |    0.06 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.260 ns | 0.0878 ns | 0.1230 ns |  7.180 ns |  2.95 |    0.06 |      - |     - |     - |         - |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.674 ns | 0.2533 ns | 0.3712 ns |  7.378 ns |  3.12 |    0.15 |      - |     - |     - |         - |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 11.755 ns | 0.3231 ns | 0.4529 ns | 12.139 ns |  4.77 |    0.18 |      - |     - |     - |         - |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 25.988 ns | 0.1020 ns | 0.1527 ns | 25.968 ns | 10.55 |    0.11 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 | 22.997 ns | 0.0481 ns | 0.0659 ns | 23.000 ns |  9.33 |    0.09 |      - |     - |     - |         - |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.620 ns | 0.0243 ns | 0.0341 ns |  9.625 ns |  3.90 |    0.03 | 0.0051 |     - |     - |      32 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.649 ns | 0.0213 ns | 0.0306 ns |  9.644 ns |  3.92 |    0.03 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.708 ns | 0.0329 ns | 0.0461 ns |  9.705 ns |  3.94 |    0.04 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 13.564 ns | 0.0333 ns | 0.0456 ns | 13.564 ns |  5.50 |    0.05 |      - |     - |     - |         - |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 17.582 ns | 0.3176 ns | 0.4754 ns | 17.583 ns |  7.12 |    0.20 |      - |     - |     - |         - |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 24.663 ns | 0.0900 ns | 0.1319 ns | 24.675 ns | 10.01 |    0.12 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 30.306 ns | 0.1103 ns | 0.1617 ns | 30.281 ns | 12.29 |    0.12 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 56.680 ns | 0.2138 ns | 0.3067 ns | 56.685 ns | 23.00 |    0.23 | 0.0446 |     - |     - |     281 B |
|                      |                 |           |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.461 ns | 0.0106 ns | 0.0149 ns |  2.460 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.464 ns | 0.0104 ns | 0.0149 ns |  2.464 ns |  1.00 |    0.01 |      - |     - |     - |         - |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |  4.137 ns | 0.0522 ns | 0.0782 ns |  4.150 ns |  1.68 |    0.04 |      - |     - |     - |         - |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |  4.131 ns | 0.0113 ns | 0.0150 ns |  4.133 ns |  1.68 |    0.01 |      - |     - |     - |         - |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.484 ns | 0.0309 ns | 0.0442 ns |  7.474 ns |  3.04 |    0.03 |      - |     - |     - |         - |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 14.517 ns | 0.2705 ns | 0.3879 ns | 14.284 ns |  5.90 |    0.16 |      - |     - |     - |         - |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 17.801 ns | 0.2947 ns | 0.4319 ns | 18.047 ns |  7.25 |    0.17 |      - |     - |     - |         - |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 18.115 ns | 0.0650 ns | 0.0973 ns | 18.085 ns |  7.36 |    0.06 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.192 ns | 0.0227 ns | 0.0325 ns |  7.188 ns |  2.92 |    0.02 |      - |     - |     - |         - |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.570 ns | 0.1793 ns | 0.2629 ns |  7.392 ns |  3.08 |    0.11 |      - |     - |     - |         - |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 12.957 ns | 0.2347 ns | 0.3440 ns | 13.198 ns |  5.27 |    0.14 |      - |     - |     - |         - |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 25.956 ns | 0.0681 ns | 0.0999 ns | 25.955 ns | 10.55 |    0.07 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 | 23.077 ns | 0.0845 ns | 0.1239 ns | 23.047 ns |  9.38 |    0.07 |      - |     - |     - |         - |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.644 ns | 0.0402 ns | 0.0589 ns |  9.644 ns |  3.92 |    0.03 | 0.0051 |     - |     - |      32 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.652 ns | 0.0419 ns | 0.0601 ns |  9.663 ns |  3.92 |    0.03 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.769 ns | 0.0461 ns | 0.0661 ns |  9.765 ns |  3.97 |    0.04 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 13.651 ns | 0.0561 ns | 0.0839 ns | 13.656 ns |  5.55 |    0.05 |      - |     - |     - |         - |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 17.583 ns | 0.3414 ns | 0.4897 ns | 17.901 ns |  7.15 |    0.19 |      - |     - |     - |         - |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 24.583 ns | 0.0578 ns | 0.0810 ns | 24.607 ns |  9.99 |    0.07 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 30.336 ns | 0.0961 ns | 0.1439 ns | 30.332 ns | 12.32 |    0.10 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 56.797 ns | 0.1298 ns | 0.1902 ns | 56.800 ns | 23.08 |    0.17 | 0.0446 |     - |     - |     281 B |
