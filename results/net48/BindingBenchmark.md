``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|   Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  66.91 ns | 1.138 ns | 1.064 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 148.79 ns | 3.025 ns | 2.971 ns |  2.23 |    0.07 | 0.0160 |     - |     - |      84 B |
| BindFive | 422.06 ns | 6.255 ns | 5.851 ns |  6.31 |    0.14 | 0.0434 |     - |     - |     228 B |
