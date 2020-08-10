``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.603 ns | 0.0786 ns | 0.0874 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.609 ns | 0.0786 ns | 0.0935 ns |  1.00 |    0.06 |      - |     - |     - |         - |
|               LogMsg |  4.649 ns | 0.0908 ns | 0.0849 ns |  1.78 |    0.06 |      - |     - |     - |         - |
|         LogMsgWithEx |  5.083 ns | 0.0915 ns | 0.0856 ns |  1.95 |    0.07 |      - |     - |     - |         - |
|           LogScalar1 |  8.338 ns | 0.1801 ns | 0.1684 ns |  3.20 |    0.10 |      - |     - |     - |         - |
|           LogScalar2 | 10.463 ns | 0.1684 ns | 0.1576 ns |  4.01 |    0.16 |      - |     - |     - |         - |
|           LogScalar3 | 10.521 ns | 0.1924 ns | 0.1800 ns |  4.04 |    0.18 |      - |     - |     - |         - |
|        LogScalarMany | 18.382 ns | 0.3924 ns | 0.4198 ns |  7.06 |    0.31 | 0.0053 |     - |     - |      28 B |
|     LogScalarStruct1 |  6.178 ns | 0.1432 ns | 0.1704 ns |  2.37 |    0.06 |      - |     - |     - |         - |
|     LogScalarStruct2 |  8.544 ns | 0.1907 ns | 0.2041 ns |  3.28 |    0.16 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.957 ns | 0.1880 ns | 0.1758 ns |  3.44 |    0.18 |      - |     - |     - |         - |
|  LogScalarStructMany | 27.130 ns | 0.3318 ns | 0.3104 ns | 10.41 |    0.43 | 0.0145 |     - |     - |      76 B |
|   LogScalarBigStruct | 19.295 ns | 0.4038 ns | 0.4321 ns |  7.40 |    0.23 |      - |     - |     - |         - |
|        LogDictionary | 10.541 ns | 0.2234 ns | 0.2089 ns |  4.04 |    0.16 | 0.0031 |     - |     - |      16 B |
|          LogSequence | 10.906 ns | 0.1917 ns | 0.1794 ns |  4.18 |    0.16 | 0.0031 |     - |     - |      16 B |
|         LogAnonymous | 10.609 ns | 0.1873 ns | 0.1752 ns |  4.07 |    0.15 | 0.0031 |     - |     - |      16 B |
|              LogMix2 | 11.039 ns | 0.1277 ns | 0.1194 ns |  4.24 |    0.16 |      - |     - |     - |         - |
|              LogMix3 | 11.130 ns | 0.1916 ns | 0.1792 ns |  4.27 |    0.17 |      - |     - |     - |         - |
|              LogMix4 | 26.012 ns | 0.4551 ns | 0.4257 ns |  9.98 |    0.41 | 0.0153 |     - |     - |      80 B |
|              LogMix5 | 31.329 ns | 0.6536 ns | 0.7527 ns | 12.04 |    0.30 | 0.0183 |     - |     - |      96 B |
|           LogMixMany | 58.881 ns | 0.7589 ns | 0.7099 ns | 22.60 |    0.95 | 0.0321 |     - |     - |     168 B |
