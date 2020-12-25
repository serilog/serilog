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
|             LogEmpty | core31 | .NET Core 3.1 |  2.281 ns | 0.0150 ns | 0.0224 ns |  2.280 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |  2.277 ns | 0.0146 ns | 0.0214 ns |  2.283 ns |  1.00 |    0.01 |      - |     - |     - |         - |
|               LogMsg | core31 | .NET Core 3.1 |  3.726 ns | 0.0434 ns | 0.0650 ns |  3.737 ns |  1.63 |    0.03 |      - |     - |     - |         - |
|         LogMsgWithEx | core31 | .NET Core 3.1 |  4.159 ns | 0.0374 ns | 0.0560 ns |  4.161 ns |  1.82 |    0.03 |      - |     - |     - |         - |
|           LogScalar1 | core31 | .NET Core 3.1 |  5.976 ns | 0.0903 ns | 0.1324 ns |  5.916 ns |  2.62 |    0.06 |      - |     - |     - |         - |
|           LogScalar2 | core31 | .NET Core 3.1 | 12.052 ns | 0.1137 ns | 0.1702 ns | 12.061 ns |  5.28 |    0.07 |      - |     - |     - |         - |
|           LogScalar3 | core31 | .NET Core 3.1 | 14.658 ns | 0.1434 ns | 0.2147 ns | 14.663 ns |  6.43 |    0.12 |      - |     - |     - |         - |
|        LogScalarMany | core31 | .NET Core 3.1 | 19.453 ns | 0.1860 ns | 0.2785 ns | 19.496 ns |  8.53 |    0.12 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |  6.647 ns | 0.9170 ns | 1.3441 ns |  5.425 ns |  2.92 |    0.60 |      - |     - |     - |         - |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |  5.881 ns | 0.0378 ns | 0.0566 ns |  5.875 ns |  2.58 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |  8.586 ns | 0.1006 ns | 0.1442 ns |  8.576 ns |  3.77 |    0.07 |      - |     - |     - |         - |
|  LogScalarStructMany | core31 | .NET Core 3.1 | 27.569 ns | 0.1772 ns | 0.2541 ns | 27.533 ns | 12.10 |    0.13 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 | 21.279 ns | 0.3286 ns | 0.4919 ns | 21.219 ns |  9.33 |    0.20 |      - |     - |     - |         - |
|        LogDictionary | core31 | .NET Core 3.1 |  9.336 ns | 0.1228 ns | 0.1837 ns |  9.338 ns |  4.09 |    0.08 | 0.0051 |     - |     - |      32 B |
|          LogSequence | core31 | .NET Core 3.1 |  9.403 ns | 0.1320 ns | 0.1935 ns |  9.377 ns |  4.13 |    0.09 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  9.354 ns | 0.2043 ns | 0.3057 ns |  9.343 ns |  4.10 |    0.15 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | core31 | .NET Core 3.1 | 11.663 ns | 0.2691 ns | 0.3859 ns | 11.847 ns |  5.12 |    0.19 |      - |     - |     - |         - |
|              LogMix3 | core31 | .NET Core 3.1 | 15.136 ns | 0.3054 ns | 0.4571 ns | 15.249 ns |  6.64 |    0.18 |      - |     - |     - |         - |
|              LogMix4 | core31 | .NET Core 3.1 | 27.432 ns | 0.1647 ns | 0.2466 ns | 27.425 ns | 12.03 |    0.13 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | core31 | .NET Core 3.1 | 32.385 ns | 0.4316 ns | 0.6460 ns | 32.355 ns | 14.20 |    0.27 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | core31 | .NET Core 3.1 | 59.234 ns | 0.5508 ns | 0.8243 ns | 58.962 ns | 25.97 |    0.47 | 0.0446 |     - |     - |     280 B |
|                      |        |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |  2.433 ns | 0.0396 ns | 0.0592 ns |  2.417 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |  2.431 ns | 0.0339 ns | 0.0508 ns |  2.424 ns |  1.00 |    0.03 |      - |     - |     - |         - |
|               LogMsg |  net48 |      .NET 4.8 |  4.081 ns | 0.0405 ns | 0.0606 ns |  4.094 ns |  1.68 |    0.04 |      - |     - |     - |         - |
|         LogMsgWithEx |  net48 |      .NET 4.8 |  4.072 ns | 0.0446 ns | 0.0667 ns |  4.068 ns |  1.67 |    0.04 |      - |     - |     - |         - |
|           LogScalar1 |  net48 |      .NET 4.8 |  7.704 ns | 0.2573 ns | 0.3690 ns |  7.964 ns |  3.17 |    0.17 |      - |     - |     - |         - |
|           LogScalar2 |  net48 |      .NET 4.8 | 13.490 ns | 0.2491 ns | 0.3729 ns | 13.400 ns |  5.55 |    0.22 |      - |     - |     - |         - |
|           LogScalar3 |  net48 |      .NET 4.8 | 17.134 ns | 0.1354 ns | 0.2027 ns | 17.122 ns |  7.04 |    0.17 |      - |     - |     - |         - |
|        LogScalarMany |  net48 |      .NET 4.8 | 17.716 ns | 0.2136 ns | 0.3197 ns | 17.766 ns |  7.28 |    0.22 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |  7.078 ns | 0.0706 ns | 0.1057 ns |  7.060 ns |  2.91 |    0.09 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |  7.495 ns | 0.2090 ns | 0.3129 ns |  7.483 ns |  3.08 |    0.15 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net48 |      .NET 4.8 | 11.136 ns | 0.0566 ns | 0.0811 ns | 11.148 ns |  4.58 |    0.12 |      - |     - |     - |         - |
|  LogScalarStructMany |  net48 |      .NET 4.8 | 25.528 ns | 0.1872 ns | 0.2802 ns | 25.554 ns | 10.50 |    0.30 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 | 22.635 ns | 0.1417 ns | 0.2121 ns | 22.683 ns |  9.31 |    0.20 |      - |     - |     - |         - |
|        LogDictionary |  net48 |      .NET 4.8 |  9.567 ns | 0.1300 ns | 0.1864 ns |  9.557 ns |  3.93 |    0.11 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net48 |      .NET 4.8 |  9.568 ns | 0.0830 ns | 0.1242 ns |  9.587 ns |  3.93 |    0.11 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  9.669 ns | 0.0761 ns | 0.1140 ns |  9.651 ns |  3.98 |    0.11 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net48 |      .NET 4.8 | 13.983 ns | 0.4466 ns | 0.6546 ns | 13.758 ns |  5.75 |    0.28 |      - |     - |     - |         - |
|              LogMix3 |  net48 |      .NET 4.8 | 16.787 ns | 0.1397 ns | 0.2091 ns | 16.745 ns |  6.90 |    0.18 |      - |     - |     - |         - |
|              LogMix4 |  net48 |      .NET 4.8 | 24.646 ns | 0.3234 ns | 0.4841 ns | 24.590 ns | 10.13 |    0.31 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net48 |      .NET 4.8 | 29.890 ns | 0.2889 ns | 0.4325 ns | 29.780 ns | 12.29 |    0.35 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net48 |      .NET 4.8 | 55.960 ns | 0.2716 ns | 0.3981 ns | 55.844 ns | 23.01 |    0.59 | 0.0446 |     - |     - |     281 B |
|                      |        |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |  2.710 ns | 0.0355 ns | 0.0531 ns |  2.703 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |  2.706 ns | 0.0383 ns | 0.0573 ns |  2.702 ns |  1.00 |    0.03 |      - |     - |     - |         - |
|               LogMsg |  net50 | .NET Core 5.0 |  3.748 ns | 0.0393 ns | 0.0588 ns |  3.757 ns |  1.38 |    0.03 |      - |     - |     - |         - |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |  3.763 ns | 0.1196 ns | 0.1789 ns |  3.737 ns |  1.39 |    0.08 |      - |     - |     - |         - |
|           LogScalar1 |  net50 | .NET Core 5.0 |  7.189 ns | 0.0728 ns | 0.1090 ns |  7.177 ns |  2.65 |    0.05 |      - |     - |     - |         - |
|           LogScalar2 |  net50 | .NET Core 5.0 |  7.416 ns | 0.0715 ns | 0.1070 ns |  7.414 ns |  2.74 |    0.07 |      - |     - |     - |         - |
|           LogScalar3 |  net50 | .NET Core 5.0 | 11.981 ns | 0.0658 ns | 0.0985 ns | 11.972 ns |  4.42 |    0.08 |      - |     - |     - |         - |
|        LogScalarMany |  net50 | .NET Core 5.0 | 12.393 ns | 0.0898 ns | 0.1344 ns | 12.408 ns |  4.57 |    0.11 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |  5.676 ns | 0.2018 ns | 0.3020 ns |  5.669 ns |  2.10 |    0.13 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |  5.792 ns | 0.0291 ns | 0.0436 ns |  5.795 ns |  2.14 |    0.05 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |  9.054 ns | 0.0783 ns | 0.1172 ns |  9.046 ns |  3.34 |    0.06 |      - |     - |     - |         - |
|  LogScalarStructMany |  net50 | .NET Core 5.0 | 21.756 ns | 0.1986 ns | 0.2973 ns | 21.906 ns |  8.03 |    0.22 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |  8.600 ns | 0.3924 ns | 0.5873 ns |  8.596 ns |  3.18 |    0.24 |      - |     - |     - |         - |
|        LogDictionary |  net50 | .NET Core 5.0 |  7.877 ns | 0.1508 ns | 0.2257 ns |  7.962 ns |  2.91 |    0.09 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net50 | .NET Core 5.0 |  7.688 ns | 0.1027 ns | 0.1537 ns |  7.697 ns |  2.84 |    0.07 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  7.742 ns | 0.0814 ns | 0.1218 ns |  7.706 ns |  2.86 |    0.08 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net50 | .NET Core 5.0 |  7.406 ns | 0.0985 ns | 0.1444 ns |  7.359 ns |  2.74 |    0.07 |      - |     - |     - |         - |
|              LogMix3 |  net50 | .NET Core 5.0 | 12.393 ns | 0.0680 ns | 0.0996 ns | 12.432 ns |  4.58 |    0.09 |      - |     - |     - |         - |
|              LogMix4 |  net50 | .NET Core 5.0 | 20.532 ns | 0.5746 ns | 0.8600 ns | 20.496 ns |  7.58 |    0.32 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net50 | .NET Core 5.0 | 24.046 ns | 0.4325 ns | 0.6473 ns | 24.100 ns |  8.87 |    0.25 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net50 | .NET Core 5.0 | 47.783 ns | 0.5313 ns | 0.7953 ns | 47.725 ns | 17.64 |    0.52 | 0.0446 |     - |     - |     280 B |
