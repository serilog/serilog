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
|    ForContextInt | core31 | .NET Core 3.1 | 85.59 ns | 0.682 ns | 1.021 ns | 0.0242 |     - |     - |     152 B |
| ForContextString | core31 | .NET Core 3.1 | 52.72 ns | 0.341 ns | 0.499 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType | core31 | .NET Core 3.1 | 88.10 ns | 0.736 ns | 1.079 ns | 0.0204 |     - |     - |     128 B |
|    ForContextInt |  net48 |      .NET 4.8 | 82.02 ns | 0.710 ns | 1.063 ns | 0.0242 |     - |     - |     152 B |
| ForContextString |  net48 |      .NET 4.8 | 52.44 ns | 0.302 ns | 0.434 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType |  net48 |      .NET 4.8 | 99.69 ns | 0.587 ns | 0.879 ns | 0.0204 |     - |     - |     128 B |
|    ForContextInt |  net50 | .NET Core 5.0 | 63.39 ns | 1.178 ns | 1.764 ns | 0.0242 |     - |     - |     152 B |
| ForContextString |  net50 | .NET Core 5.0 | 38.52 ns | 0.373 ns | 0.558 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType |  net50 | .NET Core 5.0 | 73.30 ns | 0.974 ns | 1.458 ns | 0.0204 |     - |     - |     128 B |
