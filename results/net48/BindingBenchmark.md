``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT


```
|   Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  67.64 ns | 1.365 ns | 1.518 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 158.15 ns | 3.223 ns | 3.015 ns |  2.34 |    0.05 | 0.0160 |     - |     - |      84 B |
| BindFive | 446.28 ns | 5.278 ns | 4.937 ns |  6.60 |    0.16 | 0.0434 |     - |     - |     228 B |
