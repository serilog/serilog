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
|             LogEmpty | core31 | .NET Core 3.1 |      7.830 ns |   0.3284 ns |   0.4814 ns |      7.658 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |     59.162 ns |   1.1944 ns |   1.7877 ns |     58.731 ns |     7.57 |    0.47 | 0.0088 |     - |     - |      56 B |
|               LogMsg | core31 | .NET Core 3.1 |    506.807 ns |   6.1382 ns |   9.1874 ns |    507.854 ns |    64.94 |    3.71 | 0.0210 |     - |     - |     136 B |
|         LogMsgWithEx | core31 | .NET Core 3.1 |    497.590 ns |   5.1468 ns |   7.7035 ns |    495.637 ns |    63.82 |    4.06 | 0.0210 |     - |     - |     136 B |
|           LogScalar1 | core31 | .NET Core 3.1 |    599.821 ns |   5.8068 ns |   8.5115 ns |    597.870 ns |    76.91 |    5.19 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 | core31 | .NET Core 3.1 |    708.408 ns |  43.7976 ns |  64.1980 ns |    684.539 ns |    90.60 |    7.69 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 | core31 | .NET Core 3.1 |    731.636 ns |   9.0838 ns |  13.5962 ns |    733.269 ns |    93.89 |    6.53 | 0.0734 |     - |     - |     464 B |
|        LogScalarMany | core31 | .NET Core 3.1 |    787.107 ns |   6.8286 ns |  10.0093 ns |    788.609 ns |   100.88 |    6.02 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |    627.926 ns |   7.2467 ns |  10.8465 ns |    627.438 ns |    80.56 |    5.09 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |    724.030 ns |   6.3481 ns |   9.5016 ns |    724.898 ns |    92.80 |    5.74 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |    827.849 ns |   6.3912 ns |   9.5661 ns |    828.974 ns |   106.07 |    6.07 | 0.0849 |     - |     - |     536 B |
|  LogScalarStructMany | core31 | .NET Core 3.1 |    907.139 ns |   7.4943 ns |  10.7482 ns |    909.116 ns |   116.13 |    7.01 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 |    747.993 ns |  12.7156 ns |  18.6384 ns |    747.454 ns |    95.78 |    4.49 | 0.0706 |     - |     - |     448 B |
|        LogDictionary | core31 | .NET Core 3.1 |  2,880.230 ns |  36.0859 ns |  50.5874 ns |  2,880.781 ns |   367.76 |   21.60 | 0.3395 |     - |     - |    2144 B |
|          LogSequence | core31 | .NET Core 3.1 |  1,232.066 ns |  14.5039 ns |  21.7087 ns |  1,238.245 ns |   157.95 |   10.32 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  4,896.027 ns |  50.0552 ns |  73.3702 ns |  4,912.311 ns |   627.70 |   40.86 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 | core31 | .NET Core 3.1 |    678.109 ns |   5.9194 ns |   8.8598 ns |    681.962 ns |    86.89 |    5.16 | 0.0696 |     - |     - |     440 B |
|              LogMix3 | core31 | .NET Core 3.1 |    756.630 ns |   5.4474 ns |   8.1534 ns |    758.832 ns |    96.92 |    5.67 | 0.0811 |     - |     - |     512 B |
|              LogMix4 | core31 | .NET Core 3.1 |    829.012 ns |   6.6003 ns |   9.8791 ns |    831.266 ns |   106.16 |    6.28 | 0.1116 |     - |     - |     704 B |
|              LogMix5 | core31 | .NET Core 3.1 |    910.767 ns |   6.5140 ns |   9.1317 ns |    911.383 ns |   116.30 |    6.77 | 0.1230 |     - |     - |     776 B |
|           LogMixMany | core31 | .NET Core 3.1 |  9,314.529 ns |  68.2648 ns | 100.0616 ns |  9,326.859 ns | 1,194.07 |   75.43 | 1.0223 |     - |     - |    6448 B |
|                      |        |               |               |             |             |               |          |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |      8.367 ns |   0.1209 ns |   0.1772 ns |      8.382 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |     61.058 ns |   1.1071 ns |   1.6570 ns |     60.931 ns |     7.31 |    0.26 | 0.0088 |     - |     - |      56 B |
|               LogMsg |  net48 |      .NET 4.8 |    502.645 ns |   6.2808 ns |   9.4009 ns |    501.122 ns |    60.16 |    1.88 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |  net48 |      .NET 4.8 |    498.661 ns |   6.4114 ns |   9.5963 ns |    497.718 ns |    59.58 |    1.73 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |  net48 |      .NET 4.8 |    600.157 ns |   6.5224 ns |   9.7625 ns |    598.102 ns |    71.77 |    1.78 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |  net48 |      .NET 4.8 |    668.694 ns |   5.6220 ns |   8.4148 ns |    673.271 ns |    79.91 |    1.81 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |  net48 |      .NET 4.8 |    736.678 ns |   5.9963 ns |   8.9750 ns |    736.006 ns |    88.10 |    2.29 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |  net48 |      .NET 4.8 |    780.192 ns |   5.7321 ns |   8.5795 ns |    781.388 ns |    93.26 |    2.14 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |    659.113 ns |   6.3060 ns |   9.4385 ns |    659.906 ns |    78.77 |    1.93 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |    749.336 ns |   6.0170 ns |   9.0060 ns |    752.045 ns |    89.59 |    2.15 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |  net48 |      .NET 4.8 |    856.934 ns |   5.9942 ns |   8.5967 ns |    855.418 ns |   102.40 |    2.43 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |  net48 |      .NET 4.8 |    926.288 ns |   9.1477 ns |  13.4085 ns |    927.072 ns |   110.76 |    3.24 | 0.1144 |     - |     - |     730 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 |    756.849 ns |   6.7775 ns |  10.1443 ns |    754.051 ns |    90.52 |    1.91 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |  net48 |      .NET 4.8 |  3,231.126 ns |  27.7955 ns |  41.6031 ns |  3,232.407 ns |   386.42 |    8.58 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |  net48 |      .NET 4.8 |  1,276.257 ns |  11.0200 ns |  16.4942 ns |  1,276.691 ns |   152.64 |    3.90 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  5,670.979 ns |  51.3111 ns |  75.2111 ns |  5,678.873 ns |   678.02 |   15.98 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |  net48 |      .NET 4.8 |    711.701 ns |   6.7471 ns |   9.8898 ns |    714.332 ns |    85.10 |    2.26 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |  net48 |      .NET 4.8 |    795.676 ns |   6.2759 ns |   9.3934 ns |    797.255 ns |    95.12 |    2.16 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |  net48 |      .NET 4.8 |    867.873 ns |   5.7292 ns |   8.2167 ns |    869.045 ns |   103.71 |    2.24 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |  net48 |      .NET 4.8 |    964.710 ns |  13.7990 ns |  20.6537 ns |    959.175 ns |   115.31 |    3.47 | 0.1240 |     - |     - |     786 B |
|           LogMixMany |  net48 |      .NET 4.8 | 11,105.452 ns | 163.5377 ns | 244.7756 ns | 11,101.553 ns | 1,327.00 |   33.74 | 1.0376 |     - |     - |    6596 B |
|                      |        |               |               |             |             |               |          |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |      7.835 ns |   0.1008 ns |   0.1509 ns |      7.830 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |     48.185 ns |   0.8504 ns |   1.2728 ns |     48.196 ns |     6.15 |    0.17 | 0.0089 |     - |     - |      56 B |
|               LogMsg |  net50 | .NET Core 5.0 |    270.988 ns |   3.3091 ns |   4.9529 ns |    270.513 ns |    34.60 |    1.02 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |    266.905 ns |   2.9688 ns |   4.4436 ns |    266.196 ns |    34.08 |    0.85 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |  net50 | .NET Core 5.0 |    351.648 ns |   3.5703 ns |   5.3439 ns |    351.417 ns |    44.90 |    1.18 | 0.0596 |     - |     - |     376 B |
|           LogScalar2 |  net50 | .NET Core 5.0 |    401.563 ns |   3.1286 ns |   4.6827 ns |    401.755 ns |    51.27 |    1.19 | 0.0672 |     - |     - |     424 B |
|           LogScalar3 |  net50 | .NET Core 5.0 |    463.186 ns |   5.9224 ns |   8.6809 ns |    459.771 ns |    59.19 |    1.44 | 0.0744 |     - |     - |     472 B |
|        LogScalarMany |  net50 | .NET Core 5.0 |    510.151 ns |   5.9726 ns |   8.9396 ns |    509.621 ns |    65.13 |    1.50 | 0.1001 |     - |     - |     632 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |    388.582 ns |   3.6338 ns |   5.4389 ns |    388.823 ns |    49.61 |    1.28 | 0.0634 |     - |     - |     400 B |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |    468.245 ns |   4.8089 ns |   7.1978 ns |    466.467 ns |    59.78 |    1.50 | 0.0749 |     - |     - |     472 B |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |    565.093 ns |   8.8170 ns |  13.1969 ns |    564.417 ns |    72.14 |    2.00 | 0.0858 |     - |     - |     544 B |
|  LogScalarStructMany |  net50 | .NET Core 5.0 |    625.381 ns |   7.7315 ns |  11.5721 ns |    622.838 ns |    79.84 |    1.91 | 0.1154 |     - |     - |     728 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |    500.286 ns |   7.3314 ns |  10.9733 ns |    499.357 ns |    63.88 |    1.97 | 0.0725 |     - |     - |     456 B |
|        LogDictionary |  net50 | .NET Core 5.0 |  2,409.178 ns |  23.9678 ns |  35.1317 ns |  2,409.492 ns |   307.94 |    8.58 | 0.3471 |     - |     - |    2200 B |
|          LogSequence |  net50 | .NET Core 5.0 |    899.822 ns |   5.9848 ns |   8.7724 ns |    899.611 ns |   115.00 |    2.53 | 0.1307 |     - |     - |     824 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  4,344.587 ns |  58.5949 ns |  87.7021 ns |  4,335.910 ns |   554.71 |   16.39 | 0.5493 |     - |     - |    3472 B |
|              LogMix2 |  net50 | .NET Core 5.0 |    427.134 ns |   3.0133 ns |   4.5101 ns |    426.890 ns |    54.53 |    1.27 | 0.0710 |     - |     - |     448 B |
|              LogMix3 |  net50 | .NET Core 5.0 |    502.185 ns |   6.9486 ns |  10.4003 ns |    502.238 ns |    64.12 |    1.96 | 0.0820 |     - |     - |     520 B |
|              LogMix4 |  net50 | .NET Core 5.0 |    573.664 ns |   4.5034 ns |   6.4586 ns |    573.273 ns |    73.30 |    1.36 | 0.1135 |     - |     - |     712 B |
|              LogMix5 |  net50 | .NET Core 5.0 |    630.359 ns |   7.0241 ns |  10.5134 ns |    630.644 ns |    80.49 |    2.48 | 0.1249 |     - |     - |     784 B |
|           LogMixMany |  net50 | .NET Core 5.0 |  8,231.664 ns | 100.0470 ns | 149.7456 ns |  8,248.062 ns | 1,051.07 |   32.16 | 1.0376 |     - |     - |    6537 B |
