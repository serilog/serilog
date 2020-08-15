``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.219 ns | 0.0380 ns | 0.0337 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.652 ns | 0.0792 ns | 0.1030 ns |  1.19 |    0.06 |      - |     - |     - |         - |
|               LogMsg |  3.422 ns | 0.0545 ns | 0.0483 ns |  1.54 |    0.03 |      - |     - |     - |         - |
|         LogMsgWithEx |  4.256 ns | 0.1087 ns | 0.1017 ns |  1.92 |    0.06 |      - |     - |     - |         - |
|           LogScalar1 |  6.091 ns | 0.0770 ns | 0.0720 ns |  2.74 |    0.05 |      - |     - |     - |         - |
|           LogScalar2 | 11.705 ns | 0.2531 ns | 0.3864 ns |  5.18 |    0.23 |      - |     - |     - |         - |
|           LogScalar3 | 14.884 ns | 0.3252 ns | 0.6265 ns |  6.44 |    0.17 |      - |     - |     - |         - |
|        LogScalarMany | 18.597 ns | 0.3905 ns | 0.4178 ns |  8.39 |    0.25 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  5.132 ns | 0.0936 ns | 0.0876 ns |  2.31 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct2 |  5.649 ns | 0.0843 ns | 0.0789 ns |  2.54 |    0.06 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.045 ns | 0.1192 ns | 0.1056 ns |  3.63 |    0.08 |      - |     - |     - |         - |
|  LogScalarStructMany | 27.199 ns | 0.5010 ns | 0.4686 ns | 12.25 |    0.28 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | 20.273 ns | 0.3939 ns | 0.3684 ns |  9.15 |    0.23 |      - |     - |     - |         - |
|        LogDictionary |  9.134 ns | 0.1894 ns | 0.1772 ns |  4.11 |    0.08 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  9.165 ns | 0.2106 ns | 0.2254 ns |  4.13 |    0.13 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  8.926 ns | 0.1899 ns | 0.1776 ns |  4.02 |    0.10 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | 10.853 ns | 0.1854 ns | 0.1734 ns |  4.90 |    0.12 |      - |     - |     - |         - |
|              LogMix3 | 14.353 ns | 0.3060 ns | 0.3979 ns |  6.48 |    0.21 |      - |     - |     - |         - |
|              LogMix4 | 26.082 ns | 0.3248 ns | 0.3038 ns | 11.75 |    0.26 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | 29.974 ns | 0.6228 ns | 0.8099 ns | 13.47 |    0.39 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | 56.591 ns | 0.8564 ns | 0.8011 ns | 25.54 |    0.57 | 0.0446 |     - |     - |     280 B |
