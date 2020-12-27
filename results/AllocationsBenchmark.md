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
|             LogEmpty | core31 | .NET Core 3.1 |      7.809 ns |   0.0857 ns |   0.1283 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |     64.740 ns |   1.7467 ns |   2.6143 ns |     8.29 |    0.38 | 0.0038 |     - |     - |      24 B |
|               LogMsg | core31 | .NET Core 3.1 |    445.329 ns |   1.9309 ns |   2.8303 ns |    57.04 |    1.00 | 0.0100 |     - |     - |      64 B |
|         LogMsgWithEx | core31 | .NET Core 3.1 |    447.733 ns |   3.4427 ns |   4.9374 ns |    57.35 |    1.18 | 0.0100 |     - |     - |      64 B |
|           LogScalar1 | core31 | .NET Core 3.1 |    633.359 ns |   9.6102 ns |  13.4722 ns |    81.07 |    1.95 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 | core31 | .NET Core 3.1 |    705.406 ns |   9.5977 ns |  14.3653 ns |    90.36 |    2.44 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 | core31 | .NET Core 3.1 |    768.727 ns |   6.9170 ns |   9.9201 ns |    98.47 |    1.95 | 0.0734 |     - |     - |     464 B |
|        LogScalarMany | core31 | .NET Core 3.1 |    816.330 ns |   7.7683 ns |  11.3866 ns |   104.56 |    2.14 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |    665.364 ns |   5.7455 ns |   8.5996 ns |    85.23 |    1.87 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |    760.316 ns |   8.2454 ns |  11.8253 ns |    97.39 |    2.16 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |    870.772 ns |   6.0328 ns |   9.0296 ns |   111.54 |    1.99 | 0.0849 |     - |     - |     536 B |
|  LogScalarStructMany | core31 | .NET Core 3.1 |  1,003.266 ns |  10.2675 ns |  15.3680 ns |   128.51 |    2.69 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 |    776.307 ns |  13.5867 ns |  20.3360 ns |    99.44 |    2.99 | 0.0706 |     - |     - |     448 B |
|        LogDictionary | core31 | .NET Core 3.1 |  2,841.242 ns |  31.0674 ns |  46.5003 ns |   363.91 |    6.66 | 0.3395 |     - |     - |    2144 B |
|          LogSequence | core31 | .NET Core 3.1 |  1,248.763 ns |  14.2831 ns |  21.3782 ns |   159.96 |    3.90 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  4,879.818 ns | 112.3466 ns | 168.1552 ns |   625.09 |   24.43 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 | core31 | .NET Core 3.1 |    702.867 ns |   6.5622 ns |   9.8220 ns |    90.03 |    1.69 | 0.0696 |     - |     - |     440 B |
|              LogMix3 | core31 | .NET Core 3.1 |    785.636 ns |   6.6877 ns |  10.0099 ns |   100.63 |    2.02 | 0.0811 |     - |     - |     512 B |
|              LogMix4 | core31 | .NET Core 3.1 |    877.531 ns |   7.4181 ns |  11.1031 ns |   112.40 |    2.15 | 0.1116 |     - |     - |     704 B |
|              LogMix5 | core31 | .NET Core 3.1 |    965.814 ns |   8.4385 ns |  12.1022 ns |   123.71 |    2.52 | 0.1230 |     - |     - |     776 B |
|           LogMixMany | core31 | .NET Core 3.1 |  9,129.072 ns | 204.2485 ns | 305.7096 ns | 1,169.27 |   40.68 | 1.0223 |     - |     - |    6448 B |
|                      |        |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |      8.270 ns |   0.0755 ns |   0.1130 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |     69.279 ns |   1.1125 ns |   1.6652 ns |     8.38 |    0.23 | 0.0038 |     - |     - |      24 B |
|               LogMsg |  net48 |      .NET 4.8 |    478.712 ns |   3.5234 ns |   5.2737 ns |    57.89 |    0.73 | 0.0100 |     - |     - |      64 B |
|         LogMsgWithEx |  net48 |      .NET 4.8 |    481.433 ns |   5.2743 ns |   7.8943 ns |    58.22 |    1.12 | 0.0095 |     - |     - |      64 B |
|           LogScalar1 |  net48 |      .NET 4.8 |    615.255 ns |   5.6821 ns |   8.5047 ns |    74.41 |    1.36 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |  net48 |      .NET 4.8 |    687.317 ns |   4.7912 ns |   7.1713 ns |    83.12 |    1.49 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |  net48 |      .NET 4.8 |    763.093 ns |   4.6372 ns |   6.9408 ns |    92.28 |    1.48 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |  net48 |      .NET 4.8 |    805.036 ns |   5.8587 ns |   8.7690 ns |    97.36 |    1.85 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |    656.544 ns |   5.0254 ns |   7.5218 ns |    79.40 |    1.24 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |    755.637 ns |   5.1945 ns |   7.7749 ns |    91.38 |    1.54 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |  net48 |      .NET 4.8 |    867.299 ns |   6.3733 ns |   9.1404 ns |   104.83 |    1.94 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |  net48 |      .NET 4.8 |    941.755 ns |   7.8985 ns |  11.8221 ns |   113.89 |    2.22 | 0.1154 |     - |     - |     730 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 |    747.705 ns |   4.8631 ns |   6.9745 ns |    90.37 |    1.49 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |  net48 |      .NET 4.8 |  3,269.636 ns |  36.0714 ns |  53.9900 ns |   395.40 |    7.44 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |  net48 |      .NET 4.8 |  1,357.689 ns |  13.9153 ns |  20.3969 ns |   164.16 |    3.21 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  5,774.865 ns |  37.4581 ns |  56.0655 ns |   698.41 |   13.16 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |  net48 |      .NET 4.8 |    713.541 ns |   5.9935 ns |   8.7852 ns |    86.28 |    1.66 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |  net48 |      .NET 4.8 |    801.181 ns |   5.1359 ns |   7.6871 ns |    96.89 |    1.54 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |  net48 |      .NET 4.8 |    883.047 ns |   4.8307 ns |   7.2303 ns |   106.79 |    1.70 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |  net48 |      .NET 4.8 |    975.648 ns |   9.5194 ns |  14.2482 ns |   117.99 |    2.49 | 0.1240 |     - |     - |     786 B |
|           LogMixMany |  net48 |      .NET 4.8 | 10,715.085 ns | 143.4622 ns | 214.7275 ns | 1,295.77 |   28.33 | 1.0376 |     - |     - |    6596 B |
|                      |        |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |      8.237 ns |   0.1350 ns |   0.2021 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |     51.582 ns |   0.7009 ns |   1.0490 ns |     6.27 |    0.18 | 0.0038 |     - |     - |      24 B |
|               LogMsg |  net50 | .NET Core 5.0 |    255.879 ns |   2.1329 ns |   3.1924 ns |    31.09 |    0.96 | 0.0100 |     - |     - |      64 B |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |    255.159 ns |   1.7412 ns |   2.6061 ns |    31.00 |    0.94 | 0.0100 |     - |     - |      64 B |
|           LogScalar1 |  net50 | .NET Core 5.0 |    363.004 ns |   2.3030 ns |   3.4470 ns |    44.10 |    1.10 | 0.0596 |     - |     - |     376 B |
|           LogScalar2 |  net50 | .NET Core 5.0 |    415.574 ns |   2.6979 ns |   3.9546 ns |    50.47 |    1.30 | 0.0672 |     - |     - |     424 B |
|           LogScalar3 |  net50 | .NET Core 5.0 |    477.768 ns |   4.1313 ns |   6.1835 ns |    58.03 |    1.45 | 0.0744 |     - |     - |     472 B |
|        LogScalarMany |  net50 | .NET Core 5.0 |    518.147 ns |   5.7313 ns |   8.4009 ns |    62.92 |    1.71 | 0.1001 |     - |     - |     632 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |    395.900 ns |   3.2274 ns |   4.6287 ns |    48.00 |    1.27 | 0.0634 |     - |     - |     400 B |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |    491.971 ns |   6.1814 ns |   9.2520 ns |    59.77 |    2.18 | 0.0744 |     - |     - |     472 B |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |    581.741 ns |   8.1860 ns |  12.2525 ns |    70.69 |    2.86 | 0.0858 |     - |     - |     544 B |
|  LogScalarStructMany |  net50 | .NET Core 5.0 |    637.712 ns |   6.1067 ns |   9.1402 ns |    77.48 |    2.49 | 0.1154 |     - |     - |     728 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |    491.157 ns |   5.3084 ns |   7.7810 ns |    59.65 |    1.92 | 0.0725 |     - |     - |     456 B |
|        LogDictionary |  net50 | .NET Core 5.0 |  2,425.920 ns |  22.1869 ns |  32.5213 ns |   294.66 |    9.70 | 0.3471 |     - |     - |    2200 B |
|          LogSequence |  net50 | .NET Core 5.0 |    953.135 ns |   9.0696 ns |  13.5750 ns |   115.80 |    3.68 | 0.1297 |     - |     - |     824 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  4,483.868 ns |  55.4113 ns |  82.9370 ns |   544.77 |   19.31 | 0.5493 |     - |     - |    3472 B |
|              LogMix2 |  net50 | .NET Core 5.0 |    447.200 ns |   3.9389 ns |   5.6490 ns |    54.23 |    1.86 | 0.0710 |     - |     - |     448 B |
|              LogMix3 |  net50 | .NET Core 5.0 |    528.842 ns |   5.9606 ns |   8.9216 ns |    64.24 |    1.90 | 0.0820 |     - |     - |     520 B |
|              LogMix4 |  net50 | .NET Core 5.0 |    582.535 ns |   6.5578 ns |   9.8153 ns |    70.75 |    1.38 | 0.1135 |     - |     - |     712 B |
|              LogMix5 |  net50 | .NET Core 5.0 |    665.365 ns |   7.1306 ns |  10.6727 ns |    80.85 |    2.95 | 0.1249 |     - |     - |     784 B |
|           LogMixMany |  net50 | .NET Core 5.0 |  8,371.161 ns |  84.7886 ns | 126.9076 ns | 1,017.02 |   32.88 | 1.0376 |     - |     - |    6537 B |
