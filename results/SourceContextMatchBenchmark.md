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
|                             Method |    Job |       Runtime |       Mean |    Error |   StdDev |
|----------------------------------- |------- |-------------- |-----------:|---------:|---------:|
|          Filter_MatchingFromSource | core22 | .NET Core 2.2 | 2,894.0 ns | 55.87 ns | 83.62 ns |
|                  Logger_ForContext | core22 | .NET Core 2.2 |   614.6 ns |  7.62 ns | 11.40 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 | .NET Core 2.2 |   147.4 ns |  1.51 ns |  2.26 ns |
|          Filter_MatchingFromSource | core31 | .NET Core 3.1 | 2,657.6 ns | 57.24 ns | 83.90 ns |
|                  Logger_ForContext | core31 | .NET Core 3.1 |   624.6 ns |  5.25 ns |  7.86 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 | .NET Core 3.1 |   124.1 ns |  1.27 ns |  1.86 ns |
|          Filter_MatchingFromSource |  net48 |      .NET 4.8 | 3,651.2 ns | 63.82 ns | 95.53 ns |
|                  Logger_ForContext |  net48 |      .NET 4.8 |   720.9 ns |  5.23 ns |  7.82 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 |      .NET 4.8 |   234.6 ns |  2.77 ns |  4.15 ns |
|          Filter_MatchingFromSource |  net50 | .NET Core 5.0 | 2,487.8 ns | 60.02 ns | 89.84 ns |
|                  Logger_ForContext |  net50 | .NET Core 5.0 |   506.3 ns |  4.72 ns |  7.06 ns |
| LevelOverrideMap_GetEffectiveLevel |  net50 | .NET Core 5.0 |   128.5 ns |  1.46 ns |  2.18 ns |
