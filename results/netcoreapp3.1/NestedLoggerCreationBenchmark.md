``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt | 79.44 ns | 1.563 ns | 1.672 ns | 0.0242 |     - |     - |     152 B |
| ForContextString | 49.43 ns | 0.957 ns | 0.896 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType | 85.80 ns | 1.712 ns | 1.903 ns | 0.0204 |     - |     - |     128 B |
