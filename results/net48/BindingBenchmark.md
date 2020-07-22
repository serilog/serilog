``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|   Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  67.96 ns | 1.239 ns | 1.737 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 149.43 ns | 2.907 ns | 3.232 ns |  2.19 |    0.08 | 0.0160 |     - |     - |      84 B |
| BindFive | 423.21 ns | 5.563 ns | 5.204 ns |  6.21 |    0.20 | 0.0434 |     - |     - |     228 B |
