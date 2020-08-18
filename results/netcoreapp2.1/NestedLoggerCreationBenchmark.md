``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 2.1.21 (CoreCLR 4.6.29130.01, CoreFX 4.6.29130.02), X64 RyuJIT
  DefaultJob : .NET Core 2.1.21 (CoreCLR 4.6.29130.01, CoreFX 4.6.29130.02), X64 RyuJIT


```
|           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt | 80.45 ns | 1.596 ns | 1.838 ns | 0.0241 |     - |     - |     152 B |
| ForContextString | 49.17 ns | 0.970 ns | 0.953 ns | 0.0203 |     - |     - |     128 B |
|   ForContextType | 88.60 ns | 1.555 ns | 1.455 ns | 0.0203 |     - |     - |     128 B |
