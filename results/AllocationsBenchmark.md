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
|               Method |             Job |       Jit |       Runtime |          Mean |       Error |      StdDev |    Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |---------------- |---------- |-------------- |--------------:|------------:|------------:|---------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |      8.194 ns |   0.0774 ns |   0.1110 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     68.155 ns |   1.8305 ns |   2.6253 ns |     8.32 |    0.35 | 0.0038 |     - |     - |      24 B |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    499.250 ns |   8.6120 ns |  12.8900 ns |    61.02 |    1.72 | 0.0095 |     - |     - |      64 B |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    494.674 ns |   5.9387 ns |   8.8888 ns |    60.43 |    1.26 | 0.0095 |     - |     - |      64 B |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    690.076 ns |   9.2461 ns |  13.2605 ns |    84.22 |    1.87 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    752.153 ns |   7.1513 ns |  10.2562 ns |    91.81 |    1.87 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    831.642 ns |  11.0773 ns |  15.5288 ns |   101.55 |    2.26 | 0.0734 |     - |     - |     464 B |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    888.711 ns |  10.8812 ns |  15.6054 ns |   108.47 |    2.34 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    719.740 ns |   7.8984 ns |  11.8220 ns |    87.83 |    1.71 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    832.033 ns |   7.4233 ns |  11.1108 ns |   101.57 |    1.86 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    951.649 ns |  15.0283 ns |  22.4937 ns |   116.17 |    2.98 | 0.0839 |     - |     - |     536 B |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,035.163 ns |  19.1325 ns |  28.6367 ns |   126.42 |    4.24 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    841.130 ns |   7.5712 ns |  10.8584 ns |   102.66 |    1.78 | 0.0706 |     - |     - |     448 B |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  3,050.302 ns |  44.8350 ns |  65.7186 ns |   372.74 |    8.79 | 0.3395 |     - |     - |    2144 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,396.500 ns |  61.7042 ns |  92.3559 ns |   170.91 |   11.56 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5,059.593 ns |  62.8274 ns |  94.0370 ns |   617.12 |    9.87 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    782.620 ns |   6.7020 ns |   9.8237 ns |    95.47 |    1.78 | 0.0696 |     - |     - |     440 B |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    874.286 ns |   8.1481 ns |  12.1957 ns |   106.64 |    2.08 | 0.0811 |     - |     - |     512 B |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    964.046 ns |  15.7339 ns |  23.0626 ns |   117.55 |    3.39 | 0.1106 |     - |     - |     704 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,052.875 ns |  14.5109 ns |  21.7193 ns |   128.46 |    3.14 | 0.1221 |     - |     - |     776 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9,886.574 ns | 257.2755 ns | 360.6644 ns | 1,207.07 |   41.45 | 1.0223 |     - |     - |    6449 B |
|                      |                 |           |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |      8.630 ns |   0.1175 ns |   0.1759 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |     73.524 ns |   1.8200 ns |   2.7242 ns |     8.52 |    0.35 | 0.0038 |     - |     - |      24 B |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |    520.036 ns |   6.7565 ns |  10.1128 ns |    60.28 |    1.79 | 0.0095 |     - |     - |      64 B |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |    524.603 ns |   6.9070 ns |  10.3381 ns |    60.81 |    1.55 | 0.0095 |     - |     - |      64 B |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    656.707 ns |   7.1791 ns |  10.7454 ns |    76.11 |    1.23 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    729.026 ns |   7.3907 ns |  11.0620 ns |    84.51 |    2.28 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    816.536 ns |   6.2478 ns |   9.1579 ns |    94.61 |    2.16 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |    850.762 ns |   7.5934 ns |  11.1303 ns |    98.57 |    2.32 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    695.102 ns |   6.3099 ns |   9.4443 ns |    80.57 |    1.96 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    797.487 ns |   6.9018 ns |  10.3303 ns |    92.44 |    2.30 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    909.776 ns |  10.5836 ns |  15.8410 ns |   105.46 |    2.76 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |    993.333 ns |  14.1408 ns |  21.1652 ns |   115.15 |    3.46 | 0.1144 |     - |     - |     730 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 |    823.234 ns |   7.4083 ns |  11.0884 ns |    95.43 |    2.25 | 0.0725 |     - |     - |     457 B |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3,348.775 ns |  33.2809 ns |  49.8133 ns |   388.19 |   10.07 | 0.3548 |     - |     - |    2247 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,337.672 ns |  13.9286 ns |  20.8477 ns |   155.06 |    4.11 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 |  6,065.201 ns |  80.8486 ns | 121.0104 ns |   703.08 |   20.56 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    754.880 ns |   7.4386 ns |  11.1337 ns |    87.50 |    2.04 | 0.0706 |     - |     - |     449 B |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    849.367 ns |   7.2245 ns |  10.8133 ns |    98.45 |    2.09 | 0.0820 |     - |     - |     522 B |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    920.753 ns |  10.6116 ns |  15.8830 ns |   106.74 |    3.09 | 0.1125 |     - |     - |     714 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,051.022 ns |  41.2677 ns |  60.4896 ns |   121.76 |    7.01 | 0.1240 |     - |     - |     786 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 11,291.470 ns | 136.5539 ns | 204.3875 ns | 1,308.95 |   38.25 | 1.0376 |     - |     - |    6596 B |
|                      |                 |           |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |      8.623 ns |   0.1079 ns |   0.1615 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |     75.392 ns |   2.1213 ns |   3.1093 ns |     8.74 |    0.38 | 0.0038 |     - |     - |      24 B |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |    519.112 ns |   7.0423 ns |  10.5406 ns |    60.22 |    1.77 | 0.0095 |     - |     - |      64 B |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |    521.850 ns |   6.2640 ns |   9.3756 ns |    60.54 |    1.47 | 0.0095 |     - |     - |      64 B |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    655.923 ns |   7.2314 ns |  10.8236 ns |    76.09 |    1.81 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    729.120 ns |   6.9001 ns |  10.3277 ns |    84.58 |    1.89 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    819.757 ns |   6.7233 ns |  10.0632 ns |    95.10 |    2.12 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |    852.098 ns |  12.6518 ns |  17.7360 ns |    98.77 |    2.61 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    701.680 ns |   7.1369 ns |  10.6822 ns |    81.40 |    1.94 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    799.310 ns |   8.0360 ns |  12.0279 ns |    92.73 |    2.36 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    910.901 ns |   6.0387 ns |   9.0385 ns |   105.67 |    2.21 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |    998.681 ns |  16.3129 ns |  24.4164 ns |   115.86 |    3.71 | 0.1144 |     - |     - |     730 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 |    818.324 ns |   7.3839 ns |  11.0519 ns |    94.94 |    2.28 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3,351.930 ns |  31.2140 ns |  46.7197 ns |   388.86 |    9.22 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,342.369 ns |  14.8816 ns |  22.2741 ns |   155.70 |    2.23 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 |  6,097.756 ns | 156.2488 ns | 219.0389 ns |   706.89 |   30.21 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    756.448 ns |   6.2266 ns |   9.1268 ns |    87.69 |    1.83 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    851.149 ns |   5.4474 ns |   8.1535 ns |    98.74 |    1.93 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    924.023 ns |   6.9221 ns |  10.3606 ns |   107.19 |    2.30 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,023.472 ns |  14.5476 ns |  21.7741 ns |   118.75 |    4.07 | 0.1240 |     - |     - |     786 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 11,307.275 ns | 193.5085 ns | 289.6345 ns | 1,311.66 |   38.27 | 1.0376 |     - |     - |    6596 B |
