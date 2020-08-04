``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.602 ns | 0.0779 ns | 0.0927 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.529 ns | 0.0770 ns | 0.1079 ns |  0.97 |    0.06 |      - |     - |     - |         - |
|               LogMsg |  4.548 ns | 0.0902 ns | 0.0844 ns |  1.75 |    0.08 |      - |     - |     - |         - |
|         LogMsgWithEx |  5.051 ns | 0.1097 ns | 0.1026 ns |  1.94 |    0.10 |      - |     - |     - |         - |
|           LogScalar1 |  8.404 ns | 0.1817 ns | 0.1700 ns |  3.24 |    0.17 |      - |     - |     - |         - |
|           LogScalar2 | 10.452 ns | 0.1784 ns | 0.1669 ns |  4.02 |    0.19 |      - |     - |     - |         - |
|           LogScalar3 | 10.601 ns | 0.1657 ns | 0.1550 ns |  4.08 |    0.19 |      - |     - |     - |         - |
|        LogScalarMany | 18.830 ns | 0.3528 ns | 0.3300 ns |  7.24 |    0.23 | 0.0053 |     - |     - |      28 B |
|     LogScalarStruct1 |  6.165 ns | 0.1447 ns | 0.1667 ns |  2.37 |    0.11 |      - |     - |     - |         - |
|     LogScalarStruct2 |  8.492 ns | 0.1640 ns | 0.1534 ns |  3.27 |    0.13 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.937 ns | 0.1853 ns | 0.1733 ns |  3.44 |    0.12 |      - |     - |     - |         - |
|  LogScalarStructMany | 25.802 ns | 0.3311 ns | 0.3097 ns |  9.93 |    0.45 | 0.0145 |     - |     - |      76 B |
|   LogScalarBigStruct | 18.971 ns | 0.3887 ns | 0.3635 ns |  7.30 |    0.36 |      - |     - |     - |         - |
|        LogDictionary | 10.502 ns | 0.1772 ns | 0.1657 ns |  4.04 |    0.19 | 0.0031 |     - |     - |      16 B |
|          LogSequence | 10.562 ns | 0.2314 ns | 0.2165 ns |  4.07 |    0.20 | 0.0031 |     - |     - |      16 B |
|         LogAnonymous | 10.513 ns | 0.1804 ns | 0.1688 ns |  4.05 |    0.19 | 0.0031 |     - |     - |      16 B |
|              LogMix2 | 10.112 ns | 0.1961 ns | 0.1834 ns |  3.89 |    0.19 |      - |     - |     - |         - |
|              LogMix3 | 11.185 ns | 0.1510 ns | 0.1413 ns |  4.31 |    0.20 |      - |     - |     - |         - |
|              LogMix4 | 25.800 ns | 0.3445 ns | 0.3222 ns |  9.93 |    0.45 | 0.0153 |     - |     - |      80 B |
|              LogMix5 | 30.764 ns | 0.6136 ns | 0.6565 ns | 11.86 |    0.65 | 0.0183 |     - |     - |      96 B |
|           LogMixMany | 58.992 ns | 0.4598 ns | 0.4301 ns | 22.71 |    0.98 | 0.0321 |     - |     - |     168 B |
