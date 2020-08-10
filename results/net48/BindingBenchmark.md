``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|   Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  67.80 ns | 1.370 ns | 1.631 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 163.11 ns | 3.189 ns | 3.916 ns |  2.41 |    0.07 | 0.0160 |     - |     - |      84 B |
| BindFive | 448.21 ns | 4.678 ns | 4.375 ns |  6.61 |    0.21 | 0.0434 |     - |     - |     228 B |
