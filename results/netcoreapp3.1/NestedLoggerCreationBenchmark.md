``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt | 85.05 ns | 1.507 ns | 1.410 ns | 0.0242 |     - |     - |     152 B |
| ForContextString | 53.98 ns | 0.861 ns | 0.806 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType | 88.31 ns | 1.419 ns | 1.327 ns | 0.0204 |     - |     - |     128 B |
