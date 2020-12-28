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
|    ForContextInt | core31 | .NET Core 3.1 | 79.91 ns | 0.867 ns | 1.271 ns | 0.0242 |     - |     - |     152 B |
| ForContextString | core31 | .NET Core 3.1 | 50.57 ns | 0.409 ns | 0.613 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType | core31 | .NET Core 3.1 | 86.63 ns | 1.007 ns | 1.445 ns | 0.0204 |     - |     - |     128 B |
|    ForContextInt |  net48 |      .NET 4.8 | 77.78 ns | 0.809 ns | 1.211 ns | 0.0242 |     - |     - |     152 B |
| ForContextString |  net48 |      .NET 4.8 | 49.42 ns | 0.370 ns | 0.554 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType |  net48 |      .NET 4.8 | 96.07 ns | 0.828 ns | 1.239 ns | 0.0204 |     - |     - |     128 B |
|    ForContextInt |  net50 | .NET Core 5.0 | 59.58 ns | 0.937 ns | 1.374 ns | 0.0242 |     - |     - |     152 B |
| ForContextString |  net50 | .NET Core 5.0 | 37.94 ns | 0.455 ns | 0.680 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType |  net50 | .NET Core 5.0 | 72.07 ns | 0.412 ns | 0.617 ns | 0.0204 |     - |     - |     128 B |
