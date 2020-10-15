``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
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
|          Filter_MatchingFromSource | core22 RyuJit | .NET Core 2.2 | 5,591.4 ns | 75.01 ns | 112.28 ns |
|                  Logger_ForContext | core22 RyuJit | .NET Core 2.2 | 3,135.3 ns | 17.48 ns |  25.62 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 RyuJit | .NET Core 2.2 | 2,598.2 ns | 11.92 ns |  17.84 ns |
|          Filter_MatchingFromSource | core31 RyuJit | .NET Core 3.1 | 2,315.9 ns | 32.17 ns |  47.16 ns |
|                  Logger_ForContext | core31 RyuJit | .NET Core 3.1 |   607.9 ns |  1.90 ns |   2.78 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 RyuJit | .NET Core 3.1 |   126.8 ns |  0.82 ns |   1.15 ns |
|          Filter_MatchingFromSource |  net48 RyuJit |      .NET 4.8 | 6,098.2 ns | 48.18 ns |  72.12 ns |
|                  Logger_ForContext |  net48 RyuJit |      .NET 4.8 | 3,065.8 ns | 10.86 ns |  16.25 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 RyuJit |      .NET 4.8 | 2,596.2 ns |  9.47 ns |  13.88 ns |
