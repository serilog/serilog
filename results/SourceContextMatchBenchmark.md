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
|                             Method |    Job |       Runtime |       Mean |    Error |    StdDev |
|----------------------------------- |------- |-------------- |-----------:|---------:|----------:|
|          Filter_MatchingFromSource | core22 | .NET Core 2.2 | 5,764.7 ns | 65.51 ns |  98.05 ns |
|                  Logger_ForContext | core22 | .NET Core 2.2 | 3,207.4 ns | 23.85 ns |  35.69 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 | .NET Core 2.2 | 2,671.0 ns | 24.67 ns |  36.93 ns |
|          Filter_MatchingFromSource | core31 | .NET Core 3.1 | 2,539.7 ns | 57.97 ns |  84.98 ns |
|                  Logger_ForContext | core31 | .NET Core 3.1 |   624.9 ns |  5.39 ns |   8.06 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 | .NET Core 3.1 |   125.4 ns |  1.28 ns |   1.91 ns |
|          Filter_MatchingFromSource |  net48 |      .NET 4.8 | 6,519.9 ns | 88.46 ns | 132.40 ns |
|                  Logger_ForContext |  net48 |      .NET 4.8 | 3,147.2 ns | 23.02 ns |  34.46 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 |      .NET 4.8 | 2,633.8 ns | 22.20 ns |  33.22 ns |
|          Filter_MatchingFromSource |  net50 | .NET Core 5.0 | 2,327.8 ns | 60.44 ns |  90.46 ns |
|                  Logger_ForContext |  net50 | .NET Core 5.0 |   488.2 ns |  4.78 ns |   7.15 ns |
| LevelOverrideMap_GetEffectiveLevel |  net50 | .NET Core 5.0 |   129.7 ns |  1.14 ns |   1.70 ns |
