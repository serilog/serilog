``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.489 ns | 0.0766 ns | 0.0969 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.484 ns | 0.0766 ns | 0.0969 ns |  1.00 |    0.02 |      - |     - |     - |         - |
|               LogMsg |  4.528 ns | 0.1013 ns | 0.0947 ns |  1.83 |    0.09 |      - |     - |     - |         - |
|         LogMsgWithEx |  5.036 ns | 0.1013 ns | 0.0947 ns |  2.03 |    0.09 |      - |     - |     - |         - |
|           LogScalar1 |  7.925 ns | 0.1824 ns | 0.2306 ns |  3.19 |    0.17 |      - |     - |     - |         - |
|           LogScalar2 | 10.639 ns | 0.2313 ns | 0.2164 ns |  4.29 |    0.23 |      - |     - |     - |         - |
|           LogScalar3 | 11.133 ns | 0.1277 ns | 0.1195 ns |  4.49 |    0.21 |      - |     - |     - |         - |
|        LogScalarMany | 19.311 ns | 0.4044 ns | 0.3972 ns |  7.78 |    0.27 | 0.0053 |     - |     - |      28 B |
|     LogScalarStruct1 |  6.568 ns | 0.1553 ns | 0.2019 ns |  2.64 |    0.09 |      - |     - |     - |         - |
|     LogScalarStruct2 |  8.466 ns | 0.1936 ns | 0.1901 ns |  3.41 |    0.16 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.925 ns | 0.1928 ns | 0.1803 ns |  3.60 |    0.13 |      - |     - |     - |         - |
|  LogScalarStructMany | 25.801 ns | 0.3111 ns | 0.2910 ns | 10.40 |    0.41 | 0.0145 |     - |     - |      76 B |
|   LogScalarBigStruct | 20.542 ns | 0.4052 ns | 0.3979 ns |  8.28 |    0.24 |      - |     - |     - |         - |
|        LogDictionary | 10.224 ns | 0.2148 ns | 0.2009 ns |  4.12 |    0.21 | 0.0031 |     - |     - |      16 B |
|          LogSequence | 10.325 ns | 0.2290 ns | 0.2142 ns |  4.16 |    0.20 | 0.0031 |     - |     - |      16 B |
|         LogAnonymous | 10.255 ns | 0.1976 ns | 0.1848 ns |  4.14 |    0.20 | 0.0031 |     - |     - |      16 B |
|              LogMix2 | 10.101 ns | 0.2094 ns | 0.1959 ns |  4.07 |    0.17 |      - |     - |     - |         - |
|              LogMix3 | 11.070 ns | 0.2422 ns | 0.2789 ns |  4.46 |    0.22 |      - |     - |     - |         - |
|              LogMix4 | 26.240 ns | 0.4401 ns | 0.4117 ns | 10.58 |    0.44 | 0.0153 |     - |     - |      80 B |
|              LogMix5 | 31.212 ns | 0.6498 ns | 0.8449 ns | 12.54 |    0.52 | 0.0183 |     - |     - |      96 B |
|           LogMixMany | 58.451 ns | 0.7779 ns | 0.7277 ns | 23.56 |    0.85 | 0.0321 |     - |     - |     168 B |
