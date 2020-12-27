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
|               Method |    Job |       Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |------- |-------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty | core31 | .NET Core 3.1 |  2.645 ns | 0.0349 ns | 0.0512 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |  2.659 ns | 0.0340 ns | 0.0509 ns |  1.01 |    0.03 |      - |     - |     - |         - |
|               LogMsg | core31 | .NET Core 3.1 |  3.708 ns | 0.0603 ns | 0.0884 ns |  1.40 |    0.04 |      - |     - |     - |         - |
|         LogMsgWithEx | core31 | .NET Core 3.1 |  3.665 ns | 0.0450 ns | 0.0673 ns |  1.39 |    0.04 |      - |     - |     - |         - |
|           LogScalar1 | core31 | .NET Core 3.1 |  6.045 ns | 0.1399 ns | 0.2094 ns |  2.29 |    0.09 |      - |     - |     - |         - |
|           LogScalar2 | core31 | .NET Core 3.1 | 10.988 ns | 0.0678 ns | 0.1014 ns |  4.16 |    0.09 |      - |     - |     - |         - |
|           LogScalar3 | core31 | .NET Core 3.1 | 14.375 ns | 0.1820 ns | 0.2724 ns |  5.44 |    0.09 |      - |     - |     - |         - |
|        LogScalarMany | core31 | .NET Core 3.1 | 19.038 ns | 0.1500 ns | 0.2245 ns |  7.20 |    0.18 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |  5.037 ns | 0.0309 ns | 0.0462 ns |  1.90 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |  5.586 ns | 0.1450 ns | 0.2170 ns |  2.12 |    0.10 |      - |     - |     - |         - |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |  8.326 ns | 0.0704 ns | 0.1053 ns |  3.15 |    0.08 |      - |     - |     - |         - |
|  LogScalarStructMany | core31 | .NET Core 3.1 | 26.446 ns | 0.2412 ns | 0.3535 ns | 10.00 |    0.23 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 | 20.107 ns | 0.1903 ns | 0.2848 ns |  7.61 |    0.19 |      - |     - |     - |         - |
|        LogDictionary | core31 | .NET Core 3.1 |  9.541 ns | 0.0906 ns | 0.1356 ns |  3.61 |    0.08 | 0.0051 |     - |     - |      32 B |
|          LogSequence | core31 | .NET Core 3.1 |  9.667 ns | 0.2009 ns | 0.2882 ns |  3.66 |    0.13 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  9.627 ns | 0.1103 ns | 0.1616 ns |  3.64 |    0.10 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | core31 | .NET Core 3.1 | 10.974 ns | 0.0698 ns | 0.1023 ns |  4.15 |    0.09 |      - |     - |     - |         - |
|              LogMix3 | core31 | .NET Core 3.1 | 14.761 ns | 0.2936 ns | 0.4394 ns |  5.57 |    0.18 |      - |     - |     - |         - |
|              LogMix4 | core31 | .NET Core 3.1 | 25.117 ns | 0.1721 ns | 0.2523 ns |  9.50 |    0.21 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | core31 | .NET Core 3.1 | 32.601 ns | 0.8641 ns | 1.2933 ns | 12.32 |    0.48 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | core31 | .NET Core 3.1 | 57.558 ns | 0.4309 ns | 0.6449 ns | 21.76 |    0.45 | 0.0446 |     - |     - |     280 B |
|                      |        |               |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |  2.738 ns | 0.0351 ns | 0.0525 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |  2.718 ns | 0.0404 ns | 0.0605 ns |  0.99 |    0.03 |      - |     - |     - |         - |
|               LogMsg |  net48 |      .NET 4.8 |  4.397 ns | 0.0361 ns | 0.0541 ns |  1.61 |    0.04 |      - |     - |     - |         - |
|         LogMsgWithEx |  net48 |      .NET 4.8 |  3.788 ns | 0.0427 ns | 0.0626 ns |  1.38 |    0.04 |      - |     - |     - |         - |
|           LogScalar1 |  net48 |      .NET 4.8 |  7.580 ns | 0.0766 ns | 0.1146 ns |  2.77 |    0.08 |      - |     - |     - |         - |
|           LogScalar2 |  net48 |      .NET 4.8 | 13.456 ns | 0.0729 ns | 0.1068 ns |  4.92 |    0.09 |      - |     - |     - |         - |
|           LogScalar3 |  net48 |      .NET 4.8 | 16.734 ns | 0.3001 ns | 0.4492 ns |  6.11 |    0.19 |      - |     - |     - |         - |
|        LogScalarMany |  net48 |      .NET 4.8 | 17.554 ns | 0.1831 ns | 0.2684 ns |  6.42 |    0.16 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |  6.572 ns | 0.0887 ns | 0.1328 ns |  2.40 |    0.07 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |  7.481 ns | 0.0703 ns | 0.1052 ns |  2.73 |    0.06 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net48 |      .NET 4.8 | 10.544 ns | 0.0666 ns | 0.0997 ns |  3.85 |    0.08 |      - |     - |     - |         - |
|  LogScalarStructMany |  net48 |      .NET 4.8 | 23.972 ns | 0.2061 ns | 0.3021 ns |  8.76 |    0.23 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 | 22.860 ns | 0.5316 ns | 0.7957 ns |  8.35 |    0.34 |      - |     - |     - |         - |
|        LogDictionary |  net48 |      .NET 4.8 |  9.682 ns | 0.0954 ns | 0.1427 ns |  3.54 |    0.08 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net48 |      .NET 4.8 |  9.684 ns | 0.0642 ns | 0.0961 ns |  3.54 |    0.08 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  9.767 ns | 0.0732 ns | 0.1095 ns |  3.57 |    0.08 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net48 |      .NET 4.8 | 14.222 ns | 0.1002 ns | 0.1499 ns |  5.20 |    0.10 |      - |     - |     - |         - |
|              LogMix3 |  net48 |      .NET 4.8 | 17.130 ns | 0.2075 ns | 0.3105 ns |  6.26 |    0.18 |      - |     - |     - |         - |
|              LogMix4 |  net48 |      .NET 4.8 | 24.872 ns | 0.1674 ns | 0.2505 ns |  9.09 |    0.22 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net48 |      .NET 4.8 | 30.212 ns | 0.2581 ns | 0.3863 ns | 11.04 |    0.23 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net48 |      .NET 4.8 | 54.441 ns | 0.3253 ns | 0.4870 ns | 19.89 |    0.39 | 0.0446 |     - |     - |     281 B |
|                      |        |               |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |  2.598 ns | 0.0363 ns | 0.0544 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |  2.630 ns | 0.0434 ns | 0.0636 ns |  1.01 |    0.03 |      - |     - |     - |         - |
|               LogMsg |  net50 | .NET Core 5.0 |  4.320 ns | 0.0453 ns | 0.0664 ns |  1.66 |    0.04 |      - |     - |     - |         - |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |  3.836 ns | 0.1242 ns | 0.1859 ns |  1.48 |    0.08 |      - |     - |     - |         - |
|           LogScalar1 |  net50 | .NET Core 5.0 |  6.152 ns | 0.0888 ns | 0.1330 ns |  2.37 |    0.08 |      - |     - |     - |         - |
|           LogScalar2 |  net50 | .NET Core 5.0 |  6.829 ns | 0.2610 ns | 0.3906 ns |  2.63 |    0.15 |      - |     - |     - |         - |
|           LogScalar3 |  net50 | .NET Core 5.0 | 11.672 ns | 0.0742 ns | 0.1110 ns |  4.49 |    0.08 |      - |     - |     - |         - |
|        LogScalarMany |  net50 | .NET Core 5.0 | 12.983 ns | 0.2542 ns | 0.3804 ns |  5.00 |    0.18 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |  5.602 ns | 0.2551 ns | 0.3818 ns |  2.16 |    0.15 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |  5.673 ns | 0.0440 ns | 0.0658 ns |  2.18 |    0.06 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |  8.600 ns | 0.1817 ns | 0.2664 ns |  3.31 |    0.14 |      - |     - |     - |         - |
|  LogScalarStructMany |  net50 | .NET Core 5.0 | 21.508 ns | 0.4829 ns | 0.7079 ns |  8.27 |    0.29 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |  8.175 ns | 0.1691 ns | 0.2426 ns |  3.14 |    0.11 |      - |     - |     - |         - |
|        LogDictionary |  net50 | .NET Core 5.0 |  7.960 ns | 0.1114 ns | 0.1632 ns |  3.06 |    0.07 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net50 | .NET Core 5.0 |  7.864 ns | 0.0817 ns | 0.1223 ns |  3.03 |    0.08 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  8.013 ns | 0.0919 ns | 0.1319 ns |  3.08 |    0.06 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net50 | .NET Core 5.0 |  6.454 ns | 0.0771 ns | 0.1154 ns |  2.49 |    0.08 |      - |     - |     - |         - |
|              LogMix3 |  net50 | .NET Core 5.0 | 12.107 ns | 0.3903 ns | 0.5842 ns |  4.66 |    0.24 |      - |     - |     - |         - |
|              LogMix4 |  net50 | .NET Core 5.0 | 19.848 ns | 0.2918 ns | 0.4277 ns |  7.63 |    0.23 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net50 | .NET Core 5.0 | 23.082 ns | 0.1321 ns | 0.1895 ns |  8.88 |    0.22 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net50 | .NET Core 5.0 | 46.981 ns | 0.6769 ns | 1.0132 ns | 18.09 |    0.55 | 0.0446 |     - |     - |     280 B |
