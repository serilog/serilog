``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|               Method |             Job |       Jit |       Runtime |          Mean |       Error |      StdDev |    Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |---------------- |---------- |-------------- |--------------:|------------:|------------:|---------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |      5.612 ns |   0.0342 ns |   0.0490 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     52.942 ns |   0.4902 ns |   0.7337 ns |     9.44 |    0.16 | 0.0089 |     - |     - |      56 B |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    507.610 ns |   3.5393 ns |   5.2975 ns |    90.43 |    1.36 | 0.0210 |     - |     - |     136 B |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    512.259 ns |   3.5775 ns |   5.0151 ns |    91.26 |    1.26 | 0.0210 |     - |     - |     136 B |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    608.155 ns |   3.8366 ns |   5.6236 ns |   108.39 |    0.98 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    669.033 ns |   4.7342 ns |   6.7897 ns |   119.23 |    1.94 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    723.429 ns |   2.9018 ns |   4.2534 ns |   128.88 |    1.45 | 0.0734 |     - |     - |     464 B |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    788.794 ns |   2.7638 ns |   3.9637 ns |   140.56 |    1.20 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    641.853 ns |   2.6334 ns |   3.8600 ns |   114.36 |    1.27 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    730.268 ns |   3.1068 ns |   4.6501 ns |   130.06 |    1.38 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    832.508 ns |   4.7669 ns |   6.8366 ns |   148.35 |    1.84 | 0.0849 |     - |     - |     536 B |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    905.022 ns |   3.2292 ns |   4.7333 ns |   161.29 |    1.75 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    756.348 ns |   2.9348 ns |   4.2090 ns |   134.78 |    1.62 | 0.0706 |     - |     - |     448 B |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2,826.624 ns |  19.0124 ns |  27.2670 ns |   503.68 |    4.73 | 0.3395 |     - |     - |    2144 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,282.502 ns |   6.7516 ns |  10.1055 ns |   228.56 |    3.23 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  4,798.246 ns | 181.6035 ns | 266.1921 ns |   855.31 |   50.38 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    693.890 ns |   3.2815 ns |   4.7062 ns |   123.65 |    1.43 | 0.0696 |     - |     - |     440 B |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    769.476 ns |   5.8234 ns |   8.5359 ns |   137.09 |    2.31 | 0.0811 |     - |     - |     512 B |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    848.781 ns |   2.4132 ns |   3.4609 ns |   151.25 |    1.48 | 0.1116 |     - |     - |     704 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    911.901 ns |   3.5024 ns |   5.2423 ns |   162.55 |    1.35 | 0.1230 |     - |     - |     776 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9,015.321 ns |  42.9881 ns |  64.3426 ns | 1,606.99 |   19.20 | 1.0223 |     - |     - |    6449 B |
|                      |                 |           |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |      5.712 ns |   0.0191 ns |   0.0262 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |     57.624 ns |   0.2493 ns |   0.3655 ns |    10.09 |    0.08 | 0.0088 |     - |     - |      56 B |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |    498.688 ns |   3.3666 ns |   4.9347 ns |    87.41 |    0.80 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |    508.698 ns |   6.9966 ns |  10.2555 ns |    89.13 |    1.95 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    602.311 ns |   2.6631 ns |   3.9035 ns |   105.47 |    0.76 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    672.282 ns |   2.8009 ns |   4.0170 ns |   117.74 |    0.99 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    719.387 ns |   4.3446 ns |   6.2309 ns |   125.99 |    1.23 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |    776.896 ns |   3.0692 ns |   4.4987 ns |   136.03 |    0.99 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    638.588 ns |   4.6466 ns |   6.8110 ns |   111.82 |    1.43 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    725.873 ns |   4.5545 ns |   6.8169 ns |   126.94 |    1.29 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    814.870 ns |   2.9289 ns |   4.2006 ns |   142.63 |    0.84 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |    892.826 ns |   2.5023 ns |   3.6678 ns |   156.30 |    0.79 | 0.1154 |     - |     - |     730 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 |    750.066 ns |   2.7416 ns |   4.1036 ns |   131.32 |    0.96 | 0.0725 |     - |     - |     457 B |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3,051.725 ns |   9.7898 ns |  14.3498 ns |   534.46 |    3.11 | 0.3548 |     - |     - |    2247 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,382.295 ns |  86.5286 ns | 129.5119 ns |   241.63 |   24.66 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 |  5,712.026 ns |  46.3472 ns |  63.4405 ns | 1,000.02 |   11.99 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    691.095 ns |   3.9611 ns |   5.2880 ns |   120.98 |    1.01 | 0.0706 |     - |     - |     449 B |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    757.254 ns |   3.3006 ns |   4.7336 ns |   132.66 |    0.93 | 0.0820 |     - |     - |     522 B |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    822.738 ns |   2.5652 ns |   3.7600 ns |   144.00 |    0.96 | 0.1135 |     - |     - |     714 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    930.386 ns |   7.1296 ns |  10.6713 ns |   162.59 |    2.13 | 0.1249 |     - |     - |     786 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10,497.298 ns |  60.3564 ns |  88.4696 ns | 1,839.83 |   14.89 | 1.0376 |     - |     - |    6596 B |
|                      |                 |           |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |      5.745 ns |   0.0225 ns |   0.0322 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |     57.406 ns |   0.4111 ns |   0.5763 ns |     9.99 |    0.11 | 0.0088 |     - |     - |      56 B |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |    500.210 ns |   1.6634 ns |   2.3318 ns |    87.05 |    0.75 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |    499.738 ns |   4.7074 ns |   6.9000 ns |    86.97 |    1.21 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    600.478 ns |   3.5885 ns |   5.3711 ns |   104.44 |    1.19 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    673.494 ns |   2.6568 ns |   3.7244 ns |   117.21 |    0.97 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    721.062 ns |   2.8658 ns |   4.2006 ns |   125.50 |    0.96 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |    776.798 ns |   1.8887 ns |   2.8269 ns |   135.17 |    0.80 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    640.559 ns |   8.2178 ns |  12.3000 ns |   111.50 |    2.42 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    723.003 ns |   3.3219 ns |   4.7642 ns |   125.84 |    1.03 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    818.745 ns |   4.3848 ns |   6.2885 ns |   142.51 |    1.19 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |    887.951 ns |   2.8845 ns |   4.3174 ns |   154.58 |    1.26 | 0.1154 |     - |     - |     730 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 |    750.007 ns |   2.8186 ns |   4.0424 ns |   130.54 |    0.94 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3,085.366 ns |  19.7692 ns |  28.9774 ns |   536.96 |    6.14 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,281.097 ns |  12.9369 ns |  19.3633 ns |   222.84 |    4.11 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 |  5,581.816 ns |  38.9040 ns |  55.7949 ns |   971.52 |    8.15 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    690.685 ns |   3.7785 ns |   5.6555 ns |   120.14 |    1.31 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    757.460 ns |   2.6811 ns |   4.0129 ns |   131.79 |    0.95 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    833.038 ns |  10.2889 ns |  15.3999 ns |   145.11 |    2.54 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    944.241 ns |   5.1164 ns |   7.6579 ns |   164.44 |    1.63 | 0.1249 |     - |     - |     786 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10,484.147 ns |  47.4253 ns |  69.5154 ns | 1,824.63 |   12.71 | 1.0376 |     - |     - |    6596 B |
