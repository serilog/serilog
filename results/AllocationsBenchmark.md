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
|               Method |    Job |       Runtime |          Mean |       Error |      StdDev |        Median |    Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |------- |-------------- |--------------:|------------:|------------:|--------------:|---------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty | core31 | .NET Core 3.1 |     14.320 ns |   0.5765 ns |   0.8450 ns |     14.155 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |    142.061 ns |  42.3645 ns |  63.4091 ns |    162.842 ns |    10.34 |    4.75 | 0.0088 |     - |     - |      56 B |
|               LogMsg | core31 | .NET Core 3.1 |    976.209 ns |  41.8124 ns |  62.5829 ns |    966.246 ns |    67.97 |    4.78 | 0.0210 |     - |     - |     136 B |
|         LogMsgWithEx | core31 | .NET Core 3.1 |    694.036 ns | 160.1110 ns | 239.6467 ns |    530.021 ns |    49.59 |   19.01 | 0.0210 |     - |     - |     136 B |
|           LogScalar1 | core31 | .NET Core 3.1 |    625.912 ns |   6.5581 ns |   9.8158 ns |    627.161 ns |    43.81 |    2.37 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 | core31 | .NET Core 3.1 |    691.616 ns |   8.1303 ns |  11.6603 ns |    690.852 ns |    48.69 |    2.97 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 | core31 | .NET Core 3.1 |    761.217 ns |   8.1213 ns |  12.1555 ns |    761.544 ns |    53.27 |    2.79 | 0.0734 |     - |     - |     464 B |
|        LogScalarMany | core31 | .NET Core 3.1 |    823.370 ns |   5.7359 ns |   8.0409 ns |    824.439 ns |    58.11 |    3.00 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |    658.611 ns |  13.9183 ns |  20.8323 ns |    659.810 ns |    46.07 |    2.77 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |    740.813 ns |  12.8188 ns |  19.1866 ns |    743.416 ns |    51.84 |    2.81 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |    863.436 ns |  10.1165 ns |  14.1820 ns |    866.670 ns |    60.92 |    2.98 | 0.0849 |     - |     - |     536 B |
|  LogScalarStructMany | core31 | .NET Core 3.1 |    935.376 ns |  13.7387 ns |  20.1379 ns |    929.072 ns |    65.55 |    4.33 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 |    769.417 ns |  13.1075 ns |  19.2128 ns |    769.845 ns |    53.94 |    3.91 | 0.0706 |     - |     - |     448 B |
|        LogDictionary | core31 | .NET Core 3.1 |  2,919.551 ns |  30.7153 ns |  45.0221 ns |  2,927.642 ns |   204.48 |   10.97 | 0.3395 |     - |     - |    2144 B |
|          LogSequence | core31 | .NET Core 3.1 |  1,276.245 ns |  13.1765 ns |  19.3139 ns |  1,274.135 ns |    89.40 |    5.07 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  5,056.365 ns |  47.8339 ns |  68.6019 ns |  5,064.730 ns |   355.91 |   20.48 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 | core31 | .NET Core 3.1 |    706.033 ns |   9.6149 ns |  14.3911 ns |    705.768 ns |    49.49 |    2.93 | 0.0696 |     - |     - |     440 B |
|              LogMix3 | core31 | .NET Core 3.1 |    778.258 ns |   7.7260 ns |  11.5639 ns |    775.650 ns |    54.52 |    3.59 | 0.0811 |     - |     - |     512 B |
|              LogMix4 | core31 | .NET Core 3.1 |    862.784 ns |   7.4048 ns |  10.6198 ns |    861.686 ns |    60.73 |    3.42 | 0.1116 |     - |     - |     704 B |
|              LogMix5 | core31 | .NET Core 3.1 |    943.048 ns |  11.6846 ns |  17.4889 ns |    940.132 ns |    66.07 |    4.10 | 0.1230 |     - |     - |     776 B |
|           LogMixMany | core31 | .NET Core 3.1 |  9,514.369 ns | 167.4202 ns | 250.5866 ns |  9,521.524 ns |   665.36 |   33.69 | 1.0223 |     - |     - |    6449 B |
|                      |        |               |               |             |             |               |          |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |      8.591 ns |   0.1179 ns |   0.1764 ns |      8.589 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |     64.999 ns |   1.3859 ns |   2.0744 ns |     64.962 ns |     7.57 |    0.23 | 0.0088 |     - |     - |      56 B |
|               LogMsg |  net48 |      .NET 4.8 |    520.264 ns |   6.4397 ns |   9.6387 ns |    518.754 ns |    60.57 |    1.39 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |  net48 |      .NET 4.8 |    519.939 ns |   7.0523 ns |  10.5555 ns |    519.619 ns |    60.54 |    1.65 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |  net48 |      .NET 4.8 |    620.179 ns |   7.6141 ns |  11.1606 ns |    618.855 ns |    72.20 |    2.19 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |  net48 |      .NET 4.8 |    690.360 ns |   6.9529 ns |  10.4068 ns |    690.552 ns |    80.39 |    2.04 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |  net48 |      .NET 4.8 |    758.901 ns |   5.1905 ns |   7.2763 ns |    761.072 ns |    88.32 |    1.82 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |  net48 |      .NET 4.8 |    806.358 ns |   9.0270 ns |  13.5113 ns |    807.373 ns |    93.89 |    2.32 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |    676.641 ns |   7.7202 ns |  11.5552 ns |    681.791 ns |    78.80 |    2.35 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |    772.008 ns |   7.7158 ns |  11.5487 ns |    774.137 ns |    89.89 |    1.99 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |  net48 |      .NET 4.8 |    879.370 ns |   7.7978 ns |  11.6714 ns |    879.251 ns |   102.39 |    2.28 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |  net48 |      .NET 4.8 |    963.107 ns |  12.3684 ns |  18.5124 ns |    965.845 ns |   112.15 |    3.32 | 0.1144 |     - |     - |     730 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 |    787.559 ns |  12.7827 ns |  19.1325 ns |    784.358 ns |    91.69 |    2.42 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |  net48 |      .NET 4.8 |  3,276.868 ns |  27.2876 ns |  40.8427 ns |  3,289.913 ns |   381.55 |    8.55 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |  net48 |      .NET 4.8 |  1,295.158 ns |  17.4380 ns |  26.1003 ns |  1,297.171 ns |   150.80 |    3.91 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  5,789.448 ns |  80.2297 ns | 117.5996 ns |  5,801.383 ns |   673.99 |   19.93 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |  net48 |      .NET 4.8 |    708.672 ns |   7.3527 ns |  10.3075 ns |    713.046 ns |    82.48 |    2.07 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |  net48 |      .NET 4.8 |    795.593 ns |  10.3638 ns |  14.5286 ns |    794.947 ns |    92.58 |    2.03 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |  net48 |      .NET 4.8 |    852.240 ns |   8.9352 ns |  13.3739 ns |    850.955 ns |    99.24 |    2.48 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |  net48 |      .NET 4.8 |    951.274 ns |  12.6524 ns |  18.9376 ns |    947.109 ns |   110.78 |    3.44 | 0.1249 |     - |     - |     786 B |
|           LogMixMany |  net48 |      .NET 4.8 | 10,924.904 ns | 153.7684 ns | 230.1534 ns | 10,966.084 ns | 1,272.00 |   32.42 | 1.0376 |     - |     - |    6596 B |
|                      |        |               |               |             |             |               |          |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |      7.742 ns |   0.1605 ns |   0.2403 ns |      7.813 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |     46.285 ns |   0.6270 ns |   0.9385 ns |     46.545 ns |     5.98 |    0.23 | 0.0089 |     - |     - |      56 B |
|               LogMsg |  net50 | .NET Core 5.0 |    265.006 ns |   3.9573 ns |   5.9231 ns |    264.441 ns |    34.25 |    1.10 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |    264.502 ns |   4.9818 ns |   7.4565 ns |    264.905 ns |    34.20 |    1.51 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |  net50 | .NET Core 5.0 |    345.322 ns |   4.1239 ns |   6.1725 ns |    345.172 ns |    44.64 |    1.46 | 0.0596 |     - |     - |     376 B |
|           LogScalar2 |  net50 | .NET Core 5.0 |    397.570 ns |   3.3736 ns |   5.0495 ns |    398.609 ns |    51.40 |    1.58 | 0.0672 |     - |     - |     424 B |
|           LogScalar3 |  net50 | .NET Core 5.0 |    462.967 ns |   7.8275 ns |  11.7158 ns |    458.978 ns |    59.86 |    2.42 | 0.0744 |     - |     - |     472 B |
|        LogScalarMany |  net50 | .NET Core 5.0 |    505.080 ns |   8.1893 ns |  12.2574 ns |    503.062 ns |    65.31 |    2.86 | 0.1001 |     - |     - |     632 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |    375.639 ns |   4.1351 ns |   6.1893 ns |    377.038 ns |    48.57 |    1.89 | 0.0634 |     - |     - |     400 B |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |    458.290 ns |   3.5560 ns |   4.9851 ns |    458.923 ns |    59.18 |    2.07 | 0.0749 |     - |     - |     472 B |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |    553.440 ns |   8.8101 ns |  13.1866 ns |    556.937 ns |    71.55 |    2.81 | 0.0858 |     - |     - |     544 B |
|  LogScalarStructMany |  net50 | .NET Core 5.0 |    607.717 ns |   8.8971 ns |  13.3167 ns |    610.218 ns |    78.59 |    3.42 | 0.1154 |     - |     - |     728 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |    485.770 ns |   9.7228 ns |  14.5527 ns |    482.649 ns |    62.81 |    2.90 | 0.0725 |     - |     - |     456 B |
|        LogDictionary |  net50 | .NET Core 5.0 |  2,386.648 ns |  35.3003 ns |  52.8358 ns |  2,367.350 ns |   308.62 |   13.31 | 0.3471 |     - |     - |    2200 B |
|          LogSequence |  net50 | .NET Core 5.0 |    929.000 ns |  14.4211 ns |  20.2163 ns |    932.182 ns |   119.96 |    4.72 | 0.1307 |     - |     - |     824 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  4,331.551 ns |  96.0944 ns | 143.8296 ns |  4,304.404 ns |   559.81 |   20.37 | 0.5493 |     - |     - |    3472 B |
|              LogMix2 |  net50 | .NET Core 5.0 |    423.976 ns |   4.4343 ns |   6.4997 ns |    425.894 ns |    54.73 |    1.83 | 0.0710 |     - |     - |     448 B |
|              LogMix3 |  net50 | .NET Core 5.0 |    494.912 ns |   9.2916 ns |  13.9073 ns |    494.210 ns |    63.99 |    2.79 | 0.0820 |     - |     - |     520 B |
|              LogMix4 |  net50 | .NET Core 5.0 |    550.607 ns |   8.8404 ns |  13.2319 ns |    547.404 ns |    71.16 |    1.94 | 0.1135 |     - |     - |     712 B |
|              LogMix5 |  net50 | .NET Core 5.0 |    621.668 ns |   7.9336 ns |  11.8746 ns |    619.421 ns |    80.39 |    3.41 | 0.1249 |     - |     - |     784 B |
|           LogMixMany |  net50 | .NET Core 5.0 |  8,342.901 ns | 195.1106 ns | 292.0324 ns |  8,315.897 ns | 1,078.35 |   44.18 | 1.0376 |     - |     - |    6537 B |
