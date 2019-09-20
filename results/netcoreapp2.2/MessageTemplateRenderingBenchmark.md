``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]     : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT


```
|                       Method |       Mean |     Error |    StdDev |     Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------------- |-----------:|----------:|----------:|-----------:|-------:|------:|------:|----------:|
|     TemplateWithNoProperties |   5.404 ns | 0.1384 ns | 0.2195 ns |   5.297 ns |      - |     - |     - |         - |
| TemplateWithVariedProperties | 357.210 ns | 2.1927 ns | 2.0511 ns | 357.238 ns | 0.0300 |     - |     - |      96 B |
