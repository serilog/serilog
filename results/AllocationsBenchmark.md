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
|               Method |             Job |       Jit |       Runtime |          Mean |      Error |     StdDev |    Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |---------------- |---------- |-------------- |--------------:|-----------:|-----------:|---------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |      8.245 ns |  0.1612 ns |  0.2206 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     55.880 ns |  0.4060 ns |  0.5951 ns |     6.78 |    0.20 | 0.0089 |     - |     - |      56 B |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    507.628 ns |  2.5740 ns |  3.6915 ns |    61.60 |    1.67 | 0.0210 |     - |     - |     136 B |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    507.729 ns |  2.7134 ns |  4.0614 ns |    61.61 |    1.62 | 0.0210 |     - |     - |     136 B |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    601.450 ns |  1.9369 ns |  2.8390 ns |    72.94 |    1.86 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    667.796 ns |  3.3660 ns |  4.9339 ns |    80.97 |    2.00 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    729.832 ns |  2.9215 ns |  3.9990 ns |    88.59 |    2.55 | 0.0734 |     - |     - |     464 B |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    790.729 ns |  3.9975 ns |  5.7331 ns |    95.93 |    2.09 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    646.739 ns | 14.9297 ns | 20.4360 ns |    78.50 |    3.33 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    724.875 ns |  3.6906 ns |  5.2929 ns |    87.99 |    2.22 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    833.648 ns |  6.1203 ns |  9.1606 ns |   101.02 |    2.12 | 0.0849 |     - |     - |     536 B |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    919.377 ns |  3.9931 ns |  5.8531 ns |   111.50 |    2.47 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    758.406 ns |  8.1723 ns | 11.4565 ns |    91.97 |    1.54 | 0.0706 |     - |     - |     448 B |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2,820.617 ns | 10.5503 ns | 15.7913 ns |   342.42 |    8.64 | 0.3395 |     - |     - |    2144 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,269.868 ns |  7.6161 ns | 11.3994 ns |   154.36 |    4.76 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  4,846.775 ns | 40.7418 ns | 60.9803 ns |   588.89 |   20.62 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    698.188 ns |  3.2544 ns |  4.5623 ns |    84.76 |    2.48 | 0.0696 |     - |     - |     440 B |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    778.842 ns |  6.2319 ns |  8.9377 ns |    94.62 |    3.35 | 0.0811 |     - |     - |     512 B |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    853.103 ns | 11.2383 ns | 16.4729 ns |   103.81 |    4.22 | 0.1116 |     - |     - |     704 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    927.733 ns |  4.4807 ns |  6.4260 ns |   112.59 |    2.77 | 0.1221 |     - |     - |     776 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9,199.228 ns | 50.7581 ns | 74.4005 ns | 1,116.21 |   27.55 | 1.0223 |     - |     - |    6449 B |
|                      |                 |           |               |               |            |            |          |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |      8.129 ns |  0.0456 ns |  0.0683 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |     59.901 ns |  0.3878 ns |  0.5684 ns |     7.36 |    0.09 | 0.0088 |     - |     - |      56 B |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |    503.683 ns |  1.7988 ns |  2.6367 ns |    61.93 |    0.53 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |    504.984 ns |  2.8264 ns |  4.1429 ns |    62.09 |    0.81 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    608.272 ns |  3.2068 ns |  4.7999 ns |    74.83 |    0.68 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    683.546 ns | 22.8123 ns | 34.1443 ns |    84.09 |    4.08 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    735.869 ns |  2.7744 ns |  4.1526 ns |    90.53 |    0.87 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |    778.493 ns |  2.3970 ns |  3.4377 ns |    95.70 |    0.83 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    635.784 ns |  3.4544 ns |  5.1704 ns |    78.22 |    0.90 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    727.127 ns |  3.5547 ns |  5.0981 ns |    89.39 |    1.09 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    826.819 ns |  2.3666 ns |  3.3941 ns |   101.64 |    0.75 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |    900.582 ns |  3.3547 ns |  4.9172 ns |   110.73 |    0.96 | 0.1154 |     - |     - |     730 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 |    753.654 ns |  4.2971 ns |  6.2986 ns |    92.66 |    0.76 | 0.0725 |     - |     - |     457 B |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3,144.665 ns | 59.5660 ns | 83.5032 ns |   386.49 |   10.08 | 0.3548 |     - |     - |    2247 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,262.466 ns |  4.8268 ns |  6.7666 ns |   155.17 |    1.75 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 |  5,618.182 ns | 23.0292 ns | 32.2837 ns |   690.51 |    5.30 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    681.789 ns |  2.4938 ns |  3.6553 ns |    83.83 |    0.84 | 0.0706 |     - |     - |     449 B |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    766.977 ns |  3.3364 ns |  4.8905 ns |    94.30 |    1.16 | 0.0820 |     - |     - |     522 B |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    832.107 ns |  3.9393 ns |  5.7742 ns |   102.31 |    1.16 | 0.1135 |     - |     - |     714 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    918.464 ns |  2.8184 ns |  4.0421 ns |   112.91 |    0.95 | 0.1249 |     - |     - |     786 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10,481.062 ns | 51.9721 ns | 76.1800 ns | 1,288.63 |   10.30 | 1.0376 |     - |     - |    6596 B |
|                      |                 |           |               |               |            |            |          |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |      8.142 ns |  0.0571 ns |  0.0837 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |     60.016 ns |  0.5272 ns |  0.7891 ns |     7.37 |    0.14 | 0.0088 |     - |     - |      56 B |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |    501.168 ns |  2.2351 ns |  3.2762 ns |    61.56 |    0.73 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |    507.228 ns |  4.5413 ns |  6.7972 ns |    62.33 |    1.27 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    619.527 ns |  9.1022 ns | 13.0541 ns |    76.10 |    2.16 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    695.143 ns |  3.6085 ns |  5.2892 ns |    85.39 |    1.27 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    742.102 ns | 15.4137 ns | 23.0705 ns |    91.17 |    2.59 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |    779.452 ns | 11.2306 ns | 15.7438 ns |    95.68 |    1.93 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    642.234 ns |  4.2416 ns |  6.3486 ns |    78.88 |    1.37 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    731.640 ns |  3.5235 ns |  4.8230 ns |    89.79 |    0.89 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    824.517 ns |  2.5294 ns |  3.7075 ns |   101.28 |    1.11 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |    902.073 ns |  3.1450 ns |  4.6099 ns |   110.80 |    1.42 | 0.1154 |     - |     - |     730 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 |    753.703 ns |  2.6633 ns |  3.9862 ns |    92.57 |    0.98 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3,128.528 ns | 11.7650 ns | 16.8730 ns |   384.25 |    5.43 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,262.663 ns |  5.2120 ns |  7.4749 ns |   155.08 |    2.09 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 |  5,675.744 ns | 47.9875 ns | 68.8222 ns |   697.03 |    7.23 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    679.800 ns |  1.9700 ns |  2.9486 ns |    83.49 |    0.94 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    768.232 ns |  4.0087 ns |  5.6196 ns |    94.31 |    1.45 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    822.282 ns |  1.6425 ns |  2.3557 ns |   100.99 |    1.06 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    918.520 ns |  3.2278 ns |  4.5249 ns |   112.75 |    1.02 | 0.1249 |     - |     - |     786 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10,474.987 ns | 48.3902 ns | 69.3998 ns | 1,286.52 |   17.07 | 1.0376 |     - |     - |    6596 B |
