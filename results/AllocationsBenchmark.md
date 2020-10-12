``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
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
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |      7.489 ns |   0.1214 ns |   0.1780 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |     56.805 ns |   0.7757 ns |   1.1611 ns |     7.59 |    0.27 | 0.0089 |     - |     - |      56 B |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    540.274 ns |   7.6024 ns |  11.3790 ns |    72.13 |    2.74 | 0.0210 |     - |     - |     136 B |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    532.083 ns |   7.5229 ns |  11.2599 ns |    71.09 |    1.47 | 0.0210 |     - |     - |     136 B |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    643.299 ns |   9.1037 ns |  13.3441 ns |    85.92 |    1.85 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    696.091 ns |   8.1004 ns |  12.1243 ns |    93.04 |    2.71 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    777.750 ns |   8.2071 ns |  12.2839 ns |   103.88 |    2.91 | 0.0734 |     - |     - |     464 B |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    823.894 ns |  10.5526 ns |  15.7946 ns |   110.21 |    3.39 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    681.343 ns |  11.0118 ns |  16.4820 ns |    91.16 |    3.30 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    765.764 ns |   8.4263 ns |  12.6120 ns |   102.38 |    3.07 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    897.164 ns |  21.5700 ns |  31.6171 ns |   119.84 |    4.56 | 0.0839 |     - |     - |     536 B |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    962.894 ns |   7.1894 ns |  10.5382 ns |   128.64 |    3.32 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    805.164 ns |   8.8403 ns |  13.2317 ns |   107.56 |    3.15 | 0.0706 |     - |     - |     448 B |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2,876.173 ns |  39.4587 ns |  59.0599 ns |   384.74 |   14.00 | 0.3395 |     - |     - |    2144 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,321.011 ns |  18.1847 ns |  27.2181 ns |   176.40 |    3.15 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  4,830.722 ns |  94.7735 ns | 141.8525 ns |   645.11 |   26.59 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    713.762 ns |   8.6159 ns |  12.8959 ns |    95.33 |    2.63 | 0.0696 |     - |     - |     440 B |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    821.989 ns |   9.1294 ns |  13.6645 ns |   109.85 |    3.34 | 0.0811 |     - |     - |     512 B |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    894.291 ns |  14.0107 ns |  20.9705 ns |   119.50 |    4.44 | 0.1116 |     - |     - |     704 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |    957.220 ns |  15.9059 ns |  23.8072 ns |   127.62 |    3.66 | 0.1221 |     - |     - |     776 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9,421.749 ns | 153.1057 ns | 229.1614 ns | 1,257.88 |   45.74 | 1.0223 |     - |     - |    6448 B |
|                      |                 |           |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |      8.465 ns |   0.1137 ns |   0.1703 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |     62.075 ns |   1.2262 ns |   1.8352 ns |     7.34 |    0.26 | 0.0088 |     - |     - |      56 B |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |    518.041 ns |   7.9947 ns |  11.9661 ns |    61.21 |    1.35 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |    523.082 ns |   8.5677 ns |  12.8237 ns |    61.82 |    1.82 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    627.012 ns |   8.4730 ns |  12.6820 ns |    74.13 |    2.81 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    687.872 ns |   9.9366 ns |  14.2508 ns |    81.24 |    2.02 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    755.606 ns |   7.8344 ns |  11.7262 ns |    89.29 |    1.97 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |    794.604 ns |   7.8206 ns |  11.7055 ns |    93.92 |    2.63 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    654.850 ns |   9.0568 ns |  13.5558 ns |    77.38 |    1.42 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    745.965 ns |   8.3292 ns |  12.4668 ns |    88.16 |    2.39 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    849.715 ns |   8.2063 ns |  12.2827 ns |   100.42 |    2.29 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 |    936.018 ns |  16.7225 ns |  23.9829 ns |   110.56 |    3.64 | 0.1144 |     - |     - |     730 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 |    770.976 ns |   7.5741 ns |  11.3365 ns |    91.12 |    2.24 | 0.0725 |     - |     - |     457 B |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3,192.674 ns |  36.5512 ns |  54.7082 ns |   377.34 |   10.59 | 0.3548 |     - |     - |    2247 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,291.638 ns |  18.8115 ns |  28.1561 ns |   152.65 |    4.41 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 |  5,727.797 ns |  64.7086 ns |  96.8527 ns |   676.91 |   16.55 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    700.473 ns |   7.9381 ns |  11.8814 ns |    82.78 |    2.04 | 0.0706 |     - |     - |     449 B |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    792.222 ns |   9.5578 ns |  14.3056 ns |    93.63 |    2.53 | 0.0820 |     - |     - |     522 B |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    847.282 ns |   8.1124 ns |  12.1423 ns |   100.14 |    2.43 | 0.1135 |     - |     - |     714 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 |    967.658 ns |  28.0243 ns |  41.9454 ns |   114.36 |    5.40 | 0.1249 |     - |     - |     786 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10,765.933 ns | 140.3995 ns | 210.1433 ns | 1,272.26 |   31.25 | 1.0376 |     - |     - |    6596 B |
|                      |                 |           |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |      8.387 ns |   0.1277 ns |   0.1912 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |     61.710 ns |   1.3141 ns |   1.9668 ns |     7.36 |    0.25 | 0.0088 |     - |     - |      56 B |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |    524.488 ns |  10.0702 ns |  15.0727 ns |    62.56 |    2.32 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |    522.430 ns |   8.7325 ns |  13.0703 ns |    62.33 |    2.38 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    624.632 ns |   8.3482 ns |  12.4952 ns |    74.51 |    2.23 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    687.695 ns |   8.8062 ns |  13.1807 ns |    82.02 |    1.90 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    753.854 ns |   9.6225 ns |  14.4026 ns |    89.93 |    2.88 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |    797.621 ns |  12.1802 ns |  17.4685 ns |    95.18 |    2.78 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    657.238 ns |   9.0259 ns |  13.5096 ns |    78.38 |    1.59 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    749.278 ns |   9.1926 ns |  13.7590 ns |    89.38 |    2.62 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    847.046 ns |   8.1490 ns |  12.1971 ns |   101.04 |    2.91 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 |    924.859 ns |  12.5277 ns |  18.7509 ns |   110.33 |    3.54 | 0.1144 |     - |     - |     730 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 |    775.981 ns |   9.7215 ns |  14.5507 ns |    92.57 |    2.81 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3,186.696 ns |  39.1879 ns |  58.6545 ns |   380.13 |   11.49 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1,292.717 ns |  18.3948 ns |  27.5325 ns |   154.26 |    6.50 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 |  5,749.199 ns |  79.2240 ns | 118.5788 ns |   685.73 |   18.82 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    699.049 ns |   8.6837 ns |  12.9974 ns |    83.39 |    2.47 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    788.599 ns |   8.4048 ns |  12.5799 ns |    94.07 |    2.74 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    850.135 ns |   8.6993 ns |  13.0207 ns |   101.41 |    2.72 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 |    943.698 ns |  15.1696 ns |  21.2657 ns |   112.54 |    2.93 | 0.1249 |     - |     - |     786 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10,808.315 ns | 164.4349 ns | 246.1185 ns | 1,289.39 |   44.92 | 1.0376 |     - |     - |    6596 B |
