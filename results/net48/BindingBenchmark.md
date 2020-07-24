``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|   Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  69.61 ns | 0.723 ns | 0.676 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 161.88 ns | 3.260 ns | 4.462 ns |  2.31 |    0.07 | 0.0160 |     - |     - |      84 B |
| BindFive | 457.57 ns | 9.015 ns | 7.992 ns |  6.57 |    0.12 | 0.0434 |     - |     - |     228 B |
