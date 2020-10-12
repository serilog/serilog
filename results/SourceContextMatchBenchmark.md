``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]        : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core22 RyuJit : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  core31 RyuJit : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 RyuJit  : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

Jit=RyuJit  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
|                             Method |           Job |       Runtime |       Mean |    Error |    StdDev |
|----------------------------------- |-------------- |-------------- |-----------:|---------:|----------:|
|          Filter_MatchingFromSource | core22 RyuJit | .NET Core 2.2 | 5,778.8 ns | 77.70 ns | 116.29 ns |
|                  Logger_ForContext | core22 RyuJit | .NET Core 2.2 | 3,356.2 ns | 35.10 ns |  52.54 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 RyuJit | .NET Core 2.2 | 2,678.3 ns | 31.66 ns |  46.40 ns |
|          Filter_MatchingFromSource | core31 RyuJit | .NET Core 3.1 | 2,437.2 ns | 70.78 ns | 105.94 ns |
|                  Logger_ForContext | core31 RyuJit | .NET Core 3.1 |   633.6 ns |  9.23 ns |  13.82 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 RyuJit | .NET Core 3.1 |   132.2 ns |  4.05 ns |   5.93 ns |
|          Filter_MatchingFromSource |  net48 RyuJit |      .NET 4.8 | 6,335.1 ns | 80.60 ns | 118.14 ns |
|                  Logger_ForContext |  net48 RyuJit |      .NET 4.8 | 3,166.1 ns | 41.34 ns |  61.87 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 RyuJit |      .NET 4.8 | 2,600.9 ns | 33.21 ns |  49.71 ns |
