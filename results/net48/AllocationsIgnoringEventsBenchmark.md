``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.458 ns | 0.0347 ns | 0.0325 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.448 ns | 0.0246 ns | 0.0230 ns |  1.00 |    0.02 |      - |     - |     - |         - |
|               LogMsg |  4.547 ns | 0.0309 ns | 0.0274 ns |  1.85 |    0.03 |      - |     - |     - |         - |
|         LogMsgWithEx |  5.083 ns | 0.0395 ns | 0.0369 ns |  2.07 |    0.03 |      - |     - |     - |         - |
|           LogScalar1 |  7.788 ns | 0.0573 ns | 0.0536 ns |  3.17 |    0.05 |      - |     - |     - |         - |
|           LogScalar2 | 11.038 ns | 0.0514 ns | 0.0456 ns |  4.50 |    0.06 |      - |     - |     - |         - |
|           LogScalar3 | 13.687 ns | 0.2620 ns | 0.2451 ns |  5.57 |    0.12 |      - |     - |     - |         - |
|        LogScalarMany | 18.138 ns | 0.2250 ns | 0.1879 ns |  7.40 |    0.13 | 0.0053 |     - |     - |      28 B |
|     LogScalarStruct1 |  6.059 ns | 0.1347 ns | 0.1194 ns |  2.47 |    0.06 |      - |     - |     - |         - |
|     LogScalarStruct2 |  8.074 ns | 0.0763 ns | 0.0676 ns |  3.29 |    0.05 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.591 ns | 0.0483 ns | 0.0428 ns |  3.50 |    0.05 |      - |     - |     - |         - |
|  LogScalarStructMany | 24.287 ns | 0.1286 ns | 0.1140 ns |  9.90 |    0.15 | 0.0145 |     - |     - |      76 B |
|   LogScalarBigStruct | 17.847 ns | 0.1015 ns | 0.0949 ns |  7.26 |    0.11 |      - |     - |     - |         - |
|        LogDictionary | 10.210 ns | 0.0470 ns | 0.0440 ns |  4.15 |    0.06 | 0.0031 |     - |     - |      16 B |
|          LogSequence | 10.266 ns | 0.0616 ns | 0.0576 ns |  4.18 |    0.05 | 0.0031 |     - |     - |      16 B |
|         LogAnonymous | 10.322 ns | 0.0923 ns | 0.0863 ns |  4.20 |    0.06 | 0.0031 |     - |     - |      16 B |
|              LogMix2 |  9.983 ns | 0.0717 ns | 0.0671 ns |  4.06 |    0.06 |      - |     - |     - |         - |
|              LogMix3 | 10.914 ns | 0.0784 ns | 0.0734 ns |  4.44 |    0.07 |      - |     - |     - |         - |
|              LogMix4 | 24.485 ns | 0.1397 ns | 0.1306 ns |  9.96 |    0.13 | 0.0153 |     - |     - |      80 B |
|              LogMix5 | 31.109 ns | 0.1832 ns | 0.1714 ns | 12.66 |    0.16 | 0.0183 |     - |     - |      96 B |
|           LogMixMany | 58.769 ns | 0.6206 ns | 0.5806 ns | 23.92 |    0.46 | 0.0321 |     - |     - |     168 B |
