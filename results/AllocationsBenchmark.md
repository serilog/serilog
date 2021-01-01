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
|             LogEmpty | core31 | .NET Core 3.1 |      7.409 ns |   0.1943 ns |   0.2723 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |     61.930 ns |   1.8112 ns |   2.7109 ns |     8.39 |    0.41 | 0.0088 |     - |     - |      56 B |
|               LogMsg | core31 | .NET Core 3.1 |    502.078 ns |   8.4852 ns |  12.4375 ns |    67.93 |    3.20 | 0.0210 |     - |     - |     136 B |
|         LogMsgWithEx | core31 | .NET Core 3.1 |    496.118 ns |   8.6451 ns |  12.9395 ns |    67.09 |    3.18 | 0.0210 |     - |     - |     136 B |
|           LogScalar1 | core31 | .NET Core 3.1 |    591.760 ns |   9.2696 ns |  13.2941 ns |    80.08 |    3.06 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 | core31 | .NET Core 3.1 |    692.803 ns |   9.0703 ns |  13.5759 ns |    93.58 |    3.93 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 | core31 | .NET Core 3.1 |    751.289 ns |   8.6906 ns |  12.7385 ns |   101.62 |    4.05 | 0.0734 |     - |     - |     464 B |
|        LogScalarMany | core31 | .NET Core 3.1 |    813.372 ns |   9.3166 ns |  13.6562 ns |   109.87 |    4.11 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |    655.993 ns |  12.1783 ns |  18.2279 ns |    88.75 |    3.97 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |    745.228 ns |   9.5010 ns |  13.6261 ns |   100.84 |    4.56 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |    847.178 ns |   8.1953 ns |  12.2664 ns |   114.63 |    4.38 | 0.0849 |     - |     - |     536 B |
|  LogScalarStructMany | core31 | .NET Core 3.1 |    941.815 ns |  14.3094 ns |  21.4176 ns |   127.23 |    5.74 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 |    745.432 ns |   8.7950 ns |  13.1639 ns |   100.69 |    3.78 | 0.0706 |     - |     - |     448 B |
|        LogDictionary | core31 | .NET Core 3.1 |  2,805.794 ns |  39.8513 ns |  59.6475 ns |   378.87 |   17.02 | 0.3395 |     - |     - |    2144 B |
|          LogSequence | core31 | .NET Core 3.1 |  1,193.194 ns |  21.1840 ns |  31.7072 ns |   161.49 |    7.27 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  4,756.349 ns |  74.1593 ns | 106.3571 ns |   643.27 |   29.95 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 | core31 | .NET Core 3.1 |    671.226 ns |   8.3208 ns |  12.4542 ns |    90.70 |    3.64 | 0.0696 |     - |     - |     440 B |
|              LogMix3 | core31 | .NET Core 3.1 |    748.716 ns |   8.5201 ns |  12.7525 ns |   101.24 |    4.38 | 0.0811 |     - |     - |     512 B |
|              LogMix4 | core31 | .NET Core 3.1 |    834.482 ns |   7.7989 ns |  11.6731 ns |   112.75 |    4.34 | 0.1116 |     - |     - |     704 B |
|              LogMix5 | core31 | .NET Core 3.1 |    904.607 ns |  16.5070 ns |  24.7069 ns |   122.05 |    5.71 | 0.1221 |     - |     - |     776 B |
|           LogMixMany | core31 | .NET Core 3.1 |  9,319.060 ns | 167.1492 ns | 250.1811 ns | 1,260.95 |   59.73 | 1.0223 |     - |     - |    6449 B |
|                      |        |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |      8.311 ns |   0.1047 ns |   0.1566 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |     62.617 ns |   1.2497 ns |   1.8705 ns |     7.54 |    0.28 | 0.0088 |     - |     - |      56 B |
|               LogMsg |  net48 |      .NET 4.8 |    508.375 ns |   8.7887 ns |  12.8823 ns |    61.26 |    2.28 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |  net48 |      .NET 4.8 |    501.585 ns |   7.0148 ns |  10.4994 ns |    60.38 |    1.86 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |  net48 |      .NET 4.8 |    595.305 ns |   7.1921 ns |  10.7648 ns |    71.65 |    1.32 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |  net48 |      .NET 4.8 |    662.552 ns |   9.1888 ns |  13.7534 ns |    79.75 |    2.04 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |  net48 |      .NET 4.8 |    729.421 ns |   7.2526 ns |  10.8553 ns |    87.80 |    2.21 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |  net48 |      .NET 4.8 |    768.957 ns |   8.4891 ns |  12.7062 ns |    92.56 |    2.45 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |    646.955 ns |   7.7575 ns |  11.6111 ns |    77.87 |    2.02 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |    739.219 ns |   9.4727 ns |  14.1783 ns |    88.97 |    2.13 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |  net48 |      .NET 4.8 |    845.970 ns |   7.8092 ns |  11.6884 ns |   101.83 |    2.35 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |  net48 |      .NET 4.8 |    919.336 ns |  10.0150 ns |  14.6799 ns |   110.76 |    2.67 | 0.1154 |     - |     - |     730 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 |    751.982 ns |   8.8587 ns |  13.2593 ns |    90.51 |    2.27 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |  net48 |      .NET 4.8 |  3,267.738 ns |  31.5227 ns |  47.1817 ns |   393.34 |    9.45 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |  net48 |      .NET 4.8 |  1,346.245 ns |  43.3205 ns |  64.8401 ns |   162.07 |    8.85 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  5,848.715 ns | 100.7946 ns | 150.8646 ns |   704.02 |   22.81 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |  net48 |      .NET 4.8 |    698.988 ns |   7.4639 ns |  11.1716 ns |    84.14 |    2.05 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |  net48 |      .NET 4.8 |    785.747 ns |   8.4450 ns |  12.6401 ns |    94.58 |    2.32 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |  net48 |      .NET 4.8 |    848.873 ns |   8.2608 ns |  12.3644 ns |   102.18 |    2.44 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |  net48 |      .NET 4.8 |    934.644 ns |   6.7103 ns |  10.0437 ns |   112.50 |    2.48 | 0.1249 |     - |     - |     786 B |
|           LogMixMany |  net48 |      .NET 4.8 | 10,849.524 ns | 161.5571 ns | 241.8110 ns | 1,305.88 |   34.96 | 1.0376 |     - |     - |    6596 B |
|                      |        |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |      7.907 ns |   0.1333 ns |   0.1996 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |     46.264 ns |   0.7697 ns |   1.1520 ns |     5.86 |    0.22 | 0.0089 |     - |     - |      56 B |
|               LogMsg |  net50 | .NET Core 5.0 |    297.596 ns |   4.0157 ns |   5.8861 ns |    37.61 |    0.74 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |    295.253 ns |   3.1353 ns |   4.6928 ns |    37.36 |    1.11 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |  net50 | .NET Core 5.0 |    377.426 ns |   5.3145 ns |   7.9545 ns |    47.76 |    1.43 | 0.0596 |     - |     - |     376 B |
|           LogScalar2 |  net50 | .NET Core 5.0 |    430.322 ns |   4.3274 ns |   6.4770 ns |    54.46 |    1.59 | 0.0672 |     - |     - |     424 B |
|           LogScalar3 |  net50 | .NET Core 5.0 |    484.157 ns |   8.4078 ns |  12.5843 ns |    61.26 |    1.97 | 0.0744 |     - |     - |     472 B |
|        LogScalarMany |  net50 | .NET Core 5.0 |    530.833 ns |   7.0213 ns |  10.5092 ns |    67.16 |    1.64 | 0.1001 |     - |     - |     632 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |    408.802 ns |   4.5901 ns |   6.8702 ns |    51.73 |    1.53 | 0.0634 |     - |     - |     400 B |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |    493.291 ns |   8.2111 ns |  12.2900 ns |    62.42 |    2.19 | 0.0744 |     - |     - |     472 B |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |    589.019 ns |   7.5750 ns |  11.3379 ns |    74.55 |    2.68 | 0.0858 |     - |     - |     544 B |
|  LogScalarStructMany |  net50 | .NET Core 5.0 |    640.478 ns |   8.0634 ns |  12.0690 ns |    81.05 |    2.55 | 0.1154 |     - |     - |     728 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |    512.341 ns |   9.5332 ns |  14.2688 ns |    64.84 |    2.62 | 0.0725 |     - |     - |     456 B |
|        LogDictionary |  net50 | .NET Core 5.0 |  2,456.222 ns |  36.6631 ns |  54.8756 ns |   310.72 |    6.17 | 0.3471 |     - |     - |    2200 B |
|          LogSequence |  net50 | .NET Core 5.0 |    952.601 ns |   7.9353 ns |  11.8772 ns |   120.55 |    3.32 | 0.1307 |     - |     - |     824 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  4,380.219 ns |  79.0941 ns | 118.3844 ns |   554.25 |   18.74 | 0.5493 |     - |     - |    3472 B |
|              LogMix2 |  net50 | .NET Core 5.0 |    446.153 ns |   4.0818 ns |   6.1095 ns |    56.47 |    1.79 | 0.0710 |     - |     - |     448 B |
|              LogMix3 |  net50 | .NET Core 5.0 |    539.957 ns |  10.8596 ns |  16.2542 ns |    68.32 |    2.31 | 0.0820 |     - |     - |     520 B |
|              LogMix4 |  net50 | .NET Core 5.0 |    593.556 ns |   9.8642 ns |  14.7642 ns |    75.13 |    3.11 | 0.1135 |     - |     - |     712 B |
|              LogMix5 |  net50 | .NET Core 5.0 |    647.744 ns |   9.6755 ns |  14.4818 ns |    81.98 |    2.93 | 0.1249 |     - |     - |     784 B |
|           LogMixMany |  net50 | .NET Core 5.0 |  8,244.910 ns | 135.9290 ns | 203.4521 ns | 1,043.56 |   41.69 | 1.0376 |     - |     - |    6537 B |
