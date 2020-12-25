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
|          Filter_MatchingFromSource | core22 | .NET Core 2.2 | 2,843.1 ns |  23.05 ns |  34.50 ns |
|                  Logger_ForContext | core22 | .NET Core 2.2 |   632.9 ns |   5.82 ns |   8.72 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 | .NET Core 2.2 |   150.8 ns |   1.44 ns |   2.16 ns |
|          Filter_MatchingFromSource | core31 | .NET Core 3.1 | 2,409.1 ns |  23.20 ns |  34.72 ns |
|                  Logger_ForContext | core31 | .NET Core 3.1 |   646.7 ns |   4.47 ns |   6.69 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 | .NET Core 3.1 |   137.6 ns |   2.88 ns |   4.32 ns |
|          Filter_MatchingFromSource |  net48 |      .NET 4.8 | 3,565.9 ns | 167.74 ns | 251.07 ns |
|                  Logger_ForContext |  net48 |      .NET 4.8 |   755.1 ns |   4.96 ns |   7.43 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 |      .NET 4.8 |   256.5 ns |   3.53 ns |   5.28 ns |
|          Filter_MatchingFromSource |  net50 | .NET Core 5.0 | 2,250.5 ns |  28.39 ns |  39.80 ns |
|                  Logger_ForContext |  net50 | .NET Core 5.0 |   515.7 ns |   5.34 ns |   8.00 ns |
| LevelOverrideMap_GetEffectiveLevel |  net50 | .NET Core 5.0 |   141.3 ns |   2.29 ns |   3.42 ns |
