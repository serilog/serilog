``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.226 ns | 0.0332 ns | 0.0311 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.104 ns | 0.0479 ns | 0.0449 ns |  0.95 |    0.03 |      - |     - |     - |         - |
|               LogMsg |  3.684 ns | 0.0941 ns | 0.1465 ns |  1.66 |    0.08 |      - |     - |     - |         - |
|         LogMsgWithEx |  4.091 ns | 0.0934 ns | 0.0873 ns |  1.84 |    0.05 |      - |     - |     - |         - |
|           LogScalar1 |  6.128 ns | 0.1449 ns | 0.1488 ns |  2.75 |    0.08 |      - |     - |     - |         - |
|           LogScalar2 | 11.252 ns | 0.1532 ns | 0.1881 ns |  5.05 |    0.09 |      - |     - |     - |         - |
|           LogScalar3 | 14.184 ns | 0.3015 ns | 0.3702 ns |  6.39 |    0.19 |      - |     - |     - |         - |
|        LogScalarMany | 18.352 ns | 0.3919 ns | 0.4356 ns |  8.25 |    0.21 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  5.198 ns | 0.0882 ns | 0.0825 ns |  2.34 |    0.05 |      - |     - |     - |         - |
|     LogScalarStruct2 |  5.631 ns | 0.0762 ns | 0.0713 ns |  2.53 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.894 ns | 0.2024 ns | 0.2837 ns |  3.97 |    0.17 |      - |     - |     - |         - |
|  LogScalarStructMany | 26.648 ns | 0.4241 ns | 0.3967 ns | 11.97 |    0.17 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | 20.577 ns | 0.4263 ns | 0.6884 ns |  9.44 |    0.39 |      - |     - |     - |         - |
|        LogDictionary |  8.896 ns | 0.1953 ns | 0.1826 ns |  4.00 |    0.10 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  8.832 ns | 0.1914 ns | 0.1879 ns |  3.97 |    0.10 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  8.898 ns | 0.2007 ns | 0.1877 ns |  4.00 |    0.09 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | 11.646 ns | 0.1755 ns | 0.1556 ns |  5.23 |    0.10 |      - |     - |     - |         - |
|              LogMix3 | 14.316 ns | 0.3136 ns | 0.5063 ns |  6.55 |    0.21 |      - |     - |     - |         - |
|              LogMix4 | 26.272 ns | 0.3489 ns | 0.3263 ns | 11.80 |    0.25 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | 31.107 ns | 0.6274 ns | 0.7934 ns | 13.98 |    0.39 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | 57.044 ns | 0.6399 ns | 0.5672 ns | 25.60 |    0.46 | 0.0446 |     - |     - |     280 B |
