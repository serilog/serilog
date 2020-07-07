``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.2.402
  [Host]     : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT


```
|           Method |     Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|----------:|----------:|-------:|------:|------:|----------:|
|    ForContextInt | 76.45 ns | 0.4343 ns | 0.3850 ns | 0.0241 |     - |     - |     152 B |
| ForContextString | 47.55 ns | 0.2171 ns | 0.2031 ns | 0.0203 |     - |     - |     128 B |
|   ForContextType | 83.85 ns | 0.5369 ns | 0.5022 ns | 0.0203 |     - |     - |     128 B |
