``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|               Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.277 ns | 0.0277 ns | 0.0259 ns |  2.289 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.267 ns | 0.0271 ns | 0.0254 ns |  2.271 ns |  1.00 |    0.01 |      - |     - |     - |         - |
|               LogMsg |  3.484 ns | 0.0401 ns | 0.0375 ns |  3.484 ns |  1.53 |    0.02 |      - |     - |     - |         - |
|         LogMsgWithEx |  4.243 ns | 0.2419 ns | 0.6824 ns |  4.039 ns |  1.64 |    0.10 |      - |     - |     - |         - |
|           LogScalar1 |  6.526 ns | 0.1514 ns | 0.1342 ns |  6.512 ns |  2.86 |    0.07 |      - |     - |     - |         - |
|           LogScalar2 | 11.674 ns | 0.2593 ns | 0.2547 ns | 11.553 ns |  5.12 |    0.12 |      - |     - |     - |         - |
|           LogScalar3 | 14.986 ns | 0.3045 ns | 0.2699 ns | 15.030 ns |  6.58 |    0.16 |      - |     - |     - |         - |
|        LogScalarMany | 22.490 ns | 0.5835 ns | 1.6361 ns | 22.133 ns |  9.68 |    0.68 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  5.751 ns | 0.0367 ns | 0.0343 ns |  5.755 ns |  2.53 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct2 |  5.867 ns | 0.0442 ns | 0.0392 ns |  5.858 ns |  2.58 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct3 |  9.295 ns | 0.2979 ns | 0.4894 ns |  9.150 ns |  4.19 |    0.25 |      - |     - |     - |         - |
|  LogScalarStructMany | 35.389 ns | 1.0729 ns | 2.9909 ns | 34.687 ns | 16.17 |    0.83 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | 21.473 ns | 0.1100 ns | 0.1029 ns | 21.481 ns |  9.43 |    0.12 |      - |     - |     - |         - |
|        LogDictionary | 10.123 ns | 0.0967 ns | 0.0904 ns | 10.126 ns |  4.45 |    0.07 | 0.0051 |     - |     - |      32 B |
|          LogSequence | 11.055 ns | 0.2497 ns | 0.7043 ns | 10.912 ns |  5.07 |    0.46 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | 11.087 ns | 0.2482 ns | 0.3313 ns | 11.166 ns |  4.89 |    0.15 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | 11.856 ns | 0.2100 ns | 0.1964 ns | 11.799 ns |  5.21 |    0.12 |      - |     - |     - |         - |
|              LogMix3 | 15.392 ns | 0.1276 ns | 0.1065 ns | 15.357 ns |  6.76 |    0.11 |      - |     - |     - |         - |
|              LogMix4 | 31.278 ns | 0.6426 ns | 0.9008 ns | 31.305 ns | 13.91 |    0.47 | 0.0216 |     - |     - |     136 B |
|              LogMix5 | 38.216 ns | 0.7820 ns | 1.2404 ns | 38.127 ns | 16.91 |    0.62 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | 71.808 ns | 1.6940 ns | 4.6374 ns | 70.046 ns | 32.07 |    1.82 | 0.0446 |     - |     - |     280 B |
