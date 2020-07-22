``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  3.710 ns | 0.4189 ns | 1.2350 ns |  3.419 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.659 ns | 0.0505 ns | 0.0448 ns |  2.643 ns |  0.61 |    0.20 |      - |     - |     - |         - |
|               LogMsg |  3.991 ns | 0.0802 ns | 0.0751 ns |  3.989 ns |  0.89 |    0.30 |      - |     - |     - |         - |
|         LogMsgWithEx |  5.029 ns | 0.1437 ns | 0.2869 ns |  4.922 ns |  1.35 |    0.38 |      - |     - |     - |         - |
|           LogScalar1 |  8.755 ns | 0.1420 ns | 0.1329 ns |  8.746 ns |  1.96 |    0.68 |      - |     - |     - |         - |
|           LogScalar2 | 11.152 ns | 0.2409 ns | 0.2253 ns | 11.106 ns |  2.49 |    0.84 |      - |     - |     - |         - |
|           LogScalar3 | 13.174 ns | 0.2123 ns | 0.1882 ns | 13.120 ns |  3.01 |    0.99 |      - |     - |     - |         - |
|        LogScalarMany | 20.014 ns | 0.4241 ns | 0.5048 ns | 19.942 ns |  4.25 |    1.41 | 0.0053 |     - |     - |      28 B |
|     LogScalarStruct1 |  6.214 ns | 0.1408 ns | 0.1317 ns |  6.220 ns |  1.39 |    0.48 |      - |     - |     - |         - |
|     LogScalarStruct2 |  8.680 ns | 0.2004 ns | 0.1875 ns |  8.680 ns |  1.94 |    0.66 |      - |     - |     - |         - |
|     LogScalarStruct3 |  9.120 ns | 0.1795 ns | 0.1679 ns |  9.114 ns |  2.04 |    0.69 |      - |     - |     - |         - |
|  LogScalarStructMany | 26.342 ns | 0.4041 ns | 0.3780 ns | 26.453 ns |  5.89 |    2.01 | 0.0145 |     - |     - |      76 B |
|   LogScalarBigStruct | 19.893 ns | 0.2601 ns | 0.2433 ns | 19.886 ns |  4.45 |    1.53 |      - |     - |     - |         - |
|        LogDictionary | 11.853 ns | 0.2655 ns | 0.6994 ns | 12.042 ns |  3.32 |    0.97 | 0.0031 |     - |     - |      16 B |
|          LogSequence | 11.839 ns | 0.2076 ns | 0.1734 ns | 11.801 ns |  2.77 |    0.94 | 0.0030 |     - |     - |      16 B |
|         LogAnonymous | 12.157 ns | 0.3226 ns | 0.7974 ns | 11.804 ns |  3.44 |    0.94 | 0.0031 |     - |     - |      16 B |
|              LogMix2 | 11.110 ns | 0.0511 ns | 0.0478 ns | 11.120 ns |  2.48 |    0.85 |      - |     - |     - |         - |
|              LogMix3 | 12.820 ns | 0.0636 ns | 0.0564 ns | 12.834 ns |  2.93 |    0.97 |      - |     - |     - |         - |
|              LogMix4 | 28.833 ns | 0.3394 ns | 0.3175 ns | 28.897 ns |  6.44 |    2.20 | 0.0153 |     - |     - |      80 B |
|              LogMix5 | 34.959 ns | 0.5909 ns | 0.5238 ns | 34.940 ns |  7.99 |    2.60 | 0.0183 |     - |     - |      96 B |
|           LogMixMany | 65.080 ns | 0.7746 ns | 0.7245 ns | 64.923 ns | 14.55 |    4.99 | 0.0321 |     - |     - |     168 B |
