``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|   Method |      Mean |    Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|----------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  70.44 ns | 0.931 ns |  0.871 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 156.90 ns | 3.129 ns |  3.073 ns |  2.23 |    0.05 | 0.0160 |     - |     - |      84 B |
| BindFive | 447.40 ns | 9.010 ns | 10.725 ns |  6.31 |    0.14 | 0.0429 |     - |     - |     228 B |
