``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]     : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT


```
|                       Method |       Mean |     Error |     StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------------- |-----------:|----------:|-----------:|-------:|------:|------:|----------:|
|     TemplateWithNoProperties |   5.247 ns | 0.1225 ns |  0.1146 ns |      - |     - |     - |         - |
| TemplateWithVariedProperties | 376.226 ns | 7.4571 ns | 12.0418 ns | 0.0300 |     - |     - |      96 B |
