``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.101
  [Host] : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
  core31 : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48  : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net50  : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT

Jit=RyuJit  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
|           Method |    Job |       Runtime |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |------- |-------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt | core31 | .NET Core 3.1 | 78.60 ns | 1.425 ns | 2.133 ns | 0.0242 |     - |     - |     152 B |
| ForContextString | core31 | .NET Core 3.1 | 49.99 ns | 0.487 ns | 0.729 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType | core31 | .NET Core 3.1 | 85.32 ns | 1.148 ns | 1.718 ns | 0.0204 |     - |     - |     128 B |
|    ForContextInt |  net48 |      .NET 4.8 | 76.28 ns | 0.942 ns | 1.381 ns | 0.0242 |     - |     - |     152 B |
| ForContextString |  net48 |      .NET 4.8 | 47.93 ns | 0.517 ns | 0.774 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType |  net48 |      .NET 4.8 | 93.28 ns | 1.186 ns | 1.776 ns | 0.0204 |     - |     - |     128 B |
|    ForContextInt |  net50 | .NET Core 5.0 | 61.68 ns | 0.967 ns | 1.448 ns | 0.0242 |     - |     - |     152 B |
| ForContextString |  net50 | .NET Core 5.0 | 37.07 ns | 0.511 ns | 0.765 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType |  net50 | .NET Core 5.0 | 70.28 ns | 1.180 ns | 1.766 ns | 0.0204 |     - |     - |     128 B |
