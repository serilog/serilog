``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|               Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.277 ns | 0.0725 ns | 0.1251 ns |  2.219 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.169 ns | 0.0393 ns | 0.0367 ns |  2.178 ns |  0.93 |    0.05 |      - |     - |     - |         - |
|               LogMsg |  3.606 ns | 0.0966 ns | 0.0904 ns |  3.631 ns |  1.54 |    0.09 |      - |     - |     - |         - |
|         LogMsgWithEx |  3.309 ns | 0.0932 ns | 0.0997 ns |  3.318 ns |  1.41 |    0.08 |      - |     - |     - |         - |
|           LogScalar1 |  6.407 ns | 0.1526 ns | 0.1757 ns |  6.353 ns |  2.73 |    0.19 |      - |     - |     - |         - |
|           LogScalar2 | 11.092 ns | 0.2166 ns | 0.2026 ns | 11.143 ns |  4.73 |    0.28 |      - |     - |     - |         - |
|           LogScalar3 | 15.098 ns | 0.3522 ns | 0.3915 ns | 15.023 ns |  6.41 |    0.43 |      - |     - |     - |         - |
|        LogScalarMany | 17.964 ns | 0.3149 ns | 0.2946 ns | 18.015 ns |  7.66 |    0.42 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  5.132 ns | 0.0555 ns | 0.0519 ns |  5.132 ns |  2.19 |    0.13 |      - |     - |     - |         - |
|     LogScalarStruct2 |  7.507 ns | 0.0286 ns | 0.0254 ns |  7.505 ns |  3.21 |    0.18 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.124 ns | 0.1867 ns | 0.1917 ns |  8.037 ns |  3.45 |    0.20 |      - |     - |     - |         - |
|  LogScalarStructMany | 26.878 ns | 0.5363 ns | 0.5016 ns | 26.971 ns | 11.47 |    0.65 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | 20.418 ns | 0.4338 ns | 0.4642 ns | 20.475 ns |  8.67 |    0.46 |      - |     - |     - |         - |
|        LogDictionary |  8.835 ns | 0.1999 ns | 0.2302 ns |  8.831 ns |  3.76 |    0.21 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  8.935 ns | 0.1939 ns | 0.1991 ns |  8.946 ns |  3.80 |    0.20 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  8.942 ns | 0.2046 ns | 0.2101 ns |  8.978 ns |  3.80 |    0.23 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | 11.696 ns | 0.2820 ns | 0.3765 ns | 11.635 ns |  5.05 |    0.34 |      - |     - |     - |         - |
|              LogMix3 | 14.420 ns | 0.3166 ns | 0.5946 ns | 14.348 ns |  6.37 |    0.37 |      - |     - |     - |         - |
|              LogMix4 | 26.686 ns | 0.5599 ns | 0.4963 ns | 26.716 ns | 11.43 |    0.66 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | 30.614 ns | 0.6138 ns | 0.7762 ns | 30.514 ns | 13.14 |    0.88 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | 56.735 ns | 0.6613 ns | 0.6185 ns | 56.846 ns | 24.21 |    1.44 | 0.0446 |     - |     - |     280 B |
