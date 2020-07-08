``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.660 ns | 0.0254 ns | 0.0212 ns |  2.663 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.632 ns | 0.0151 ns | 0.0142 ns |  2.631 ns |  0.99 |    0.01 |      - |     - |     - |         - |
|               LogMsg |  4.987 ns | 0.0768 ns | 0.0718 ns |  4.956 ns |  1.88 |    0.04 |      - |     - |     - |         - |
|         LogMsgWithEx |  5.525 ns | 0.0420 ns | 0.0393 ns |  5.523 ns |  2.08 |    0.02 |      - |     - |     - |         - |
|           LogScalar1 |  8.975 ns | 0.0550 ns | 0.0514 ns |  8.978 ns |  3.38 |    0.03 |      - |     - |     - |         - |
|           LogScalar2 | 11.125 ns | 0.1293 ns | 0.1210 ns | 11.080 ns |  4.19 |    0.07 |      - |     - |     - |         - |
|           LogScalar3 | 11.154 ns | 0.1366 ns | 0.1277 ns | 11.160 ns |  4.19 |    0.06 |      - |     - |     - |         - |
|        LogScalarMany | 21.467 ns | 0.4433 ns | 0.4147 ns | 21.659 ns |  8.07 |    0.20 | 0.0053 |     - |     - |      28 B |
|     LogScalarStruct1 |  6.528 ns | 0.1087 ns | 0.1017 ns |  6.518 ns |  2.45 |    0.05 |      - |     - |     - |         - |
|     LogScalarStruct2 |  9.211 ns | 0.1095 ns | 0.1024 ns |  9.221 ns |  3.46 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct3 |  9.346 ns | 0.1282 ns | 0.1199 ns |  9.333 ns |  3.51 |    0.05 |      - |     - |     - |         - |
|  LogScalarStructMany | 29.366 ns | 0.6125 ns | 0.5729 ns | 29.409 ns | 11.06 |    0.22 | 0.0145 |     - |     - |      76 B |
|   LogScalarBigStruct | 20.162 ns | 0.2247 ns | 0.2102 ns | 20.162 ns |  7.57 |    0.10 |      - |     - |     - |         - |
|        LogDictionary | 11.533 ns | 0.1434 ns | 0.1342 ns | 11.575 ns |  4.34 |    0.06 | 0.0031 |     - |     - |      16 B |
|          LogSequence | 11.649 ns | 0.1492 ns | 0.1395 ns | 11.651 ns |  4.38 |    0.06 | 0.0031 |     - |     - |      16 B |
|         LogAnonymous | 11.590 ns | 0.1520 ns | 0.1422 ns | 11.642 ns |  4.36 |    0.07 | 0.0031 |     - |     - |      16 B |
|              LogMix2 | 10.828 ns | 0.1027 ns | 0.0858 ns | 10.824 ns |  4.07 |    0.03 |      - |     - |     - |         - |
|              LogMix3 | 11.823 ns | 0.1219 ns | 0.1140 ns | 11.823 ns |  4.45 |    0.06 |      - |     - |     - |         - |
|              LogMix4 | 30.126 ns | 0.6273 ns | 0.8586 ns | 29.911 ns | 11.34 |    0.43 | 0.0153 |     - |     - |      80 B |
|              LogMix5 | 38.526 ns | 1.3035 ns | 3.6550 ns | 37.400 ns | 14.24 |    0.93 | 0.0183 |     - |     - |      96 B |
|           LogMixMany | 74.590 ns | 1.6171 ns | 4.6398 ns | 73.201 ns | 27.89 |    1.90 | 0.0321 |     - |     - |     168 B |
