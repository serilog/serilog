``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|                       Method |       Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------------- |-----------:|----------:|----------:|-------:|------:|------:|----------:|
|     TemplateWithNoProperties |   3.565 ns | 0.0357 ns | 0.0279 ns |      - |     - |     - |         - |
| TemplateWithVariedProperties | 413.991 ns | 4.0758 ns | 3.6131 ns | 0.0114 |     - |     - |      60 B |
