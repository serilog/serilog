``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0


```
|   Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  81.33 ns |  1.693 ns |  4.520 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 187.12 ns |  3.797 ns |  6.448 ns |  2.25 |    0.17 | 0.0267 |     - |     - |      84 B |
| BindFive | 522.65 ns | 10.213 ns | 13.634 ns |  6.21 |    0.47 | 0.0725 |     - |     - |     228 B |
