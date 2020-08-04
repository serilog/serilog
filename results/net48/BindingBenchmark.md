``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|   Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  67.11 ns | 1.394 ns | 1.431 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 150.32 ns | 2.856 ns | 2.805 ns |  2.24 |    0.08 | 0.0160 |     - |     - |      84 B |
| BindFive | 418.30 ns | 6.093 ns | 5.700 ns |  6.23 |    0.16 | 0.0434 |     - |     - |     228 B |
