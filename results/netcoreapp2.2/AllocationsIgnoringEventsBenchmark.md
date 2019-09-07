``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]     : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT


```
|               Method |       Mean |      Error |     StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |-----------:|-----------:|-----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   3.363 ns |  0.1026 ns |  0.1797 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   3.353 ns |  0.0986 ns |  0.1174 ns |  0.99 |    0.07 |      - |     - |     - |         - |
|               LogMsg |   5.911 ns |  0.1488 ns |  0.3203 ns |  1.78 |    0.16 |      - |     - |     - |         - |
|         LogMsgWithEx |   5.643 ns |  0.1096 ns |  0.1025 ns |  1.69 |    0.10 |      - |     - |     - |         - |
|           LogScalar1 |   6.605 ns |  0.1645 ns |  0.3541 ns |  1.98 |    0.14 |      - |     - |     - |         - |
|           LogScalar2 |  13.125 ns |  0.2939 ns |  0.4830 ns |  3.90 |    0.26 |      - |     - |     - |         - |
|           LogScalar3 |  17.371 ns |  0.3765 ns |  0.7775 ns |  5.21 |    0.36 |      - |     - |     - |         - |
|        LogScalarMany |  23.412 ns |  0.4883 ns |  0.5813 ns |  6.90 |    0.42 | 0.0178 |     - |     - |      56 B |
|     LogScalarStruct1 |   5.945 ns |  0.1410 ns |  0.1250 ns |  1.77 |    0.09 |      - |     - |     - |         - |
|     LogScalarStruct2 |   6.340 ns |  0.1564 ns |  0.2569 ns |  1.88 |    0.09 |      - |     - |     - |         - |
|     LogScalarStruct3 |  10.049 ns |  0.2645 ns |  0.3531 ns |  2.97 |    0.23 |      - |     - |     - |         - |
|  LogScalarStructMany |  30.164 ns |  0.6347 ns |  1.1921 ns |  8.99 |    0.67 | 0.0483 |     - |     - |     152 B |
|   LogScalarBigStruct |  23.851 ns |  0.5048 ns |  0.6739 ns |  7.05 |    0.46 |      - |     - |     - |         - |
|        LogDictionary |  11.070 ns |  0.2536 ns |  0.6361 ns |  3.36 |    0.31 | 0.0102 |     - |     - |      32 B |
|          LogSequence |  10.909 ns |  0.2470 ns |  0.3917 ns |  3.24 |    0.22 | 0.0102 |     - |     - |      32 B |
|         LogAnonymous |  10.530 ns |  0.2330 ns |  0.2774 ns |  3.10 |    0.20 | 0.0102 |     - |     - |      32 B |
|              LogMix2 |  13.088 ns |  0.2918 ns |  0.5034 ns |  3.90 |    0.26 |      - |     - |     - |         - |
|              LogMix3 |  17.040 ns |  0.3672 ns |  0.5608 ns |  5.05 |    0.31 |      - |     - |     - |         - |
|              LogMix4 |  29.330 ns |  0.6089 ns |  1.2299 ns |  8.72 |    0.35 | 0.0432 |     - |     - |     136 B |
|              LogMix5 |  34.686 ns |  0.7239 ns |  1.6040 ns | 10.38 |    0.65 | 0.0533 |     - |     - |     168 B |
|           LogMixMany |  73.304 ns |  1.4990 ns |  3.5039 ns | 21.69 |    1.65 | 0.0889 |     - |     - |     280 B |
|               LogAll | 281.563 ns | 10.9676 ns | 31.1134 ns | 91.25 |    9.70 | 0.0606 |     - |     - |     192 B |
