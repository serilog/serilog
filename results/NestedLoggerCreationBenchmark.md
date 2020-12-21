``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.404
  [Host]          : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|           Method |             Job |       Jit |       Runtime |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------------- |---------- |-------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 80.05 ns | 0.863 ns | 1.291 ns | 0.0242 |     - |     - |     152 B |
| ForContextString |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 51.36 ns | 0.434 ns | 0.636 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 87.54 ns | 0.876 ns | 1.311 ns | 0.0204 |     - |     - |     128 B |
|    ForContextInt | net48 LegacyJit | LegacyJit |      .NET 4.8 | 80.27 ns | 0.909 ns | 1.360 ns | 0.0242 |     - |     - |     152 B |
| ForContextString | net48 LegacyJit | LegacyJit |      .NET 4.8 | 50.12 ns | 0.421 ns | 0.631 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType | net48 LegacyJit | LegacyJit |      .NET 4.8 | 97.44 ns | 0.834 ns | 1.248 ns | 0.0204 |     - |     - |     128 B |
|    ForContextInt |    net48 RyuJit |    RyuJit |      .NET 4.8 | 79.56 ns | 0.843 ns | 1.262 ns | 0.0242 |     - |     - |     152 B |
| ForContextString |    net48 RyuJit |    RyuJit |      .NET 4.8 | 49.75 ns | 0.351 ns | 0.525 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType |    net48 RyuJit |    RyuJit |      .NET 4.8 | 97.67 ns | 1.004 ns | 1.503 ns | 0.0204 |     - |     - |     128 B |
