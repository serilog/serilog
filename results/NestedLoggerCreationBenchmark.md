``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|           Method |             Job |       Jit |       Runtime |     Mean |    Error |   StdDev |   Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------------- |---------- |-------------- |---------:|---------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 75.95 ns | 0.253 ns | 0.363 ns | 76.00 ns | 0.0242 |     - |     - |     152 B |
| ForContextString |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 48.28 ns | 0.167 ns | 0.239 ns | 48.27 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 82.11 ns | 0.692 ns | 1.014 ns | 82.16 ns | 0.0204 |     - |     - |     128 B |
|    ForContextInt | net48 LegacyJit | LegacyJit |      .NET 4.8 | 76.67 ns | 1.339 ns | 1.963 ns | 77.89 ns | 0.0242 |     - |     - |     152 B |
| ForContextString | net48 LegacyJit | LegacyJit |      .NET 4.8 | 47.72 ns | 0.162 ns | 0.242 ns | 47.71 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType | net48 LegacyJit | LegacyJit |      .NET 4.8 | 94.21 ns | 0.742 ns | 1.087 ns | 94.07 ns | 0.0204 |     - |     - |     128 B |
|    ForContextInt |    net48 RyuJit |    RyuJit |      .NET 4.8 | 74.79 ns | 0.230 ns | 0.337 ns | 74.75 ns | 0.0242 |     - |     - |     152 B |
| ForContextString |    net48 RyuJit |    RyuJit |      .NET 4.8 | 47.58 ns | 0.213 ns | 0.312 ns | 47.53 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType |    net48 RyuJit |    RyuJit |      .NET 4.8 | 92.22 ns | 0.636 ns | 0.912 ns | 92.11 ns | 0.0204 |     - |     - |     128 B |
