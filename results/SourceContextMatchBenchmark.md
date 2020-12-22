``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.404
  [Host]        : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  core22 RyuJit : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  core31 RyuJit : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48 RyuJit  : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT

Jit=RyuJit  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
|                             Method |           Job |       Runtime |       Mean |    Error |   StdDev |
|----------------------------------- |-------------- |-------------- |-----------:|---------:|---------:|
|          Filter_MatchingFromSource | core22 RyuJit | .NET Core 2.2 | 5,944.5 ns | 37.68 ns | 56.40 ns |
|                  Logger_ForContext | core22 RyuJit | .NET Core 2.2 | 3,386.8 ns | 22.47 ns | 33.63 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 RyuJit | .NET Core 2.2 | 2,812.0 ns | 22.21 ns | 33.24 ns |
|          Filter_MatchingFromSource | core31 RyuJit | .NET Core 3.1 | 2,427.7 ns | 25.15 ns | 37.64 ns |
|                  Logger_ForContext | core31 RyuJit | .NET Core 3.1 |   656.3 ns |  5.85 ns |  8.57 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 RyuJit | .NET Core 3.1 |   142.2 ns |  2.34 ns |  3.51 ns |
|          Filter_MatchingFromSource |  net48 RyuJit |      .NET 4.8 | 6,442.4 ns | 40.64 ns | 59.57 ns |
|                  Logger_ForContext |  net48 RyuJit |      .NET 4.8 | 3,323.2 ns | 18.77 ns | 26.92 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 RyuJit |      .NET 4.8 | 2,831.3 ns | 32.99 ns | 49.37 ns |
