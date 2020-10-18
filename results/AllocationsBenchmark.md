``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.403
  [Host]          : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|               Method |             Job |       Jit |       Runtime |          Mean |      Error |      StdDev |    Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |---------------- |---------- |-------------- |--------------:|-----------:|------------:|---------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |      8.396 ns |  0.0652 ns |   0.0935 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     55.846 ns |  1.0785 ns |   1.4763 ns |     6.65 |    0.18 | 0.0089 |     - |     - |      56 B |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    518.361 ns |  2.7239 ns |   3.9927 ns |    61.72 |    0.84 | 0.0210 |     - |     - |     136 B |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    509.373 ns |  2.9992 ns |   4.3963 ns |    60.69 |    1.00 | 0.0210 |     - |     - |     136 B |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    616.843 ns | 11.5161 ns |  16.5161 ns |    73.47 |    1.72 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    668.838 ns |  6.5436 ns |   9.5915 ns |    79.58 |    1.69 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    730.000 ns |  2.2492 ns |   3.3665 ns |    86.96 |    1.02 | 0.0734 |     - |     - |     464 B |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    782.184 ns |  3.3933 ns |   4.9739 ns |    93.19 |    1.05 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    647.244 ns |  5.1008 ns |   7.6346 ns |    77.04 |    1.65 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    721.379 ns |  3.9571 ns |   5.8003 ns |    85.93 |    1.39 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    829.779 ns |  4.1212 ns |   6.0407 ns |    98.81 |    1.57 | 0.0849 |     - |     - |     536 B |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    902.215 ns |  7.9922 ns |  10.9399 ns |   107.43 |    2.05 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    762.512 ns |  6.8611 ns |  10.2693 ns |    90.86 |    0.78 | 0.0706 |     - |     - |     448 B |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2,756.590 ns | 11.9082 ns |  17.0784 ns |   328.36 |    4.07 | 0.3395 |     - |     - |    2144 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,260.805 ns | 13.5951 ns |  19.9275 ns |   150.13 |    3.72 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  4,861.434 ns | 61.0839 ns |  89.5361 ns |   579.25 |   10.68 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    696.210 ns |  5.3594 ns |   7.8558 ns |    82.93 |    0.72 | 0.0696 |     - |     - |     440 B |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    781.053 ns | 10.0743 ns |  15.0788 ns |    93.06 |    1.75 | 0.0811 |     - |     - |     512 B |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    839.976 ns |  3.7595 ns |   5.6271 ns |   100.05 |    0.94 | 0.1116 |     - |     - |     704 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    910.859 ns |  4.2988 ns |   6.1653 ns |   108.50 |    1.55 | 0.1230 |     - |     - |     776 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9,053.699 ns | 77.9367 ns | 116.6521 ns | 1,077.18 |   22.84 | 1.0223 |     - |     - |    6449 B |
|                      |                 |           |               |               |            |             |          |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |      8.019 ns |  0.0237 ns |   0.0332 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |     57.956 ns |  0.4401 ns |   0.6587 ns |     7.22 |    0.09 | 0.0088 |     - |     - |      56 B |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |    504.784 ns |  2.0096 ns |   2.8821 ns |    62.96 |    0.38 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |    505.215 ns |  5.5425 ns |   7.9489 ns |    62.97 |    1.03 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    599.282 ns |  2.4554 ns |   3.6751 ns |    74.72 |    0.56 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    657.623 ns |  2.2467 ns |   3.2931 ns |    82.02 |    0.50 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    727.217 ns |  3.3725 ns |   5.0479 ns |    90.65 |    0.73 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |    767.185 ns |  3.2102 ns |   4.8048 ns |    95.63 |    0.68 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    631.254 ns |  4.2624 ns |   5.8345 ns |    78.70 |    0.76 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    721.628 ns |  5.6396 ns |   8.0881 ns |    90.01 |    1.11 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    813.235 ns |  3.7111 ns |   5.4397 ns |   101.43 |    0.91 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |    886.087 ns |  3.0856 ns |   4.3256 ns |   110.49 |    0.71 | 0.1154 |     - |     - |     730 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 |    747.194 ns |  1.9766 ns |   2.9585 ns |    93.18 |    0.53 | 0.0725 |     - |     - |     457 B |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3,063.908 ns | 11.2584 ns |  16.5024 ns |   381.89 |    2.15 | 0.3548 |     - |     - |    2247 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,264.081 ns | 13.2271 ns |  19.3880 ns |   157.49 |    2.56 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 |  5,571.730 ns | 22.0229 ns |  32.9629 ns |   694.80 |    4.96 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    676.822 ns |  4.5070 ns |   6.7458 ns |    84.47 |    0.99 | 0.0706 |     - |     - |     449 B |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    766.020 ns |  6.4423 ns |   9.2393 ns |    95.54 |    1.27 | 0.0820 |     - |     - |     522 B |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    816.919 ns |  2.7970 ns |   4.0999 ns |   101.90 |    0.72 | 0.1135 |     - |     - |     714 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    921.495 ns |  7.2166 ns |  10.5780 ns |   114.82 |    1.37 | 0.1249 |     - |     - |     786 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10,293.381 ns | 56.3588 ns |  80.8281 ns | 1,283.11 |   10.58 | 1.0376 |     - |     - |    6596 B |
|                      |                 |           |               |               |            |             |          |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |      8.004 ns |  0.0285 ns |   0.0400 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |     57.473 ns |  0.4216 ns |   0.6180 ns |     7.18 |    0.08 | 0.0089 |     - |     - |      56 B |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |    502.740 ns |  4.5385 ns |   6.6524 ns |    62.75 |    0.86 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |    497.772 ns |  2.1484 ns |   3.0118 ns |    62.19 |    0.48 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    597.105 ns |  2.5218 ns |   3.7745 ns |    74.60 |    0.60 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    656.717 ns |  2.1834 ns |   3.1313 ns |    82.04 |    0.48 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    718.578 ns |  2.1148 ns |   3.0998 ns |    89.74 |    0.58 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |    765.603 ns |  3.8336 ns |   5.6192 ns |    95.67 |    0.92 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    626.066 ns |  2.6290 ns |   3.8536 ns |    78.22 |    0.77 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    713.454 ns |  1.9026 ns |   2.7888 ns |    89.13 |    0.53 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    809.908 ns |  2.9436 ns |   4.3146 ns |   101.25 |    0.59 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |    893.968 ns |  6.8567 ns |  10.0505 ns |   111.58 |    1.36 | 0.1154 |     - |     - |     730 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 |    752.100 ns |  4.1726 ns |   5.9843 ns |    93.95 |    0.81 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3,040.817 ns | 15.7716 ns |  23.1178 ns |   380.06 |    3.44 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,243.348 ns |  6.8767 ns |  10.2928 ns |   155.35 |    1.54 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 |  5,574.057 ns | 22.6170 ns |  32.4367 ns |   696.21 |    4.90 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    667.384 ns |  2.5098 ns |   3.6788 ns |    83.37 |    0.63 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    757.595 ns |  3.0471 ns |   4.3701 ns |    94.64 |    0.70 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    824.193 ns |  7.0923 ns |  10.6154 ns |   102.84 |    1.44 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    913.872 ns |  2.4692 ns |   3.6193 ns |   114.16 |    0.73 | 0.1249 |     - |     - |     786 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10,302.136 ns | 87.2980 ns | 125.2002 ns | 1,287.70 |   16.69 | 1.0376 |     - |     - |    6596 B |
