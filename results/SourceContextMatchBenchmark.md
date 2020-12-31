``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.101
  [Host] : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
  core22 : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  core31 : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48  : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net50  : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT

Jit=RyuJit  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
|                             Method |    Job |       Runtime |       Mean |     Error |    StdDev |
|----------------------------------- |------- |-------------- |-----------:|----------:|----------:|
|          Filter_MatchingFromSource | core22 | .NET Core 2.2 | 5,800.3 ns |  87.65 ns | 131.18 ns |
|                  Logger_ForContext | core22 | .NET Core 2.2 | 3,230.9 ns |  34.98 ns |  52.35 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 | .NET Core 2.2 | 2,686.7 ns |  35.40 ns |  52.98 ns |
|          Filter_MatchingFromSource | core31 | .NET Core 3.1 | 2,415.0 ns |  69.43 ns | 103.92 ns |
|                  Logger_ForContext | core31 | .NET Core 3.1 |   621.9 ns |   9.80 ns |  14.67 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 | .NET Core 3.1 |   126.6 ns |   1.61 ns |   2.36 ns |
|          Filter_MatchingFromSource |  net48 |      .NET 4.8 | 6,335.0 ns | 107.19 ns | 160.43 ns |
|                  Logger_ForContext |  net48 |      .NET 4.8 | 3,200.1 ns |  41.78 ns |  58.57 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 |      .NET 4.8 | 2,678.5 ns |  32.58 ns |  48.76 ns |
|          Filter_MatchingFromSource |  net50 | .NET Core 5.0 | 2,273.5 ns |  54.79 ns |  82.01 ns |
|                  Logger_ForContext |  net50 | .NET Core 5.0 |   494.0 ns |   8.09 ns |  12.11 ns |
| LevelOverrideMap_GetEffectiveLevel |  net50 | .NET Core 5.0 |   128.8 ns |   2.01 ns |   3.01 ns |
