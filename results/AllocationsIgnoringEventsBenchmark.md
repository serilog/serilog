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
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.542 ns | 0.1835 ns | 0.2746 ns |  2.525 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.297 ns | 0.0285 ns | 0.0399 ns |  2.299 ns |  0.90 |    0.10 |      - |     - |     - |         - |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  4.318 ns | 0.0308 ns | 0.0462 ns |  4.315 ns |  1.72 |    0.18 |      - |     - |     - |         - |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  4.228 ns | 0.0296 ns | 0.0443 ns |  4.224 ns |  1.68 |    0.18 |      - |     - |     - |         - |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  6.296 ns | 0.1313 ns | 0.1965 ns |  6.311 ns |  2.51 |    0.34 |      - |     - |     - |         - |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 11.842 ns | 0.1725 ns | 0.2582 ns | 11.876 ns |  4.70 |    0.46 |      - |     - |     - |         - |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 15.184 ns | 0.3866 ns | 0.5786 ns | 14.984 ns |  6.05 |    0.81 |      - |     - |     - |         - |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 19.408 ns | 0.1903 ns | 0.2848 ns | 19.439 ns |  7.72 |    0.81 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.343 ns | 0.0533 ns | 0.0764 ns |  5.350 ns |  2.11 |    0.25 |      - |     - |     - |         - |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  6.775 ns | 0.8072 ns | 1.1576 ns |  6.766 ns |  2.72 |    0.73 |      - |     - |     - |         - |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  8.496 ns | 0.0823 ns | 0.1180 ns |  8.470 ns |  3.35 |    0.33 |      - |     - |     - |         - |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 28.905 ns | 0.5125 ns | 0.7670 ns | 28.581 ns | 11.52 |    1.50 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 20.987 ns | 0.1341 ns | 0.2007 ns | 20.988 ns |  8.35 |    0.91 |      - |     - |     - |         - |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.584 ns | 0.1535 ns | 0.2152 ns |  9.559 ns |  3.77 |    0.43 | 0.0051 |     - |     - |      32 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.665 ns | 0.2117 ns | 0.3168 ns |  9.561 ns |  3.85 |    0.49 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.502 ns | 0.0763 ns | 0.1142 ns |  9.531 ns |  3.78 |    0.42 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 11.722 ns | 0.1845 ns | 0.2762 ns | 11.705 ns |  4.66 |    0.53 |      - |     - |     - |         - |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 16.503 ns | 0.1595 ns | 0.2338 ns | 16.464 ns |  6.54 |    0.73 |      - |     - |     - |         - |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 28.691 ns | 0.8684 ns | 1.2998 ns | 27.981 ns | 11.41 |    1.27 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 32.517 ns | 0.2768 ns | 0.4142 ns | 32.506 ns | 12.93 |    1.34 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 59.951 ns | 0.5544 ns | 0.8127 ns | 60.054 ns | 23.75 |    2.46 | 0.0446 |     - |     - |     280 B |
|                      |                 |           |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.837 ns | 0.0310 ns | 0.0464 ns |  2.840 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.842 ns | 0.0334 ns | 0.0500 ns |  2.835 ns |  1.00 |    0.01 |      - |     - |     - |         - |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |  4.547 ns | 0.0337 ns | 0.0505 ns |  4.555 ns |  1.60 |    0.03 |      - |     - |     - |         - |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3.887 ns | 0.0369 ns | 0.0553 ns |  3.901 ns |  1.37 |    0.03 |      - |     - |     - |         - |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  8.840 ns | 0.0546 ns | 0.0817 ns |  8.845 ns |  3.12 |    0.06 |      - |     - |     - |         - |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 14.436 ns | 0.2636 ns | 0.3864 ns | 14.317 ns |  5.09 |    0.15 |      - |     - |     - |         - |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 18.572 ns | 0.1470 ns | 0.2200 ns | 18.600 ns |  6.55 |    0.13 |      - |     - |     - |         - |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 18.331 ns | 0.0976 ns | 0.1461 ns | 18.314 ns |  6.46 |    0.10 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.867 ns | 0.0548 ns | 0.0820 ns |  7.862 ns |  2.77 |    0.05 |      - |     - |     - |         - |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  8.343 ns | 0.0640 ns | 0.0958 ns |  8.316 ns |  2.94 |    0.06 |      - |     - |     - |         - |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 11.964 ns | 0.2181 ns | 0.3197 ns | 11.770 ns |  4.22 |    0.12 |      - |     - |     - |         - |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 27.039 ns | 0.5076 ns | 0.7597 ns | 26.814 ns |  9.53 |    0.32 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 | 22.933 ns | 0.1544 ns | 0.2214 ns | 22.972 ns |  8.09 |    0.13 |      - |     - |     - |         - |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.307 ns | 0.2349 ns | 0.3516 ns | 10.195 ns |  3.63 |    0.13 | 0.0051 |     - |     - |      32 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.263 ns | 0.0732 ns | 0.1096 ns | 10.280 ns |  3.62 |    0.08 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.231 ns | 0.0873 ns | 0.1224 ns | 10.239 ns |  3.61 |    0.06 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 14.423 ns | 0.2999 ns | 0.4489 ns | 14.519 ns |  5.08 |    0.18 |      - |     - |     - |         - |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 18.155 ns | 0.2761 ns | 0.4133 ns | 18.143 ns |  6.40 |    0.18 |      - |     - |     - |         - |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 27.262 ns | 0.5058 ns | 0.7571 ns | 26.983 ns |  9.61 |    0.32 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 33.192 ns | 0.6620 ns | 0.9909 ns | 33.212 ns | 11.70 |    0.39 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 58.175 ns | 0.2199 ns | 0.3292 ns | 58.221 ns | 20.51 |    0.33 | 0.0446 |     - |     - |     281 B |
|                      |                 |           |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.804 ns | 0.0334 ns | 0.0500 ns |  2.801 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.833 ns | 0.0336 ns | 0.0502 ns |  2.833 ns |  1.01 |    0.02 |      - |     - |     - |         - |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |  4.543 ns | 0.0336 ns | 0.0503 ns |  4.545 ns |  1.62 |    0.03 |      - |     - |     - |         - |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3.877 ns | 0.0267 ns | 0.0391 ns |  3.881 ns |  1.38 |    0.03 |      - |     - |     - |         - |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  8.834 ns | 0.0733 ns | 0.1096 ns |  8.840 ns |  3.15 |    0.09 |      - |     - |     - |         - |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 14.019 ns | 0.1308 ns | 0.1876 ns | 13.973 ns |  5.00 |    0.12 |      - |     - |     - |         - |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 18.055 ns | 0.5604 ns | 0.8038 ns | 18.569 ns |  6.43 |    0.27 |      - |     - |     - |         - |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 18.330 ns | 0.1598 ns | 0.2392 ns | 18.292 ns |  6.54 |    0.11 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.915 ns | 0.1118 ns | 0.1639 ns |  7.887 ns |  2.82 |    0.07 |      - |     - |     - |         - |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  8.357 ns | 0.0617 ns | 0.0924 ns |  8.332 ns |  2.98 |    0.06 |      - |     - |     - |         - |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 11.292 ns | 0.0644 ns | 0.0924 ns | 11.326 ns |  4.03 |    0.08 |      - |     - |     - |         - |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 27.043 ns | 0.4900 ns | 0.7334 ns | 26.792 ns |  9.65 |    0.29 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 | 23.610 ns | 0.4469 ns | 0.6551 ns | 23.895 ns |  8.42 |    0.31 |      - |     - |     - |         - |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.409 ns | 0.1686 ns | 0.2524 ns | 10.337 ns |  3.71 |    0.12 | 0.0051 |     - |     - |      32 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.285 ns | 0.0769 ns | 0.1151 ns | 10.300 ns |  3.67 |    0.08 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.588 ns | 0.4105 ns | 0.5755 ns | 10.398 ns |  3.77 |    0.22 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 13.779 ns | 0.1186 ns | 0.1738 ns | 13.753 ns |  4.91 |    0.09 |      - |     - |     - |         - |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 17.423 ns | 0.1546 ns | 0.2314 ns | 17.363 ns |  6.22 |    0.12 |      - |     - |     - |         - |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 27.288 ns | 0.3562 ns | 0.5332 ns | 27.141 ns |  9.73 |    0.23 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 31.866 ns | 0.3283 ns | 0.4913 ns | 31.721 ns | 11.37 |    0.30 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 57.988 ns | 0.5698 ns | 0.8529 ns | 57.815 ns | 20.69 |    0.46 | 0.0446 |     - |     - |     281 B |
