``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  DefaultJob : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.636 ns | 0.0800 ns | 0.1291 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.612 ns | 0.0791 ns | 0.0879 ns |  0.98 |    0.06 |      - |     - |     - |         - |
|               LogMsg |  4.485 ns | 0.1101 ns | 0.1030 ns |  1.67 |    0.08 |      - |     - |     - |         - |
|         LogMsgWithEx |  5.826 ns | 0.0812 ns | 0.0760 ns |  2.16 |    0.11 |      - |     - |     - |         - |
|           LogScalar1 |  8.396 ns | 0.1852 ns | 0.1982 ns |  3.14 |    0.18 |      - |     - |     - |         - |
|           LogScalar2 | 10.942 ns | 0.2175 ns | 0.2034 ns |  4.06 |    0.21 |      - |     - |     - |         - |
|           LogScalar3 | 14.321 ns | 0.3035 ns | 0.3727 ns |  5.38 |    0.33 |      - |     - |     - |         - |
|        LogScalarMany | 17.621 ns | 0.3764 ns | 0.3520 ns |  6.55 |    0.36 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  5.042 ns | 0.0933 ns | 0.0873 ns |  1.87 |    0.09 |      - |     - |     - |         - |
|     LogScalarStruct2 |  5.355 ns | 0.1323 ns | 0.1811 ns |  2.02 |    0.11 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.376 ns | 0.1785 ns | 0.1669 ns |  3.11 |    0.18 |      - |     - |     - |         - |
|  LogScalarStructMany | 26.449 ns | 0.4319 ns | 0.4040 ns |  9.82 |    0.47 | 0.0241 |     - |     - |     152 B |
|   LogScalarBigStruct | 20.117 ns | 0.4137 ns | 0.3870 ns |  7.47 |    0.40 |      - |     - |     - |         - |
|        LogDictionary |  9.493 ns | 0.2000 ns | 0.1871 ns |  3.53 |    0.20 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  9.362 ns | 0.1950 ns | 0.1824 ns |  3.48 |    0.15 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  9.273 ns | 0.2018 ns | 0.1888 ns |  3.45 |    0.20 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | 10.952 ns | 0.1681 ns | 0.1573 ns |  4.07 |    0.23 |      - |     - |     - |         - |
|              LogMix3 | 14.199 ns | 0.2987 ns | 0.3556 ns |  5.33 |    0.34 |      - |     - |     - |         - |
|              LogMix4 | 24.791 ns | 0.3870 ns | 0.3431 ns |  9.18 |    0.44 | 0.0216 |     - |     - |     136 B |
|              LogMix5 | 31.319 ns | 0.6467 ns | 0.8852 ns | 11.82 |    0.58 | 0.0266 |     - |     - |     168 B |
|           LogMixMany | 55.547 ns | 0.6676 ns | 0.6245 ns | 20.64 |    1.06 | 0.0445 |     - |     - |     280 B |
