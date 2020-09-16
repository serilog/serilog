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
|               Method |             Job |       Jit |       Runtime |          Mean |       Error |      StdDev |        Median |    Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |---------------- |---------- |-------------- |--------------:|------------:|------------:|--------------:|---------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |      8.043 ns |   0.4169 ns |   0.5845 ns |      8.295 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     60.634 ns |   1.9341 ns |   2.8948 ns |     59.641 ns |     7.54 |    0.71 | 0.0089 |     - |     - |      56 B |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    537.143 ns |   9.8056 ns |  14.6766 ns |    533.043 ns |    66.91 |    5.48 | 0.0210 |     - |     - |     136 B |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    536.498 ns |   9.0288 ns |  13.5139 ns |    537.902 ns |    66.96 |    4.68 | 0.0210 |     - |     - |     136 B |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    649.931 ns |   8.2466 ns |  12.0878 ns |    652.135 ns |    81.31 |    6.07 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    719.960 ns |   8.4872 ns |  11.8979 ns |    717.898 ns |    89.97 |    6.69 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    771.964 ns |   9.3695 ns |  14.0238 ns |    769.750 ns |    96.53 |    7.14 | 0.0734 |     - |     - |     464 B |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    834.288 ns |  20.5657 ns |  30.1449 ns |    832.437 ns |   104.42 |    5.77 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    651.949 ns |   3.1488 ns |   4.3101 ns |    652.398 ns |    81.29 |    6.29 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    762.146 ns |  13.8768 ns |  20.3404 ns |    763.176 ns |    95.22 |    7.98 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    892.159 ns |  12.5515 ns |  18.7865 ns |    892.319 ns |   111.44 |    8.00 | 0.0849 |     - |     - |     536 B |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    911.055 ns |   3.7998 ns |   5.5696 ns |    912.190 ns |   113.84 |    8.01 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    788.764 ns |  30.6955 ns |  44.9930 ns |    750.523 ns |    98.44 |   12.23 | 0.0706 |     - |     - |     448 B |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2,888.680 ns |  72.2655 ns | 108.1636 ns |  2,853.700 ns |   361.43 |   15.02 | 0.3395 |     - |     - |    2144 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,268.304 ns |  15.2159 ns |  22.3032 ns |  1,269.600 ns |   158.11 |   11.15 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5,031.333 ns |  66.8327 ns |  97.9625 ns |  5,016.079 ns |   629.78 |   51.53 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    739.621 ns |  13.4189 ns |  20.0848 ns |    742.942 ns |    92.61 |    8.25 | 0.0696 |     - |     - |     440 B |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    804.568 ns |  10.5151 ns |  14.7407 ns |    797.553 ns |   100.62 |    8.52 | 0.0811 |     - |     - |     512 B |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    859.824 ns |   3.2128 ns |   4.5039 ns |    858.981 ns |   107.43 |    7.59 | 0.1116 |     - |     - |     704 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    923.814 ns |   3.6947 ns |   5.0573 ns |    922.244 ns |   115.16 |    8.52 | 0.1221 |     - |     - |     776 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9,201.566 ns |  51.9278 ns |  76.1151 ns |  9,181.883 ns | 1,149.03 |   89.11 | 1.0223 |     - |     - |    6448 B |
|                      |                 |           |               |               |             |             |               |          |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |      8.228 ns |   0.0419 ns |   0.0601 ns |      8.243 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |     60.586 ns |   0.4444 ns |   0.6373 ns |     60.641 ns |     7.36 |    0.10 | 0.0088 |     - |     - |      56 B |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |    526.040 ns |   2.1607 ns |   3.0291 ns |    526.037 ns |    63.91 |    0.48 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |    524.356 ns |   1.3836 ns |   2.0281 ns |    524.408 ns |    63.73 |    0.62 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    640.530 ns |   2.6884 ns |   3.7688 ns |    641.875 ns |    77.82 |    0.68 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    702.473 ns |   2.9399 ns |   4.2163 ns |    703.110 ns |    85.38 |    0.81 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    765.662 ns |   1.5872 ns |   2.2763 ns |    765.492 ns |    93.06 |    0.73 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |    814.248 ns |   2.2484 ns |   3.2957 ns |    814.326 ns |    98.96 |    0.92 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    665.375 ns |   6.3198 ns |   9.0637 ns |    662.808 ns |    80.87 |    1.32 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    760.408 ns |   2.1570 ns |   3.0936 ns |    760.280 ns |    92.42 |    0.77 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    863.807 ns |   1.8057 ns |   2.6468 ns |    863.649 ns |   104.98 |    0.87 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |    941.289 ns |   2.8591 ns |   4.0081 ns |    940.009 ns |   114.35 |    0.84 | 0.1154 |     - |     - |     730 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 |    763.117 ns |   1.5055 ns |   2.2533 ns |    762.793 ns |    92.74 |    0.78 | 0.0725 |     - |     - |     457 B |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3,122.967 ns |   8.7612 ns |  12.5651 ns |  3,123.491 ns |   379.55 |    3.04 | 0.3548 |     - |     - |    2247 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,305.242 ns |  13.4918 ns |  20.1939 ns |  1,304.874 ns |   158.84 |    2.25 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 |  6,245.435 ns | 417.6387 ns | 612.1694 ns |  5,691.429 ns |   761.39 |   72.16 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    719.708 ns |   2.1901 ns |   3.2102 ns |    720.416 ns |    87.48 |    0.62 | 0.0706 |     - |     - |     449 B |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    804.012 ns |   1.3318 ns |   1.9101 ns |    803.904 ns |    97.72 |    0.75 | 0.0820 |     - |     - |     522 B |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    867.908 ns |   1.2799 ns |   1.8761 ns |    867.650 ns |   105.47 |    0.79 | 0.1135 |     - |     - |     714 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    966.746 ns |   3.6777 ns |   5.3907 ns |    967.455 ns |   117.48 |    1.27 | 0.1240 |     - |     - |     786 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10,826.726 ns | 155.6678 ns | 228.1758 ns | 10,672.571 ns | 1,316.81 |   25.08 | 1.0376 |     - |     - |    6596 B |
|                      |                 |           |               |               |             |             |               |          |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |      8.256 ns |   0.0307 ns |   0.0450 ns |      8.254 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |     60.852 ns |   0.6575 ns |   0.9638 ns |     60.724 ns |     7.37 |    0.13 | 0.0088 |     - |     - |      56 B |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |    526.323 ns |   1.3004 ns |   1.8651 ns |    526.919 ns |    63.75 |    0.46 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |    527.563 ns |   1.5371 ns |   2.2045 ns |    527.636 ns |    63.90 |    0.48 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    637.608 ns |   0.9306 ns |   1.3641 ns |    637.628 ns |    77.23 |    0.43 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    703.399 ns |   2.0388 ns |   2.9239 ns |    702.840 ns |    85.20 |    0.55 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    764.867 ns |   2.2351 ns |   3.2056 ns |    764.263 ns |    92.64 |    0.64 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |    818.531 ns |   1.7048 ns |   2.4989 ns |    818.992 ns |    99.15 |    0.63 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    663.012 ns |   6.8466 ns |   9.8192 ns |    661.055 ns |    80.31 |    1.26 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    795.792 ns |   8.8451 ns |  12.9650 ns |    791.565 ns |    96.40 |    1.78 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    892.648 ns |  25.7454 ns |  37.7373 ns |    870.673 ns |   108.12 |    4.51 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |    940.515 ns |   1.7738 ns |   2.4867 ns |    939.658 ns |   113.91 |    0.66 | 0.1154 |     - |     - |     730 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 |    763.360 ns |   1.7362 ns |   2.4901 ns |    763.361 ns |    92.46 |    0.60 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3,146.654 ns |  24.9651 ns |  34.9975 ns |  3,159.319 ns |   381.10 |    5.10 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,285.480 ns |   7.6807 ns |  11.2582 ns |  1,286.287 ns |   155.71 |    1.73 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 |  5,662.640 ns |  29.3874 ns |  42.1465 ns |  5,677.400 ns |   685.87 |    6.90 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    723.425 ns |   2.0832 ns |   3.0535 ns |    723.033 ns |    87.63 |    0.63 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    802.051 ns |   1.6177 ns |   2.2678 ns |    802.046 ns |    97.14 |    0.67 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    868.305 ns |   1.4279 ns |   2.0478 ns |    867.852 ns |   105.17 |    0.59 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    964.931 ns |   2.2546 ns |   3.3746 ns |    964.796 ns |   116.87 |    0.79 | 0.1240 |     - |     - |     786 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10,804.742 ns | 160.7111 ns | 235.5682 ns | 10,685.182 ns | 1,308.81 |   30.41 | 1.0376 |     - |     - |    6596 B |
