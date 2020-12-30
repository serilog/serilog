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
|          Filter_MatchingFromSource | core22 | .NET Core 2.2 | 2,819.0 ns | 57.14 ns | 85.52 ns |
|                  Logger_ForContext | core22 | .NET Core 2.2 |   614.8 ns |  7.44 ns | 11.13 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 | .NET Core 2.2 |   140.7 ns |  2.41 ns |  3.61 ns |
|          Filter_MatchingFromSource | core31 | .NET Core 3.1 | 2,512.6 ns | 53.77 ns | 80.48 ns |
|                  Logger_ForContext | core31 | .NET Core 3.1 |   624.4 ns |  8.83 ns | 13.21 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 | .NET Core 3.1 |   129.4 ns |  2.10 ns |  3.14 ns |
|          Filter_MatchingFromSource |  net48 |      .NET 4.8 | 3,482.2 ns | 51.25 ns | 76.71 ns |
|                  Logger_ForContext |  net48 |      .NET 4.8 |   729.0 ns |  9.56 ns | 14.30 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 |      .NET 4.8 |   236.3 ns |  2.53 ns |  3.79 ns |
|          Filter_MatchingFromSource |  net50 | .NET Core 5.0 | 2,350.3 ns | 50.07 ns | 74.94 ns |
|                  Logger_ForContext |  net50 | .NET Core 5.0 |   502.6 ns |  9.17 ns | 13.73 ns |
| LevelOverrideMap_GetEffectiveLevel |  net50 | .NET Core 5.0 |   130.4 ns |  2.32 ns |  3.47 ns |
