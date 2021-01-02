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
|             LogEmpty | core31 | .NET Core 3.1 |      7.270 ns |   0.0829 ns |   0.1215 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |     58.770 ns |   1.2432 ns |   1.8607 ns |     8.09 |    0.26 | 0.0089 |     - |     - |      56 B |
|               LogMsg | core31 | .NET Core 3.1 |    492.679 ns |   6.0118 ns |   8.9982 ns |    67.78 |    1.42 | 0.0210 |     - |     - |     136 B |
|         LogMsgWithEx | core31 | .NET Core 3.1 |    499.403 ns |   4.9001 ns |   7.3342 ns |    68.69 |    1.24 | 0.0210 |     - |     - |     136 B |
|           LogScalar1 | core31 | .NET Core 3.1 |    632.929 ns |  57.5296 ns |  86.1076 ns |    86.49 |   11.96 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 | core31 | .NET Core 3.1 |    661.840 ns |   9.0338 ns |  13.2416 ns |    91.06 |    2.08 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 | core31 | .NET Core 3.1 |    729.127 ns |  11.3636 ns |  17.0085 ns |   100.35 |    3.32 | 0.0734 |     - |     - |     464 B |
|        LogScalarMany | core31 | .NET Core 3.1 |    770.643 ns |   6.3212 ns |   9.4613 ns |   106.04 |    2.12 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |    620.870 ns |   7.0474 ns |  10.3299 ns |    85.42 |    1.92 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |    705.926 ns |   6.2251 ns |   9.3174 ns |    97.05 |    1.93 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |    818.348 ns |  10.2546 ns |  15.0311 ns |   112.60 |    2.87 | 0.0849 |     - |     - |     536 B |
|  LogScalarStructMany | core31 | .NET Core 3.1 |    896.847 ns |   6.1224 ns |   8.9741 ns |   123.40 |    2.40 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 |    736.834 ns |   7.2581 ns |  10.4093 ns |   101.44 |    2.17 | 0.0706 |     - |     - |     448 B |
|        LogDictionary | core31 | .NET Core 3.1 |  2,773.872 ns |  21.6903 ns |  32.4650 ns |   381.80 |    8.08 | 0.3395 |     - |     - |    2144 B |
|          LogSequence | core31 | .NET Core 3.1 |  1,214.390 ns |  18.5712 ns |  26.6343 ns |   167.21 |    5.55 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  4,845.339 ns |  52.0137 ns |  77.8517 ns |   666.45 |   13.43 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 | core31 | .NET Core 3.1 |    670.834 ns |   7.0371 ns |  10.5328 ns |    92.29 |    2.40 | 0.0696 |     - |     - |     440 B |
|              LogMix3 | core31 | .NET Core 3.1 |    755.554 ns |  11.0032 ns |  15.4249 ns |   104.00 |    2.68 | 0.0811 |     - |     - |     512 B |
|              LogMix4 | core31 | .NET Core 3.1 |    825.220 ns |   5.6948 ns |   8.5237 ns |   113.54 |    2.22 | 0.1116 |     - |     - |     704 B |
|              LogMix5 | core31 | .NET Core 3.1 |    905.879 ns |   5.4363 ns |   7.9685 ns |   124.64 |    2.27 | 0.1230 |     - |     - |     776 B |
|           LogMixMany | core31 | .NET Core 3.1 |  9,017.994 ns |  95.2043 ns | 142.4974 ns | 1,240.55 |   34.62 | 1.0223 |     - |     - |    6449 B |
|                      |        |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |      8.156 ns |   0.1022 ns |   0.1529 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |     60.322 ns |   0.8311 ns |   1.2440 ns |     7.40 |    0.21 | 0.0088 |     - |     - |      56 B |
|               LogMsg |  net48 |      .NET 4.8 |    496.880 ns |   5.0727 ns |   7.5926 ns |    60.95 |    1.46 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |  net48 |      .NET 4.8 |    489.079 ns |   6.0653 ns |   9.0782 ns |    59.99 |    1.77 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |  net48 |      .NET 4.8 |    589.574 ns |   6.2324 ns |   9.3283 ns |    72.32 |    1.78 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |  net48 |      .NET 4.8 |    656.243 ns |   5.3355 ns |   7.9859 ns |    80.50 |    1.91 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |  net48 |      .NET 4.8 |    724.847 ns |   4.6218 ns |   6.9178 ns |    88.91 |    1.90 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |  net48 |      .NET 4.8 |    766.177 ns |   6.0545 ns |   9.0620 ns |    93.98 |    2.14 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |    640.328 ns |   6.2684 ns |   9.3823 ns |    78.53 |    1.47 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |    732.580 ns |   6.2423 ns |   9.3432 ns |    89.85 |    1.96 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |  net48 |      .NET 4.8 |    833.447 ns |   6.4150 ns |   9.6016 ns |   102.23 |    2.40 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |  net48 |      .NET 4.8 |    901.972 ns |   4.2292 ns |   6.1992 ns |   110.59 |    2.20 | 0.1154 |     - |     - |     730 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 |    741.636 ns |   6.5321 ns |   9.3682 ns |    90.99 |    2.03 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |  net48 |      .NET 4.8 |  3,136.617 ns |  23.0788 ns |  34.5432 ns |   384.73 |    8.16 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |  net48 |      .NET 4.8 |  1,258.319 ns |  11.9742 ns |  17.9224 ns |   154.33 |    3.18 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  5,567.675 ns |  64.1493 ns |  96.0156 ns |   682.91 |   17.16 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |  net48 |      .NET 4.8 |    685.348 ns |   6.5351 ns |   9.7814 ns |    84.06 |    1.99 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |  net48 |      .NET 4.8 |    781.292 ns |   9.6674 ns |  13.8648 ns |    95.86 |    2.49 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |  net48 |      .NET 4.8 |    847.550 ns |   5.1980 ns |   7.6191 ns |   103.92 |    2.13 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |  net48 |      .NET 4.8 |    935.502 ns |   6.1422 ns |   9.1934 ns |   114.74 |    2.28 | 0.1249 |     - |     - |     786 B |
|           LogMixMany |  net48 |      .NET 4.8 | 10,789.357 ns | 243.8633 ns | 365.0032 ns | 1,323.23 |   46.60 | 1.0376 |     - |     - |    6596 B |
|                      |        |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |      7.558 ns |   0.0960 ns |   0.1437 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |     45.885 ns |   0.4559 ns |   0.6823 ns |     6.07 |    0.15 | 0.0089 |     - |     - |      56 B |
|               LogMsg |  net50 | .NET Core 5.0 |    268.522 ns |   3.5401 ns |   5.2986 ns |    35.54 |    1.05 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |    266.014 ns |   4.1599 ns |   6.2264 ns |    35.21 |    1.23 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |  net50 | .NET Core 5.0 |    345.816 ns |   3.2875 ns |   4.8188 ns |    45.76 |    1.05 | 0.0596 |     - |     - |     376 B |
|           LogScalar2 |  net50 | .NET Core 5.0 |    397.720 ns |   2.9464 ns |   4.4101 ns |    52.64 |    1.13 | 0.0672 |     - |     - |     424 B |
|           LogScalar3 |  net50 | .NET Core 5.0 |    449.709 ns |   3.1620 ns |   4.7327 ns |    59.52 |    1.38 | 0.0749 |     - |     - |     472 B |
|        LogScalarMany |  net50 | .NET Core 5.0 |    502.447 ns |   6.8128 ns |  10.1971 ns |    66.49 |    1.57 | 0.1001 |     - |     - |     632 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |    377.402 ns |   3.2911 ns |   4.9260 ns |    49.95 |    1.25 | 0.0634 |     - |     - |     400 B |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |    463.058 ns |   5.3046 ns |   7.9396 ns |    61.28 |    1.14 | 0.0744 |     - |     - |     472 B |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |    551.547 ns |   5.8851 ns |   8.8085 ns |    73.01 |    2.15 | 0.0858 |     - |     - |     544 B |
|  LogScalarStructMany |  net50 | .NET Core 5.0 |    607.706 ns |   6.7650 ns |  10.1255 ns |    80.43 |    1.88 | 0.1154 |     - |     - |     728 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |    487.887 ns |   3.4851 ns |   5.2164 ns |    64.57 |    1.34 | 0.0725 |     - |     - |     456 B |
|        LogDictionary |  net50 | .NET Core 5.0 |  2,412.137 ns |  25.2666 ns |  37.8179 ns |   319.22 |    6.00 | 0.3471 |     - |     - |    2200 B |
|          LogSequence |  net50 | .NET Core 5.0 |    910.990 ns |   4.3231 ns |   6.4706 ns |   120.57 |    2.45 | 0.1307 |     - |     - |     824 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  4,332.777 ns |  79.0534 ns | 118.3235 ns |   573.41 |   17.49 | 0.5493 |     - |     - |    3472 B |
|              LogMix2 |  net50 | .NET Core 5.0 |    419.830 ns |   2.8194 ns |   4.2199 ns |    55.57 |    1.19 | 0.0710 |     - |     - |     448 B |
|              LogMix3 |  net50 | .NET Core 5.0 |    499.860 ns |   6.3144 ns |   9.4511 ns |    66.15 |    1.40 | 0.0820 |     - |     - |     520 B |
|              LogMix4 |  net50 | .NET Core 5.0 |    560.613 ns |   9.9458 ns |  14.2640 ns |    74.26 |    2.07 | 0.1135 |     - |     - |     712 B |
|              LogMix5 |  net50 | .NET Core 5.0 |    620.216 ns |   5.7959 ns |   8.6751 ns |    82.09 |    2.05 | 0.1249 |     - |     - |     784 B |
|           LogMixMany |  net50 | .NET Core 5.0 |  8,096.442 ns |  83.8411 ns | 125.4894 ns | 1,071.51 |   22.42 | 1.0376 |     - |     - |    6537 B |
