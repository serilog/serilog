``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]     : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT


```
|               Method |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |-----------:|----------:|----------:|-----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   3.283 ns | 0.0967 ns | 0.1224 ns |   3.244 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   3.212 ns | 0.0542 ns | 0.0507 ns |   3.204 ns |  0.96 |    0.03 |      - |     - |     - |         - |
|               LogMsg |   5.365 ns | 0.1432 ns | 0.2508 ns |   5.233 ns |  1.67 |    0.13 |      - |     - |     - |         - |
|         LogMsgWithEx |   5.727 ns | 0.0155 ns | 0.0138 ns |   5.724 ns |  1.71 |    0.05 |      - |     - |     - |         - |
|           LogScalar1 |   6.695 ns | 0.1644 ns | 0.3007 ns |   6.564 ns |  2.10 |    0.13 |      - |     - |     - |         - |
|           LogScalar2 |  12.488 ns | 0.0380 ns | 0.0356 ns |  12.495 ns |  3.74 |    0.12 |      - |     - |     - |         - |
|           LogScalar3 |  16.609 ns | 0.1080 ns | 0.1010 ns |  16.645 ns |  4.98 |    0.17 |      - |     - |     - |         - |
|        LogScalarMany |  18.692 ns | 0.3792 ns | 0.3725 ns |  18.604 ns |  5.62 |    0.22 | 0.0178 |     - |     - |      56 B |
|     LogScalarStruct1 |   5.866 ns | 0.0711 ns | 0.0665 ns |   5.890 ns |  1.76 |    0.07 |      - |     - |     - |         - |
|     LogScalarStruct2 |   6.229 ns | 0.0586 ns | 0.0489 ns |   6.246 ns |  1.85 |    0.06 |      - |     - |     - |         - |
|     LogScalarStruct3 |   9.650 ns | 0.0802 ns | 0.0670 ns |   9.653 ns |  2.87 |    0.08 |      - |     - |     - |         - |
|  LogScalarStructMany |  27.930 ns | 0.0754 ns | 0.0705 ns |  27.912 ns |  8.37 |    0.29 | 0.0483 |     - |     - |     152 B |
|   LogScalarBigStruct |  22.986 ns | 0.4845 ns | 0.4046 ns |  22.957 ns |  6.83 |    0.20 |      - |     - |     - |         - |
|        LogDictionary |   9.214 ns | 0.0495 ns | 0.0463 ns |   9.200 ns |  2.76 |    0.10 | 0.0102 |     - |     - |      32 B |
|          LogSequence |   9.282 ns | 0.0863 ns | 0.0807 ns |   9.311 ns |  2.78 |    0.10 | 0.0102 |     - |     - |      32 B |
|         LogAnonymous |   9.372 ns | 0.2162 ns | 0.3366 ns |   9.191 ns |  2.88 |    0.16 | 0.0102 |     - |     - |      32 B |
|              LogMix2 |  13.215 ns | 0.0524 ns | 0.0490 ns |  13.213 ns |  3.96 |    0.14 |      - |     - |     - |         - |
|              LogMix3 |  16.306 ns | 0.1490 ns | 0.1321 ns |  16.361 ns |  4.86 |    0.15 |      - |     - |     - |         - |
|              LogMix4 |  27.510 ns | 0.6557 ns | 1.0014 ns |  27.070 ns |  8.43 |    0.32 | 0.0432 |     - |     - |     136 B |
|              LogMix5 |  31.827 ns | 0.4027 ns | 0.3767 ns |  32.048 ns |  9.53 |    0.32 | 0.0533 |     - |     - |     168 B |
|           LogMixMany |  64.608 ns | 0.7954 ns | 0.7440 ns |  64.822 ns | 19.35 |    0.62 | 0.0889 |     - |     - |     280 B |
|               LogAll | 242.798 ns | 4.9057 ns | 7.6376 ns | 238.688 ns | 74.53 |    2.69 | 0.0608 |     - |     - |     192 B |
