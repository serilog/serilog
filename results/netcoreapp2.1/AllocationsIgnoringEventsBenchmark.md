``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  DefaultJob : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             LogEmpty |  2.856 ns | 0.0617 ns | 0.0577 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| LogEmptyWithEnricher |  2.855 ns | 0.0655 ns | 0.0612 ns |  1.00 |    0.03 |      - |     - |     - |         - |
|               LogMsg |  5.704 ns | 0.0763 ns | 0.0714 ns |  2.00 |    0.05 |      - |     - |     - |         - |
|         LogMsgWithEx |  6.583 ns | 0.1569 ns | 0.1541 ns |  2.31 |    0.08 |      - |     - |     - |         - |
|           LogScalar1 |  5.756 ns | 0.0591 ns | 0.0553 ns |  2.02 |    0.05 |      - |     - |     - |         - |
|           LogScalar2 | 11.900 ns | 0.1661 ns | 0.1554 ns |  4.17 |    0.10 |      - |     - |     - |         - |
|           LogScalar3 | 14.698 ns | 0.2592 ns | 0.2425 ns |  5.15 |    0.10 |      - |     - |     - |         - |
|        LogScalarMany | 18.226 ns | 0.3275 ns | 0.3063 ns |  6.38 |    0.09 | 0.0089 |     - |     - |      56 B |
|     LogScalarStruct1 |  5.971 ns | 0.0541 ns | 0.0480 ns |  2.09 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct2 |  5.429 ns | 0.0612 ns | 0.0572 ns |  1.90 |    0.04 |      - |     - |     - |         - |
|     LogScalarStruct3 |  8.537 ns | 0.1672 ns | 0.1564 ns |  2.99 |    0.07 |      - |     - |     - |         - |
|  LogScalarStructMany | 26.387 ns | 0.2349 ns | 0.2197 ns |  9.24 |    0.21 | 0.0241 |     - |     - |     152 B |
|   LogScalarBigStruct | 20.382 ns | 0.3134 ns | 0.2932 ns |  7.14 |    0.17 |      - |     - |     - |         - |
|        LogDictionary |  8.953 ns | 0.1952 ns | 0.1826 ns |  3.14 |    0.10 | 0.0051 |     - |     - |      32 B |
|          LogSequence |  8.919 ns | 0.1727 ns | 0.1615 ns |  3.12 |    0.06 | 0.0051 |     - |     - |      32 B |
|         LogAnonymous |  8.768 ns | 0.1769 ns | 0.1655 ns |  3.07 |    0.07 | 0.0051 |     - |     - |      32 B |
|              LogMix2 | 11.004 ns | 0.1468 ns | 0.1373 ns |  3.85 |    0.10 |      - |     - |     - |         - |
|              LogMix3 | 14.448 ns | 0.3059 ns | 0.3142 ns |  5.06 |    0.15 |      - |     - |     - |         - |
|              LogMix4 | 24.173 ns | 0.2811 ns | 0.2629 ns |  8.47 |    0.21 | 0.0216 |     - |     - |     136 B |
|              LogMix5 | 29.119 ns | 0.5850 ns | 0.6007 ns | 10.22 |    0.29 | 0.0266 |     - |     - |     168 B |
|           LogMixMany | 56.461 ns | 1.1254 ns | 1.1557 ns | 19.77 |    0.62 | 0.0445 |     - |     - |     280 B |
