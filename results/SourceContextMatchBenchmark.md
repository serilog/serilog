``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]        : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  core22 RyuJit : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  core31 RyuJit : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  net48 RyuJit  : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT

Jit=RyuJit  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
|                             Method |           Job |       Runtime |       Mean |     Error |    StdDev |
|----------------------------------- |-------------- |-------------- |-----------:|----------:|----------:|
|          Filter_MatchingFromSource | core22 RyuJit | .NET Core 2.2 | 5,887.1 ns | 125.35 ns | 175.72 ns |
|                  Logger_ForContext | core22 RyuJit | .NET Core 2.2 | 3,467.8 ns | 123.39 ns | 184.68 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 RyuJit | .NET Core 2.2 | 3,082.4 ns |  82.56 ns | 123.58 ns |
|          Filter_MatchingFromSource | core31 RyuJit | .NET Core 3.1 | 2,873.3 ns | 122.74 ns | 183.71 ns |
|                  Logger_ForContext | core31 RyuJit | .NET Core 3.1 |   695.9 ns |   6.56 ns |   9.41 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 RyuJit | .NET Core 3.1 |   143.9 ns |   1.78 ns |   2.50 ns |
|          Filter_MatchingFromSource |  net48 RyuJit |      .NET 4.8 | 6,600.0 ns |  74.87 ns | 112.06 ns |
|                  Logger_ForContext |  net48 RyuJit |      .NET 4.8 | 3,400.1 ns |  32.48 ns |  48.61 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 RyuJit |      .NET 4.8 | 2,752.0 ns |  24.63 ns |  36.11 ns |
