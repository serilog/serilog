``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.648 ns | 0.0801 ns | 0.1013 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.645 ns | 0.0796 ns | 0.0781 ns |  1.00 |    0.03 |      - |     - |     - |         - |
|               LogMsg |  3.800 ns | 0.1003 ns | 0.0985 ns |  1.44 |    0.07 |      - |     - |     - |         - |
|         LogMsgWithEx |  4.303 ns | 0.1004 ns | 0.0939 ns |  1.63 |    0.06 |      - |     - |     - |         - |
|           LogScalar1 |  6.405 ns | 0.1543 ns | 0.1585 ns |  2.42 |    0.14 |      - |     - |     - |         - |
|           LogScalar2 | 11.005 ns | 0.2462 ns | 0.1922 ns |  4.16 |    0.17 |      - |     - |     - |         - |
|           LogScalar3 | 15.670 ns | 0.3353 ns | 0.5019 ns |  5.89 |    0.28 |      - |     - |     - |         - |
|        LogScalarMany | 18.544 ns | 0.3836 ns | 0.3939 ns |  7.01 |    0.32 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  5.080 ns | 0.0943 ns | 0.0882 ns |  1.93 |    0.08 |      - |     - |     - |         - |
|     LogScalarStruct2 |  5.689 ns | 0.0943 ns | 0.0882 ns |  2.16 |    0.07 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.238 ns | 0.1897 ns | 0.1949 ns |  3.11 |    0.12 |      - |     - |     - |         - |
|  LogScalarStructMany | 27.292 ns | 0.3809 ns | 0.3563 ns | 10.34 |    0.37 | 0.0242 |     - |     - |     152 B |
|   LogScalarBigStruct | 20.364 ns | 0.4092 ns | 0.4712 ns |  7.71 |    0.37 |      - |     - |     - |         - |
|        LogDictionary |  8.943 ns | 0.1968 ns | 0.2021 ns |  3.38 |    0.09 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  8.929 ns | 0.1965 ns | 0.2018 ns |  3.37 |    0.11 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  8.993 ns | 0.1998 ns | 0.1869 ns |  3.41 |    0.19 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | 11.599 ns | 0.2159 ns | 0.2019 ns |  4.39 |    0.15 |      - |     - |     - |         - |
|              LogMix3 | 14.176 ns | 0.3041 ns | 0.4361 ns |  5.37 |    0.31 |      - |     - |     - |         - |
|              LogMix4 | 24.985 ns | 0.3951 ns | 0.3696 ns |  9.47 |    0.36 | 0.0217 |     - |     - |     136 B |
|              LogMix5 | 30.352 ns | 0.6301 ns | 0.8624 ns | 11.48 |    0.66 | 0.0268 |     - |     - |     168 B |
|           LogMixMany | 56.561 ns | 0.6189 ns | 0.5789 ns | 21.43 |    0.80 | 0.0446 |     - |     - |     280 B |
