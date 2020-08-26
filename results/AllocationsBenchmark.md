``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]          : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|               Method |             Job |       Jit |       Runtime |          Mean |       Error |      StdDev |        Median |    Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |---------------- |---------- |-------------- |--------------:|------------:|------------:|--------------:|---------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |      8.060 ns |   0.0545 ns |   0.0799 ns |      8.068 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     63.635 ns |   3.0140 ns |   4.2252 ns |     60.561 ns |     7.89 |    0.54 | 0.0088 |     - |     - |      56 B |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    675.879 ns |  87.5260 ns | 125.5272 ns |    685.055 ns |    83.89 |   15.67 | 0.0210 |     - |     - |     136 B |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    629.791 ns |  42.7544 ns |  62.6689 ns |    620.197 ns |    78.13 |    7.63 | 0.0210 |     - |     - |     136 B |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    661.862 ns |   2.5769 ns |   3.6125 ns |    660.945 ns |    82.09 |    0.97 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    785.001 ns |  59.6536 ns |  87.4395 ns |    744.255 ns |    97.40 |   10.86 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,176.346 ns | 186.1918 ns | 278.6831 ns |  1,110.598 ns |   145.97 |   35.25 | 0.0725 |     - |     - |     464 B |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    924.111 ns |  66.0846 ns |  98.9123 ns |    864.975 ns |   114.98 |   12.09 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    694.250 ns |   5.9466 ns |   8.7165 ns |    691.444 ns |    86.14 |    1.03 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    781.882 ns |   6.4137 ns |   9.5997 ns |    780.316 ns |    97.04 |    1.45 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    890.312 ns |   8.2120 ns |  12.2914 ns |    889.813 ns |   110.43 |    1.89 | 0.0849 |     - |     - |     536 B |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    980.485 ns |  11.7165 ns |  17.1739 ns |    978.281 ns |   121.66 |    2.21 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    828.156 ns |  10.0204 ns |  14.6878 ns |    827.645 ns |   102.76 |    2.05 | 0.0706 |     - |     - |     448 B |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2,968.409 ns |  25.2843 ns |  37.8443 ns |  2,975.267 ns |   368.43 |    6.14 | 0.3395 |     - |     - |    2144 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,357.427 ns |  16.7160 ns |  25.0197 ns |  1,352.859 ns |   168.55 |    3.58 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5,078.553 ns |  54.8959 ns |  82.1656 ns |  5,075.928 ns |   629.58 |   11.15 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    764.631 ns |  48.1168 ns |  72.0189 ns |    737.030 ns |    94.79 |    9.18 | 0.0696 |     - |     - |     440 B |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    898.727 ns |  30.2592 ns |  41.4191 ns |    887.498 ns |   111.49 |    5.76 | 0.0811 |     - |     - |     512 B |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    968.343 ns |  12.8591 ns |  18.4421 ns |    961.037 ns |   120.18 |    2.66 | 0.1116 |     - |     - |     704 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,072.622 ns |  26.5251 ns |  39.7015 ns |  1,069.036 ns |   133.10 |    5.44 | 0.1221 |     - |     - |     776 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10,224.831 ns |  98.5718 ns | 138.1838 ns | 10,257.694 ns | 1,268.16 |   19.25 | 1.0223 |     - |     - |    6449 B |
|                      |                 |           |               |               |             |             |               |          |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |      8.702 ns |   0.0606 ns |   0.0907 ns |      8.678 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |     62.312 ns |   0.7370 ns |   1.1031 ns |     62.309 ns |     7.16 |    0.14 | 0.0088 |     - |     - |      56 B |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |    604.974 ns |  37.4543 ns |  56.0598 ns |    606.576 ns |    69.52 |    6.37 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |    638.322 ns |  20.8224 ns |  29.8628 ns |    638.970 ns |    73.37 |    3.75 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    818.296 ns |  32.3754 ns |  47.4554 ns |    803.869 ns |    94.03 |    5.45 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,011.447 ns | 138.0194 ns | 206.5810 ns |    901.211 ns |   116.22 |   23.63 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    952.445 ns |  92.8382 ns | 138.9559 ns |    895.141 ns |   109.45 |   15.91 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |    911.083 ns |   7.7926 ns |  11.4223 ns |    914.376 ns |   104.70 |    1.95 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    784.355 ns |  28.1165 ns |  41.2128 ns |    813.666 ns |    90.12 |    4.65 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    867.075 ns |  24.4074 ns |  33.4091 ns |    857.272 ns |    99.63 |    4.22 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,113.011 ns | 136.2961 ns | 186.5636 ns |  1,024.627 ns |   127.82 |   20.98 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,087.041 ns |  25.6693 ns |  35.1364 ns |  1,070.408 ns |   124.90 |    4.37 | 0.1144 |     - |     - |     730 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,015.521 ns |  74.6972 ns | 107.1285 ns |    983.764 ns |   116.68 |   12.17 | 0.0725 |     - |     - |     457 B |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3,785.875 ns |  19.6637 ns |  27.5658 ns |  3,783.498 ns |   435.13 |    5.69 | 0.3510 |     - |     - |    2247 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,415.113 ns |   6.6573 ns |   9.7582 ns |  1,413.282 ns |   162.62 |    2.22 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 |  6,269.057 ns |  57.5684 ns |  86.1656 ns |  6,253.983 ns |   720.49 |   13.19 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    947.342 ns |  92.6528 ns | 138.6784 ns |    953.002 ns |   108.91 |   16.28 | 0.0706 |     - |     - |     449 B |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    962.682 ns |  24.8565 ns |  37.2041 ns |    952.862 ns |   110.64 |    4.60 | 0.0820 |     - |     - |     522 B |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,136.021 ns |  59.4231 ns |  85.2228 ns |  1,119.238 ns |   130.54 |    9.76 | 0.1125 |     - |     - |     714 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,314.276 ns |  53.7915 ns |  77.1461 ns |  1,305.138 ns |   151.04 |    9.23 | 0.1240 |     - |     - |     786 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 12,920.469 ns | 304.5199 ns | 436.7333 ns | 12,746.667 ns | 1,484.84 |   54.46 | 1.0376 |     - |     - |    6596 B |
|                      |                 |           |               |               |             |             |               |          |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |      9.908 ns |   0.8193 ns |   1.2263 ns |      9.364 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |     82.754 ns |   9.9506 ns |  14.2708 ns |     75.771 ns |     8.60 |    2.02 | 0.0088 |     - |     - |      56 B |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |    651.394 ns |  23.6047 ns |  33.8531 ns |    642.667 ns |    67.33 |    8.44 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |    652.847 ns |  33.2896 ns |  49.8263 ns |    639.000 ns |    66.47 |    6.33 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    785.255 ns |  21.8225 ns |  31.2972 ns |    779.972 ns |    81.05 |    8.87 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    979.053 ns |  50.2906 ns |  75.2725 ns |    995.753 ns |   100.17 |   13.48 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,038.572 ns | 141.3117 ns | 202.6650 ns |    941.598 ns |   106.31 |   18.85 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,059.281 ns |  81.4383 ns | 119.3712 ns |  1,042.941 ns |   108.70 |   17.49 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    856.915 ns |  44.6755 ns |  66.8681 ns |    851.730 ns |    87.63 |   11.62 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    907.882 ns |  41.6614 ns |  59.7495 ns |    879.375 ns |    93.56 |   10.33 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,109.436 ns |  81.3077 ns | 119.1797 ns |  1,073.619 ns |   114.21 |   20.40 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,182.446 ns |  71.3401 ns | 102.3139 ns |  1,150.797 ns |   122.21 |   17.74 | 0.1144 |     - |     - |     730 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,071.850 ns |  95.5947 ns | 143.0816 ns |  1,056.117 ns |   108.89 |   13.98 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3,794.777 ns | 195.8072 ns | 274.4943 ns |  3,635.207 ns |   394.90 |   55.26 | 0.3510 |     - |     - |    2247 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,604.177 ns | 123.5998 ns | 173.2697 ns |  1,533.189 ns |   166.90 |   26.92 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 |  6,615.725 ns | 174.9338 ns | 245.2327 ns |  6,615.375 ns |   686.89 |   73.30 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    764.029 ns |   7.5820 ns |  10.6290 ns |    764.244 ns |    79.28 |    7.54 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    862.450 ns |   9.7042 ns |  14.5248 ns |    865.134 ns |    88.20 |    9.92 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    973.662 ns |  39.3100 ns |  58.8373 ns |    966.022 ns |    99.87 |   14.66 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,077.580 ns |  38.3281 ns |  56.1808 ns |  1,072.819 ns |   110.05 |    9.54 | 0.1240 |     - |     - |     786 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 11,426.417 ns | 132.7713 ns | 198.7258 ns | 11,428.809 ns | 1,168.38 |  129.77 | 1.0376 |     - |     - |    6596 B |
