``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]     : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT


```
|           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt | 92.87 ns | 1.853 ns | 2.410 ns | 0.0483 |     - |     - |     152 B |
| ForContextString | 59.23 ns | 1.451 ns | 1.286 ns | 0.0407 |     - |     - |     128 B |
|   ForContextType | 99.94 ns | 1.966 ns | 2.264 ns | 0.0407 |     - |     - |     128 B |
