``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt | 71.57 ns | 1.437 ns | 1.597 ns | 0.0242 |     - |     - |     152 B |
| ForContextString | 42.57 ns | 0.821 ns | 0.768 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType | 80.51 ns | 1.577 ns | 1.475 ns | 0.0204 |     - |     - |     128 B |
