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
|               Method |             Job |       Jit |       Runtime |          Mean |       Error |      StdDev |        Median |    Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |---------------- |---------- |-------------- |--------------:|------------:|------------:|--------------:|---------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |      8.768 ns |   0.0658 ns |   0.0985 ns |      8.736 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     60.064 ns |   0.5504 ns |   0.7716 ns |     59.838 ns |     6.85 |    0.13 | 0.0088 |     - |     - |      56 B |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    511.923 ns |   1.8435 ns |   2.6438 ns |    511.448 ns |    58.39 |    0.67 | 0.0210 |     - |     - |     136 B |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    511.944 ns |   4.2113 ns |   6.1729 ns |    512.749 ns |    58.39 |    0.67 | 0.0210 |     - |     - |     136 B |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    613.807 ns |   1.9670 ns |   2.8833 ns |    613.263 ns |    70.02 |    0.71 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    688.626 ns |   5.3092 ns |   7.6143 ns |    686.377 ns |    78.55 |    1.37 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    764.947 ns |   7.2434 ns |  10.6173 ns |    762.005 ns |    87.26 |    1.75 | 0.0734 |     - |     - |     464 B |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    832.960 ns |   5.5979 ns |   8.0283 ns |    832.698 ns |    95.01 |    1.24 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    665.662 ns |   5.3268 ns |   7.8079 ns |    663.816 ns |    75.93 |    1.11 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    773.023 ns |   7.9676 ns |  11.1695 ns |    769.303 ns |    88.16 |    1.60 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    882.292 ns |  10.0223 ns |  14.3737 ns |    878.161 ns |   100.64 |    2.20 | 0.0849 |     - |     - |     536 B |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    971.105 ns |   8.3009 ns |  11.9050 ns |    971.113 ns |   110.76 |    1.69 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    779.109 ns |   8.3979 ns |  12.5696 ns |    779.017 ns |    88.87 |    1.83 | 0.0706 |     - |     - |     448 B |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  3,038.440 ns |  67.0777 ns |  98.3216 ns |  2,999.289 ns |   346.60 |   11.93 | 0.3395 |     - |     - |    2144 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,351.316 ns |  12.2942 ns |  17.2348 ns |  1,353.468 ns |   154.12 |    2.81 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5,175.345 ns |  54.4660 ns |  81.5221 ns |  5,176.915 ns |   590.33 |   11.27 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    719.875 ns |   7.0508 ns |   9.8842 ns |    718.977 ns |    82.11 |    1.85 | 0.0696 |     - |     - |     440 B |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    822.445 ns |   8.5428 ns |  11.9759 ns |    821.528 ns |    93.80 |    1.78 | 0.0811 |     - |     - |     512 B |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    881.971 ns |   4.6205 ns |   6.6266 ns |    882.485 ns |   100.60 |    1.37 | 0.1116 |     - |     - |     704 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    976.644 ns |   8.0018 ns |  11.7289 ns |    977.518 ns |   111.42 |    2.22 | 0.1221 |     - |     - |     776 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10,106.112 ns | 144.2604 ns | 211.4551 ns | 10,057.634 ns | 1,152.83 |   27.34 | 1.0223 |     - |     - |    6448 B |
|                      |                 |           |               |               |             |             |               |          |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |      9.020 ns |   0.0472 ns |   0.0677 ns |      9.026 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |     65.976 ns |   0.7826 ns |   1.1472 ns |     66.123 ns |     7.31 |    0.14 | 0.0088 |     - |     - |      56 B |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |    538.201 ns |   3.3111 ns |   4.8534 ns |    537.177 ns |    59.64 |    0.73 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |    537.071 ns |   4.0553 ns |   6.0699 ns |    536.176 ns |    59.56 |    0.83 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    635.933 ns |   3.6386 ns |   5.2184 ns |    635.753 ns |    70.51 |    0.87 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    711.649 ns |   4.7683 ns |   6.8385 ns |    710.404 ns |    78.90 |    0.99 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    792.479 ns |   5.0772 ns |   7.4420 ns |    790.970 ns |    87.90 |    1.09 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |    841.865 ns |   4.9438 ns |   7.3996 ns |    842.595 ns |    93.35 |    1.09 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    676.821 ns |   4.1716 ns |   6.1147 ns |    676.682 ns |    75.01 |    0.94 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    780.977 ns |   5.6711 ns |   8.3126 ns |    781.231 ns |    86.62 |    1.03 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    875.825 ns |   3.8431 ns |   5.5117 ns |    874.845 ns |    97.11 |    0.93 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |    946.659 ns |   3.4758 ns |   5.0948 ns |    946.904 ns |   104.95 |    1.03 | 0.1154 |     - |     - |     730 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 |    770.631 ns |   4.6942 ns |   6.5806 ns |    771.846 ns |    85.45 |    0.95 | 0.0725 |     - |     - |     457 B |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3,395.798 ns |  54.2790 ns |  76.0916 ns |  3,447.605 ns |   376.55 |    8.78 | 0.3548 |     - |     - |    2247 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,330.211 ns |   4.1609 ns |   5.9675 ns |  1,330.435 ns |   147.48 |    1.24 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 |  6,039.835 ns |  24.2130 ns |  33.1430 ns |  6,041.106 ns |   669.41 |    6.44 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    721.377 ns |   1.7582 ns |   2.4066 ns |    721.274 ns |    79.95 |    0.70 | 0.0706 |     - |     - |     449 B |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    822.845 ns |   4.9665 ns |   7.2799 ns |    823.990 ns |    91.25 |    1.10 | 0.0820 |     - |     - |     522 B |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    886.522 ns |  10.2884 ns |  14.7553 ns |    881.261 ns |    98.29 |    1.94 | 0.1135 |     - |     - |     714 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    977.981 ns |   6.3794 ns |   9.1491 ns |    978.071 ns |   108.43 |    1.32 | 0.1240 |     - |     - |     786 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 11,476.133 ns |  58.4224 ns |  83.7876 ns | 11,481.754 ns | 1,272.41 |   14.14 | 1.0376 |     - |     - |    6596 B |
|                      |                 |           |               |               |             |             |               |          |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |      8.821 ns |   0.0246 ns |   0.0345 ns |      8.826 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |     63.655 ns |   0.4984 ns |   0.7306 ns |     63.416 ns |     7.22 |    0.08 | 0.0088 |     - |     - |      56 B |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |    528.000 ns |   1.3809 ns |   2.0241 ns |    527.772 ns |    59.84 |    0.36 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |    526.348 ns |   1.4714 ns |   2.0627 ns |    526.314 ns |    59.67 |    0.28 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    627.347 ns |   3.8184 ns |   5.4763 ns |    628.156 ns |    71.15 |    0.65 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    700.251 ns |   4.0209 ns |   5.8938 ns |    699.470 ns |    79.42 |    0.74 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    773.213 ns |   2.3365 ns |   3.2755 ns |    772.828 ns |    87.65 |    0.49 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |    814.169 ns |   3.4848 ns |   5.1079 ns |    812.927 ns |    92.26 |    0.73 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    662.452 ns |   4.2909 ns |   6.0153 ns |    661.337 ns |    75.10 |    0.63 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    760.379 ns |   2.6286 ns |   3.8529 ns |    761.084 ns |    86.24 |    0.51 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    870.000 ns |   2.5009 ns |   3.7432 ns |    870.646 ns |    98.61 |    0.57 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |    951.692 ns |   4.6884 ns |   6.4175 ns |    950.620 ns |   107.92 |    0.90 | 0.1154 |     - |     - |     730 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 |    769.496 ns |   2.8312 ns |   4.1500 ns |    770.538 ns |    87.23 |    0.49 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3,366.348 ns |  13.3778 ns |  19.6089 ns |  3,363.908 ns |   381.61 |    2.55 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,324.398 ns |   5.6515 ns |   8.2839 ns |  1,324.084 ns |   150.20 |    0.96 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 |  5,971.940 ns |  19.9564 ns |  28.6209 ns |  5,972.596 ns |   677.00 |    4.16 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    721.008 ns |   3.6536 ns |   5.3554 ns |    721.500 ns |    81.71 |    0.64 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    815.956 ns |   2.4157 ns |   3.4645 ns |    816.082 ns |    92.51 |    0.61 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    901.960 ns |   4.7785 ns |   6.8532 ns |    899.859 ns |   102.25 |    0.90 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    972.818 ns |   4.2677 ns |   6.2555 ns |    971.465 ns |   110.21 |    0.70 | 0.1240 |     - |     - |     786 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 11,353.780 ns |  56.0733 ns |  83.9279 ns | 11,354.007 ns | 1,285.84 |   10.66 | 1.0376 |     - |     - |    6596 B |
