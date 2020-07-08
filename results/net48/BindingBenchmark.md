``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|   Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  80.91 ns | 1.007 ns | 0.942 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 162.68 ns | 3.316 ns | 3.101 ns |  2.01 |    0.04 | 0.0160 |     - |     - |      84 B |
| BindFive | 461.19 ns | 6.956 ns | 6.507 ns |  5.70 |    0.07 | 0.0434 |     - |     - |     228 B |
