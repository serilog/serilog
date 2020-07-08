``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|   Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  71.63 ns | 1.360 ns | 1.205 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 166.35 ns | 2.968 ns | 2.478 ns |  2.33 |    0.05 | 0.0160 |     - |     - |      84 B |
| BindFive | 463.34 ns | 4.860 ns | 4.309 ns |  6.47 |    0.09 | 0.0434 |     - |     - |     228 B |
