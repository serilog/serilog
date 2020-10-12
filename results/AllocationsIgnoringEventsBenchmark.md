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
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.755 ns | 0.0504 ns | 0.0755 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.743 ns | 0.0504 ns | 0.0755 ns |  1.00 |    0.03 |      - |     - |     - |         - |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  4.538 ns | 0.0823 ns | 0.1127 ns |  1.65 |    0.06 |      - |     - |     - |         - |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  4.229 ns | 0.0456 ns | 0.0682 ns |  1.54 |    0.05 |      - |     - |     - |         - |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  6.382 ns | 0.1035 ns | 0.1549 ns |  2.32 |    0.08 |      - |     - |     - |         - |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 11.974 ns | 0.3640 ns | 0.5221 ns |  4.35 |    0.22 |      - |     - |     - |         - |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 15.637 ns | 0.5002 ns | 0.7487 ns |  5.68 |    0.33 |      - |     - |     - |         - |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 20.034 ns | 0.2853 ns | 0.4270 ns |  7.28 |    0.20 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.349 ns | 0.0436 ns | 0.0639 ns |  1.94 |    0.05 |      - |     - |     - |         - |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.970 ns | 0.1569 ns | 0.2299 ns |  2.17 |    0.10 |      - |     - |     - |         - |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  8.639 ns | 0.0961 ns | 0.1409 ns |  3.14 |    0.10 |      - |     - |     - |         - |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 28.552 ns | 0.5158 ns | 0.7561 ns | 10.37 |    0.49 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 21.023 ns | 0.2494 ns | 0.3732 ns |  7.64 |    0.20 |      - |     - |     - |         - |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.325 ns | 0.1226 ns | 0.1836 ns |  3.39 |    0.08 | 0.0051 |     - |     - |      32 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.632 ns | 0.1237 ns | 0.1852 ns |  3.50 |    0.08 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.713 ns | 0.1540 ns | 0.2305 ns |  3.53 |    0.11 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 11.706 ns | 0.2982 ns | 0.4464 ns |  4.25 |    0.21 |      - |     - |     - |         - |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 14.817 ns | 0.1782 ns | 0.2668 ns |  5.38 |    0.20 |      - |     - |     - |         - |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 27.579 ns | 0.2017 ns | 0.3020 ns | 10.02 |    0.31 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 32.579 ns | 0.6765 ns | 1.0125 ns | 11.84 |    0.48 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 59.820 ns | 0.9851 ns | 1.4128 ns | 21.76 |    0.80 | 0.0446 |     - |     - |     280 B |
|                      |                 |           |               |           |           |           |       |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.845 ns | 0.0489 ns | 0.0732 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.848 ns | 0.0531 ns | 0.0794 ns |  1.00 |    0.04 |      - |     - |     - |         - |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |  4.569 ns | 0.0532 ns | 0.0797 ns |  1.61 |    0.05 |      - |     - |     - |         - |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3.844 ns | 0.0797 ns | 0.1194 ns |  1.35 |    0.06 |      - |     - |     - |         - |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  8.605 ns | 0.0998 ns | 0.1493 ns |  3.03 |    0.07 |      - |     - |     - |         - |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 14.750 ns | 0.6334 ns | 0.9084 ns |  5.19 |    0.34 |      - |     - |     - |         - |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 17.986 ns | 0.5916 ns | 0.8855 ns |  6.32 |    0.32 |      - |     - |     - |         - |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 18.331 ns | 0.2748 ns | 0.4114 ns |  6.45 |    0.21 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.362 ns | 0.0866 ns | 0.1296 ns |  2.59 |    0.09 |      - |     - |     - |         - |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  8.212 ns | 0.1473 ns | 0.2204 ns |  2.89 |    0.11 |      - |     - |     - |         - |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 12.355 ns | 0.4567 ns | 0.6836 ns |  4.35 |    0.29 |      - |     - |     - |         - |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 26.265 ns | 0.2389 ns | 0.3502 ns |  9.23 |    0.24 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 | 23.148 ns | 0.2682 ns | 0.4015 ns |  8.14 |    0.25 |      - |     - |     - |         - |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.309 ns | 0.5401 ns | 0.8084 ns |  3.63 |    0.30 | 0.0051 |     - |     - |      32 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.992 ns | 0.1367 ns | 0.2046 ns |  3.51 |    0.10 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.099 ns | 0.1149 ns | 0.1685 ns |  3.55 |    0.11 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 14.893 ns | 0.4281 ns | 0.6407 ns |  5.24 |    0.25 |      - |     - |     - |         - |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 17.123 ns | 0.1600 ns | 0.2346 ns |  6.02 |    0.19 |      - |     - |     - |         - |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 25.873 ns | 0.2654 ns | 0.3806 ns |  9.11 |    0.22 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 31.736 ns | 0.5606 ns | 0.8039 ns | 11.18 |    0.44 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 59.174 ns | 2.6093 ns | 3.7422 ns | 20.84 |    1.45 | 0.0446 |     - |     - |     281 B |
|                      |                 |           |               |           |           |           |       |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.906 ns | 0.0952 ns | 0.1395 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.836 ns | 0.0528 ns | 0.0790 ns |  0.98 |    0.06 |      - |     - |     - |         - |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |  4.555 ns | 0.0560 ns | 0.0838 ns |  1.57 |    0.08 |      - |     - |     - |         - |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3.944 ns | 0.0492 ns | 0.0736 ns |  1.36 |    0.07 |      - |     - |     - |         - |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  8.629 ns | 0.1111 ns | 0.1663 ns |  2.98 |    0.15 |      - |     - |     - |         - |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 14.088 ns | 0.0571 ns | 0.0837 ns |  4.86 |    0.22 |      - |     - |     - |         - |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 18.274 ns | 0.8485 ns | 1.2699 ns |  6.27 |    0.36 |      - |     - |     - |         - |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 18.337 ns | 0.3118 ns | 0.4667 ns |  6.32 |    0.33 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.390 ns | 0.1081 ns | 0.1618 ns |  2.55 |    0.13 |      - |     - |     - |         - |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  8.049 ns | 0.0852 ns | 0.1249 ns |  2.78 |    0.13 |      - |     - |     - |         - |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 11.517 ns | 0.1793 ns | 0.2683 ns |  3.97 |    0.24 |      - |     - |     - |         - |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 26.317 ns | 0.2322 ns | 0.3475 ns |  9.08 |    0.42 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 | 23.330 ns | 0.2466 ns | 0.3691 ns |  8.04 |    0.35 |      - |     - |     - |         - |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.104 ns | 0.1266 ns | 0.1895 ns |  3.48 |    0.17 | 0.0051 |     - |     - |      32 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.981 ns | 0.1140 ns | 0.1707 ns |  3.44 |    0.16 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.014 ns | 0.1050 ns | 0.1572 ns |  3.45 |    0.15 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 14.475 ns | 0.2175 ns | 0.3120 ns |  4.99 |    0.29 |      - |     - |     - |         - |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 17.131 ns | 0.2106 ns | 0.3152 ns |  5.91 |    0.30 |      - |     - |     - |         - |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 25.976 ns | 0.2111 ns | 0.3160 ns |  8.96 |    0.39 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 31.436 ns | 0.4363 ns | 0.6530 ns | 10.85 |    0.61 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 56.712 ns | 0.7581 ns | 1.1347 ns | 19.53 |    0.96 | 0.0446 |     - |     - |     281 B |
