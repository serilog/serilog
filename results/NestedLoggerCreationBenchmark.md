``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|           Method |             Job |       Jit |       Runtime |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------------- |---------- |-------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 76.29 ns | 0.277 ns | 0.415 ns | 0.0242 |     - |     - |     152 B |
| ForContextString |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 48.51 ns | 0.132 ns | 0.193 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 84.29 ns | 0.346 ns | 0.508 ns | 0.0204 |     - |     - |     128 B |
|    ForContextInt | net48 LegacyJit | LegacyJit |      .NET 4.8 | 74.48 ns | 0.175 ns | 0.250 ns | 0.0242 |     - |     - |     152 B |
| ForContextString | net48 LegacyJit | LegacyJit |      .NET 4.8 | 47.97 ns | 0.108 ns | 0.158 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType | net48 LegacyJit | LegacyJit |      .NET 4.8 | 93.35 ns | 0.438 ns | 0.656 ns | 0.0204 |     - |     - |     128 B |
|    ForContextInt |    net48 RyuJit |    RyuJit |      .NET 4.8 | 74.14 ns | 0.155 ns | 0.232 ns | 0.0242 |     - |     - |     152 B |
| ForContextString |    net48 RyuJit |    RyuJit |      .NET 4.8 | 47.75 ns | 0.100 ns | 0.147 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType |    net48 RyuJit |    RyuJit |      .NET 4.8 | 91.75 ns | 0.223 ns | 0.327 ns | 0.0204 |     - |     - |     128 B |
