``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.203 ns | 0.0831 ns | 0.0778 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.183 ns | 0.0424 ns | 0.0396 ns |  0.99 |    0.04 |      - |     - |     - |         - |
|               LogMsg |  3.327 ns | 0.0785 ns | 0.0734 ns |  1.51 |    0.07 |      - |     - |     - |         - |
|         LogMsgWithEx |  3.330 ns | 0.0909 ns | 0.0973 ns |  1.52 |    0.08 |      - |     - |     - |         - |
|           LogScalar1 |  7.405 ns | 0.1733 ns | 0.3768 ns |  3.24 |    0.28 |      - |     - |     - |         - |
|           LogScalar2 | 11.617 ns | 0.2472 ns | 0.2847 ns |  5.26 |    0.23 |      - |     - |     - |         - |
|           LogScalar3 | 15.779 ns | 0.3287 ns | 0.3653 ns |  7.17 |    0.25 |      - |     - |     - |         - |
|        LogScalarMany | 18.185 ns | 0.3819 ns | 0.3572 ns |  8.26 |    0.31 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  5.124 ns | 0.1015 ns | 0.0949 ns |  2.33 |    0.09 |      - |     - |     - |         - |
|     LogScalarStruct2 |  5.678 ns | 0.1346 ns | 0.1653 ns |  2.58 |    0.13 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.202 ns | 0.1870 ns | 0.1837 ns |  3.73 |    0.15 |      - |     - |     - |         - |
|  LogScalarStructMany | 27.713 ns | 0.4063 ns | 0.3800 ns | 12.59 |    0.40 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | 21.151 ns | 0.3421 ns | 0.3200 ns |  9.61 |    0.38 |      - |     - |     - |         - |
|        LogDictionary |  9.039 ns | 0.2006 ns | 0.2230 ns |  4.11 |    0.17 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  8.974 ns | 0.1662 ns | 0.1555 ns |  4.08 |    0.14 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  9.202 ns | 0.4664 ns | 0.5184 ns |  4.19 |    0.30 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | 12.452 ns | 0.1886 ns | 0.1764 ns |  5.66 |    0.22 |      - |     - |     - |         - |
|              LogMix3 | 15.892 ns | 0.3431 ns | 0.3813 ns |  7.23 |    0.33 |      - |     - |     - |         - |
|              LogMix4 | 24.991 ns | 0.2320 ns | 0.2170 ns | 11.36 |    0.40 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | 30.744 ns | 0.6525 ns | 0.8252 ns | 14.02 |    0.55 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | 57.370 ns | 1.1627 ns | 1.3389 ns | 26.07 |    1.02 | 0.0446 |     - |     - |     280 B |
