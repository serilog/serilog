``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|                       Method |       Mean |     Error |     StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------------- |-----------:|----------:|-----------:|-------:|------:|------:|----------:|
|     TemplateWithNoProperties |   3.594 ns | 0.1003 ns |  0.1591 ns |      - |     - |     - |         - |
| TemplateWithVariedProperties | 311.191 ns | 6.2154 ns | 10.0368 ns | 0.0153 |     - |     - |      96 B |
