``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.210 ns | 0.0437 ns | 0.0408 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.221 ns | 0.0675 ns | 0.0632 ns |  1.01 |    0.04 |      - |     - |     - |         - |
|               LogMsg |  3.665 ns | 0.0957 ns | 0.0983 ns |  1.66 |    0.05 |      - |     - |     - |         - |
|         LogMsgWithEx |  4.090 ns | 0.1009 ns | 0.0944 ns |  1.85 |    0.05 |      - |     - |     - |         - |
|           LogScalar1 |  6.138 ns | 0.1481 ns | 0.2123 ns |  2.77 |    0.10 |      - |     - |     - |         - |
|           LogScalar2 | 11.474 ns | 0.2487 ns | 0.3486 ns |  5.15 |    0.22 |      - |     - |     - |         - |
|           LogScalar3 | 14.842 ns | 0.3212 ns | 0.4062 ns |  6.74 |    0.19 |      - |     - |     - |         - |
|        LogScalarMany | 18.348 ns | 0.3763 ns | 0.3865 ns |  8.31 |    0.21 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  5.045 ns | 0.1094 ns | 0.1023 ns |  2.28 |    0.06 |      - |     - |     - |         - |
|     LogScalarStruct2 |  5.655 ns | 0.0860 ns | 0.0804 ns |  2.56 |    0.06 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.600 ns | 0.1958 ns | 0.2745 ns |  3.87 |    0.16 |      - |     - |     - |         - |
|  LogScalarStructMany | 26.927 ns | 0.4624 ns | 0.4326 ns | 12.19 |    0.34 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | 19.900 ns | 0.4124 ns | 0.4235 ns |  9.00 |    0.24 |      - |     - |     - |         - |
|        LogDictionary |  8.692 ns | 0.1939 ns | 0.2381 ns |  3.92 |    0.11 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  8.760 ns | 0.1910 ns | 0.1962 ns |  3.97 |    0.13 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  8.869 ns | 0.2041 ns | 0.2005 ns |  4.01 |    0.12 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | 10.893 ns | 0.1906 ns | 0.1783 ns |  4.93 |    0.12 |      - |     - |     - |         - |
|              LogMix3 | 14.090 ns | 0.3011 ns | 0.3584 ns |  6.37 |    0.18 |      - |     - |     - |         - |
|              LogMix4 | 26.024 ns | 0.4352 ns | 0.4071 ns | 11.78 |    0.32 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | 31.108 ns | 0.6330 ns | 0.7535 ns | 14.08 |    0.40 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | 55.392 ns | 0.8662 ns | 0.8102 ns | 25.08 |    0.72 | 0.0446 |     - |     - |     280 B |
