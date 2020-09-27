``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|               Method |             Job |       Jit |       Runtime |          Mean |       Error |      StdDev |    Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |---------------- |---------- |-------------- |--------------:|------------:|------------:|---------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |      8.075 ns |   0.0802 ns |   0.1201 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     62.201 ns |   0.5745 ns |   0.8421 ns |     7.70 |    0.15 | 0.0038 |     - |     - |      24 B |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    484.099 ns |   5.3410 ns |   7.8287 ns |    59.94 |    1.27 | 0.0095 |     - |     - |      64 B |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    482.529 ns |   4.0394 ns |   5.9209 ns |    59.75 |    1.17 | 0.0095 |     - |     - |      64 B |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    672.646 ns |   5.1890 ns |   7.6059 ns |    83.29 |    1.65 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    737.339 ns |   3.7011 ns |   5.5396 ns |    91.33 |    1.45 | 0.0839 |     - |     - |     528 B |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    820.508 ns |   4.4375 ns |   6.6418 ns |   101.64 |    1.83 | 0.0916 |     - |     - |     576 B |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    890.939 ns |  15.6070 ns |  22.3831 ns |   110.33 |    3.51 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    697.493 ns |   6.4247 ns |   9.0066 ns |    86.36 |    1.90 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    815.601 ns |   5.3344 ns |   7.8190 ns |   100.99 |    1.64 | 0.0916 |     - |     - |     576 B |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    927.085 ns |   7.0239 ns |  10.2955 ns |   114.79 |    1.69 | 0.1030 |     - |     - |     648 B |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,011.087 ns |   8.2364 ns |  12.0729 ns |   125.20 |    2.65 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    814.954 ns |  15.8397 ns |  23.7081 ns |   100.95 |    3.33 | 0.0706 |     - |     - |     448 B |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2,994.942 ns |  17.8246 ns |  26.6790 ns |   371.00 |    6.84 | 0.3395 |     - |     - |    2144 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,387.974 ns |  34.1037 ns |  49.9888 ns |   171.86 |    6.68 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5,234.599 ns | 159.9295 ns | 224.1988 ns |   648.17 |   30.41 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    762.168 ns |   5.5646 ns |   8.1565 ns |    94.37 |    1.71 | 0.0877 |     - |     - |     552 B |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    870.483 ns |  15.1693 ns |  22.7047 ns |   107.83 |    3.24 | 0.0992 |     - |     - |     624 B |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    949.450 ns |   7.9810 ns |  11.6984 ns |   117.56 |    2.22 | 0.1106 |     - |     - |     704 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,020.487 ns |   9.0383 ns |  13.5281 ns |   126.41 |    2.44 | 0.1221 |     - |     - |     776 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9,942.648 ns |  87.3906 ns | 130.8022 ns | 1,231.64 |   25.40 | 1.0223 |     - |     - |    6449 B |
|                      |                 |           |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |      8.598 ns |   0.0704 ns |   0.1054 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |     68.106 ns |   1.6559 ns |   2.4785 ns |     7.92 |    0.30 | 0.0038 |     - |     - |      24 B |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |    517.244 ns |   3.8777 ns |   5.8039 ns |    60.16 |    0.95 | 0.0095 |     - |     - |      64 B |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |    530.528 ns |  10.1467 ns |  15.1870 ns |    61.71 |    2.02 | 0.0095 |     - |     - |      64 B |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    663.575 ns |   5.1113 ns |   7.4921 ns |    77.20 |    1.16 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    754.062 ns |   4.5701 ns |   6.6989 ns |    87.73 |    1.38 | 0.0849 |     - |     - |     538 B |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    828.323 ns |   4.5691 ns |   6.8388 ns |    96.35 |    1.13 | 0.0925 |     - |     - |     586 B |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |    884.522 ns |  10.3838 ns |  15.2204 ns |   102.91 |    2.37 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    703.647 ns |   4.2529 ns |   6.2339 ns |    81.87 |    1.42 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    824.445 ns |   5.9763 ns |   8.7600 ns |    95.92 |    1.48 | 0.0925 |     - |     - |     586 B |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    939.400 ns |   4.2720 ns |   6.3941 ns |   109.27 |    1.36 | 0.1040 |     - |     - |     658 B |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,025.585 ns |   9.3584 ns |  13.7174 ns |   119.32 |    2.20 | 0.1144 |     - |     - |     730 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 |    819.134 ns |   3.8345 ns |   5.7393 ns |    95.28 |    1.40 | 0.0725 |     - |     - |     457 B |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3,611.723 ns |  62.5228 ns |  93.5812 ns |   420.09 |   11.39 | 0.3548 |     - |     - |    2247 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,364.353 ns |  22.9371 ns |  34.3312 ns |   158.70 |    4.47 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 |  6,060.255 ns |  45.7300 ns |  67.0305 ns |   705.07 |   12.46 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    791.430 ns |   9.4458 ns |  14.1381 ns |    92.06 |    1.99 | 0.0887 |     - |     - |     562 B |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    879.610 ns |   4.8567 ns |   7.2693 ns |   102.31 |    1.32 | 0.1001 |     - |     - |     634 B |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    947.927 ns |   7.2904 ns |  10.9119 ns |   110.26 |    1.89 | 0.1125 |     - |     - |     714 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,050.488 ns |   8.1631 ns |  12.2181 ns |   122.18 |    1.58 | 0.1240 |     - |     - |     786 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 11,675.730 ns | 221.9732 ns | 325.3655 ns | 1,358.37 |   41.23 | 1.0376 |     - |     - |    6596 B |
|                      |                 |           |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |      8.617 ns |   0.0755 ns |   0.1131 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |     71.265 ns |   1.5228 ns |   2.2792 ns |     8.27 |    0.30 | 0.0038 |     - |     - |      24 B |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |    518.528 ns |   4.2293 ns |   6.3303 ns |    60.18 |    1.10 | 0.0095 |     - |     - |      64 B |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |    525.954 ns |  10.9083 ns |  16.3270 ns |    61.04 |    2.00 | 0.0095 |     - |     - |      64 B |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    665.282 ns |   5.1517 ns |   7.7108 ns |    77.21 |    0.80 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    760.396 ns |   5.0258 ns |   7.3667 ns |    88.31 |    1.27 | 0.0849 |     - |     - |     538 B |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    824.175 ns |   2.5475 ns |   3.7342 ns |    95.72 |    1.27 | 0.0925 |     - |     - |     586 B |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |    877.052 ns |   3.3944 ns |   5.0805 ns |   101.79 |    1.43 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    704.103 ns |   4.0427 ns |   6.0509 ns |    81.72 |    1.25 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    821.890 ns |   4.5685 ns |   6.6965 ns |    95.45 |    1.41 | 0.0925 |     - |     - |     586 B |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    943.696 ns |   5.9493 ns |   8.7205 ns |   109.60 |    1.93 | 0.1030 |     - |     - |     658 B |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,025.023 ns |   8.5386 ns |  12.7802 ns |   118.97 |    2.23 | 0.1144 |     - |     - |     730 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 |    824.279 ns |  12.4152 ns |  18.5824 ns |    95.67 |    2.45 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3,610.530 ns |  33.3475 ns |  46.7486 ns |   419.58 |    7.90 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,348.887 ns |   9.1031 ns |  13.6250 ns |   156.55 |    2.50 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 |  6,099.118 ns |  85.7799 ns | 123.0230 ns |   708.11 |   18.78 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    788.053 ns |  13.5876 ns |  20.3373 ns |    91.46 |    2.58 | 0.0887 |     - |     - |     562 B |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    889.434 ns |  14.7445 ns |  22.0689 ns |   103.23 |    2.92 | 0.1001 |     - |     - |     634 B |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    947.015 ns |   5.3440 ns |   7.9987 ns |   109.91 |    1.59 | 0.1125 |     - |     - |     714 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,051.516 ns |   8.2800 ns |  12.3930 ns |   122.03 |    1.69 | 0.1240 |     - |     - |     786 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 11,469.597 ns |  75.7487 ns | 113.3770 ns | 1,331.21 |   23.20 | 1.0376 |     - |     - |    6596 B |
