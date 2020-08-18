``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT


```
|   Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  67.75 ns | 1.200 ns | 1.064 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 148.97 ns | 3.013 ns | 3.700 ns |  2.20 |    0.06 | 0.0160 |     - |     - |      84 B |
| BindFive | 414.95 ns | 6.048 ns | 5.657 ns |  6.12 |    0.14 | 0.0434 |     - |     - |     228 B |
