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
|               Method |    Job |       Runtime |          Mean |      Error |      StdDev |    Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |------- |-------------- |--------------:|-----------:|------------:|---------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty | core31 | .NET Core 3.1 |      8.627 ns |  0.0791 ns |   0.1184 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |     57.739 ns |  0.5703 ns |   0.8537 ns |     6.69 |    0.14 | 0.0089 |     - |     - |      56 B |
|               LogMsg | core31 | .NET Core 3.1 |    504.783 ns |  4.7070 ns |   7.0452 ns |    58.52 |    1.09 | 0.0210 |     - |     - |     136 B |
|         LogMsgWithEx | core31 | .NET Core 3.1 |    503.038 ns |  4.1162 ns |   6.0335 ns |    58.36 |    1.11 | 0.0210 |     - |     - |     136 B |
|           LogScalar1 | core31 | .NET Core 3.1 |    606.866 ns |  8.4502 ns |  12.6479 ns |    70.36 |    1.85 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 | core31 | .NET Core 3.1 |    670.197 ns |  6.1512 ns |   9.2068 ns |    77.69 |    0.96 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 | core31 | .NET Core 3.1 |    738.644 ns |  5.8101 ns |   8.6963 ns |    85.63 |    1.43 | 0.0734 |     - |     - |     464 B |
|        LogScalarMany | core31 | .NET Core 3.1 |    796.404 ns |  5.0104 ns |   7.4994 ns |    92.33 |    1.43 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |    649.127 ns |  4.1314 ns |   6.1838 ns |    75.25 |    1.16 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |    738.531 ns |  4.9958 ns |   7.4775 ns |    85.62 |    1.30 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |    841.030 ns |  5.2702 ns |   7.8881 ns |    97.50 |    1.50 | 0.0849 |     - |     - |     536 B |
|  LogScalarStructMany | core31 | .NET Core 3.1 |    931.886 ns |  9.3215 ns |  13.9520 ns |   108.03 |    1.95 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 |    747.425 ns |  6.1171 ns |   8.7729 ns |    86.76 |    1.43 | 0.0706 |     - |     - |     448 B |
|        LogDictionary | core31 | .NET Core 3.1 |  2,944.666 ns | 16.9955 ns |  25.4380 ns |   341.37 |    5.13 | 0.3395 |     - |     - |    2144 B |
|          LogSequence | core31 | .NET Core 3.1 |  1,248.001 ns | 18.7079 ns |  27.4217 ns |   144.78 |    3.89 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  4,990.509 ns | 73.7502 ns | 105.7703 ns |   579.34 |   15.72 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 | core31 | .NET Core 3.1 |    700.440 ns |  8.0091 ns |  11.9877 ns |    81.20 |    1.50 | 0.0696 |     - |     - |     440 B |
|              LogMix3 | core31 | .NET Core 3.1 |    775.520 ns |  4.5033 ns |   6.7403 ns |    89.90 |    1.26 | 0.0811 |     - |     - |     512 B |
|              LogMix4 | core31 | .NET Core 3.1 |    855.163 ns |  6.6599 ns |   9.9682 ns |    99.15 |    2.06 | 0.1116 |     - |     - |     704 B |
|              LogMix5 | core31 | .NET Core 3.1 |    935.947 ns |  7.5486 ns |  11.2984 ns |   108.51 |    2.09 | 0.1230 |     - |     - |     776 B |
|           LogMixMany | core31 | .NET Core 3.1 |  9,385.575 ns | 80.5543 ns | 120.5699 ns | 1,088.10 |   21.50 | 1.0223 |     - |     - |    6448 B |
|                      |        |               |               |            |             |          |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |      8.705 ns |  0.0649 ns |   0.0971 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |     62.236 ns |  0.5970 ns |   0.8936 ns |     7.15 |    0.14 | 0.0088 |     - |     - |      56 B |
|               LogMsg |  net48 |      .NET 4.8 |    517.446 ns |  3.6429 ns |   5.4526 ns |    59.45 |    1.11 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |  net48 |      .NET 4.8 |    516.158 ns |  3.5394 ns |   5.2976 ns |    59.30 |    0.94 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |  net48 |      .NET 4.8 |    616.982 ns |  5.4218 ns |   7.7758 ns |    70.87 |    1.28 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |  net48 |      .NET 4.8 |    683.449 ns |  4.4718 ns |   6.6931 ns |    78.53 |    1.40 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |  net48 |      .NET 4.8 |    759.346 ns |  5.7889 ns |   8.6645 ns |    87.24 |    1.39 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |  net48 |      .NET 4.8 |    795.138 ns |  4.1142 ns |   6.1579 ns |    91.35 |    1.18 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |    650.347 ns |  4.2077 ns |   6.1676 ns |    74.73 |    0.83 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |    743.578 ns |  4.6559 ns |   6.9687 ns |    85.43 |    1.31 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |  net48 |      .NET 4.8 |    855.902 ns |  4.3185 ns |   6.4637 ns |    98.34 |    1.33 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |  net48 |      .NET 4.8 |    931.982 ns |  3.9079 ns |   5.8492 ns |   107.08 |    1.36 | 0.1154 |     - |     - |     730 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 |    754.005 ns |  5.3601 ns |   8.0227 ns |    86.63 |    1.26 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |  net48 |      .NET 4.8 |  3,298.977 ns | 19.6458 ns |  29.4049 ns |   379.03 |    5.61 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |  net48 |      .NET 4.8 |  1,353.177 ns | 36.6319 ns |  54.8288 ns |   155.48 |    6.73 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  5,948.629 ns | 72.6655 ns | 106.5121 ns |   683.56 |   14.16 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |  net48 |      .NET 4.8 |    701.255 ns |  6.0340 ns |   9.0314 ns |    80.57 |    1.29 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |  net48 |      .NET 4.8 |    804.734 ns |  4.7789 ns |   7.1529 ns |    92.45 |    1.15 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |  net48 |      .NET 4.8 |    867.113 ns |  5.3693 ns |   8.0366 ns |    99.62 |    1.39 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |  net48 |      .NET 4.8 |    959.425 ns | 11.2850 ns |  16.5413 ns |   110.25 |    2.38 | 0.1240 |     - |     - |     786 B |
|           LogMixMany |  net48 |      .NET 4.8 | 11,328.281 ns | 98.5552 ns | 147.5128 ns | 1,301.51 |   21.89 | 1.0376 |     - |     - |    6596 B |
|                      |        |               |               |            |             |          |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |      7.826 ns |  0.0625 ns |   0.0936 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |     47.591 ns |  0.5727 ns |   0.8572 ns |     6.08 |    0.13 | 0.0089 |     - |     - |      56 B |
|               LogMsg |  net50 | .NET Core 5.0 |    279.012 ns |  3.2528 ns |   4.8687 ns |    35.66 |    0.84 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |    272.798 ns |  2.4424 ns |   3.6557 ns |    34.86 |    0.54 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |  net50 | .NET Core 5.0 |    360.494 ns |  3.2113 ns |   4.8065 ns |    46.07 |    0.79 | 0.0596 |     - |     - |     376 B |
|           LogScalar2 |  net50 | .NET Core 5.0 |    407.819 ns |  3.3917 ns |   5.0766 ns |    52.12 |    0.88 | 0.0672 |     - |     - |     424 B |
|           LogScalar3 |  net50 | .NET Core 5.0 |    470.440 ns |  3.5443 ns |   5.3050 ns |    60.12 |    0.98 | 0.0744 |     - |     - |     472 B |
|        LogScalarMany |  net50 | .NET Core 5.0 |    518.204 ns |  5.3216 ns |   7.9650 ns |    66.22 |    1.34 | 0.1001 |     - |     - |     632 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |    391.758 ns |  2.3368 ns |   3.4976 ns |    50.06 |    0.75 | 0.0634 |     - |     - |     400 B |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |    477.327 ns |  3.5346 ns |   5.2905 ns |    61.00 |    0.97 | 0.0744 |     - |     - |     472 B |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |    577.614 ns |  6.3939 ns |   9.5700 ns |    73.81 |    1.12 | 0.0858 |     - |     - |     544 B |
|  LogScalarStructMany |  net50 | .NET Core 5.0 |    640.600 ns |  4.9269 ns |   7.3743 ns |    81.86 |    1.07 | 0.1154 |     - |     - |     728 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |    499.343 ns |  5.3795 ns |   8.0519 ns |    63.81 |    1.27 | 0.0725 |     - |     - |     456 B |
|        LogDictionary |  net50 | .NET Core 5.0 |  2,494.815 ns | 22.7993 ns |  34.1249 ns |   318.84 |    7.10 | 0.3471 |     - |     - |    2200 B |
|          LogSequence |  net50 | .NET Core 5.0 |    966.702 ns |  8.7645 ns |  13.1184 ns |   123.54 |    2.40 | 0.1297 |     - |     - |     824 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  4,402.908 ns | 46.1359 ns |  69.0540 ns |   562.67 |   11.92 | 0.5493 |     - |     - |    3472 B |
|              LogMix2 |  net50 | .NET Core 5.0 |    432.710 ns |  2.9717 ns |   4.3559 ns |    55.27 |    0.90 | 0.0710 |     - |     - |     448 B |
|              LogMix3 |  net50 | .NET Core 5.0 |    513.144 ns |  6.2207 ns |   9.1182 ns |    65.54 |    1.34 | 0.0820 |     - |     - |     520 B |
|              LogMix4 |  net50 | .NET Core 5.0 |    581.068 ns |  5.7792 ns |   8.6501 ns |    74.26 |    1.63 | 0.1135 |     - |     - |     712 B |
|              LogMix5 |  net50 | .NET Core 5.0 |    656.415 ns |  8.3189 ns |  12.1937 ns |    83.84 |    1.40 | 0.1249 |     - |     - |     784 B |
|           LogMixMany |  net50 | .NET Core 5.0 |  8,588.237 ns | 82.1148 ns | 120.3628 ns | 1,096.95 |   19.29 | 1.0376 |     - |     - |    6537 B |
