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
|          Filter_MatchingFromSource | core22 | .NET Core 2.2 | 5,742.7 ns | 39.47 ns | 59.07 ns |
|                  Logger_ForContext | core22 | .NET Core 2.2 | 3,349.0 ns | 17.40 ns | 26.04 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 | .NET Core 2.2 | 2,777.1 ns | 16.13 ns | 24.15 ns |
|          Filter_MatchingFromSource | core31 | .NET Core 3.1 | 2,327.5 ns | 17.36 ns | 25.98 ns |
|                  Logger_ForContext | core31 | .NET Core 3.1 |   653.5 ns |  5.78 ns |  8.66 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 | .NET Core 3.1 |   139.9 ns |  3.36 ns |  4.92 ns |
|          Filter_MatchingFromSource |  net48 |      .NET 4.8 | 6,413.0 ns | 37.43 ns | 56.03 ns |
|                  Logger_ForContext |  net48 |      .NET 4.8 | 3,288.1 ns | 16.84 ns | 25.20 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 |      .NET 4.8 | 2,774.8 ns | 20.14 ns | 30.15 ns |
|          Filter_MatchingFromSource |  net50 | .NET Core 5.0 | 2,182.8 ns | 18.37 ns | 27.50 ns |
|                  Logger_ForContext |  net50 | .NET Core 5.0 |   518.3 ns |  6.08 ns |  9.11 ns |
| LevelOverrideMap_GetEffectiveLevel |  net50 | .NET Core 5.0 |   140.6 ns |  2.72 ns |  4.08 ns |
