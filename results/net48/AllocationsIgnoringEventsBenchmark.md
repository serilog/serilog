``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.571 ns | 0.0545 ns | 0.0510 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.568 ns | 0.0552 ns | 0.0489 ns |  1.00 |    0.03 |      - |     - |     - |         - |
|               LogMsg |  4.771 ns | 0.0723 ns | 0.0676 ns |  1.86 |    0.03 |      - |     - |     - |         - |
|         LogMsgWithEx |  5.398 ns | 0.0571 ns | 0.0534 ns |  2.10 |    0.04 |      - |     - |     - |         - |
|           LogScalar1 |  8.770 ns | 0.1225 ns | 0.1145 ns |  3.41 |    0.09 |      - |     - |     - |         - |
|           LogScalar2 | 11.152 ns | 0.1318 ns | 0.1168 ns |  4.33 |    0.09 |      - |     - |     - |         - |
|           LogScalar3 | 11.147 ns | 0.1334 ns | 0.1248 ns |  4.34 |    0.09 |      - |     - |     - |         - |
|        LogScalarMany | 20.383 ns | 0.3705 ns | 0.3466 ns |  7.93 |    0.22 | 0.0053 |     - |     - |      28 B |
|     LogScalarStruct1 |  6.439 ns | 0.0800 ns | 0.0748 ns |  2.51 |    0.07 |      - |     - |     - |         - |
|     LogScalarStruct2 |  8.871 ns | 0.1238 ns | 0.1098 ns |  3.45 |    0.08 |      - |     - |     - |         - |
|     LogScalarStruct3 |  9.288 ns | 0.0759 ns | 0.0593 ns |  3.61 |    0.06 |      - |     - |     - |         - |
|  LogScalarStructMany | 28.490 ns | 0.4408 ns | 0.4123 ns | 11.08 |    0.17 | 0.0145 |     - |     - |      76 B |
|   LogScalarBigStruct | 19.947 ns | 0.2288 ns | 0.2140 ns |  7.76 |    0.15 |      - |     - |     - |         - |
|        LogDictionary | 11.319 ns | 0.1616 ns | 0.1512 ns |  4.40 |    0.11 | 0.0031 |     - |     - |      16 B |
|          LogSequence | 11.290 ns | 0.1176 ns | 0.1100 ns |  4.39 |    0.08 | 0.0031 |     - |     - |      16 B |
|         LogAnonymous | 11.333 ns | 0.1438 ns | 0.1345 ns |  4.41 |    0.12 | 0.0031 |     - |     - |      16 B |
|              LogMix2 | 10.789 ns | 0.2158 ns | 0.2019 ns |  4.20 |    0.10 |      - |     - |     - |         - |
|              LogMix3 | 11.734 ns | 0.1588 ns | 0.1485 ns |  4.57 |    0.10 |      - |     - |     - |         - |
|              LogMix4 | 28.779 ns | 0.4194 ns | 0.3923 ns | 11.20 |    0.29 | 0.0153 |     - |     - |      80 B |
|              LogMix5 | 33.868 ns | 0.5028 ns | 0.4703 ns | 13.18 |    0.36 | 0.0183 |     - |     - |      96 B |
|           LogMixMany | 64.031 ns | 1.0483 ns | 0.9293 ns | 24.87 |    0.46 | 0.0321 |     - |     - |     168 B |
