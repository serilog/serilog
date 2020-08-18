``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 2.1.21 (CoreCLR 4.6.29130.01, CoreFX 4.6.29130.02), X64 RyuJIT
  DefaultJob : .NET Core 2.1.21 (CoreCLR 4.6.29130.01, CoreFX 4.6.29130.02), X64 RyuJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.580 ns | 0.0762 ns | 0.0964 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.591 ns | 0.0778 ns | 0.1012 ns |  1.01 |    0.04 |      - |     - |     - |         - |
|               LogMsg |  4.515 ns | 0.1064 ns | 0.1182 ns |  1.75 |    0.08 |      - |     - |     - |         - |
|         LogMsgWithEx |  5.739 ns | 0.0752 ns | 0.0667 ns |  2.23 |    0.09 |      - |     - |     - |         - |
|           LogScalar1 |  6.546 ns | 0.1570 ns | 0.1869 ns |  2.54 |    0.11 |      - |     - |     - |         - |
|           LogScalar2 | 11.038 ns | 0.2075 ns | 0.1941 ns |  4.28 |    0.19 |      - |     - |     - |         - |
|           LogScalar3 | 14.490 ns | 0.3152 ns | 0.3987 ns |  5.62 |    0.25 |      - |     - |     - |         - |
|        LogScalarMany | 17.314 ns | 0.3736 ns | 0.4724 ns |  6.72 |    0.29 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  5.073 ns | 0.1204 ns | 0.1126 ns |  1.97 |    0.09 |      - |     - |     - |         - |
|     LogScalarStruct2 |  5.348 ns | 0.0926 ns | 0.0866 ns |  2.07 |    0.09 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.349 ns | 0.1919 ns | 0.1885 ns |  3.23 |    0.11 |      - |     - |     - |         - |
|  LogScalarStructMany | 26.270 ns | 0.4279 ns | 0.4002 ns | 10.19 |    0.43 | 0.0241 |     - |     - |     152 B |
|   LogScalarBigStruct | 20.045 ns | 0.3779 ns | 0.3535 ns |  7.77 |    0.20 |      - |     - |     - |         - |
|        LogDictionary |  9.345 ns | 0.2089 ns | 0.2236 ns |  3.62 |    0.21 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  9.505 ns | 0.2126 ns | 0.1989 ns |  3.69 |    0.15 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  9.664 ns | 0.2013 ns | 0.1883 ns |  3.75 |    0.11 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | 10.832 ns | 0.2292 ns | 0.2144 ns |  4.20 |    0.18 |      - |     - |     - |         - |
|              LogMix3 | 14.081 ns | 0.3040 ns | 0.3619 ns |  5.46 |    0.24 |      - |     - |     - |         - |
|              LogMix4 | 24.425 ns | 0.4260 ns | 0.3985 ns |  9.47 |    0.41 | 0.0216 |     - |     - |     136 B |
|              LogMix5 | 31.214 ns | 0.6328 ns | 0.7287 ns | 12.10 |    0.50 | 0.0266 |     - |     - |     168 B |
|           LogMixMany | 54.137 ns | 0.8298 ns | 0.7762 ns | 20.99 |    0.87 | 0.0445 |     - |     - |     280 B |
