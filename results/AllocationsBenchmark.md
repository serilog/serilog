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
|             LogEmpty | core31 | .NET Core 3.1 |      8.785 ns |   0.0856 ns |   0.1281 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |     57.226 ns |   0.2730 ns |   0.4086 ns |     6.52 |    0.11 | 0.0089 |     - |     - |      56 B |
|               LogMsg | core31 | .NET Core 3.1 |    509.105 ns |   3.9383 ns |   5.8946 ns |    57.96 |    1.12 | 0.0210 |     - |     - |     136 B |
|         LogMsgWithEx | core31 | .NET Core 3.1 |    509.646 ns |   9.2377 ns |  13.5406 ns |    58.06 |    2.13 | 0.0210 |     - |     - |     136 B |
|           LogScalar1 | core31 | .NET Core 3.1 |    602.902 ns |   5.2330 ns |   7.8325 ns |    68.64 |    1.47 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 | core31 | .NET Core 3.1 |    739.262 ns |  41.8639 ns |  62.6599 ns |    84.18 |    7.38 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 | core31 | .NET Core 3.1 |    810.219 ns |  15.6503 ns |  23.4247 ns |    92.23 |    2.53 | 0.0734 |     - |     - |     464 B |
|        LogScalarMany | core31 | .NET Core 3.1 |    874.342 ns |  13.6816 ns |  20.4780 ns |    99.55 |    2.99 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |    691.626 ns |   7.7792 ns |  11.4027 ns |    78.78 |    1.83 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |    820.345 ns |  21.8954 ns |  32.0940 ns |    93.47 |    4.56 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |    843.266 ns |   5.9346 ns |   8.8827 ns |    96.01 |    1.82 | 0.0849 |     - |     - |     536 B |
|  LogScalarStructMany | core31 | .NET Core 3.1 |    927.369 ns |   6.0126 ns |   8.9993 ns |   105.58 |    1.94 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 |    755.869 ns |   4.9655 ns |   7.2784 ns |    86.10 |    1.52 | 0.0706 |     - |     - |     448 B |
|        LogDictionary | core31 | .NET Core 3.1 |  2,879.272 ns |  19.5637 ns |  29.2820 ns |   327.79 |    5.45 | 0.3395 |     - |     - |    2144 B |
|          LogSequence | core31 | .NET Core 3.1 |  1,281.020 ns |  15.8267 ns |  23.1986 ns |   145.89 |    2.33 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  4,914.007 ns |  66.8730 ns | 100.0923 ns |   559.31 |    5.50 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 | core31 | .NET Core 3.1 |    687.891 ns |   5.3727 ns |   8.0417 ns |    78.32 |    1.48 | 0.0696 |     - |     - |     440 B |
|              LogMix3 | core31 | .NET Core 3.1 |    778.355 ns |   5.7813 ns |   8.6532 ns |    88.61 |    1.28 | 0.0811 |     - |     - |     512 B |
|              LogMix4 | core31 | .NET Core 3.1 |    860.708 ns |   7.5645 ns |  11.3222 ns |    98.00 |    2.26 | 0.1116 |     - |     - |     704 B |
|              LogMix5 | core31 | .NET Core 3.1 |    932.828 ns |   4.0478 ns |   6.0585 ns |   106.20 |    1.77 | 0.1230 |     - |     - |     776 B |
|           LogMixMany | core31 | .NET Core 3.1 |  9,494.367 ns |  68.3144 ns | 102.2498 ns | 1,080.83 |   14.24 | 1.0223 |     - |     - |    6449 B |
|                      |        |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |      8.732 ns |   0.0661 ns |   0.0989 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |     62.070 ns |   0.6290 ns |   0.9415 ns |     7.11 |    0.13 | 0.0088 |     - |     - |      56 B |
|               LogMsg |  net48 |      .NET 4.8 |    518.472 ns |   3.9359 ns |   5.8911 ns |    59.38 |    0.74 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |  net48 |      .NET 4.8 |    519.534 ns |   4.8116 ns |   7.2017 ns |    59.51 |    1.27 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |  net48 |      .NET 4.8 |    612.096 ns |   4.2427 ns |   6.3502 ns |    70.11 |    1.22 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |  net48 |      .NET 4.8 |    686.442 ns |   5.2446 ns |   7.8499 ns |    78.63 |    1.40 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |  net48 |      .NET 4.8 |    756.659 ns |   4.8405 ns |   7.2451 ns |    86.67 |    1.15 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |  net48 |      .NET 4.8 |    797.979 ns |   5.2845 ns |   7.9096 ns |    91.40 |    1.34 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |    648.813 ns |   5.0358 ns |   7.5374 ns |    74.32 |    1.19 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |    743.893 ns |   4.4691 ns |   6.6892 ns |    85.21 |    1.18 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |  net48 |      .NET 4.8 |    855.004 ns |   4.7260 ns |   7.0736 ns |    97.93 |    1.30 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |  net48 |      .NET 4.8 |    939.532 ns |   8.9700 ns |  13.4259 ns |   107.61 |    1.88 | 0.1144 |     - |     - |     730 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 |    753.250 ns |   5.6354 ns |   8.4349 ns |    86.28 |    1.35 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |  net48 |      .NET 4.8 |  3,349.942 ns |  25.7364 ns |  38.5210 ns |   383.71 |    6.22 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |  net48 |      .NET 4.8 |  1,305.121 ns |  10.2714 ns |  15.3737 ns |   149.49 |    2.33 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  6,116.861 ns |  44.0351 ns |  64.5461 ns |   701.00 |   10.53 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |  net48 |      .NET 4.8 |    700.228 ns |   5.3330 ns |   7.9822 ns |    80.20 |    1.05 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |  net48 |      .NET 4.8 |    802.429 ns |   4.1421 ns |   6.1998 ns |    91.91 |    1.45 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |  net48 |      .NET 4.8 |    866.102 ns |   3.7170 ns |   5.4484 ns |    99.26 |    1.23 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |  net48 |      .NET 4.8 |    952.586 ns |   7.2370 ns |  10.6080 ns |   109.17 |    1.69 | 0.1240 |     - |     - |     786 B |
|           LogMixMany |  net48 |      .NET 4.8 | 11,180.546 ns | 106.9141 ns | 160.0239 ns | 1,280.63 |   23.50 | 1.0376 |     - |     - |    6596 B |
|                      |        |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |      7.674 ns |   0.0864 ns |   0.1293 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |     46.276 ns |   0.3618 ns |   0.5303 ns |     6.03 |    0.11 | 0.0089 |     - |     - |      56 B |
|               LogMsg |  net50 | .NET Core 5.0 |    280.113 ns |   2.8864 ns |   4.3203 ns |    36.51 |    0.72 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |    273.187 ns |   1.9951 ns |   2.9243 ns |    35.61 |    0.77 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |  net50 | .NET Core 5.0 |    355.930 ns |   3.3398 ns |   4.9989 ns |    46.39 |    0.89 | 0.0596 |     - |     - |     376 B |
|           LogScalar2 |  net50 | .NET Core 5.0 |    408.964 ns |   3.1549 ns |   4.7222 ns |    53.30 |    1.09 | 0.0672 |     - |     - |     424 B |
|           LogScalar3 |  net50 | .NET Core 5.0 |    473.399 ns |   2.8320 ns |   4.2388 ns |    61.70 |    1.13 | 0.0749 |     - |     - |     472 B |
|        LogScalarMany |  net50 | .NET Core 5.0 |    521.615 ns |   5.3326 ns |   7.9815 ns |    67.99 |    1.71 | 0.1001 |     - |     - |     632 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |    393.206 ns |   3.0532 ns |   4.5698 ns |    51.25 |    1.07 | 0.0634 |     - |     - |     400 B |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |    483.064 ns |   5.1868 ns |   7.7633 ns |    62.96 |    1.47 | 0.0744 |     - |     - |     472 B |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |    571.576 ns |   5.4224 ns |   8.1161 ns |    74.51 |    1.99 | 0.0858 |     - |     - |     544 B |
|  LogScalarStructMany |  net50 | .NET Core 5.0 |    640.516 ns |   5.6375 ns |   8.4379 ns |    83.48 |    1.54 | 0.1154 |     - |     - |     728 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |    506.274 ns |   5.5001 ns |   8.2323 ns |    65.99 |    1.62 | 0.0725 |     - |     - |     456 B |
|        LogDictionary |  net50 | .NET Core 5.0 |  2,446.547 ns |  25.1581 ns |  37.6554 ns |   318.83 |    4.65 | 0.3471 |     - |     - |    2200 B |
|          LogSequence |  net50 | .NET Core 5.0 |    943.800 ns |   7.7308 ns |  11.5711 ns |   123.01 |    2.39 | 0.1297 |     - |     - |     824 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  4,487.325 ns |  55.1593 ns |  82.5599 ns |   584.86 |   13.94 | 0.5493 |     - |     - |    3472 B |
|              LogMix2 |  net50 | .NET Core 5.0 |    435.266 ns |   4.3522 ns |   6.5141 ns |    56.73 |    1.28 | 0.0710 |     - |     - |     448 B |
|              LogMix3 |  net50 | .NET Core 5.0 |    513.894 ns |   4.3368 ns |   6.4912 ns |    66.98 |    1.49 | 0.0820 |     - |     - |     520 B |
|              LogMix4 |  net50 | .NET Core 5.0 |    578.001 ns |   4.6180 ns |   6.9120 ns |    75.34 |    1.86 | 0.1135 |     - |     - |     712 B |
|              LogMix5 |  net50 | .NET Core 5.0 |    655.596 ns |   5.4330 ns |   8.1318 ns |    85.45 |    1.73 | 0.1249 |     - |     - |     784 B |
|           LogMixMany |  net50 | .NET Core 5.0 |  8,649.443 ns |  98.2688 ns | 140.9341 ns | 1,126.88 |   28.87 | 1.0376 |     - |     - |    6537 B |
