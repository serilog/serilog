``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
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
|    ForContextInt |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 75.89 ns | 0.250 ns | 0.375 ns | 75.94 ns | 0.0242 |     - |     - |     152 B |
| ForContextString |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 48.35 ns | 0.494 ns | 0.692 ns | 48.16 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 86.16 ns | 1.727 ns | 2.421 ns | 87.69 ns | 0.0204 |     - |     - |     128 B |
|    ForContextInt | net48 LegacyJit | LegacyJit |      .NET 4.8 | 74.53 ns | 0.333 ns | 0.498 ns | 74.57 ns | 0.0242 |     - |     - |     152 B |
| ForContextString | net48 LegacyJit | LegacyJit |      .NET 4.8 | 47.04 ns | 0.229 ns | 0.343 ns | 47.00 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType | net48 LegacyJit | LegacyJit |      .NET 4.8 | 93.56 ns | 1.068 ns | 1.531 ns | 93.67 ns | 0.0204 |     - |     - |     128 B |
|    ForContextInt |    net48 RyuJit |    RyuJit |      .NET 4.8 | 74.73 ns | 0.395 ns | 0.566 ns | 74.70 ns | 0.0242 |     - |     - |     152 B |
| ForContextString |    net48 RyuJit |    RyuJit |      .NET 4.8 | 46.63 ns | 0.176 ns | 0.264 ns | 46.62 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType |    net48 RyuJit |    RyuJit |      .NET 4.8 | 97.16 ns | 1.593 ns | 2.385 ns | 97.35 ns | 0.0204 |     - |     - |     128 B |
