``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]     : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT


```
|           Method |     Mean |     Error |    StdDev |   Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|----------:|----------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt | 89.81 ns | 0.6061 ns | 0.5669 ns | 89.94 ns | 0.0483 |     - |     - |     152 B |
| ForContextString | 57.25 ns | 1.0671 ns | 1.6296 ns | 56.31 ns | 0.0407 |     - |     - |     128 B |
|   ForContextType | 98.73 ns | 1.3182 ns | 1.1007 ns | 98.67 ns | 0.0407 |     - |     - |     128 B |
