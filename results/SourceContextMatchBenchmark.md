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
|          Filter_MatchingFromSource | core22 | .NET Core 2.2 | 2,888.6 ns | 40.63 ns | 58.27 ns |
|                  Logger_ForContext | core22 | .NET Core 2.2 |   630.3 ns |  8.92 ns | 13.07 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 | .NET Core 2.2 |   148.9 ns |  1.51 ns |  2.26 ns |
|          Filter_MatchingFromSource | core31 | .NET Core 3.1 | 2,571.2 ns | 46.64 ns | 69.81 ns |
|                  Logger_ForContext | core31 | .NET Core 3.1 |   626.2 ns |  6.95 ns | 10.40 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 | .NET Core 3.1 |   130.2 ns |  1.64 ns |  2.45 ns |
|          Filter_MatchingFromSource |  net48 |      .NET 4.8 | 3,556.5 ns | 51.52 ns | 75.51 ns |
|                  Logger_ForContext |  net48 |      .NET 4.8 |   738.5 ns |  5.94 ns |  8.88 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 |      .NET 4.8 |   237.4 ns |  2.14 ns |  3.20 ns |
|          Filter_MatchingFromSource |  net50 | .NET Core 5.0 | 2,520.0 ns | 57.80 ns | 86.51 ns |
|                  Logger_ForContext |  net50 | .NET Core 5.0 |   521.9 ns |  8.44 ns | 12.38 ns |
| LevelOverrideMap_GetEffectiveLevel |  net50 | .NET Core 5.0 |   126.5 ns |  3.49 ns |  5.22 ns |
