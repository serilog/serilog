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
|               Method |             Job |       Jit |       Runtime |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |---------------- |---------- |-------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1.599 ns | 0.1731 ns | 0.2590 ns |  1.598 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1.612 ns | 0.1857 ns | 0.2663 ns |  1.840 ns |  1.05 |    0.33 |      - |     - |     - |         - |
|               LogMsg |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  1.828 ns | 0.0267 ns | 0.0392 ns |  1.817 ns |  1.17 |    0.18 |      - |     - |     - |         - |
|         LogMsgWithEx |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.005 ns | 0.0130 ns | 0.0183 ns |  2.002 ns |  1.26 |    0.21 |      - |     - |     - |         - |
|           LogScalar1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  6.697 ns | 0.1736 ns | 0.2318 ns |  6.815 ns |  4.17 |    0.81 |      - |     - |     - |         - |
|           LogScalar2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10.749 ns | 0.0694 ns | 0.0902 ns | 10.724 ns |  6.63 |    1.10 |      - |     - |     - |         - |
|           LogScalar3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 12.021 ns | 0.3596 ns | 0.5157 ns | 12.396 ns |  7.67 |    1.55 |      - |     - |     - |         - |
|        LogScalarMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 16.411 ns | 0.1744 ns | 0.2557 ns | 16.515 ns | 10.49 |    1.86 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.421 ns | 0.1141 ns | 0.1672 ns |  5.530 ns |  3.47 |    0.66 |      - |     - |     - |         - |
|     LogScalarStruct2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  5.705 ns | 0.2174 ns | 0.3254 ns |  5.678 ns |  3.63 |    0.39 |      - |     - |     - |         - |
|     LogScalarStruct3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  6.223 ns | 0.1031 ns | 0.1544 ns |  6.167 ns |  3.98 |    0.58 |      - |     - |     - |         - |
|  LogScalarStructMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 24.767 ns | 0.1803 ns | 0.2642 ns | 24.793 ns | 15.79 |    2.52 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 19.947 ns | 0.1679 ns | 0.2513 ns | 19.928 ns | 12.78 |    1.94 |      - |     - |     - |         - |
|        LogDictionary |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  7.245 ns | 0.0412 ns | 0.0604 ns |  7.240 ns |  4.63 |    0.78 | 0.0051 |     - |     - |      32 B |
|          LogSequence |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  7.297 ns | 0.0392 ns | 0.0550 ns |  7.287 ns |  4.60 |    0.75 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  7.297 ns | 0.0407 ns | 0.0570 ns |  7.305 ns |  4.60 |    0.76 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10.616 ns | 0.0454 ns | 0.0666 ns | 10.611 ns |  6.77 |    1.11 |      - |     - |     - |         - |
|              LogMix3 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 11.964 ns | 0.4026 ns | 0.5902 ns | 12.171 ns |  7.58 |    0.91 |      - |     - |     - |         - |
|              LogMix4 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 24.012 ns | 0.0742 ns | 0.1088 ns | 23.990 ns | 15.31 |    2.46 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 29.353 ns | 0.2419 ns | 0.3621 ns | 29.328 ns | 18.81 |    2.90 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 54.756 ns | 0.3015 ns | 0.4513 ns | 54.623 ns | 35.16 |    5.92 | 0.0446 |     - |     - |     280 B |
|                      |                 |           |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1.645 ns | 0.0108 ns | 0.0152 ns |  1.644 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1.643 ns | 0.0076 ns | 0.0112 ns |  1.646 ns |  1.00 |    0.01 |      - |     - |     - |         - |
|               LogMsg | net48 LegacyJit | LegacyJit |      .NET 4.8 |  1.640 ns | 0.0137 ns | 0.0196 ns |  1.640 ns |  1.00 |    0.02 |      - |     - |     - |         - |
|         LogMsgWithEx | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.025 ns | 0.0108 ns | 0.0159 ns |  2.026 ns |  1.23 |    0.02 |      - |     - |     - |         - |
|           LogScalar1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  8.278 ns | 0.2413 ns | 0.3537 ns |  8.387 ns |  5.01 |    0.21 |      - |     - |     - |         - |
|           LogScalar2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 12.653 ns | 0.0755 ns | 0.1058 ns | 12.628 ns |  7.69 |    0.10 |      - |     - |     - |         - |
|           LogScalar3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 13.822 ns | 0.0816 ns | 0.1171 ns | 13.841 ns |  8.40 |    0.11 |      - |     - |     - |         - |
|        LogScalarMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 14.638 ns | 0.0526 ns | 0.0772 ns | 14.638 ns |  8.90 |    0.09 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.144 ns | 0.0288 ns | 0.0423 ns |  7.151 ns |  4.34 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct2 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.331 ns | 0.0347 ns | 0.0508 ns |  7.326 ns |  4.45 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct3 | net48 LegacyJit | LegacyJit |      .NET 4.8 |  8.024 ns | 0.0352 ns | 0.0482 ns |  8.024 ns |  4.88 |    0.06 |      - |     - |     - |         - |
|  LogScalarStructMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 23.083 ns | 0.5561 ns | 0.8323 ns | 23.062 ns | 14.09 |    0.52 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | net48 LegacyJit | LegacyJit |      .NET 4.8 | 22.928 ns | 0.0830 ns | 0.1190 ns | 22.905 ns | 13.94 |    0.14 |      - |     - |     - |         - |
|        LogDictionary | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.052 ns | 0.0410 ns | 0.0614 ns |  7.056 ns |  4.29 |    0.06 | 0.0051 |     - |     - |      32 B |
|          LogSequence | net48 LegacyJit | LegacyJit |      .NET 4.8 |  7.040 ns | 0.0304 ns | 0.0446 ns |  7.038 ns |  4.28 |    0.04 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | net48 LegacyJit | LegacyJit |      .NET 4.8 |  6.903 ns | 0.0305 ns | 0.0457 ns |  6.901 ns |  4.19 |    0.04 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 13.590 ns | 0.0914 ns | 0.1368 ns | 13.551 ns |  8.26 |    0.11 |      - |     - |     - |         - |
|              LogMix3 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 14.006 ns | 0.3922 ns | 0.5498 ns | 13.589 ns |  8.51 |    0.35 |      - |     - |     - |         - |
|              LogMix4 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 21.566 ns | 0.1313 ns | 0.1924 ns | 21.513 ns | 13.12 |    0.18 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | net48 LegacyJit | LegacyJit |      .NET 4.8 | 26.931 ns | 0.0737 ns | 0.1057 ns | 26.924 ns | 16.37 |    0.16 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | net48 LegacyJit | LegacyJit |      .NET 4.8 | 52.516 ns | 0.9513 ns | 1.4239 ns | 52.006 ns | 31.98 |    0.96 | 0.0446 |     - |     - |     281 B |
|                      |                 |           |               |           |           |           |           |       |         |        |       |       |           |
|             LogEmpty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1.652 ns | 0.0119 ns | 0.0177 ns |  1.649 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1.645 ns | 0.0077 ns | 0.0108 ns |  1.642 ns |  1.00 |    0.01 |      - |     - |     - |         - |
|               LogMsg |    net48 RyuJit |    RyuJit |      .NET 4.8 |  1.635 ns | 0.0121 ns | 0.0178 ns |  1.631 ns |  0.99 |    0.01 |      - |     - |     - |         - |
|         LogMsgWithEx |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.036 ns | 0.0097 ns | 0.0145 ns |  2.034 ns |  1.23 |    0.02 |      - |     - |     - |         - |
|           LogScalar1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  8.841 ns | 0.0577 ns | 0.0846 ns |  8.827 ns |  5.35 |    0.08 |      - |     - |     - |         - |
|           LogScalar2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 12.835 ns | 0.1846 ns | 0.2648 ns | 12.817 ns |  7.78 |    0.19 |      - |     - |     - |         - |
|           LogScalar3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 14.013 ns | 0.2088 ns | 0.3060 ns | 14.074 ns |  8.48 |    0.20 |      - |     - |     - |         - |
|        LogScalarMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 14.679 ns | 0.0736 ns | 0.1055 ns | 14.686 ns |  8.89 |    0.11 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.182 ns | 0.0437 ns | 0.0626 ns |  7.190 ns |  4.35 |    0.06 |      - |     - |     - |         - |
|     LogScalarStruct2 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.310 ns | 0.0384 ns | 0.0563 ns |  7.307 ns |  4.42 |    0.06 |      - |     - |     - |         - |
|     LogScalarStruct3 |    net48 RyuJit |    RyuJit |      .NET 4.8 |  8.016 ns | 0.0323 ns | 0.0473 ns |  8.019 ns |  4.85 |    0.06 |      - |     - |     - |         - |
|  LogScalarStructMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 22.271 ns | 0.0661 ns | 0.0948 ns | 22.285 ns | 13.49 |    0.14 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct |    net48 RyuJit |    RyuJit |      .NET 4.8 | 22.101 ns | 0.0914 ns | 0.1340 ns | 22.119 ns | 13.38 |    0.16 |      - |     - |     - |         - |
|        LogDictionary |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.036 ns | 0.0357 ns | 0.0535 ns |  7.029 ns |  4.26 |    0.05 | 0.0051 |     - |     - |      32 B |
|          LogSequence |    net48 RyuJit |    RyuJit |      .NET 4.8 |  7.031 ns | 0.0283 ns | 0.0423 ns |  7.029 ns |  4.26 |    0.05 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |    net48 RyuJit |    RyuJit |      .NET 4.8 |  6.875 ns | 0.0414 ns | 0.0607 ns |  6.860 ns |  4.16 |    0.06 | 0.0051 |     - |     - |      32 B |
|              LogMix2 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 13.430 ns | 0.3818 ns | 0.5715 ns | 13.466 ns |  8.13 |    0.37 |      - |     - |     - |         - |
|              LogMix3 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 14.733 ns | 0.5361 ns | 0.7516 ns | 15.159 ns |  8.92 |    0.47 |      - |     - |     - |         - |
|              LogMix4 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 20.955 ns | 0.1364 ns | 0.1957 ns | 20.926 ns | 12.69 |    0.20 | 0.0217 |     - |     - |     136 B |
|              LogMix5 |    net48 RyuJit |    RyuJit |      .NET 4.8 | 26.733 ns | 0.1896 ns | 0.2838 ns | 26.726 ns | 16.19 |    0.27 | 0.0268 |     - |     - |     168 B |
|           LogMixMany |    net48 RyuJit |    RyuJit |      .NET 4.8 | 51.775 ns | 0.1329 ns | 0.1862 ns | 51.796 ns | 31.35 |    0.34 | 0.0446 |     - |     - |     281 B |
