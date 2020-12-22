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
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.169 ns | 0.0165 ns | 0.0247 ns |  2.172 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.168 ns | 0.0200 ns | 0.0300 ns |  2.177 ns |  1.00 |    0.02 |      - |     - |     - |         - |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  3.720 ns | 0.0254 ns | 0.0373 ns |  3.717 ns |  1.71 |    0.03 |      - |     - |     - |         - |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  3.957 ns | 0.2088 ns | 0.3125 ns |  3.969 ns |  1.83 |    0.15 |      - |     - |     - |         - |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  6.549 ns | 0.1247 ns | 0.1867 ns |  6.556 ns |  3.02 |    0.10 |      - |     - |     - |         - |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 12.422 ns | 0.6690 ns | 1.0013 ns | 12.401 ns |  5.73 |    0.46 |      - |     - |     - |         - |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 15.208 ns | 0.3912 ns | 0.5855 ns | 14.976 ns |  7.01 |    0.30 |      - |     - |     - |         - |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 19.560 ns | 0.2012 ns | 0.3011 ns | 19.612 ns |  9.02 |    0.18 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.255 ns | 0.0374 ns | 0.0549 ns |  5.263 ns |  2.42 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.718 ns | 0.0418 ns | 0.0626 ns |  5.711 ns |  2.64 |    0.05 |      - |     - |     - |         - |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  8.740 ns | 0.1478 ns | 0.2167 ns |  8.824 ns |  4.03 |    0.11 |      - |     - |     - |         - |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 30.220 ns | 0.8338 ns | 1.2480 ns | 30.216 ns | 13.94 |    0.62 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 21.499 ns | 0.3548 ns | 0.5311 ns | 21.487 ns |  9.91 |    0.26 |      - |     - |     - |         - |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.417 ns | 0.1471 ns | 0.2202 ns |  9.467 ns |  4.34 |    0.12 | 0.0051 |     - |     - |      32 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.600 ns | 0.1175 ns | 0.1685 ns |  9.575 ns |  4.43 |    0.10 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.567 ns | 0.1395 ns | 0.2089 ns |  9.614 ns |  4.41 |    0.11 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 11.405 ns | 0.0849 ns | 0.1190 ns | 11.411 ns |  5.26 |    0.09 |      - |     - |     - |         - |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 15.307 ns | 0.3812 ns | 0.5706 ns | 15.324 ns |  7.06 |    0.29 |      - |     - |     - |         - |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 26.326 ns | 0.1659 ns | 0.2380 ns | 26.391 ns | 12.14 |    0.18 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 32.146 ns | 0.4044 ns | 0.6053 ns | 32.159 ns | 14.82 |    0.33 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 61.703 ns | 0.8723 ns | 1.3057 ns | 61.770 ns | 28.45 |    0.60 | 0.0446 |     - |     - |     280 B |
|                      |                 |           |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.470 ns | 0.0463 ns | 0.0692 ns |  2.461 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.463 ns | 0.0352 ns | 0.0526 ns |  2.447 ns |  1.00 |    0.04 |      - |     - |     - |         - |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |  4.106 ns | 0.0431 ns | 0.0645 ns |  4.111 ns |  1.66 |    0.05 |      - |     - |     - |         - |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |  4.122 ns | 0.0410 ns | 0.0614 ns |  4.125 ns |  1.67 |    0.06 |      - |     - |     - |         - |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.453 ns | 0.0685 ns | 0.1025 ns |  7.430 ns |  3.02 |    0.09 |      - |     - |     - |         - |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 13.316 ns | 0.1136 ns | 0.1700 ns | 13.312 ns |  5.39 |    0.13 |      - |     - |     - |         - |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 17.394 ns | 0.1641 ns | 0.2456 ns | 17.381 ns |  7.05 |    0.22 |      - |     - |     - |         - |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 18.059 ns | 0.2010 ns | 0.3009 ns | 18.022 ns |  7.32 |    0.22 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.419 ns | 0.2045 ns | 0.3061 ns |  7.427 ns |  3.01 |    0.15 |      - |     - |     - |         - |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.290 ns | 0.0983 ns | 0.1472 ns |  7.251 ns |  2.95 |    0.11 |      - |     - |     - |         - |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 11.232 ns | 0.0882 ns | 0.1321 ns | 11.228 ns |  4.55 |    0.16 |      - |     - |     - |         - |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 25.886 ns | 0.1913 ns | 0.2863 ns | 25.934 ns | 10.49 |    0.36 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 | 22.845 ns | 0.1506 ns | 0.2208 ns | 22.901 ns |  9.25 |    0.27 |      - |     - |     - |         - |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.618 ns | 0.1258 ns | 0.1882 ns |  9.630 ns |  3.90 |    0.13 | 0.0051 |     - |     - |      32 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.596 ns | 0.1022 ns | 0.1530 ns |  9.575 ns |  3.89 |    0.11 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.716 ns | 0.1030 ns | 0.1510 ns |  9.668 ns |  3.94 |    0.12 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 13.611 ns | 0.2099 ns | 0.3077 ns | 13.530 ns |  5.51 |    0.23 |      - |     - |     - |         - |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 17.589 ns | 0.3773 ns | 0.5648 ns | 17.597 ns |  7.13 |    0.28 |      - |     - |     - |         - |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 24.495 ns | 0.2028 ns | 0.3035 ns | 24.593 ns |  9.92 |    0.25 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 30.225 ns | 0.4019 ns | 0.6016 ns | 30.163 ns | 12.25 |    0.42 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 56.763 ns | 0.3181 ns | 0.4761 ns | 56.744 ns | 23.00 |    0.63 | 0.0446 |     - |     - |     281 B |
|                      |                 |           |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.450 ns | 0.0324 ns | 0.0485 ns |  2.441 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.466 ns | 0.0409 ns | 0.0613 ns |  2.461 ns |  1.01 |    0.02 |      - |     - |     - |         - |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |  4.105 ns | 0.0458 ns | 0.0685 ns |  4.114 ns |  1.68 |    0.04 |      - |     - |     - |         - |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |  4.138 ns | 0.0330 ns | 0.0483 ns |  4.155 ns |  1.69 |    0.04 |      - |     - |     - |         - |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.427 ns | 0.0704 ns | 0.1054 ns |  7.392 ns |  3.03 |    0.07 |      - |     - |     - |         - |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 13.369 ns | 0.1267 ns | 0.1857 ns | 13.392 ns |  5.45 |    0.10 |      - |     - |     - |         - |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 18.174 ns | 0.1475 ns | 0.2208 ns | 18.183 ns |  7.42 |    0.19 |      - |     - |     - |         - |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 17.999 ns | 0.1920 ns | 0.2874 ns | 17.925 ns |  7.35 |    0.16 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.233 ns | 0.0941 ns | 0.1408 ns |  7.234 ns |  2.95 |    0.08 |      - |     - |     - |         - |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.671 ns | 0.1715 ns | 0.2566 ns |  7.718 ns |  3.13 |    0.11 |      - |     - |     - |         - |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 11.871 ns | 0.4410 ns | 0.6601 ns | 11.883 ns |  4.84 |    0.25 |      - |     - |     - |         - |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 25.812 ns | 0.1719 ns | 0.2573 ns | 25.839 ns | 10.54 |    0.18 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 | 26.608 ns | 2.5493 ns | 3.8157 ns | 26.578 ns | 10.86 |    1.51 |      - |     - |     - |         - |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.652 ns | 0.1358 ns | 0.2033 ns |  9.691 ns |  3.94 |    0.11 | 0.0051 |     - |     - |      32 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.635 ns | 0.1276 ns | 0.1911 ns |  9.673 ns |  3.93 |    0.12 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.654 ns | 0.0889 ns | 0.1331 ns |  9.619 ns |  3.94 |    0.09 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 14.225 ns | 0.4777 ns | 0.7003 ns | 13.728 ns |  5.81 |    0.34 |      - |     - |     - |         - |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 16.978 ns | 0.1527 ns | 0.2238 ns | 16.976 ns |  6.93 |    0.16 |      - |     - |     - |         - |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 24.592 ns | 0.1855 ns | 0.2776 ns | 24.618 ns | 10.04 |    0.27 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 30.426 ns | 0.4216 ns | 0.6311 ns | 30.416 ns | 12.42 |    0.44 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 56.666 ns | 0.3797 ns | 0.5684 ns | 56.650 ns | 23.13 |    0.53 | 0.0446 |     - |     - |     281 B |
