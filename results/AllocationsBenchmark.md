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
|               Method |             Job |       Jit |       Runtime |          Mean |       Error |      StdDev |    Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |---------------- |---------- |-------------- |--------------:|------------:|------------:|---------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |      7.846 ns |   0.0844 ns |   0.1263 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     59.759 ns |   0.8013 ns |   1.1994 ns |     7.62 |    0.21 | 0.0088 |     - |     - |      56 B |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    506.116 ns |   5.3428 ns |   7.9969 ns |    64.52 |    1.36 | 0.0210 |     - |     - |     136 B |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    506.913 ns |   4.4428 ns |   6.5122 ns |    64.65 |    1.38 | 0.0210 |     - |     - |     136 B |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    614.472 ns |   6.5093 ns |   9.7429 ns |    78.35 |    1.99 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    734.602 ns |  53.0868 ns |  79.4578 ns |    93.71 |   10.80 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    759.825 ns |   7.2009 ns |  10.7779 ns |    96.87 |    2.12 | 0.0734 |     - |     - |     464 B |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    816.361 ns |   5.7843 ns |   8.6577 ns |   104.07 |    1.59 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    651.350 ns |   6.2613 ns |   9.3717 ns |    83.04 |    1.84 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    765.596 ns |   8.9484 ns |  13.3935 ns |    97.61 |    2.50 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    858.202 ns |   4.6971 ns |   7.0303 ns |   109.41 |    1.50 | 0.0849 |     - |     - |     536 B |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    947.940 ns |   8.4106 ns |  12.5886 ns |   120.86 |    2.87 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    779.792 ns |   6.5419 ns |   9.7916 ns |    99.43 |    2.42 | 0.0706 |     - |     - |     448 B |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  3,072.839 ns |  56.6697 ns |  83.0657 ns |   391.88 |   12.62 | 0.3395 |     - |     - |    2144 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,300.333 ns |  12.2429 ns |  18.3246 ns |   165.79 |    3.69 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  4,928.164 ns |  49.7805 ns |  74.5091 ns |   628.37 |   16.55 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    697.099 ns |   6.8962 ns |  10.3218 ns |    88.88 |    2.07 | 0.0696 |     - |     - |     440 B |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    800.098 ns |   9.9828 ns |  14.9418 ns |   102.02 |    2.93 | 0.0811 |     - |     - |     512 B |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    874.784 ns |   6.9622 ns |   9.9850 ns |   111.46 |    1.63 | 0.1116 |     - |     - |     704 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    952.602 ns |   8.3682 ns |  12.5252 ns |   121.45 |    2.65 | 0.1221 |     - |     - |     776 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9,682.658 ns |  82.5134 ns | 123.5021 ns | 1,234.59 |   30.81 | 1.0223 |     - |     - |    6449 B |
|                      |                 |           |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |      8.762 ns |   0.0820 ns |   0.1227 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |     62.794 ns |   0.6390 ns |   0.9565 ns |     7.17 |    0.15 | 0.0088 |     - |     - |      56 B |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |    521.756 ns |   5.0081 ns |   7.4959 ns |    59.56 |    1.24 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |    523.326 ns |   4.3741 ns |   6.4115 ns |    59.71 |    1.22 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    619.340 ns |   5.5594 ns |   8.3211 ns |    70.70 |    1.23 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    689.274 ns |   6.2841 ns |   9.4057 ns |    78.69 |    1.63 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    769.883 ns |   6.6558 ns |   9.9620 ns |    87.88 |    1.61 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |    811.279 ns |   5.6519 ns |   8.4595 ns |    92.61 |    1.56 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    662.108 ns |   6.5107 ns |   9.7450 ns |    75.59 |    1.89 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    755.160 ns |   5.4724 ns |   8.1908 ns |    86.21 |    1.77 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    865.629 ns |   5.4606 ns |   8.1732 ns |    98.81 |    1.63 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |    950.205 ns |  11.6103 ns |  17.3777 ns |   108.47 |    2.55 | 0.1144 |     - |     - |     730 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 |    761.687 ns |   5.8364 ns |   8.7356 ns |    86.95 |    1.75 | 0.0725 |     - |     - |     457 B |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3,346.173 ns |  24.8903 ns |  37.2546 ns |   381.99 |    7.25 | 0.3548 |     - |     - |    2247 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,315.587 ns |   9.7987 ns |  14.6662 ns |   150.19 |    3.18 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 |  6,010.722 ns |  64.5530 ns |  96.6199 ns |   686.21 |   17.38 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    710.616 ns |   5.1379 ns |   7.6902 ns |    81.12 |    1.69 | 0.0706 |     - |     - |     449 B |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    824.995 ns |   4.9712 ns |   7.4407 ns |    94.18 |    1.56 | 0.0820 |     - |     - |     522 B |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    891.589 ns |   6.0396 ns |   8.8528 ns |   101.72 |    1.54 | 0.1135 |     - |     - |     714 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    976.829 ns |  10.9561 ns |  16.3986 ns |   111.51 |    2.28 | 0.1240 |     - |     - |     786 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 11,447.359 ns | 130.0956 ns | 190.6925 ns | 1,305.98 |   23.28 | 1.0376 |     - |     - |    6596 B |
|                      |                 |           |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |      8.776 ns |   0.0751 ns |   0.1124 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |     63.482 ns |   0.8559 ns |   1.2811 ns |     7.23 |    0.18 | 0.0088 |     - |     - |      56 B |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |    523.013 ns |   5.2737 ns |   7.8935 ns |    59.61 |    1.29 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |    523.902 ns |   4.9692 ns |   7.4377 ns |    59.71 |    1.22 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    620.868 ns |   5.5364 ns |   8.2866 ns |    70.76 |    1.55 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    701.801 ns |   7.5373 ns |  11.2815 ns |    79.97 |    1.29 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    769.492 ns |   5.6162 ns |   8.4061 ns |    87.69 |    1.42 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |    814.151 ns |   6.6053 ns |   9.8864 ns |    92.78 |    1.44 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    660.607 ns |   4.7484 ns |   7.1072 ns |    75.29 |    1.42 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    760.029 ns |   6.1719 ns |   9.2378 ns |    86.62 |    1.71 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    868.819 ns |   6.1533 ns |   9.2100 ns |    99.01 |    1.69 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |    950.196 ns |   6.7526 ns |  10.1069 ns |   108.29 |    1.78 | 0.1154 |     - |     - |     730 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 |    766.955 ns |   6.1341 ns |   9.1812 ns |    87.40 |    1.45 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3,309.212 ns |  21.7208 ns |  32.5107 ns |   377.12 |    5.57 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,314.870 ns |  10.7347 ns |  16.0672 ns |   149.85 |    2.86 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 |  5,967.748 ns |  48.8596 ns |  73.1307 ns |   680.14 |   13.68 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    720.417 ns |   4.9759 ns |   7.1363 ns |    82.10 |    1.10 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    812.411 ns |   6.6610 ns |   9.9698 ns |    92.59 |    1.64 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    874.805 ns |   5.1485 ns |   7.3838 ns |    99.70 |    1.58 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    966.381 ns |   7.8714 ns |  11.7816 ns |   110.13 |    1.83 | 0.1240 |     - |     - |     786 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 11,661.420 ns | 225.3121 ns | 323.1359 ns | 1,328.97 |   38.53 | 1.0376 |     - |     - |    6596 B |
