``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  DefaultJob : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT


```
|           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt | 79.29 ns | 1.560 ns | 1.734 ns | 0.0241 |     - |     - |     152 B |
| ForContextString | 48.49 ns | 0.890 ns | 0.832 ns | 0.0203 |     - |     - |     128 B |
|   ForContextType | 88.45 ns | 1.445 ns | 1.352 ns | 0.0203 |     - |     - |     128 B |