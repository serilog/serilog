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
|               Method |             Job |       Jit |       Runtime |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |---------------- |---------- |-------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.529 ns | 0.0179 ns | 0.0268 ns |  2.527 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.530 ns | 0.0118 ns | 0.0172 ns |  2.527 ns |  1.00 |    0.01 |      - |     - |     - |         - |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  4.717 ns | 0.0184 ns | 0.0275 ns |  4.715 ns |  1.87 |    0.02 |      - |     - |     - |         - |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  4.272 ns | 0.0465 ns | 0.0652 ns |  4.266 ns |  1.69 |    0.03 |      - |     - |     - |         - |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.980 ns | 0.2521 ns | 0.3774 ns |  6.002 ns |  2.37 |    0.15 |      - |     - |     - |         - |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 11.828 ns | 0.7203 ns | 1.0559 ns | 12.747 ns |  4.68 |    0.41 |      - |     - |     - |         - |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 14.352 ns | 0.3167 ns | 0.4740 ns | 14.307 ns |  5.68 |    0.19 |      - |     - |     - |         - |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 18.120 ns | 0.1135 ns | 0.1664 ns | 18.152 ns |  7.17 |    0.09 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.333 ns | 0.2060 ns | 0.3084 ns |  5.340 ns |  2.11 |    0.12 |      - |     - |     - |         - |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.558 ns | 0.0480 ns | 0.0704 ns |  5.533 ns |  2.20 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  8.050 ns | 0.0357 ns | 0.0501 ns |  8.058 ns |  3.18 |    0.04 |      - |     - |     - |         - |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 27.110 ns | 0.1555 ns | 0.2279 ns | 27.143 ns | 10.73 |    0.13 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 19.837 ns | 0.0709 ns | 0.1039 ns | 19.831 ns |  7.85 |    0.09 |      - |     - |     - |         - |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.174 ns | 0.0895 ns | 0.1312 ns |  9.147 ns |  3.63 |    0.06 | 0.0051 |     - |     - |      32 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.315 ns | 0.0715 ns | 0.1025 ns |  9.298 ns |  3.68 |    0.06 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.215 ns | 0.0378 ns | 0.0529 ns |  9.202 ns |  3.64 |    0.04 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 11.646 ns | 0.7173 ns | 1.0287 ns | 11.648 ns |  4.61 |    0.40 |      - |     - |     - |         - |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 14.197 ns | 0.1922 ns | 0.2877 ns | 14.192 ns |  5.61 |    0.12 |      - |     - |     - |         - |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 25.331 ns | 0.4024 ns | 0.5898 ns | 25.642 ns | 10.02 |    0.26 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 31.601 ns | 0.2987 ns | 0.4471 ns | 31.583 ns | 12.50 |    0.21 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 54.993 ns | 0.1321 ns | 0.1852 ns | 55.021 ns | 21.74 |    0.22 | 0.0446 |     - |     - |     280 B |
|                      |                 |           |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.648 ns | 0.0208 ns | 0.0311 ns |  2.645 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.643 ns | 0.0222 ns | 0.0325 ns |  2.638 ns |  1.00 |    0.02 |      - |     - |     - |         - |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3.616 ns | 0.0213 ns | 0.0319 ns |  3.612 ns |  1.37 |    0.02 |      - |     - |     - |         - |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |  4.048 ns | 0.0200 ns | 0.0287 ns |  4.045 ns |  1.53 |    0.02 |      - |     - |     - |         - |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.559 ns | 0.0451 ns | 0.0675 ns |  7.559 ns |  2.86 |    0.04 |      - |     - |     - |         - |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 13.185 ns | 0.4453 ns | 0.6528 ns | 13.635 ns |  4.98 |    0.28 |      - |     - |     - |         - |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 17.269 ns | 0.3882 ns | 0.5811 ns | 17.276 ns |  6.52 |    0.20 |      - |     - |     - |         - |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 16.992 ns | 0.1144 ns | 0.1676 ns | 17.025 ns |  6.42 |    0.09 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.104 ns | 0.1604 ns | 0.2351 ns |  7.227 ns |  2.68 |    0.08 |      - |     - |     - |         - |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.538 ns | 0.0447 ns | 0.0670 ns |  7.532 ns |  2.85 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.886 ns | 0.1048 ns | 0.1504 ns | 10.894 ns |  4.11 |    0.06 |      - |     - |     - |         - |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 24.130 ns | 0.0791 ns | 0.1109 ns | 24.152 ns |  9.12 |    0.12 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 | 22.405 ns | 0.5488 ns | 0.7871 ns | 22.413 ns |  8.46 |    0.27 |      - |     - |     - |         - |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.333 ns | 0.0990 ns | 0.1451 ns |  9.292 ns |  3.53 |    0.07 | 0.0051 |     - |     - |      32 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.228 ns | 0.0428 ns | 0.0641 ns |  9.220 ns |  3.49 |    0.04 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.516 ns | 0.1840 ns | 0.2753 ns |  9.412 ns |  3.59 |    0.11 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 13.392 ns | 0.0698 ns | 0.1044 ns | 13.367 ns |  5.06 |    0.08 |      - |     - |     - |         - |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 17.137 ns | 0.5720 ns | 0.8385 ns | 16.516 ns |  6.47 |    0.30 |      - |     - |     - |         - |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 24.055 ns | 0.1096 ns | 0.1641 ns | 24.077 ns |  9.09 |    0.13 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 28.870 ns | 0.2754 ns | 0.4037 ns | 28.813 ns | 10.91 |    0.22 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 53.062 ns | 0.2027 ns | 0.2971 ns | 53.010 ns | 20.05 |    0.24 | 0.0446 |     - |     - |     281 B |
|                      |                 |           |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.641 ns | 0.0202 ns | 0.0302 ns |  2.641 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.631 ns | 0.0216 ns | 0.0317 ns |  2.630 ns |  1.00 |    0.02 |      - |     - |     - |         - |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3.618 ns | 0.0206 ns | 0.0303 ns |  3.620 ns |  1.37 |    0.02 |      - |     - |     - |         - |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |  4.067 ns | 0.0179 ns | 0.0268 ns |  4.064 ns |  1.54 |    0.02 |      - |     - |     - |         - |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.832 ns | 0.1368 ns | 0.2048 ns |  7.854 ns |  2.97 |    0.08 |      - |     - |     - |         - |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 14.062 ns | 0.5865 ns | 0.8412 ns | 14.682 ns |  5.32 |    0.34 |      - |     - |     - |         - |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 16.217 ns | 0.1055 ns | 0.1479 ns | 16.186 ns |  6.14 |    0.08 |      - |     - |     - |         - |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 16.928 ns | 0.0682 ns | 0.1000 ns | 16.922 ns |  6.41 |    0.09 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  6.885 ns | 0.0449 ns | 0.0629 ns |  6.900 ns |  2.61 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.614 ns | 0.0742 ns | 0.1065 ns |  7.587 ns |  2.88 |    0.06 |      - |     - |     - |         - |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.729 ns | 0.0337 ns | 0.0484 ns | 10.723 ns |  4.06 |    0.05 |      - |     - |     - |         - |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 24.603 ns | 0.2184 ns | 0.3132 ns | 24.674 ns |  9.31 |    0.14 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 | 21.726 ns | 0.0901 ns | 0.1348 ns | 21.721 ns |  8.23 |    0.11 |      - |     - |     - |         - |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.313 ns | 0.0382 ns | 0.0571 ns |  9.321 ns |  3.53 |    0.04 | 0.0051 |     - |     - |      32 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.212 ns | 0.0834 ns | 0.1222 ns |  9.235 ns |  3.49 |    0.06 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.265 ns | 0.0682 ns | 0.0978 ns |  9.267 ns |  3.51 |    0.05 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 12.793 ns | 0.0612 ns | 0.0878 ns | 12.809 ns |  4.84 |    0.07 |      - |     - |     - |         - |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 17.616 ns | 0.5221 ns | 0.7653 ns | 18.207 ns |  6.67 |    0.28 |      - |     - |     - |         - |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 24.060 ns | 0.0800 ns | 0.1147 ns | 24.060 ns |  9.11 |    0.12 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 28.496 ns | 0.1138 ns | 0.1596 ns | 28.499 ns | 10.78 |    0.14 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 52.982 ns | 0.1400 ns | 0.1963 ns | 52.954 ns | 20.05 |    0.25 | 0.0446 |     - |     - |     281 B |
