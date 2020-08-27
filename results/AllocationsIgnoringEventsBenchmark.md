``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]          : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|               Method |             Job |       Jit |       Runtime |      Mean |     Error |     StdDev |    Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |---------------- |---------- |-------------- |----------:|----------:|-----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.499 ns | 0.1456 ns |  0.2135 ns |  2.346 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.354 ns | 0.0767 ns |  0.1124 ns |  2.300 ns |  0.95 |    0.12 |      - |     - |     - |         - |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  4.138 ns | 0.1165 ns |  0.1707 ns |  4.089 ns |  1.67 |    0.14 |      - |     - |     - |         - |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  4.413 ns | 0.0539 ns |  0.0790 ns |  4.387 ns |  1.78 |    0.16 |      - |     - |     - |         - |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  6.767 ns | 0.1673 ns |  0.2399 ns |  6.862 ns |  2.73 |    0.20 |      - |     - |     - |         - |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 12.852 ns | 0.7701 ns |  1.0796 ns | 12.198 ns |  5.24 |    0.82 |      - |     - |     - |         - |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 16.243 ns | 0.5385 ns |  0.7894 ns | 16.224 ns |  6.55 |    0.66 |      - |     - |     - |         - |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 20.212 ns | 0.6597 ns |  0.9462 ns | 19.974 ns |  8.19 |    1.02 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.355 ns | 0.0480 ns |  0.0718 ns |  5.357 ns |  2.16 |    0.19 |      - |     - |     - |         - |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.827 ns | 0.0513 ns |  0.0735 ns |  5.838 ns |  2.35 |    0.20 |      - |     - |     - |         - |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  8.693 ns | 0.3454 ns |  0.5063 ns |  8.522 ns |  3.49 |    0.24 |      - |     - |     - |         - |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 30.248 ns | 0.5646 ns |  0.8451 ns | 30.307 ns | 12.20 |    1.17 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 20.822 ns | 0.2103 ns |  0.3083 ns | 20.770 ns |  8.39 |    0.75 |      - |     - |     - |         - |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 14.857 ns | 2.5367 ns |  3.6381 ns | 13.299 ns |  6.08 |    1.88 | 0.0051 |     - |     - |      32 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 11.156 ns | 0.4845 ns |  0.7101 ns | 10.976 ns |  4.49 |    0.44 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 11.690 ns | 0.7046 ns |  1.0327 ns | 11.540 ns |  4.71 |    0.58 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 11.566 ns | 0.1068 ns |  0.1531 ns | 11.596 ns |  4.68 |    0.42 |      - |     - |     - |         - |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 16.486 ns | 0.7935 ns |  1.1631 ns | 17.070 ns |  6.68 |    1.00 |      - |     - |     - |         - |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 29.583 ns | 0.4314 ns |  0.6457 ns | 29.603 ns | 11.91 |    1.00 | 0.0216 |     - |     - |     136 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 34.658 ns | 0.4356 ns |  0.6520 ns | 34.733 ns | 13.97 |    1.23 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 64.197 ns | 0.8269 ns |  1.2377 ns | 64.032 ns | 25.87 |    2.16 | 0.0446 |     - |     - |     280 B |
|                      |                 |           |               |           |           |            |           |       |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.440 ns | 0.0361 ns |  0.0540 ns |  2.442 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.444 ns | 0.0509 ns |  0.0731 ns |  2.446 ns |  1.00 |    0.04 |      - |     - |     - |         - |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |  4.049 ns | 0.0412 ns |  0.0616 ns |  4.043 ns |  1.66 |    0.04 |      - |     - |     - |         - |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |  4.085 ns | 0.0459 ns |  0.0673 ns |  4.079 ns |  1.67 |    0.04 |      - |     - |     - |         - |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.594 ns | 0.1436 ns |  0.2149 ns |  7.544 ns |  3.11 |    0.12 |      - |     - |     - |         - |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 13.921 ns | 0.4723 ns |  0.6923 ns | 13.383 ns |  5.71 |    0.31 |      - |     - |     - |         - |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 17.865 ns | 0.3395 ns |  0.5081 ns | 17.873 ns |  7.33 |    0.29 |      - |     - |     - |         - |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 18.753 ns | 0.2207 ns |  0.3303 ns | 18.730 ns |  7.69 |    0.22 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.653 ns | 0.4325 ns |  0.6473 ns |  7.664 ns |  3.14 |    0.26 |      - |     - |     - |         - |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.180 ns | 0.0938 ns |  0.1404 ns |  7.161 ns |  2.94 |    0.07 |      - |     - |     - |         - |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 11.164 ns | 0.1071 ns |  0.1602 ns | 11.169 ns |  4.58 |    0.13 |      - |     - |     - |         - |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 30.695 ns | 1.1966 ns |  1.7911 ns | 30.043 ns | 12.59 |    0.82 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 | 24.067 ns | 0.6423 ns |  0.9614 ns | 23.876 ns |  9.87 |    0.50 |      - |     - |     - |         - |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.872 ns | 0.7627 ns |  1.1415 ns | 10.424 ns |  4.46 |    0.49 | 0.0051 |     - |     - |      32 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.700 ns | 0.5827 ns |  0.8541 ns | 10.434 ns |  4.38 |    0.33 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.552 ns | 0.1093 ns |  0.1496 ns | 10.543 ns |  4.32 |    0.14 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 15.652 ns | 1.4774 ns |  2.0711 ns | 14.655 ns |  6.40 |    0.79 |      - |     - |     - |         - |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 18.423 ns | 0.7717 ns |  1.0563 ns | 18.316 ns |  7.55 |    0.48 |      - |     - |     - |         - |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 29.550 ns | 0.9479 ns |  1.3895 ns | 29.517 ns | 12.12 |    0.72 | 0.0216 |     - |     - |     136 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 40.098 ns | 2.3792 ns |  3.5611 ns | 38.722 ns | 16.45 |    1.58 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 67.883 ns | 2.2409 ns |  3.1414 ns | 67.648 ns | 27.80 |    1.43 | 0.0446 |     - |     - |     281 B |
|                      |                 |           |               |           |           |            |           |       |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.531 ns | 0.0640 ns |  0.0855 ns |  2.497 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3.040 ns | 0.2513 ns |  0.3761 ns |  2.993 ns |  1.20 |    0.14 |      - |     - |     - |         - |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |  4.452 ns | 0.3170 ns |  0.4745 ns |  4.316 ns |  1.79 |    0.16 |      - |     - |     - |         - |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |  4.127 ns | 0.0319 ns |  0.0467 ns |  4.134 ns |  1.63 |    0.06 |      - |     - |     - |         - |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.624 ns | 0.0759 ns |  0.1113 ns |  7.603 ns |  3.02 |    0.11 |      - |     - |     - |         - |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 13.704 ns | 0.3061 ns |  0.4487 ns | 13.845 ns |  5.45 |    0.20 |      - |     - |     - |         - |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 17.500 ns | 0.1830 ns |  0.2739 ns | 17.481 ns |  6.91 |    0.28 |      - |     - |     - |         - |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 19.207 ns | 0.1278 ns |  0.1874 ns | 19.192 ns |  7.60 |    0.27 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.102 ns | 0.0673 ns |  0.0965 ns |  7.096 ns |  2.81 |    0.10 |      - |     - |     - |         - |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.522 ns | 0.1794 ns |  0.2630 ns |  7.541 ns |  2.96 |    0.17 |      - |     - |     - |         - |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 11.446 ns | 0.2942 ns |  0.4312 ns | 11.323 ns |  4.53 |    0.20 |      - |     - |     - |         - |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 34.035 ns | 1.3720 ns |  1.9234 ns | 33.358 ns | 13.50 |    0.97 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 | 26.583 ns | 0.8887 ns |  1.2745 ns | 26.906 ns | 10.46 |    0.58 |      - |     - |     - |         - |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 13.343 ns | 2.6336 ns |  3.7771 ns | 11.847 ns |  5.36 |    1.67 | 0.0051 |     - |     - |      32 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 | 14.237 ns | 2.2206 ns |  3.1847 ns | 13.093 ns |  5.67 |    1.46 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 | 12.890 ns | 0.9939 ns |  1.3268 ns | 12.224 ns |  5.09 |    0.46 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 16.472 ns | 0.9752 ns |  1.3985 ns | 16.027 ns |  6.51 |    0.64 |      - |     - |     - |         - |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 21.937 ns | 1.0883 ns |  1.5609 ns | 21.720 ns |  8.71 |    0.72 |      - |     - |     - |         - |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 40.061 ns | 3.5244 ns |  5.2751 ns | 40.055 ns | 16.16 |    2.11 | 0.0216 |     - |     - |     136 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 46.985 ns | 4.3044 ns |  6.3094 ns | 46.486 ns | 18.60 |    2.88 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 84.570 ns | 7.9177 ns | 11.3553 ns | 81.050 ns | 32.94 |    3.87 | 0.0446 |     - |     - |     281 B |
