``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0


```
|                       Method |       Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|                EmptyTemplate |   169.8 ns |  3.425 ns |  6.516 ns |  1.00 |    0.00 | 0.0482 |     - |     - |     152 B |
| DefaultConsoleOutputTemplate | 2,325.1 ns | 45.668 ns | 79.983 ns | 13.67 |    0.80 | 0.4692 |     - |     - |    1478 B |
