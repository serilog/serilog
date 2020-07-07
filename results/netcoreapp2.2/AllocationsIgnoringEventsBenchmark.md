``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.2.402
  [Host]     : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.735 ns | 0.0148 ns | 0.0131 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.720 ns | 0.0286 ns | 0.0267 ns |  0.99 |    0.01 |      - |     - |     - |         - |
|               LogMsg |  4.522 ns | 0.0292 ns | 0.0273 ns |  1.65 |    0.01 |      - |     - |     - |         - |
|         LogMsgWithEx |  4.803 ns | 0.0397 ns | 0.0371 ns |  1.76 |    0.01 |      - |     - |     - |         - |
|           LogScalar1 |  5.463 ns | 0.0330 ns | 0.0293 ns |  2.00 |    0.01 |      - |     - |     - |         - |
|           LogScalar2 | 11.967 ns | 0.0479 ns | 0.0448 ns |  4.38 |    0.02 |      - |     - |     - |         - |
|           LogScalar3 | 14.794 ns | 0.0751 ns | 0.0702 ns |  5.41 |    0.03 |      - |     - |     - |         - |
|        LogScalarMany | 16.128 ns | 0.0690 ns | 0.0645 ns |  5.90 |    0.03 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  4.947 ns | 0.0197 ns | 0.0185 ns |  1.81 |    0.01 |      - |     - |     - |         - |
|     LogScalarStruct2 |  5.393 ns | 0.0410 ns | 0.0364 ns |  1.97 |    0.02 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.548 ns | 0.0432 ns | 0.0404 ns |  3.13 |    0.02 |      - |     - |     - |         - |
|  LogScalarStructMany | 24.007 ns | 0.1304 ns | 0.1219 ns |  8.78 |    0.05 | 0.0241 |     - |     - |     152 B |
|   LogScalarBigStruct | 19.556 ns | 0.1490 ns | 0.1394 ns |  7.15 |    0.06 |      - |     - |     - |         - |
|        LogDictionary |  7.830 ns | 0.0531 ns | 0.0497 ns |  2.86 |    0.03 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  7.883 ns | 0.0672 ns | 0.0628 ns |  2.88 |    0.03 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  7.755 ns | 0.0461 ns | 0.0431 ns |  2.84 |    0.02 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | 11.316 ns | 0.0624 ns | 0.0584 ns |  4.14 |    0.03 |      - |     - |     - |         - |
|              LogMix3 | 14.663 ns | 0.1091 ns | 0.1021 ns |  5.36 |    0.05 |      - |     - |     - |         - |
|              LogMix4 | 23.504 ns | 0.1833 ns | 0.1715 ns |  8.60 |    0.07 | 0.0216 |     - |     - |     136 B |
|              LogMix5 | 26.495 ns | 0.0728 ns | 0.0681 ns |  9.69 |    0.06 | 0.0267 |     - |     - |     168 B |
|           LogMixMany | 53.398 ns | 0.5573 ns | 0.5213 ns | 19.55 |    0.21 | 0.0445 |     - |     - |     280 B |
