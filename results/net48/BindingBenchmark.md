``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0


```
|   Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  78.04 ns | 0.3653 ns | 0.3417 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 179.85 ns | 3.6083 ns | 5.1749 ns |  2.32 |    0.08 | 0.0267 |     - |     - |      84 B |
| BindFive | 500.96 ns | 4.6956 ns | 3.6661 ns |  6.42 |    0.05 | 0.0725 |     - |     - |     228 B |
