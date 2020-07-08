``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|                       Method |       Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------------- |-----------:|----------:|----------:|-------:|------:|------:|----------:|
|     TemplateWithNoProperties |   3.651 ns | 0.0481 ns | 0.0450 ns |      - |     - |     - |         - |
| TemplateWithVariedProperties | 407.000 ns | 4.8752 ns | 4.5603 ns | 0.0114 |     - |     - |      60 B |
