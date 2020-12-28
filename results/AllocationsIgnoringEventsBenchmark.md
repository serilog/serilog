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
|             LogEmpty | core31 | .NET Core 3.1 |  2.673 ns | 0.0394 ns | 0.0590 ns |  2.667 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |  2.445 ns | 0.1578 ns | 0.2363 ns |  2.431 ns |  0.92 |    0.09 |      - |     - |     - |         - |
|               LogMsg | core31 | .NET Core 3.1 |  3.896 ns | 0.0658 ns | 0.0984 ns |  3.902 ns |  1.46 |    0.04 |      - |     - |     - |         - |
|         LogMsgWithEx | core31 | .NET Core 3.1 |  3.825 ns | 0.2879 ns | 0.4309 ns |  3.802 ns |  1.43 |    0.16 |      - |     - |     - |         - |
|           LogScalar1 | core31 | .NET Core 3.1 |  5.953 ns | 0.0403 ns | 0.0603 ns |  5.971 ns |  2.23 |    0.05 |      - |     - |     - |         - |
|           LogScalar2 | core31 | .NET Core 3.1 | 11.229 ns | 0.0908 ns | 0.1359 ns | 11.241 ns |  4.20 |    0.11 |      - |     - |     - |         - |
|           LogScalar3 | core31 | .NET Core 3.1 | 14.761 ns | 0.3881 ns | 0.5809 ns | 14.565 ns |  5.52 |    0.27 |      - |     - |     - |         - |
|        LogScalarMany | core31 | .NET Core 3.1 | 19.473 ns | 0.2709 ns | 0.4054 ns | 19.457 ns |  7.29 |    0.20 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |  5.456 ns | 0.0731 ns | 0.1095 ns |  5.453 ns |  2.04 |    0.06 |      - |     - |     - |         - |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |  5.714 ns | 0.0405 ns | 0.0606 ns |  5.728 ns |  2.14 |    0.05 |      - |     - |     - |         - |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |  8.664 ns | 0.2213 ns | 0.3243 ns |  8.631 ns |  3.24 |    0.14 |      - |     - |     - |         - |
|  LogScalarStructMany | core31 | .NET Core 3.1 | 27.337 ns | 0.2848 ns | 0.4175 ns | 27.283 ns | 10.23 |    0.32 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 | 20.562 ns | 0.1685 ns | 0.2521 ns | 20.561 ns |  7.69 |    0.18 |      - |     - |     - |         - |
|        LogDictionary | core31 | .NET Core 3.1 |  9.222 ns | 0.0945 ns | 0.1414 ns |  9.223 ns |  3.45 |    0.11 | 0.0051 |     - |     - |      32 B |
|          LogSequence | core31 | .NET Core 3.1 |  9.204 ns | 0.1512 ns | 0.2264 ns |  9.171 ns |  3.44 |    0.11 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  9.181 ns | 0.2297 ns | 0.3439 ns |  9.225 ns |  3.44 |    0.15 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | core31 | .NET Core 3.1 | 11.112 ns | 0.0866 ns | 0.1270 ns | 11.124 ns |  4.16 |    0.10 |      - |     - |     - |         - |
|              LogMix3 | core31 | .NET Core 3.1 | 14.428 ns | 0.1544 ns | 0.2310 ns | 14.382 ns |  5.40 |    0.19 |      - |     - |     - |         - |
|              LogMix4 | core31 | .NET Core 3.1 | 26.809 ns | 0.1615 ns | 0.2417 ns | 26.863 ns | 10.03 |    0.23 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | core31 | .NET Core 3.1 | 31.517 ns | 0.4602 ns | 0.6888 ns | 31.463 ns | 11.79 |    0.33 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | core31 | .NET Core 3.1 | 58.336 ns | 0.7035 ns | 0.9862 ns | 58.395 ns | 21.81 |    0.59 | 0.0446 |     - |     - |     280 B |
|                      |        |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |  2.770 ns | 0.0385 ns | 0.0564 ns |  2.765 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |  2.760 ns | 0.0443 ns | 0.0663 ns |  2.749 ns |  1.00 |    0.04 |      - |     - |     - |         - |
|               LogMsg |  net48 |      .NET 4.8 |  3.781 ns | 0.0483 ns | 0.0723 ns |  3.783 ns |  1.37 |    0.04 |      - |     - |     - |         - |
|         LogMsgWithEx |  net48 |      .NET 4.8 |  4.235 ns | 0.0377 ns | 0.0564 ns |  4.232 ns |  1.53 |    0.04 |      - |     - |     - |         - |
|           LogScalar1 |  net48 |      .NET 4.8 |  7.469 ns | 0.1736 ns | 0.2544 ns |  7.472 ns |  2.70 |    0.11 |      - |     - |     - |         - |
|           LogScalar2 |  net48 |      .NET 4.8 | 13.266 ns | 0.2517 ns | 0.3768 ns | 13.305 ns |  4.80 |    0.18 |      - |     - |     - |         - |
|           LogScalar3 |  net48 |      .NET 4.8 | 17.390 ns | 0.1959 ns | 0.2933 ns | 17.395 ns |  6.28 |    0.18 |      - |     - |     - |         - |
|        LogScalarMany |  net48 |      .NET 4.8 | 18.120 ns | 0.2079 ns | 0.3112 ns | 18.118 ns |  6.55 |    0.21 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |  7.064 ns | 0.2215 ns | 0.3176 ns |  7.126 ns |  2.55 |    0.12 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |  7.540 ns | 0.0805 ns | 0.1205 ns |  7.535 ns |  2.73 |    0.08 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net48 |      .NET 4.8 | 10.733 ns | 0.0717 ns | 0.1074 ns | 10.709 ns |  3.88 |    0.08 |      - |     - |     - |         - |
|  LogScalarStructMany |  net48 |      .NET 4.8 | 26.640 ns | 0.1502 ns | 0.2248 ns | 26.671 ns |  9.63 |    0.20 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 | 22.213 ns | 0.1805 ns | 0.2702 ns | 22.224 ns |  8.02 |    0.18 |      - |     - |     - |         - |
|        LogDictionary |  net48 |      .NET 4.8 |  9.487 ns | 0.1067 ns | 0.1597 ns |  9.495 ns |  3.43 |    0.09 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net48 |      .NET 4.8 |  9.501 ns | 0.1125 ns | 0.1684 ns |  9.492 ns |  3.43 |    0.09 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  9.630 ns | 0.1001 ns | 0.1468 ns |  9.679 ns |  3.48 |    0.10 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net48 |      .NET 4.8 | 13.636 ns | 0.2761 ns | 0.4133 ns | 13.654 ns |  4.92 |    0.16 |      - |     - |     - |         - |
|              LogMix3 |  net48 |      .NET 4.8 | 17.694 ns | 0.8686 ns | 1.3001 ns | 17.826 ns |  6.37 |    0.50 |      - |     - |     - |         - |
|              LogMix4 |  net48 |      .NET 4.8 | 25.142 ns | 0.1683 ns | 0.2519 ns | 25.200 ns |  9.08 |    0.19 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net48 |      .NET 4.8 | 30.584 ns | 0.3645 ns | 0.5456 ns | 30.713 ns | 11.04 |    0.33 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net48 |      .NET 4.8 | 54.090 ns | 0.3114 ns | 0.4565 ns | 54.040 ns | 19.54 |    0.44 | 0.0446 |     - |     - |     281 B |
|                      |        |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |  2.270 ns | 0.0170 ns | 0.0255 ns |  2.271 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |  2.450 ns | 0.1359 ns | 0.2034 ns |  2.435 ns |  1.08 |    0.09 |      - |     - |     - |         - |
|               LogMsg |  net50 | .NET Core 5.0 |  3.637 ns | 0.0429 ns | 0.0643 ns |  3.658 ns |  1.60 |    0.03 |      - |     - |     - |         - |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |  3.543 ns | 0.0420 ns | 0.0629 ns |  3.552 ns |  1.56 |    0.04 |      - |     - |     - |         - |
|           LogScalar1 |  net50 | .NET Core 5.0 |  6.365 ns | 0.1153 ns | 0.1726 ns |  6.363 ns |  2.80 |    0.08 |      - |     - |     - |         - |
|           LogScalar2 |  net50 | .NET Core 5.0 |  6.750 ns | 0.1009 ns | 0.1510 ns |  6.714 ns |  2.97 |    0.07 |      - |     - |     - |         - |
|           LogScalar3 |  net50 | .NET Core 5.0 | 11.818 ns | 0.0878 ns | 0.1313 ns | 11.846 ns |  5.21 |    0.09 |      - |     - |     - |         - |
|        LogScalarMany |  net50 | .NET Core 5.0 | 12.690 ns | 0.0884 ns | 0.1324 ns | 12.710 ns |  5.59 |    0.08 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |  5.272 ns | 0.0346 ns | 0.0507 ns |  5.281 ns |  2.32 |    0.03 |      - |     - |     - |         - |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |  5.945 ns | 0.1634 ns | 0.2343 ns |  5.833 ns |  2.62 |    0.10 |      - |     - |     - |         - |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |  8.469 ns | 0.0885 ns | 0.1270 ns |  8.478 ns |  3.73 |    0.07 |      - |     - |     - |         - |
|  LogScalarStructMany |  net50 | .NET Core 5.0 | 21.928 ns | 0.8135 ns | 1.1667 ns | 21.701 ns |  9.66 |    0.51 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |  7.979 ns | 0.0944 ns | 0.1413 ns |  8.014 ns |  3.51 |    0.07 |      - |     - |     - |         - |
|        LogDictionary |  net50 | .NET Core 5.0 |  8.226 ns | 0.1080 ns | 0.1583 ns |  8.225 ns |  3.62 |    0.09 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  net50 | .NET Core 5.0 |  8.122 ns | 0.0739 ns | 0.1105 ns |  8.107 ns |  3.58 |    0.06 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  8.360 ns | 0.1647 ns | 0.2465 ns |  8.367 ns |  3.68 |    0.11 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |  net50 | .NET Core 5.0 |  6.750 ns | 0.0908 ns | 0.1359 ns |  6.747 ns |  2.97 |    0.06 |      - |     - |     - |         - |
|              LogMix3 |  net50 | .NET Core 5.0 | 12.287 ns | 0.2715 ns | 0.4064 ns | 12.239 ns |  5.41 |    0.19 |      - |     - |     - |         - |
|              LogMix4 |  net50 | .NET Core 5.0 | 20.480 ns | 0.4196 ns | 0.5882 ns | 20.527 ns |  9.02 |    0.28 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |  net50 | .NET Core 5.0 | 23.331 ns | 0.1885 ns | 0.2821 ns | 23.361 ns | 10.28 |    0.18 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |  net50 | .NET Core 5.0 | 53.670 ns | 3.1681 ns | 4.5436 ns | 56.863 ns | 23.64 |    2.05 | 0.0446 |     - |     - |     280 B |
