``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt | 83.06 ns | 1.643 ns | 2.460 ns | 0.0242 |     - |     - |     152 B |
| ForContextString | 50.39 ns | 0.953 ns | 1.455 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType | 88.21 ns | 2.257 ns | 2.111 ns | 0.0204 |     - |     - |     128 B |
