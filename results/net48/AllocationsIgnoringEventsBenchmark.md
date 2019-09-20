``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0


```
|               Method |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |-----------:|----------:|----------:|-----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |   2.797 ns | 0.0327 ns | 0.0306 ns |   2.791 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |   2.813 ns | 0.0532 ns | 0.0497 ns |   2.802 ns |  1.01 |    0.02 |      - |     - |     - |         - |
|               LogMsg |   5.168 ns | 0.0199 ns | 0.0186 ns |   5.169 ns |  1.85 |    0.02 |      - |     - |     - |         - |
|         LogMsgWithEx |   5.729 ns | 0.0614 ns | 0.0574 ns |   5.737 ns |  2.05 |    0.02 |      - |     - |     - |         - |
|           LogScalar1 |   8.793 ns | 0.0755 ns | 0.0589 ns |   8.804 ns |  3.14 |    0.04 |      - |     - |     - |         - |
|           LogScalar2 |  11.987 ns | 0.3333 ns | 0.5089 ns |  11.748 ns |  4.39 |    0.22 |      - |     - |     - |         - |
|           LogScalar3 |  12.092 ns | 0.0384 ns | 0.0321 ns |  12.091 ns |  4.33 |    0.06 |      - |     - |     - |         - |
|        LogScalarMany |  20.728 ns | 0.4387 ns | 0.6830 ns |  20.373 ns |  7.39 |    0.22 | 0.0089 |     - |     - |      28 B |
|     LogScalarStruct1 |   6.979 ns | 0.0578 ns | 0.0541 ns |   6.979 ns |  2.50 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct2 |   9.371 ns | 0.0416 ns | 0.0389 ns |   9.380 ns |  3.35 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct3 |  10.471 ns | 0.2364 ns | 0.2903 ns |  10.300 ns |  3.78 |    0.11 |      - |     - |     - |         - |
|  LogScalarStructMany |  28.383 ns | 0.0842 ns | 0.0787 ns |  28.386 ns | 10.15 |    0.10 | 0.0241 |     - |     - |      76 B |
|   LogScalarBigStruct |  20.639 ns | 0.0760 ns | 0.0674 ns |  20.654 ns |  7.38 |    0.09 |      - |     - |     - |         - |
|        LogDictionary |  12.926 ns | 0.3735 ns | 1.0657 ns |  12.314 ns |  4.73 |    0.50 | 0.0051 |     - |     - |      16 B |
|          LogSequence |  12.229 ns | 0.1565 ns | 0.1464 ns |  12.249 ns |  4.37 |    0.08 | 0.0051 |     - |     - |      16 B |
|         LogAnonymous |  11.945 ns | 0.0816 ns | 0.0681 ns |  11.921 ns |  4.27 |    0.06 | 0.0051 |     - |     - |      16 B |
|              LogMix2 |  11.734 ns | 0.0491 ns | 0.0410 ns |  11.739 ns |  4.20 |    0.06 |      - |     - |     - |         - |
|              LogMix3 |  12.975 ns | 0.2905 ns | 0.3229 ns |  12.820 ns |  4.65 |    0.12 |      - |     - |     - |         - |
|              LogMix4 |  28.868 ns | 0.4372 ns | 0.4089 ns |  29.028 ns | 10.32 |    0.16 | 0.0255 |     - |     - |      80 B |
|              LogMix5 |  34.651 ns | 0.1549 ns | 0.1449 ns |  34.630 ns | 12.39 |    0.13 | 0.0305 |     - |     - |      96 B |
|           LogMixMany |  69.497 ns | 0.8639 ns | 0.7658 ns |  69.320 ns | 24.86 |    0.39 | 0.0534 |     - |     - |     168 B |
|               LogAll | 235.090 ns | 4.6241 ns | 6.7779 ns | 232.866 ns | 85.20 |    2.51 | 0.0305 |     - |     - |      96 B |
