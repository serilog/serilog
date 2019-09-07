``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0


```
|                       Method |       Mean |     Error |     StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------------- |-----------:|----------:|-----------:|-------:|------:|------:|----------:|
|     TemplateWithNoProperties |   3.823 ns | 0.1091 ns |  0.1632 ns |      - |     - |     - |         - |
| TemplateWithVariedProperties | 460.420 ns | 9.2324 ns | 20.6497 ns | 0.0191 |     - |     - |      60 B |
