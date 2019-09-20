``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0


```
|         Method |     Mean |     Error |    StdDev |   Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |---------:|----------:|----------:|---------:|-------:|------:|------:|----------:|
| FormatToOutput | 1.571 us | 0.0329 us | 0.0532 us | 1.544 us | 0.1450 |     - |     - |     457 B |
