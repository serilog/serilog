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
|               Method |    Job |       Runtime |          Mean |       Error |      StdDev |    Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |------- |-------------- |--------------:|------------:|------------:|---------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty | core31 | .NET Core 3.1 |      8.560 ns |   0.0836 ns |   0.1251 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |     57.901 ns |   0.8380 ns |   1.2543 ns |     6.77 |    0.19 | 0.0088 |     - |     - |      56 B |
|               LogMsg | core31 | .NET Core 3.1 |    488.674 ns |   5.4313 ns |   8.1293 ns |    57.09 |    1.01 | 0.0210 |     - |     - |     136 B |
|         LogMsgWithEx | core31 | .NET Core 3.1 |    494.838 ns |   5.4944 ns |   8.0536 ns |    57.78 |    1.00 | 0.0210 |     - |     - |     136 B |
|           LogScalar1 | core31 | .NET Core 3.1 |    591.826 ns |   5.4996 ns |   8.2315 ns |    69.16 |    1.68 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 | core31 | .NET Core 3.1 |    650.301 ns |   7.7346 ns |  11.3373 ns |    75.94 |    1.48 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 | core31 | .NET Core 3.1 |    713.905 ns |   5.5961 ns |   7.8450 ns |    83.33 |    1.56 | 0.0734 |     - |     - |     464 B |
|        LogScalarMany | core31 | .NET Core 3.1 |    776.053 ns |   5.4203 ns |   7.9450 ns |    90.63 |    1.49 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |    630.637 ns |   5.4910 ns |   8.2187 ns |    73.68 |    0.90 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |    717.483 ns |   5.8453 ns |   8.3831 ns |    83.84 |    1.52 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |    820.222 ns |   5.0251 ns |   7.5214 ns |    95.84 |    1.71 | 0.0849 |     - |     - |     536 B |
|  LogScalarStructMany | core31 | .NET Core 3.1 |    902.793 ns |   4.4418 ns |   6.6483 ns |   105.48 |    1.77 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 |    723.779 ns |   7.9474 ns |  11.3980 ns |    84.57 |    1.73 | 0.0706 |     - |     - |     448 B |
|        LogDictionary | core31 | .NET Core 3.1 |  2,790.804 ns |  24.5874 ns |  36.0399 ns |   325.92 |    6.08 | 0.3395 |     - |     - |    2144 B |
|          LogSequence | core31 | .NET Core 3.1 |  1,219.214 ns |  25.5626 ns |  38.2609 ns |   142.48 |    5.70 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  4,697.262 ns |  61.5603 ns |  92.1405 ns |   548.86 |   14.79 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 | core31 | .NET Core 3.1 |    660.688 ns |   6.8085 ns |  10.1906 ns |    77.20 |    1.69 | 0.0696 |     - |     - |     440 B |
|              LogMix3 | core31 | .NET Core 3.1 |    740.803 ns |   6.6899 ns |  10.0132 ns |    86.56 |    1.73 | 0.0811 |     - |     - |     512 B |
|              LogMix4 | core31 | .NET Core 3.1 |    811.901 ns |   5.6187 ns |   8.2358 ns |    94.82 |    1.88 | 0.1116 |     - |     - |     704 B |
|              LogMix5 | core31 | .NET Core 3.1 |    890.790 ns |   6.5225 ns |   9.7626 ns |   104.08 |    1.76 | 0.1230 |     - |     - |     776 B |
|           LogMixMany | core31 | .NET Core 3.1 |  9,097.882 ns | 111.1680 ns | 166.3911 ns | 1,062.97 |   23.65 | 1.0223 |     - |     - |    6449 B |
|                      |        |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |      8.257 ns |   0.0759 ns |   0.1136 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |     61.063 ns |   0.9731 ns |   1.4565 ns |     7.40 |    0.22 | 0.0088 |     - |     - |      56 B |
|               LogMsg |  net48 |      .NET 4.8 |    497.923 ns |   6.0076 ns |   8.9919 ns |    60.31 |    1.35 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |  net48 |      .NET 4.8 |    499.900 ns |   5.6562 ns |   8.4659 ns |    60.55 |    1.30 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |  net48 |      .NET 4.8 |    588.755 ns |   6.0430 ns |   9.0449 ns |    71.31 |    1.38 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |  net48 |      .NET 4.8 |    654.697 ns |   5.9407 ns |   8.8918 ns |    79.30 |    1.38 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |  net48 |      .NET 4.8 |    727.863 ns |   5.8578 ns |   8.4011 ns |    88.17 |    1.44 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |  net48 |      .NET 4.8 |    770.317 ns |   4.7068 ns |   7.0449 ns |    93.31 |    1.43 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |    622.905 ns |   4.9787 ns |   7.4519 ns |    75.45 |    1.29 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |    714.103 ns |   5.1952 ns |   7.7759 ns |    86.50 |    1.53 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |  net48 |      .NET 4.8 |    832.903 ns |  10.7113 ns |  16.0322 ns |   100.89 |    2.20 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |  net48 |      .NET 4.8 |    911.535 ns |   5.7612 ns |   8.6232 ns |   110.42 |    2.15 | 0.1154 |     - |     - |     730 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 |    728.711 ns |   5.9736 ns |   8.7560 ns |    88.27 |    1.77 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |  net48 |      .NET 4.8 |  3,188.264 ns |  20.9290 ns |  31.3255 ns |   386.20 |    6.72 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |  net48 |      .NET 4.8 |  1,319.710 ns |  30.0939 ns |  45.0432 ns |   159.84 |    5.45 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  5,852.014 ns |  48.6189 ns |  72.7704 ns |   708.89 |   14.72 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |  net48 |      .NET 4.8 |    678.779 ns |   5.9113 ns |   8.8478 ns |    82.22 |    1.44 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |  net48 |      .NET 4.8 |    770.208 ns |   4.5927 ns |   6.8741 ns |    93.30 |    1.72 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |  net48 |      .NET 4.8 |    835.864 ns |   5.1121 ns |   7.6515 ns |   101.25 |    1.63 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |  net48 |      .NET 4.8 |    927.802 ns |   5.9646 ns |   8.7428 ns |   112.39 |    2.07 | 0.1249 |     - |     - |     786 B |
|           LogMixMany |  net48 |      .NET 4.8 | 10,903.994 ns | 165.7672 ns | 248.1126 ns | 1,320.74 |   32.18 | 1.0376 |     - |     - |    6596 B |
|                      |        |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |      7.381 ns |   0.0902 ns |   0.1351 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |     45.543 ns |   0.5021 ns |   0.7359 ns |     6.17 |    0.16 | 0.0089 |     - |     - |      56 B |
|               LogMsg |  net50 | .NET Core 5.0 |    266.858 ns |   2.9503 ns |   4.4159 ns |    36.17 |    0.93 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |    260.214 ns |   3.7240 ns |   5.5740 ns |    35.27 |    0.94 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |  net50 | .NET Core 5.0 |    345.170 ns |   2.9826 ns |   4.4642 ns |    46.78 |    1.04 | 0.0596 |     - |     - |     376 B |
|           LogScalar2 |  net50 | .NET Core 5.0 |    395.082 ns |   6.9802 ns |  10.0108 ns |    53.53 |    1.80 | 0.0672 |     - |     - |     424 B |
|           LogScalar3 |  net50 | .NET Core 5.0 |    444.402 ns |   3.0906 ns |   4.5302 ns |    60.23 |    1.20 | 0.0749 |     - |     - |     472 B |
|        LogScalarMany |  net50 | .NET Core 5.0 |    497.467 ns |   5.7868 ns |   8.6614 ns |    67.42 |    1.74 | 0.1001 |     - |     - |     632 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |    382.161 ns |   4.3018 ns |   6.4387 ns |    51.79 |    1.30 | 0.0634 |     - |     - |     400 B |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |    458.233 ns |   2.3636 ns |   3.4645 ns |    62.11 |    1.26 | 0.0749 |     - |     - |     472 B |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |    548.242 ns |   5.2834 ns |   7.9080 ns |    74.29 |    1.26 | 0.0858 |     - |     - |     544 B |
|  LogScalarStructMany |  net50 | .NET Core 5.0 |    603.593 ns |   5.9749 ns |   8.9430 ns |    81.80 |    1.76 | 0.1154 |     - |     - |     728 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |    482.477 ns |   6.5944 ns |   9.8702 ns |    65.39 |    1.85 | 0.0725 |     - |     - |     456 B |
|        LogDictionary |  net50 | .NET Core 5.0 |  2,355.398 ns |  25.0926 ns |  35.9871 ns |   319.13 |    8.09 | 0.3471 |     - |     - |    2200 B |
|          LogSequence |  net50 | .NET Core 5.0 |    906.024 ns |   7.1604 ns |  10.7174 ns |   122.79 |    2.79 | 0.1307 |     - |     - |     824 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  4,344.242 ns |  47.8798 ns |  71.6642 ns |   588.82 |   16.50 | 0.5493 |     - |     - |    3472 B |
|              LogMix2 |  net50 | .NET Core 5.0 |    414.669 ns |   2.8800 ns |   4.3107 ns |    56.20 |    1.23 | 0.0710 |     - |     - |     448 B |
|              LogMix3 |  net50 | .NET Core 5.0 |    488.400 ns |   5.5822 ns |   8.3551 ns |    66.20 |    1.91 | 0.0820 |     - |     - |     520 B |
|              LogMix4 |  net50 | .NET Core 5.0 |    550.459 ns |   6.9128 ns |  10.3467 ns |    74.59 |    1.18 | 0.1135 |     - |     - |     712 B |
|              LogMix5 |  net50 | .NET Core 5.0 |    621.653 ns |   5.7853 ns |   8.4801 ns |    84.25 |    1.71 | 0.1249 |     - |     - |     784 B |
|           LogMixMany |  net50 | .NET Core 5.0 |  8,213.294 ns | 104.3092 ns | 156.1250 ns | 1,113.41 |   38.82 | 1.0376 |     - |     - |    6537 B |
