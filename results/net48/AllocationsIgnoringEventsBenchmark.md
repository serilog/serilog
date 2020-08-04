``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.482 ns | 0.0765 ns | 0.0939 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.467 ns | 0.0756 ns | 0.0871 ns |  1.00 |    0.06 |      - |     - |     - |         - |
|               LogMsg |  4.551 ns | 0.1009 ns | 0.0944 ns |  1.83 |    0.09 |      - |     - |     - |         - |
|         LogMsgWithEx |  5.047 ns | 0.1139 ns | 0.1065 ns |  2.03 |    0.08 |      - |     - |     - |         - |
|           LogScalar1 |  8.047 ns | 0.1861 ns | 0.2143 ns |  3.25 |    0.15 |      - |     - |     - |         - |
|           LogScalar2 | 10.231 ns | 0.1839 ns | 0.1720 ns |  4.12 |    0.18 |      - |     - |     - |         - |
|           LogScalar3 | 10.510 ns | 0.1989 ns | 0.1861 ns |  4.23 |    0.18 |      - |     - |     - |         - |
|        LogScalarMany | 19.150 ns | 0.4015 ns | 0.3756 ns |  7.70 |    0.19 | 0.0053 |     - |     - |      28 B |
|     LogScalarStruct1 |  6.362 ns | 0.1496 ns | 0.1780 ns |  2.57 |    0.10 |      - |     - |     - |         - |
|     LogScalarStruct2 |  8.439 ns | 0.1760 ns | 0.1647 ns |  3.39 |    0.14 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.862 ns | 0.1724 ns | 0.1613 ns |  3.56 |    0.15 |      - |     - |     - |         - |
|  LogScalarStructMany | 25.866 ns | 0.4752 ns | 0.3968 ns | 10.42 |    0.42 | 0.0145 |     - |     - |      76 B |
|   LogScalarBigStruct | 20.675 ns | 0.4089 ns | 0.3825 ns |  8.32 |    0.43 |      - |     - |     - |         - |
|        LogDictionary | 10.318 ns | 0.1737 ns | 0.1625 ns |  4.15 |    0.17 | 0.0031 |     - |     - |      16 B |
|          LogSequence | 10.321 ns | 0.1757 ns | 0.2024 ns |  4.16 |    0.19 | 0.0031 |     - |     - |      16 B |
|         LogAnonymous | 10.284 ns | 0.1688 ns | 0.1579 ns |  4.14 |    0.18 | 0.0031 |     - |     - |      16 B |
|              LogMix2 | 10.171 ns | 0.1846 ns | 0.1727 ns |  4.09 |    0.19 |      - |     - |     - |         - |
|              LogMix3 | 13.867 ns | 0.1455 ns | 0.1361 ns |  5.58 |    0.21 |      - |     - |     - |         - |
|              LogMix4 | 26.812 ns | 0.2951 ns | 0.2761 ns | 10.79 |    0.45 | 0.0153 |     - |     - |      80 B |
|              LogMix5 | 31.578 ns | 0.6492 ns | 0.7215 ns | 12.76 |    0.40 | 0.0183 |     - |     - |      96 B |
|           LogMixMany | 57.369 ns | 0.6973 ns | 0.6522 ns | 23.08 |    0.89 | 0.0321 |     - |     - |     168 B |
