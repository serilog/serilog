``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|               Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.282 ns | 0.0692 ns | 0.0647 ns |  2.276 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.265 ns | 0.0704 ns | 0.0692 ns |  2.278 ns |  0.99 |    0.03 |      - |     - |     - |         - |
|               LogMsg |  3.390 ns | 0.0920 ns | 0.0985 ns |  3.384 ns |  1.48 |    0.06 |      - |     - |     - |         - |
|         LogMsgWithEx |  3.460 ns | 0.0821 ns | 0.0728 ns |  3.476 ns |  1.52 |    0.06 |      - |     - |     - |         - |
|           LogScalar1 |  6.467 ns | 0.1253 ns | 0.1110 ns |  6.452 ns |  2.84 |    0.12 |      - |     - |     - |         - |
|           LogScalar2 | 12.369 ns | 0.1414 ns | 0.1253 ns | 12.376 ns |  5.42 |    0.16 |      - |     - |     - |         - |
|           LogScalar3 | 15.300 ns | 0.3252 ns | 0.4341 ns | 15.203 ns |  6.79 |    0.29 |      - |     - |     - |         - |
|        LogScalarMany | 20.894 ns | 0.4472 ns | 0.9136 ns | 20.740 ns |  8.99 |    0.48 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  5.722 ns | 0.1434 ns | 0.2011 ns |  5.726 ns |  2.49 |    0.09 |      - |     - |     - |         - |
|     LogScalarStruct2 |  5.975 ns | 0.1476 ns | 0.1579 ns |  5.942 ns |  2.63 |    0.12 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.574 ns | 0.1980 ns | 0.1852 ns |  8.606 ns |  3.76 |    0.14 |      - |     - |     - |         - |
|  LogScalarStructMany | 29.542 ns | 0.6031 ns | 0.5347 ns | 29.655 ns | 12.95 |    0.39 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | 24.778 ns | 1.4870 ns | 4.3140 ns | 22.772 ns |  9.46 |    0.35 |      - |     - |     - |         - |
|        LogDictionary |  9.608 ns | 0.2691 ns | 0.4570 ns |  9.503 ns |  4.25 |    0.18 | 0.0051 |     - |     - |      32 B |
|          LogSequence | 10.650 ns | 0.2359 ns | 0.3149 ns | 10.622 ns |  4.71 |    0.24 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous | 10.584 ns | 0.2402 ns | 0.6575 ns | 10.439 ns |  4.47 |    0.25 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | 11.639 ns | 0.2182 ns | 0.2041 ns | 11.550 ns |  5.10 |    0.18 |      - |     - |     - |         - |
|              LogMix3 | 16.360 ns | 0.3505 ns | 0.3896 ns | 16.322 ns |  7.15 |    0.25 |      - |     - |     - |         - |
|              LogMix4 | 28.491 ns | 0.5982 ns | 0.6401 ns | 28.526 ns | 12.51 |    0.36 | 0.0216 |     - |     - |     136 B |
|              LogMix5 | 35.058 ns | 0.7218 ns | 0.8023 ns | 35.034 ns | 15.40 |    0.46 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | 69.400 ns | 1.9957 ns | 5.3955 ns | 67.732 ns | 30.43 |    2.85 | 0.0446 |     - |     - |     280 B |
