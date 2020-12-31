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
|             LogEmpty | core31 | .NET Core 3.1 |      6.153 ns |   0.2266 ns |   0.3392 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |     55.973 ns |   0.5512 ns |   0.8250 ns |     9.13 |    0.55 | 0.0089 |     - |     - |      56 B |
|               LogMsg | core31 | .NET Core 3.1 |    495.075 ns |   7.9748 ns |  11.9363 ns |    80.69 |    4.68 | 0.0210 |     - |     - |     136 B |
|         LogMsgWithEx | core31 | .NET Core 3.1 |    482.431 ns |   7.4649 ns |  11.1731 ns |    78.66 |    4.96 | 0.0210 |     - |     - |     136 B |
|           LogScalar1 | core31 | .NET Core 3.1 |    608.756 ns |  14.6958 ns |  20.6015 ns |    99.96 |    7.47 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 | core31 | .NET Core 3.1 |    649.124 ns |   9.5161 ns |  14.2433 ns |   105.82 |    6.32 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 | core31 | .NET Core 3.1 |    708.121 ns |   8.4168 ns |  12.5979 ns |   115.41 |    6.31 | 0.0734 |     - |     - |     464 B |
|        LogScalarMany | core31 | .NET Core 3.1 |    766.014 ns |   8.9904 ns |  13.4564 ns |   124.87 |    7.27 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |    620.186 ns |   5.6246 ns |   8.4187 ns |   101.13 |    6.24 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |    707.364 ns |   9.4616 ns |  14.1617 ns |   115.28 |    6.22 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |    817.422 ns |   9.6980 ns |  14.2152 ns |   133.39 |    7.83 | 0.0849 |     - |     - |     536 B |
|  LogScalarStructMany | core31 | .NET Core 3.1 |    898.117 ns |   6.8743 ns |  10.2891 ns |   146.42 |    8.50 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 |    729.360 ns |  12.1746 ns |  17.4605 ns |   119.54 |    6.66 | 0.0706 |     - |     - |     448 B |
|        LogDictionary | core31 | .NET Core 3.1 |  2,782.074 ns |  37.8996 ns |  56.7264 ns |   453.66 |   29.14 | 0.3395 |     - |     - |    2144 B |
|          LogSequence | core31 | .NET Core 3.1 |  1,199.964 ns |  20.4407 ns |  30.5947 ns |   195.76 |   14.22 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  4,877.708 ns |  80.3949 ns | 120.3313 ns |   795.71 |   57.36 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 | core31 | .NET Core 3.1 |    652.413 ns |   9.9702 ns |  14.9229 ns |   106.38 |    6.84 | 0.0696 |     - |     - |     440 B |
|              LogMix3 | core31 | .NET Core 3.1 |    741.507 ns |   8.8241 ns |  13.2075 ns |   120.88 |    7.22 | 0.0811 |     - |     - |     512 B |
|              LogMix4 | core31 | .NET Core 3.1 |    818.575 ns |   7.7362 ns |  11.3396 ns |   133.54 |    6.95 | 0.1116 |     - |     - |     704 B |
|              LogMix5 | core31 | .NET Core 3.1 |    903.665 ns |   7.5426 ns |  11.0559 ns |   147.47 |    8.51 | 0.1230 |     - |     - |     776 B |
|           LogMixMany | core31 | .NET Core 3.1 |  9,012.462 ns | 157.7865 ns | 231.2815 ns | 1,469.83 |   75.08 | 1.0223 |     - |     - |    6449 B |
|                      |        |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |      5.833 ns |   0.0559 ns |   0.0837 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |     59.918 ns |   1.3937 ns |   2.0860 ns |    10.27 |    0.37 | 0.0088 |     - |     - |      56 B |
|               LogMsg |  net48 |      .NET 4.8 |    489.714 ns |   7.0709 ns |  10.5833 ns |    83.97 |    2.03 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |  net48 |      .NET 4.8 |    491.410 ns |   8.9870 ns |  13.4514 ns |    84.26 |    2.48 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |  net48 |      .NET 4.8 |    586.328 ns |   7.9128 ns |  11.8435 ns |   100.54 |    2.48 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |  net48 |      .NET 4.8 |    665.683 ns |   8.3226 ns |  12.4568 ns |   114.15 |    2.69 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |  net48 |      .NET 4.8 |    719.620 ns |  10.1458 ns |  15.1857 ns |   123.40 |    3.05 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |  net48 |      .NET 4.8 |    778.073 ns |  10.4520 ns |  15.6440 ns |   133.42 |    3.39 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |    641.556 ns |   9.9323 ns |  14.8662 ns |   110.01 |    2.89 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |    730.203 ns |   9.3202 ns |  13.9501 ns |   125.22 |    3.16 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |  net48 |      .NET 4.8 |    829.707 ns |   8.9036 ns |  13.3265 ns |   142.27 |    2.75 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |  net48 |      .NET 4.8 |    909.836 ns |   7.2444 ns |  10.8431 ns |   156.00 |    2.15 | 0.1154 |     - |     - |     730 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 |    747.624 ns |   7.6763 ns |  11.4896 ns |   128.20 |    2.56 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |  net48 |      .NET 4.8 |  3,177.921 ns |  29.1637 ns |  43.6509 ns |   544.92 |    9.57 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |  net48 |      .NET 4.8 |  1,276.309 ns |  18.7258 ns |  28.0279 ns |   218.86 |    5.89 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  5,633.025 ns |  78.7010 ns | 117.7960 ns |   965.91 |   23.56 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |  net48 |      .NET 4.8 |    706.250 ns |   8.6891 ns |  13.0055 ns |   121.10 |    2.57 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |  net48 |      .NET 4.8 |    776.250 ns |   6.9540 ns |  10.4084 ns |   133.11 |    2.89 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |  net48 |      .NET 4.8 |    849.770 ns |   8.2442 ns |  12.3395 ns |   145.72 |    3.14 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |  net48 |      .NET 4.8 |    942.026 ns |   7.8757 ns |  11.7879 ns |   161.55 |    3.91 | 0.1249 |     - |     - |     786 B |
|           LogMixMany |  net48 |      .NET 4.8 | 10,806.309 ns | 204.8956 ns | 300.3333 ns | 1,852.15 |   54.63 | 1.0376 |     - |     - |    6596 B |
|                      |        |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |      5.793 ns |   0.0870 ns |   0.1302 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |     44.355 ns |   0.9186 ns |   1.3749 ns |     7.66 |    0.27 | 0.0089 |     - |     - |      56 B |
|               LogMsg |  net50 | .NET Core 5.0 |    261.563 ns |   4.2484 ns |   6.3588 ns |    45.18 |    1.68 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |    258.907 ns |   4.8493 ns |   7.2582 ns |    44.71 |    1.50 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |  net50 | .NET Core 5.0 |    341.370 ns |   5.3174 ns |   7.9589 ns |    58.96 |    2.14 | 0.0596 |     - |     - |     376 B |
|           LogScalar2 |  net50 | .NET Core 5.0 |    391.083 ns |   5.0957 ns |   7.6270 ns |    67.54 |    2.00 | 0.0672 |     - |     - |     424 B |
|           LogScalar3 |  net50 | .NET Core 5.0 |    450.006 ns |   3.7990 ns |   5.5685 ns |    77.67 |    2.08 | 0.0749 |     - |     - |     472 B |
|        LogScalarMany |  net50 | .NET Core 5.0 |    500.964 ns |   7.6447 ns |  11.4423 ns |    86.53 |    3.17 | 0.1001 |     - |     - |     632 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |    378.750 ns |   4.8258 ns |   6.9210 ns |    65.33 |    1.76 | 0.0634 |     - |     - |     400 B |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |    448.075 ns |   3.9562 ns |   5.9214 ns |    77.38 |    2.08 | 0.0749 |     - |     - |     472 B |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |    542.593 ns |   8.1300 ns |  11.9168 ns |    93.63 |    2.32 | 0.0858 |     - |     - |     544 B |
|  LogScalarStructMany |  net50 | .NET Core 5.0 |    609.794 ns |   9.2088 ns |  13.7832 ns |   105.32 |    3.79 | 0.1154 |     - |     - |     728 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |    476.113 ns |   8.6677 ns |  12.9735 ns |    82.24 |    3.48 | 0.0725 |     - |     - |     456 B |
|        LogDictionary |  net50 | .NET Core 5.0 |  2,397.778 ns |  48.1259 ns |  72.0326 ns |   414.20 |   18.52 | 0.3471 |     - |     - |    2200 B |
|          LogSequence |  net50 | .NET Core 5.0 |    920.172 ns |  28.1680 ns |  42.1606 ns |   158.90 |    7.99 | 0.1307 |     - |     - |     824 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  4,267.191 ns |  74.6073 ns | 111.6687 ns |   736.83 |   22.55 | 0.5493 |     - |     - |    3472 B |
|              LogMix2 |  net50 | .NET Core 5.0 |    412.880 ns |   6.3684 ns |   9.5319 ns |    71.31 |    2.61 | 0.0710 |     - |     - |     448 B |
|              LogMix3 |  net50 | .NET Core 5.0 |    494.963 ns |   8.8821 ns |  13.2943 ns |    85.49 |    3.37 | 0.0820 |     - |     - |     520 B |
|              LogMix4 |  net50 | .NET Core 5.0 |    549.053 ns |   8.9166 ns |  13.3460 ns |    94.82 |    3.34 | 0.1135 |     - |     - |     712 B |
|              LogMix5 |  net50 | .NET Core 5.0 |    620.232 ns |   9.0266 ns |  13.5106 ns |   107.10 |    2.88 | 0.1249 |     - |     - |     784 B |
|           LogMixMany |  net50 | .NET Core 5.0 |  8,338.174 ns | 143.6052 ns | 214.9414 ns | 1,440.00 |   50.79 | 1.0376 |     - |     - |    6537 B |
