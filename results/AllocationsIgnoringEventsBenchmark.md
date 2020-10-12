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
|               Method |             Job |       Jit |       Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |---------------- |---------- |-------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.372 ns | 0.1846 ns | 0.2764 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.134 ns | 0.0299 ns | 0.0447 ns |  0.91 |    0.11 |      - |     - |     - |         - |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  3.685 ns | 0.1414 ns | 0.2116 ns |  1.57 |    0.17 |      - |     - |     - |         - |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  3.492 ns | 0.0654 ns | 0.0979 ns |  1.49 |    0.18 |      - |     - |     - |         - |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  6.470 ns | 0.1426 ns | 0.2134 ns |  2.77 |    0.35 |      - |     - |     - |         - |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 11.170 ns | 0.1868 ns | 0.2796 ns |  4.78 |    0.61 |      - |     - |     - |         - |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 15.248 ns | 0.7094 ns | 1.0618 ns |  6.56 |    1.17 |      - |     - |     - |         - |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 19.103 ns | 0.2794 ns | 0.4182 ns |  8.16 |    0.99 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.090 ns | 0.0671 ns | 0.1004 ns |  2.17 |    0.26 |      - |     - |     - |         - |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.506 ns | 0.0570 ns | 0.0854 ns |  2.35 |    0.27 |      - |     - |     - |         - |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  8.783 ns | 0.2276 ns | 0.3406 ns |  3.74 |    0.32 |      - |     - |     - |         - |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 27.729 ns | 0.2738 ns | 0.4098 ns | 11.85 |    1.44 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 20.500 ns | 0.4712 ns | 0.7052 ns |  8.74 |    0.89 |      - |     - |     - |         - |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.130 ns | 0.1832 ns | 0.2742 ns |  3.90 |    0.43 | 0.0051 |     - |     - |      32 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.032 ns | 0.1304 ns | 0.1952 ns |  3.86 |    0.44 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.096 ns | 0.1444 ns | 0.2161 ns |  3.88 |    0.44 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 11.077 ns | 0.1566 ns | 0.2344 ns |  4.73 |    0.58 |      - |     - |     - |         - |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 15.430 ns | 0.9487 ns | 1.4200 ns |  6.65 |    1.33 |      - |     - |     - |         - |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 26.028 ns | 0.3459 ns | 0.5177 ns | 11.10 |    1.15 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 30.740 ns | 0.5635 ns | 0.8435 ns | 13.13 |    1.57 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 58.509 ns | 0.8209 ns | 1.2286 ns | 24.99 |    2.92 | 0.0446 |     - |     - |     280 B |
|                      |                 |           |               |           |           |           |       |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.385 ns | 0.0410 ns | 0.0614 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.386 ns | 0.0226 ns | 0.0339 ns |  1.00 |    0.02 |      - |     - |     - |         - |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |  4.001 ns | 0.0653 ns | 0.0977 ns |  1.68 |    0.07 |      - |     - |     - |         - |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |  4.067 ns | 0.1382 ns | 0.2068 ns |  1.71 |    0.09 |      - |     - |     - |         - |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  8.303 ns | 0.1242 ns | 0.1859 ns |  3.48 |    0.12 |      - |     - |     - |         - |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 13.294 ns | 0.2172 ns | 0.3250 ns |  5.58 |    0.18 |      - |     - |     - |         - |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 17.003 ns | 0.2444 ns | 0.3658 ns |  7.13 |    0.23 |      - |     - |     - |         - |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 17.310 ns | 0.3131 ns | 0.4686 ns |  7.26 |    0.26 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.676 ns | 0.1647 ns | 0.2465 ns |  3.22 |    0.14 |      - |     - |     - |         - |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.595 ns | 0.1406 ns | 0.2105 ns |  3.19 |    0.11 |      - |     - |     - |         - |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 11.129 ns | 0.1463 ns | 0.2189 ns |  4.67 |    0.15 |      - |     - |     - |         - |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 24.864 ns | 0.6755 ns | 0.9688 ns | 10.43 |    0.46 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 | 22.988 ns | 0.6269 ns | 0.9383 ns |  9.64 |    0.48 |      - |     - |     - |         - |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.399 ns | 0.1299 ns | 0.1945 ns |  3.94 |    0.16 | 0.0051 |     - |     - |      32 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.335 ns | 0.1525 ns | 0.2283 ns |  3.92 |    0.17 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.719 ns | 0.1874 ns | 0.2805 ns |  4.08 |    0.17 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 13.729 ns | 0.2402 ns | 0.3595 ns |  5.76 |    0.27 |      - |     - |     - |         - |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 16.890 ns | 0.3636 ns | 0.5442 ns |  7.09 |    0.29 |      - |     - |     - |         - |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 23.647 ns | 0.3012 ns | 0.4509 ns |  9.92 |    0.36 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 29.003 ns | 0.5369 ns | 0.8036 ns | 12.16 |    0.36 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 55.247 ns | 1.5882 ns | 2.3280 ns | 23.16 |    1.18 | 0.0446 |     - |     - |     281 B |
|                      |                 |           |               |           |           |           |       |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.371 ns | 0.0660 ns | 0.0989 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.363 ns | 0.0423 ns | 0.0633 ns |  1.00 |    0.06 |      - |     - |     - |         - |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3.977 ns | 0.0677 ns | 0.1014 ns |  1.68 |    0.07 |      - |     - |     - |         - |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |  4.003 ns | 0.0622 ns | 0.0931 ns |  1.69 |    0.06 |      - |     - |     - |         - |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  8.295 ns | 0.0743 ns | 0.1089 ns |  3.51 |    0.15 |      - |     - |     - |         - |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 14.324 ns | 0.0989 ns | 0.1481 ns |  6.05 |    0.26 |      - |     - |     - |         - |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 17.287 ns | 0.3884 ns | 0.5813 ns |  7.30 |    0.37 |      - |     - |     - |         - |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 17.323 ns | 0.3364 ns | 0.5036 ns |  7.32 |    0.35 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.523 ns | 0.1068 ns | 0.1598 ns |  3.18 |    0.15 |      - |     - |     - |         - |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  8.440 ns | 0.6450 ns | 0.9654 ns |  3.57 |    0.46 |      - |     - |     - |         - |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 11.336 ns | 0.1661 ns | 0.2486 ns |  4.79 |    0.25 |      - |     - |     - |         - |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 24.665 ns | 0.3010 ns | 0.4505 ns | 10.42 |    0.45 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 | 23.137 ns | 0.4807 ns | 0.7195 ns |  9.77 |    0.42 |      - |     - |     - |         - |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.442 ns | 0.2190 ns | 0.3278 ns |  3.99 |    0.23 | 0.0051 |     - |     - |      32 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.291 ns | 0.1544 ns | 0.2263 ns |  3.93 |    0.14 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.396 ns | 0.1390 ns | 0.2080 ns |  3.97 |    0.16 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 13.197 ns | 0.1043 ns | 0.1561 ns |  5.57 |    0.24 |      - |     - |     - |         - |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 17.103 ns | 0.4265 ns | 0.6384 ns |  7.22 |    0.39 |      - |     - |     - |         - |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 23.593 ns | 0.2591 ns | 0.3878 ns |  9.97 |    0.44 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 29.095 ns | 0.5579 ns | 0.8350 ns | 12.29 |    0.52 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 54.349 ns | 0.5267 ns | 0.7884 ns | 22.96 |    1.01 | 0.0446 |     - |     - |     281 B |
