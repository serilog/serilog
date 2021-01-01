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
|          Filter_MatchingFromSource | core22 | .NET Core 2.2 | 5,854.5 ns | 43.34 ns | 63.52 ns |
|                  Logger_ForContext | core22 | .NET Core 2.2 | 3,133.6 ns | 15.83 ns | 23.20 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 | .NET Core 2.2 | 2,590.0 ns | 20.72 ns | 31.01 ns |
|          Filter_MatchingFromSource | core31 | .NET Core 3.1 | 2,513.0 ns | 54.29 ns | 81.27 ns |
|                  Logger_ForContext | core31 | .NET Core 3.1 |   610.7 ns |  4.54 ns |  6.65 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 | .NET Core 3.1 |   126.3 ns |  0.41 ns |  0.59 ns |
|          Filter_MatchingFromSource |  net48 |      .NET 4.8 | 6,167.5 ns | 52.08 ns | 76.34 ns |
|                  Logger_ForContext |  net48 |      .NET 4.8 | 3,075.2 ns | 11.09 ns | 15.54 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 |      .NET 4.8 | 2,606.0 ns | 23.28 ns | 34.12 ns |
|          Filter_MatchingFromSource |  net50 | .NET Core 5.0 | 2,308.8 ns | 39.81 ns | 58.36 ns |
|                  Logger_ForContext |  net50 | .NET Core 5.0 |   493.9 ns |  2.81 ns |  4.12 ns |
| LevelOverrideMap_GetEffectiveLevel |  net50 | .NET Core 5.0 |   127.6 ns |  0.70 ns |  1.00 ns |
