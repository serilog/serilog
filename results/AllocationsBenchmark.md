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
|             LogEmpty | core31 | .NET Core 3.1 |      8.547 ns |   0.3231 ns |   0.4836 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | core31 | .NET Core 3.1 |     57.157 ns |   1.0026 ns |   1.5006 ns |     6.71 |    0.41 | 0.0088 |     - |     - |      56 B |
|               LogMsg | core31 | .NET Core 3.1 |    494.763 ns |   7.2431 ns |  10.8411 ns |    58.05 |    3.21 | 0.0210 |     - |     - |     136 B |
|         LogMsgWithEx | core31 | .NET Core 3.1 |    490.598 ns |   7.8843 ns |  11.8009 ns |    57.56 |    3.26 | 0.0210 |     - |     - |     136 B |
|           LogScalar1 | core31 | .NET Core 3.1 |    588.717 ns |   5.7855 ns |   8.2975 ns |    69.35 |    3.82 | 0.0582 |     - |     - |     368 B |
|           LogScalar2 | core31 | .NET Core 3.1 |    656.002 ns |   8.6610 ns |  12.9634 ns |    77.02 |    5.10 | 0.0658 |     - |     - |     416 B |
|           LogScalar3 | core31 | .NET Core 3.1 |    715.282 ns |   9.6848 ns |  14.4958 ns |    83.93 |    4.74 | 0.0734 |     - |     - |     464 B |
|        LogScalarMany | core31 | .NET Core 3.1 |    775.510 ns |   8.0183 ns |  12.0015 ns |    91.03 |    5.64 | 0.0992 |     - |     - |     624 B |
|     LogScalarStruct1 | core31 | .NET Core 3.1 |    622.750 ns |   9.5139 ns |  13.9453 ns |    73.26 |    4.59 | 0.0620 |     - |     - |     392 B |
|     LogScalarStruct2 | core31 | .NET Core 3.1 |    719.692 ns |   8.4269 ns |  12.6130 ns |    84.47 |    5.22 | 0.0734 |     - |     - |     464 B |
|     LogScalarStruct3 | core31 | .NET Core 3.1 |    813.573 ns |   9.0299 ns |  13.5156 ns |    95.49 |    5.87 | 0.0849 |     - |     - |     536 B |
|  LogScalarStructMany | core31 | .NET Core 3.1 |    900.746 ns |   8.7226 ns |  13.0555 ns |   105.70 |    5.97 | 0.1144 |     - |     - |     720 B |
|   LogScalarBigStruct | core31 | .NET Core 3.1 |    740.344 ns |  13.4856 ns |  19.3406 ns |    87.30 |    6.54 | 0.0706 |     - |     - |     448 B |
|        LogDictionary | core31 | .NET Core 3.1 |  2,808.280 ns |  45.1462 ns |  67.5727 ns |   329.36 |   16.45 | 0.3395 |     - |     - |    2144 B |
|          LogSequence | core31 | .NET Core 3.1 |  1,241.110 ns |  30.7128 ns |  45.9695 ns |   145.39 |    4.08 | 0.1297 |     - |     - |     816 B |
|         LogAnonymous | core31 | .NET Core 3.1 |  4,854.503 ns |  88.7509 ns | 132.8382 ns |   569.67 |   34.77 | 0.5417 |     - |     - |    3432 B |
|              LogMix2 | core31 | .NET Core 3.1 |    672.610 ns |   8.8659 ns |  13.2700 ns |    78.95 |    5.03 | 0.0696 |     - |     - |     440 B |
|              LogMix3 | core31 | .NET Core 3.1 |    751.748 ns |   9.2687 ns |  13.2929 ns |    88.55 |    4.90 | 0.0811 |     - |     - |     512 B |
|              LogMix4 | core31 | .NET Core 3.1 |    826.447 ns |   7.5797 ns |  11.3449 ns |    96.99 |    5.65 | 0.1116 |     - |     - |     704 B |
|              LogMix5 | core31 | .NET Core 3.1 |    905.461 ns |   6.6513 ns |   9.7495 ns |   106.48 |    5.57 | 0.1230 |     - |     - |     776 B |
|           LogMixMany | core31 | .NET Core 3.1 |  9,276.638 ns | 215.6048 ns | 322.7071 ns | 1,087.40 |   48.21 | 1.0223 |     - |     - |    6449 B |
|                      |        |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |  net48 |      .NET 4.8 |      8.382 ns |   0.1058 ns |   0.1584 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net48 |      .NET 4.8 |     61.266 ns |   1.3604 ns |   2.0363 ns |     7.31 |    0.30 | 0.0088 |     - |     - |      56 B |
|               LogMsg |  net48 |      .NET 4.8 |    502.768 ns |   7.1426 ns |  10.6907 ns |    60.00 |    1.77 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |  net48 |      .NET 4.8 |    502.385 ns |   8.1499 ns |  12.1985 ns |    59.96 |    1.95 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |  net48 |      .NET 4.8 |    592.539 ns |   8.7917 ns |  13.1590 ns |    70.71 |    1.92 | 0.0591 |     - |     - |     377 B |
|           LogScalar2 |  net48 |      .NET 4.8 |    664.829 ns |   7.3496 ns |  11.0005 ns |    79.33 |    1.78 | 0.0668 |     - |     - |     425 B |
|           LogScalar3 |  net48 |      .NET 4.8 |    730.672 ns |   8.6497 ns |  12.9465 ns |    87.21 |    2.60 | 0.0744 |     - |     - |     473 B |
|        LogScalarMany |  net48 |      .NET 4.8 |    768.608 ns |   8.2262 ns |  12.3126 ns |    91.72 |    2.27 | 0.1001 |     - |     - |     634 B |
|     LogScalarStruct1 |  net48 |      .NET 4.8 |    645.394 ns |   7.8794 ns |  11.7935 ns |    77.03 |    2.48 | 0.0629 |     - |     - |     401 B |
|     LogScalarStruct2 |  net48 |      .NET 4.8 |    740.882 ns |   7.9488 ns |  11.8973 ns |    88.41 |    2.08 | 0.0744 |     - |     - |     473 B |
|     LogScalarStruct3 |  net48 |      .NET 4.8 |    842.991 ns |   7.2986 ns |  10.9242 ns |   100.60 |    2.11 | 0.0858 |     - |     - |     546 B |
|  LogScalarStructMany |  net48 |      .NET 4.8 |    915.762 ns |   6.5287 ns |   9.7719 ns |   109.29 |    2.50 | 0.1154 |     - |     - |     730 B |
|   LogScalarBigStruct |  net48 |      .NET 4.8 |    745.107 ns |   7.7803 ns |  11.6452 ns |    88.92 |    2.19 | 0.0725 |     - |     - |     457 B |
|        LogDictionary |  net48 |      .NET 4.8 |  3,234.077 ns |  32.9229 ns |  49.2775 ns |   385.97 |   10.12 | 0.3548 |     - |     - |    2247 B |
|          LogSequence |  net48 |      .NET 4.8 |  1,287.722 ns |  18.6069 ns |  27.8500 ns |   153.66 |    4.02 | 0.1297 |     - |     - |     826 B |
|         LogAnonymous |  net48 |      .NET 4.8 |  5,873.280 ns |  95.8402 ns | 140.4813 ns |   700.34 |   23.45 | 0.5493 |     - |     - |    3483 B |
|              LogMix2 |  net48 |      .NET 4.8 |    698.117 ns |   9.4002 ns |  14.0698 ns |    83.32 |    2.45 | 0.0706 |     - |     - |     449 B |
|              LogMix3 |  net48 |      .NET 4.8 |    792.107 ns |   8.0627 ns |  12.0678 ns |    94.53 |    2.14 | 0.0820 |     - |     - |     522 B |
|              LogMix4 |  net48 |      .NET 4.8 |    854.200 ns |   7.8790 ns |  11.7929 ns |   101.94 |    2.27 | 0.1135 |     - |     - |     714 B |
|              LogMix5 |  net48 |      .NET 4.8 |    943.572 ns |  11.2877 ns |  16.8948 ns |   112.60 |    2.57 | 0.1249 |     - |     - |     786 B |
|           LogMixMany |  net48 |      .NET 4.8 | 11,036.712 ns | 148.8031 ns | 222.7215 ns | 1,317.09 |   36.20 | 1.0376 |     - |     - |    6596 B |
|                      |        |               |               |             |             |          |         |        |       |       |           |
|             LogEmpty |  net50 | .NET Core 5.0 |      7.657 ns |   0.1525 ns |   0.2283 ns |     1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  net50 | .NET Core 5.0 |     46.191 ns |   0.7099 ns |   1.0625 ns |     6.04 |    0.21 | 0.0089 |     - |     - |      56 B |
|               LogMsg |  net50 | .NET Core 5.0 |    268.114 ns |   4.3902 ns |   6.5711 ns |    35.05 |    1.53 | 0.0229 |     - |     - |     144 B |
|         LogMsgWithEx |  net50 | .NET Core 5.0 |    263.813 ns |   6.8884 ns |  10.3102 ns |    34.50 |    2.02 | 0.0229 |     - |     - |     144 B |
|           LogScalar1 |  net50 | .NET Core 5.0 |    350.431 ns |   4.7466 ns |   7.1045 ns |    45.80 |    1.60 | 0.0596 |     - |     - |     376 B |
|           LogScalar2 |  net50 | .NET Core 5.0 |    399.605 ns |   4.4492 ns |   6.6594 ns |    52.23 |    1.71 | 0.0672 |     - |     - |     424 B |
|           LogScalar3 |  net50 | .NET Core 5.0 |    459.306 ns |   6.1986 ns |   9.2778 ns |    60.04 |    2.13 | 0.0744 |     - |     - |     472 B |
|        LogScalarMany |  net50 | .NET Core 5.0 |    504.714 ns |  10.6294 ns |  15.9096 ns |    65.96 |    2.70 | 0.1001 |     - |     - |     632 B |
|     LogScalarStruct1 |  net50 | .NET Core 5.0 |    389.272 ns |   5.0036 ns |   7.4891 ns |    50.89 |    2.03 | 0.0634 |     - |     - |     400 B |
|     LogScalarStruct2 |  net50 | .NET Core 5.0 |    460.141 ns |   3.3837 ns |   4.9598 ns |    60.16 |    1.85 | 0.0749 |     - |     - |     472 B |
|     LogScalarStruct3 |  net50 | .NET Core 5.0 |    558.242 ns |  10.4454 ns |  15.6342 ns |    72.95 |    2.32 | 0.0858 |     - |     - |     544 B |
|  LogScalarStructMany |  net50 | .NET Core 5.0 |    615.507 ns |  10.6910 ns |  16.0018 ns |    80.41 |    1.91 | 0.1154 |     - |     - |     728 B |
|   LogScalarBigStruct |  net50 | .NET Core 5.0 |    479.224 ns |   5.9443 ns |   8.8971 ns |    62.64 |    2.31 | 0.0725 |     - |     - |     456 B |
|        LogDictionary |  net50 | .NET Core 5.0 |  2,400.678 ns |  31.4647 ns |  47.0949 ns |   313.84 |   12.44 | 0.3471 |     - |     - |    2200 B |
|          LogSequence |  net50 | .NET Core 5.0 |    914.858 ns |   7.7237 ns |  11.5605 ns |   119.58 |    3.66 | 0.1307 |     - |     - |     824 B |
|         LogAnonymous |  net50 | .NET Core 5.0 |  4,419.951 ns |  88.2654 ns | 132.1115 ns |   577.69 |   23.23 | 0.5493 |     - |     - |    3472 B |
|              LogMix2 |  net50 | .NET Core 5.0 |    420.351 ns |   7.0752 ns |  10.5898 ns |    54.95 |    2.34 | 0.0710 |     - |     - |     448 B |
|              LogMix3 |  net50 | .NET Core 5.0 |    501.002 ns |   8.5282 ns |  12.7645 ns |    65.48 |    2.39 | 0.0820 |     - |     - |     520 B |
|              LogMix4 |  net50 | .NET Core 5.0 |    562.381 ns |   8.4313 ns |  12.6196 ns |    73.53 |    3.31 | 0.1135 |     - |     - |     712 B |
|              LogMix5 |  net50 | .NET Core 5.0 |    624.686 ns |   9.6076 ns |  14.3802 ns |    81.65 |    3.04 | 0.1249 |     - |     - |     784 B |
|           LogMixMany |  net50 | .NET Core 5.0 |  8,366.786 ns | 132.8728 ns | 198.8777 ns | 1,093.69 |   43.34 | 1.0376 |     - |     - |    6537 B |
