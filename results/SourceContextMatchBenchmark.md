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
|          Filter_MatchingFromSource | core22 | .NET Core 2.2 | 6,049.1 ns | 77.42 ns | 111.03 ns |
|                  Logger_ForContext | core22 | .NET Core 2.2 | 3,247.9 ns | 25.49 ns |  37.37 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 | .NET Core 2.2 | 2,650.3 ns | 25.27 ns |  37.82 ns |
|          Filter_MatchingFromSource | core31 | .NET Core 3.1 | 2,579.1 ns | 52.18 ns |  78.11 ns |
|                  Logger_ForContext | core31 | .NET Core 3.1 |   680.8 ns |  6.78 ns |  10.15 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 | .NET Core 3.1 |   129.1 ns |  2.83 ns |   4.24 ns |
|          Filter_MatchingFromSource |  net48 |      .NET 4.8 | 6,516.8 ns | 66.77 ns |  97.88 ns |
|                  Logger_ForContext |  net48 |      .NET 4.8 | 3,228.3 ns | 18.53 ns |  27.73 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 |      .NET 4.8 | 2,676.8 ns | 18.21 ns |  27.25 ns |
|          Filter_MatchingFromSource |  net50 | .NET Core 5.0 | 2,406.7 ns | 53.78 ns |  78.84 ns |
|                  Logger_ForContext |  net50 | .NET Core 5.0 |   503.0 ns |  5.65 ns |   8.28 ns |
| LevelOverrideMap_GetEffectiveLevel |  net50 | .NET Core 5.0 |   124.8 ns |  1.29 ns |   1.93 ns |
