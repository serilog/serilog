``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|               Method |             Job |       Jit |       Runtime |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |---------------- |---------- |-------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.349 ns | 0.1864 ns | 0.2733 ns |  2.569 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.099 ns | 0.0052 ns | 0.0075 ns |  2.099 ns |  0.91 |    0.11 |      - |     - |     - |         - |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  3.594 ns | 0.0378 ns | 0.0565 ns |  3.597 ns |  1.55 |    0.20 |      - |     - |     - |         - |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  3.741 ns | 0.1985 ns | 0.2971 ns |  3.732 ns |  1.60 |    0.08 |      - |     - |     - |         - |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  6.327 ns | 0.0253 ns | 0.0378 ns |  6.334 ns |  2.73 |    0.33 |      - |     - |     - |         - |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 11.788 ns | 0.2457 ns | 0.3677 ns | 11.616 ns |  5.06 |    0.48 |      - |     - |     - |         - |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 14.081 ns | 0.0524 ns | 0.0784 ns | 14.098 ns |  6.07 |    0.70 |      - |     - |     - |         - |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 18.567 ns | 0.1699 ns | 0.2544 ns | 18.491 ns |  8.02 |    0.99 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.153 ns | 0.1342 ns | 0.2009 ns |  5.079 ns |  2.23 |    0.34 |      - |     - |     - |         - |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.813 ns | 0.2831 ns | 0.4238 ns |  5.825 ns |  2.53 |    0.47 |      - |     - |     - |         - |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  8.128 ns | 0.0410 ns | 0.0587 ns |  8.130 ns |  3.52 |    0.41 |      - |     - |     - |         - |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 27.556 ns | 0.1461 ns | 0.2187 ns | 27.597 ns | 11.88 |    1.36 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 20.431 ns | 0.2490 ns | 0.3727 ns | 20.447 ns |  8.79 |    0.89 |      - |     - |     - |         - |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.048 ns | 0.0836 ns | 0.1116 ns |  9.096 ns |  3.97 |    0.50 | 0.0051 |     - |     - |      32 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  8.998 ns | 0.1019 ns | 0.1494 ns |  9.051 ns |  3.88 |    0.40 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.550 ns | 0.5227 ns | 0.7662 ns |  9.013 ns |  4.09 |    0.26 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 11.567 ns | 0.4681 ns | 0.6563 ns | 12.092 ns |  5.06 |    0.85 |      - |     - |     - |         - |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 14.954 ns | 0.7463 ns | 1.0939 ns | 14.149 ns |  6.41 |    0.47 |      - |     - |     - |         - |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 24.887 ns | 0.0550 ns | 0.0788 ns | 24.893 ns | 10.78 |    1.25 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 30.436 ns | 0.1205 ns | 0.1767 ns | 30.394 ns | 13.13 |    1.58 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 56.648 ns | 0.1550 ns | 0.2320 ns | 56.597 ns | 24.44 |    2.89 | 0.0446 |     - |     - |     280 B |
|                      |                 |           |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.333 ns | 0.0158 ns | 0.0237 ns |  2.331 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.338 ns | 0.0225 ns | 0.0315 ns |  2.336 ns |  1.00 |    0.02 |      - |     - |     - |         - |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3.897 ns | 0.0166 ns | 0.0249 ns |  3.901 ns |  1.67 |    0.02 |      - |     - |     - |         - |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3.912 ns | 0.0185 ns | 0.0271 ns |  3.914 ns |  1.68 |    0.02 |      - |     - |     - |         - |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.665 ns | 0.0281 ns | 0.0403 ns |  7.668 ns |  3.29 |    0.03 |      - |     - |     - |         - |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 13.207 ns | 0.2719 ns | 0.3899 ns | 12.910 ns |  5.66 |    0.18 |      - |     - |     - |         - |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 16.991 ns | 0.2921 ns | 0.4373 ns | 16.957 ns |  7.28 |    0.21 |      - |     - |     - |         - |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 16.867 ns | 0.0650 ns | 0.0911 ns | 16.850 ns |  7.23 |    0.09 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.316 ns | 0.0282 ns | 0.0414 ns |  7.308 ns |  3.14 |    0.03 |      - |     - |     - |         - |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.230 ns | 0.0410 ns | 0.0601 ns |  7.216 ns |  3.10 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.994 ns | 0.0273 ns | 0.0400 ns | 11.005 ns |  4.71 |    0.05 |      - |     - |     - |         - |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 24.507 ns | 0.0719 ns | 0.1031 ns | 24.535 ns | 10.51 |    0.11 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 | 22.151 ns | 0.0720 ns | 0.1078 ns | 22.167 ns |  9.49 |    0.10 |      - |     - |     - |         - |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.047 ns | 0.0203 ns | 0.0298 ns |  9.047 ns |  3.88 |    0.04 | 0.0051 |     - |     - |      32 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.057 ns | 0.1065 ns | 0.1594 ns |  8.994 ns |  3.88 |    0.08 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.143 ns | 0.0287 ns | 0.0420 ns |  9.136 ns |  3.92 |    0.05 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 13.621 ns | 0.0508 ns | 0.0744 ns | 13.636 ns |  5.84 |    0.06 |      - |     - |     - |         - |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 16.302 ns | 0.0309 ns | 0.0444 ns | 16.306 ns |  6.99 |    0.07 |      - |     - |     - |         - |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 23.503 ns | 0.0399 ns | 0.0572 ns | 23.497 ns | 10.08 |    0.11 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 29.738 ns | 0.1052 ns | 0.1542 ns | 29.741 ns | 12.75 |    0.15 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 54.922 ns | 0.0703 ns | 0.1052 ns | 54.932 ns | 23.54 |    0.24 | 0.0446 |     - |     - |     281 B |
|                      |                 |           |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.333 ns | 0.0164 ns | 0.0235 ns |  2.331 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.334 ns | 0.0159 ns | 0.0237 ns |  2.340 ns |  1.00 |    0.02 |      - |     - |     - |         - |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3.923 ns | 0.0256 ns | 0.0367 ns |  3.915 ns |  1.68 |    0.03 |      - |     - |     - |         - |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3.926 ns | 0.0124 ns | 0.0181 ns |  3.931 ns |  1.68 |    0.02 |      - |     - |     - |         - |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.766 ns | 0.0795 ns | 0.1165 ns |  7.685 ns |  3.33 |    0.07 |      - |     - |     - |         - |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 12.829 ns | 0.0445 ns | 0.0624 ns | 12.838 ns |  5.51 |    0.06 |      - |     - |     - |         - |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 17.597 ns | 0.0712 ns | 0.0998 ns | 17.605 ns |  7.55 |    0.07 |      - |     - |     - |         - |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 16.790 ns | 0.0516 ns | 0.0739 ns | 16.791 ns |  7.20 |    0.09 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.344 ns | 0.0258 ns | 0.0385 ns |  7.346 ns |  3.15 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.200 ns | 0.0270 ns | 0.0395 ns |  7.203 ns |  3.09 |    0.03 |      - |     - |     - |         - |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 11.296 ns | 0.2405 ns | 0.3525 ns | 11.047 ns |  4.84 |    0.15 |      - |     - |     - |         - |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 24.410 ns | 0.0705 ns | 0.1011 ns | 24.428 ns | 10.47 |    0.12 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 | 22.060 ns | 0.0562 ns | 0.0841 ns | 22.070 ns |  9.46 |    0.10 |      - |     - |     - |         - |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.030 ns | 0.0225 ns | 0.0315 ns |  9.028 ns |  3.88 |    0.03 | 0.0051 |     - |     - |      32 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 |  8.964 ns | 0.0223 ns | 0.0313 ns |  8.972 ns |  3.85 |    0.04 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.130 ns | 0.0226 ns | 0.0332 ns |  9.132 ns |  3.91 |    0.04 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 13.395 ns | 0.2019 ns | 0.3021 ns | 13.382 ns |  5.75 |    0.15 |      - |     - |     - |         - |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 16.767 ns | 0.3191 ns | 0.4474 ns | 17.009 ns |  7.20 |    0.21 |      - |     - |     - |         - |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 23.846 ns | 0.1368 ns | 0.2005 ns | 23.906 ns | 10.23 |    0.15 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 29.664 ns | 0.0956 ns | 0.1431 ns | 29.696 ns | 12.72 |    0.14 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 54.931 ns | 0.1518 ns | 0.2128 ns | 54.903 ns | 23.58 |    0.20 | 0.0446 |     - |     - |     281 B |
