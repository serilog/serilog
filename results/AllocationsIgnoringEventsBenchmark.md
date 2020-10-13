``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|               Method |             Job |       Jit |       Runtime |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |---------------- |---------- |-------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.493 ns | 0.0167 ns | 0.0239 ns |  2.491 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.332 ns | 0.1259 ns | 0.1806 ns |  2.478 ns |  0.94 |    0.07 |      - |     - |     - |         - |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  3.480 ns | 0.0181 ns | 0.0271 ns |  3.477 ns |  1.40 |    0.02 |      - |     - |     - |         - |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  3.652 ns | 0.2192 ns | 0.3000 ns |  3.439 ns |  1.46 |    0.12 |      - |     - |     - |         - |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.750 ns | 0.0191 ns | 0.0274 ns |  5.743 ns |  2.31 |    0.03 |      - |     - |     - |         - |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 11.572 ns | 0.5095 ns | 0.7468 ns | 12.086 ns |  4.63 |    0.30 |      - |     - |     - |         - |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 15.391 ns | 0.0918 ns | 0.1346 ns | 15.381 ns |  6.17 |    0.08 |      - |     - |     - |         - |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 18.392 ns | 0.1012 ns | 0.1514 ns | 18.375 ns |  7.38 |    0.08 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.478 ns | 0.2830 ns | 0.4235 ns |  5.484 ns |  2.19 |    0.17 |      - |     - |     - |         - |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.646 ns | 0.0819 ns | 0.1225 ns |  5.652 ns |  2.27 |    0.05 |      - |     - |     - |         - |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  8.365 ns | 0.1771 ns | 0.2425 ns |  8.363 ns |  3.35 |    0.10 |      - |     - |     - |         - |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 26.220 ns | 0.2493 ns | 0.3575 ns | 26.072 ns | 10.52 |    0.20 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 20.039 ns | 0.3257 ns | 0.4774 ns | 19.911 ns |  8.04 |    0.23 |      - |     - |     - |         - |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  8.732 ns | 0.0400 ns | 0.0573 ns |  8.718 ns |  3.50 |    0.05 | 0.0051 |     - |     - |      32 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  8.955 ns | 0.0513 ns | 0.0752 ns |  8.938 ns |  3.59 |    0.05 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.194 ns | 0.0491 ns | 0.0719 ns |  9.192 ns |  3.69 |    0.05 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 11.959 ns | 0.6178 ns | 0.9247 ns | 11.330 ns |  4.76 |    0.36 |      - |     - |     - |         - |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 14.719 ns | 0.4719 ns | 0.7064 ns | 14.558 ns |  5.88 |    0.28 |      - |     - |     - |         - |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 26.130 ns | 0.5501 ns | 0.8233 ns | 25.630 ns | 10.45 |    0.31 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 30.428 ns | 0.1538 ns | 0.2205 ns | 30.453 ns | 12.21 |    0.16 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 55.515 ns | 0.2684 ns | 0.3849 ns | 55.444 ns | 22.27 |    0.30 | 0.0446 |     - |     - |     280 B |
|                      |                 |           |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.523 ns | 0.1961 ns | 0.2812 ns |  2.427 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.400 ns | 0.0606 ns | 0.0908 ns |  2.362 ns |  0.96 |    0.09 |      - |     - |     - |         - |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3.971 ns | 0.0401 ns | 0.0601 ns |  3.957 ns |  1.59 |    0.14 |      - |     - |     - |         - |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3.964 ns | 0.0303 ns | 0.0445 ns |  3.945 ns |  1.59 |    0.14 |      - |     - |     - |         - |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.083 ns | 0.5238 ns | 0.7840 ns |  9.052 ns |  3.62 |    0.53 |      - |     - |     - |         - |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 13.404 ns | 0.3708 ns | 0.5319 ns | 13.590 ns |  5.35 |    0.42 |      - |     - |     - |         - |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 17.006 ns | 0.1295 ns | 0.1772 ns | 16.979 ns |  6.77 |    0.63 |      - |     - |     - |         - |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 17.947 ns | 0.8361 ns | 1.2514 ns | 17.462 ns |  7.15 |    0.33 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.725 ns | 0.1252 ns | 0.1874 ns |  7.720 ns |  3.09 |    0.27 |      - |     - |     - |         - |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.624 ns | 0.1032 ns | 0.1513 ns |  7.617 ns |  3.05 |    0.30 |      - |     - |     - |         - |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 11.355 ns | 0.1654 ns | 0.2318 ns | 11.301 ns |  4.53 |    0.37 |      - |     - |     - |         - |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 24.772 ns | 0.2081 ns | 0.2985 ns | 24.802 ns |  9.91 |    0.88 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 | 23.240 ns | 0.5358 ns | 0.8019 ns | 23.239 ns |  9.31 |    0.74 |      - |     - |     - |         - |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.371 ns | 0.1207 ns | 0.1806 ns |  9.348 ns |  3.75 |    0.34 | 0.0051 |     - |     - |      32 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.210 ns | 0.0703 ns | 0.1008 ns |  9.207 ns |  3.68 |    0.32 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.359 ns | 0.0884 ns | 0.1268 ns |  9.345 ns |  3.74 |    0.32 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 13.317 ns | 0.1395 ns | 0.2088 ns | 13.301 ns |  5.33 |    0.48 |      - |     - |     - |         - |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 17.518 ns | 0.3554 ns | 0.5097 ns | 17.474 ns |  7.02 |    0.73 |      - |     - |     - |         - |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 23.559 ns | 0.1877 ns | 0.2751 ns | 23.556 ns |  9.43 |    0.81 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 28.895 ns | 0.3125 ns | 0.4482 ns | 28.807 ns | 11.57 |    1.09 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 54.414 ns | 0.4330 ns | 0.6481 ns | 54.165 ns | 21.75 |    1.85 | 0.0446 |     - |     - |     281 B |
|                      |                 |           |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.359 ns | 0.0217 ns | 0.0304 ns |  2.354 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.373 ns | 0.0318 ns | 0.0466 ns |  2.350 ns |  1.01 |    0.02 |      - |     - |     - |         - |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3.977 ns | 0.0462 ns | 0.0677 ns |  3.968 ns |  1.69 |    0.03 |      - |     - |     - |         - |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3.965 ns | 0.0590 ns | 0.0847 ns |  3.965 ns |  1.68 |    0.04 |      - |     - |     - |         - |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  8.100 ns | 0.0228 ns | 0.0319 ns |  8.098 ns |  3.43 |    0.05 |      - |     - |     - |         - |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 12.931 ns | 0.2404 ns | 0.3370 ns | 13.153 ns |  5.48 |    0.13 |      - |     - |     - |         - |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 17.000 ns | 0.0845 ns | 0.1265 ns | 16.967 ns |  7.21 |    0.12 |      - |     - |     - |         - |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 16.801 ns | 0.0604 ns | 0.0846 ns | 16.799 ns |  7.12 |    0.10 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.328 ns | 0.0486 ns | 0.0728 ns |  7.326 ns |  3.11 |    0.05 |      - |     - |     - |         - |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.326 ns | 0.0321 ns | 0.0461 ns |  7.326 ns |  3.11 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.963 ns | 0.2753 ns | 0.4035 ns | 10.816 ns |  4.65 |    0.17 |      - |     - |     - |         - |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 23.868 ns | 0.0672 ns | 0.0985 ns | 23.847 ns | 10.12 |    0.14 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 | 24.437 ns | 2.0351 ns | 2.7857 ns | 26.850 ns | 10.37 |    1.23 |      - |     - |     - |         - |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.060 ns | 0.0297 ns | 0.0406 ns |  9.056 ns |  3.84 |    0.05 | 0.0051 |     - |     - |      32 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 |  8.974 ns | 0.0378 ns | 0.0554 ns |  8.969 ns |  3.81 |    0.06 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.115 ns | 0.0306 ns | 0.0448 ns |  9.111 ns |  3.86 |    0.05 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 12.881 ns | 0.0520 ns | 0.0778 ns | 12.872 ns |  5.46 |    0.08 |      - |     - |     - |         - |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 16.671 ns | 0.3462 ns | 0.4965 ns | 16.694 ns |  7.08 |    0.25 |      - |     - |     - |         - |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 23.090 ns | 0.2045 ns | 0.2799 ns | 23.168 ns |  9.79 |    0.14 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 27.872 ns | 0.1450 ns | 0.2033 ns | 27.848 ns | 11.82 |    0.19 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 52.843 ns | 0.1677 ns | 0.2351 ns | 52.851 ns | 22.40 |    0.32 | 0.0446 |     - |     - |     281 B |
