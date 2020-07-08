``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.292 ns | 0.0297 ns | 0.0278 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.379 ns | 0.0307 ns | 0.0287 ns |  1.04 |    0.02 |      - |     - |     - |         - |
|               LogMsg |  3.726 ns | 0.0258 ns | 0.0229 ns |  1.63 |    0.02 |      - |     - |     - |         - |
|         LogMsgWithEx |  3.733 ns | 0.0440 ns | 0.0411 ns |  1.63 |    0.02 |      - |     - |     - |         - |
|           LogScalar1 |  6.510 ns | 0.0447 ns | 0.0418 ns |  2.84 |    0.03 |      - |     - |     - |         - |
|           LogScalar2 | 12.463 ns | 0.1306 ns | 0.1221 ns |  5.44 |    0.09 |      - |     - |     - |         - |
|           LogScalar3 | 15.746 ns | 0.3325 ns | 0.4205 ns |  6.78 |    0.15 |      - |     - |     - |         - |
|        LogScalarMany | 19.958 ns | 0.4257 ns | 0.4371 ns |  8.70 |    0.16 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  5.512 ns | 0.0727 ns | 0.0680 ns |  2.41 |    0.05 |      - |     - |     - |         - |
|     LogScalarStruct2 |  5.856 ns | 0.0598 ns | 0.0560 ns |  2.55 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.799 ns | 0.0689 ns | 0.0644 ns |  3.84 |    0.05 |      - |     - |     - |         - |
|  LogScalarStructMany | 30.157 ns | 0.6215 ns | 0.8297 ns | 13.08 |    0.39 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | 21.600 ns | 0.1978 ns | 0.1754 ns |  9.42 |    0.14 |      - |     - |     - |         - |
|        LogDictionary |  9.873 ns | 0.2211 ns | 0.5169 ns |  4.37 |    0.14 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  9.510 ns | 0.2046 ns | 0.1913 ns |  4.15 |    0.09 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  9.782 ns | 0.2216 ns | 0.2464 ns |  4.27 |    0.13 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | 12.035 ns | 0.3471 ns | 0.3409 ns |  5.25 |    0.16 |      - |     - |     - |         - |
|              LogMix3 | 15.625 ns | 0.2129 ns | 0.1992 ns |  6.82 |    0.13 |      - |     - |     - |         - |
|              LogMix4 | 27.006 ns | 0.5632 ns | 0.7519 ns | 11.81 |    0.38 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | 33.020 ns | 0.7049 ns | 1.2160 ns | 14.69 |    0.63 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | 60.358 ns | 1.1888 ns | 1.4152 ns | 26.41 |    0.73 | 0.0446 |     - |     - |     280 B |
