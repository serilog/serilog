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
|                             Method |           Job |       Runtime |       Mean |     Error |    StdDev |
|----------------------------------- |-------------- |-------------- |-----------:|----------:|----------:|
|          Filter_MatchingFromSource | core22 RyuJit | .NET Core 2.2 | 6,026.1 ns | 108.77 ns | 162.81 ns |
|                  Logger_ForContext | core22 RyuJit | .NET Core 2.2 | 3,340.0 ns |  34.00 ns |  50.89 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 RyuJit | .NET Core 2.2 | 2,672.1 ns |  27.30 ns |  40.86 ns |
|          Filter_MatchingFromSource | core31 RyuJit | .NET Core 3.1 | 2,574.5 ns |  62.71 ns |  93.87 ns |
|                  Logger_ForContext | core31 RyuJit | .NET Core 3.1 |   693.5 ns |   7.09 ns |  10.61 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 RyuJit | .NET Core 3.1 |   133.2 ns |   2.91 ns |   4.35 ns |
|          Filter_MatchingFromSource |  net48 RyuJit |      .NET 4.8 | 6,591.2 ns |  80.51 ns | 118.01 ns |
|                  Logger_ForContext |  net48 RyuJit |      .NET 4.8 | 3,306.7 ns |  26.87 ns |  40.22 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 RyuJit |      .NET 4.8 | 2,684.0 ns |  27.84 ns |  40.81 ns |
