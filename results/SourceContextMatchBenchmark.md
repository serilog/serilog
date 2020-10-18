``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.403
  [Host]        : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  core22 RyuJit : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  core31 RyuJit : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  net48 RyuJit  : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

Jit=RyuJit  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
|                             Method |           Job |       Runtime |       Mean |    Error |   StdDev |
|----------------------------------- |-------------- |-------------- |-----------:|---------:|---------:|
|          Filter_MatchingFromSource | core22 RyuJit | .NET Core 2.2 | 2,715.3 ns | 32.58 ns | 48.76 ns |
|                  Logger_ForContext | core22 RyuJit | .NET Core 2.2 |   604.8 ns |  9.20 ns | 13.19 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 RyuJit | .NET Core 2.2 |   145.1 ns |  0.58 ns |  0.85 ns |
|          Filter_MatchingFromSource | core31 RyuJit | .NET Core 3.1 | 2,397.0 ns | 27.84 ns | 41.67 ns |
|                  Logger_ForContext | core31 RyuJit | .NET Core 3.1 |   608.6 ns |  4.12 ns |  6.17 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 RyuJit | .NET Core 3.1 |   126.2 ns |  0.74 ns |  1.07 ns |
|          Filter_MatchingFromSource |  net48 RyuJit |      .NET 4.8 | 3,292.4 ns | 27.79 ns | 41.59 ns |
|                  Logger_ForContext |  net48 RyuJit |      .NET 4.8 |   720.9 ns |  2.71 ns |  3.97 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 RyuJit |      .NET 4.8 |   248.6 ns |  0.99 ns |  1.45 ns |
