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
|                             Method |    Job |       Runtime |       Mean |    Error |    StdDev |
|----------------------------------- |------- |-------------- |-----------:|---------:|----------:|
|          Filter_MatchingFromSource | core22 | .NET Core 2.2 | 5,909.1 ns | 78.85 ns | 118.02 ns |
|                  Logger_ForContext | core22 | .NET Core 2.2 | 3,265.4 ns | 37.16 ns |  55.62 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 | .NET Core 2.2 | 2,661.5 ns | 32.03 ns |  47.95 ns |
|          Filter_MatchingFromSource | core31 | .NET Core 3.1 | 2,508.4 ns | 48.92 ns |  70.15 ns |
|                  Logger_ForContext | core31 | .NET Core 3.1 |   634.2 ns |  8.41 ns |  12.59 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 | .NET Core 3.1 |   127.5 ns |  2.42 ns |   3.62 ns |
|          Filter_MatchingFromSource |  net48 |      .NET 4.8 | 6,419.0 ns | 78.02 ns | 116.77 ns |
|                  Logger_ForContext |  net48 |      .NET 4.8 | 3,180.3 ns | 27.30 ns |  40.86 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 |      .NET 4.8 | 2,674.7 ns | 30.87 ns |  46.20 ns |
|          Filter_MatchingFromSource |  net50 | .NET Core 5.0 | 2,283.1 ns | 59.16 ns |  84.85 ns |
|                  Logger_ForContext |  net50 | .NET Core 5.0 |   502.6 ns |  9.94 ns |  14.87 ns |
| LevelOverrideMap_GetEffectiveLevel |  net50 | .NET Core 5.0 |   126.9 ns |  2.10 ns |   3.14 ns |
