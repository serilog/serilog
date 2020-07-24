``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.725 ns | 0.0639 ns | 0.0567 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.576 ns | 0.0399 ns | 0.0373 ns |  0.95 |    0.03 |      - |     - |     - |         - |
|               LogMsg |  4.747 ns | 0.0474 ns | 0.0443 ns |  1.74 |    0.05 |      - |     - |     - |         - |
|         LogMsgWithEx |  5.215 ns | 0.0516 ns | 0.0483 ns |  1.92 |    0.05 |      - |     - |     - |         - |
|           LogScalar1 |  8.655 ns | 0.0462 ns | 0.0433 ns |  3.18 |    0.07 |      - |     - |     - |         - |
|           LogScalar2 | 10.883 ns | 0.0902 ns | 0.0800 ns |  3.99 |    0.08 |      - |     - |     - |         - |
|           LogScalar3 | 11.012 ns | 0.1052 ns | 0.0932 ns |  4.04 |    0.10 |      - |     - |     - |         - |
|        LogScalarMany | 19.726 ns | 0.2666 ns | 0.2493 ns |  7.23 |    0.18 | 0.0053 |     - |     - |      28 B |
|     LogScalarStruct1 |  6.381 ns | 0.0755 ns | 0.0669 ns |  2.34 |    0.05 |      - |     - |     - |         - |
|     LogScalarStruct2 |  8.823 ns | 0.0789 ns | 0.0738 ns |  3.24 |    0.07 |      - |     - |     - |         - |
|     LogScalarStruct3 |  9.239 ns | 0.0867 ns | 0.0811 ns |  3.39 |    0.08 |      - |     - |     - |         - |
|  LogScalarStructMany | 26.774 ns | 0.1419 ns | 0.1328 ns |  9.83 |    0.20 | 0.0145 |     - |     - |      76 B |
|   LogScalarBigStruct | 19.762 ns | 0.1821 ns | 0.1703 ns |  7.26 |    0.14 |      - |     - |     - |         - |
|        LogDictionary | 10.935 ns | 0.1142 ns | 0.1068 ns |  4.01 |    0.11 | 0.0031 |     - |     - |      16 B |
|          LogSequence | 10.984 ns | 0.1141 ns | 0.1067 ns |  4.03 |    0.11 | 0.0031 |     - |     - |      16 B |
|         LogAnonymous | 10.960 ns | 0.1358 ns | 0.1270 ns |  4.02 |    0.11 | 0.0031 |     - |     - |      16 B |
|              LogMix2 | 10.540 ns | 0.1015 ns | 0.0950 ns |  3.87 |    0.09 |      - |     - |     - |         - |
|              LogMix3 | 12.157 ns | 0.0924 ns | 0.0864 ns |  4.46 |    0.09 |      - |     - |     - |         - |
|              LogMix4 | 26.984 ns | 0.1897 ns | 0.1775 ns |  9.90 |    0.23 | 0.0153 |     - |     - |      80 B |
|              LogMix5 | 32.022 ns | 0.4768 ns | 0.4460 ns | 11.76 |    0.25 | 0.0183 |     - |     - |      96 B |
|           LogMixMany | 60.576 ns | 0.8173 ns | 0.7645 ns | 22.25 |    0.38 | 0.0321 |     - |     - |     168 B |
