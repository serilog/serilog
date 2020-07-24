``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.314 ns | 0.0645 ns | 0.0571 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.314 ns | 0.0809 ns | 0.0900 ns |  1.00 |    0.04 |      - |     - |     - |         - |
|               LogMsg |  3.549 ns | 0.0490 ns | 0.0409 ns |  1.53 |    0.04 |      - |     - |     - |         - |
|         LogMsgWithEx |  3.623 ns | 0.0531 ns | 0.0496 ns |  1.57 |    0.04 |      - |     - |     - |         - |
|           LogScalar1 |  6.419 ns | 0.1114 ns | 0.1042 ns |  2.78 |    0.06 |      - |     - |     - |         - |
|           LogScalar2 | 11.690 ns | 0.1270 ns | 0.1188 ns |  5.06 |    0.15 |      - |     - |     - |         - |
|           LogScalar3 | 15.367 ns | 0.3290 ns | 0.5313 ns |  6.46 |    0.33 |      - |     - |     - |         - |
|        LogScalarMany | 19.550 ns | 0.3189 ns | 0.2983 ns |  8.46 |    0.23 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  5.284 ns | 0.0453 ns | 0.0424 ns |  2.29 |    0.06 |      - |     - |     - |         - |
|     LogScalarStruct2 |  5.919 ns | 0.1088 ns | 0.1018 ns |  2.55 |    0.07 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.507 ns | 0.1310 ns | 0.1225 ns |  3.69 |    0.13 |      - |     - |     - |         - |
|  LogScalarStructMany | 27.744 ns | 0.5723 ns | 0.6124 ns | 11.97 |    0.38 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | 21.007 ns | 0.2116 ns | 0.1979 ns |  9.07 |    0.24 |      - |     - |     - |         - |
|        LogDictionary |  9.419 ns | 0.1575 ns | 0.1473 ns |  4.07 |    0.13 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  9.368 ns | 0.1205 ns | 0.1127 ns |  4.05 |    0.12 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  9.398 ns | 0.1281 ns | 0.1198 ns |  4.06 |    0.12 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | 11.351 ns | 0.1016 ns | 0.0901 ns |  4.91 |    0.13 |      - |     - |     - |         - |
|              LogMix3 | 14.830 ns | 0.2407 ns | 0.2251 ns |  6.42 |    0.17 |      - |     - |     - |         - |
|              LogMix4 | 26.053 ns | 0.3543 ns | 0.2766 ns | 11.22 |    0.31 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | 31.932 ns | 0.4949 ns | 0.4629 ns | 13.82 |    0.40 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | 59.503 ns | 0.9262 ns | 0.8663 ns | 25.74 |    0.77 | 0.0446 |     - |     - |     280 B |
