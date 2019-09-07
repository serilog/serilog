``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0


```
|               Method |       Mean |     Error |     StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |-----------:|----------:|-----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   2.972 ns | 0.0905 ns |  0.1561 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   2.933 ns | 0.0890 ns |  0.1360 ns |  0.98 |    0.04 |      - |     - |     - |         - |
|               LogMsg |   5.657 ns | 0.1424 ns |  0.2132 ns |  1.90 |    0.11 |      - |     - |     - |         - |
|         LogMsgWithEx |   6.107 ns | 0.1530 ns |  0.3228 ns |  2.06 |    0.16 |      - |     - |     - |         - |
|           LogScalar1 |   9.606 ns | 0.2228 ns |  0.2652 ns |  3.21 |    0.18 |      - |     - |     - |         - |
|           LogScalar2 |  12.272 ns | 0.1955 ns |  0.1829 ns |  4.06 |    0.25 |      - |     - |     - |         - |
|           LogScalar3 |  12.505 ns | 0.2692 ns |  0.5186 ns |  4.22 |    0.31 |      - |     - |     - |         - |
|        LogScalarMany |  21.341 ns | 0.4591 ns |  0.5466 ns |  7.12 |    0.46 | 0.0089 |     - |     - |      28 B |
|     LogScalarStruct1 |   7.478 ns | 0.1934 ns |  0.2895 ns |  2.52 |    0.16 |      - |     - |     - |         - |
|     LogScalarStruct2 |  10.812 ns | 0.2469 ns |  0.2425 ns |  3.59 |    0.22 |      - |     - |     - |         - |
|     LogScalarStruct3 |  10.489 ns | 0.2334 ns |  0.2293 ns |  3.48 |    0.22 |      - |     - |     - |         - |
|  LogScalarStructMany |  29.732 ns | 0.6247 ns |  1.2476 ns | 10.05 |    0.77 | 0.0241 |     - |     - |      76 B |
|   LogScalarBigStruct |  22.723 ns | 0.4813 ns |  0.4943 ns |  7.55 |    0.44 |      - |     - |     - |         - |
|        LogDictionary |  12.722 ns | 0.2858 ns |  0.3509 ns |  4.26 |    0.27 | 0.0051 |     - |     - |      16 B |
|          LogSequence |  12.530 ns | 0.2370 ns |  0.2217 ns |  4.14 |    0.24 | 0.0051 |     - |     - |      16 B |
|         LogAnonymous |  12.635 ns | 0.2738 ns |  0.3260 ns |  4.22 |    0.25 | 0.0051 |     - |     - |      16 B |
|              LogMix2 |  12.089 ns | 0.1893 ns |  0.1771 ns |  4.00 |    0.24 |      - |     - |     - |         - |
|              LogMix3 |  13.484 ns | 0.3953 ns |  0.4393 ns |  4.49 |    0.27 |      - |     - |     - |         - |
|              LogMix4 |  29.684 ns | 0.6244 ns |  1.1573 ns | 10.01 |    0.67 | 0.0255 |     - |     - |      80 B |
|              LogMix5 |  37.128 ns | 0.7801 ns |  2.1355 ns | 12.38 |    0.92 | 0.0305 |     - |     - |      96 B |
|           LogMixMany |  72.355 ns | 1.4785 ns |  3.3673 ns | 24.60 |    1.32 | 0.0534 |     - |     - |     168 B |
|               LogAll | 272.937 ns | 6.9673 ns | 19.8781 ns | 90.33 |    9.23 | 0.0305 |     - |     - |      96 B |
