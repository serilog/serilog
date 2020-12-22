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
|                             Method |           Job |       Runtime |       Mean |    Error |   StdDev |     Median |
|----------------------------------- |-------------- |-------------- |-----------:|---------:|---------:|-----------:|
|          Filter_MatchingFromSource | core22 RyuJit | .NET Core 2.2 | 2,774.2 ns |  5.65 ns |  8.10 ns | 2,773.2 ns |
|                  Logger_ForContext | core22 RyuJit | .NET Core 2.2 |   648.6 ns |  3.11 ns |  4.46 ns |   649.1 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 RyuJit | .NET Core 2.2 |   152.4 ns |  1.16 ns |  1.73 ns |   152.3 ns |
|          Filter_MatchingFromSource | core31 RyuJit | .NET Core 3.1 | 2,438.4 ns | 38.77 ns | 55.60 ns | 2,456.9 ns |
|                  Logger_ForContext | core31 RyuJit | .NET Core 3.1 |   651.6 ns |  9.74 ns | 13.97 ns |   661.3 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 RyuJit | .NET Core 3.1 |   140.9 ns |  1.76 ns |  2.64 ns |   141.5 ns |
|          Filter_MatchingFromSource |  net48 RyuJit |      .NET 4.8 | 3,409.3 ns |  9.49 ns | 14.20 ns | 3,406.8 ns |
|                  Logger_ForContext |  net48 RyuJit |      .NET 4.8 |   760.8 ns |  1.86 ns |  2.79 ns |   760.8 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 RyuJit |      .NET 4.8 |   256.9 ns |  2.90 ns |  4.34 ns |   257.6 ns |
