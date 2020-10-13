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
|          Filter_MatchingFromSource | core22 RyuJit | .NET Core 2.2 | 5,638.6 ns | 98.19 ns | 146.97 ns |
|                  Logger_ForContext | core22 RyuJit | .NET Core 2.2 | 3,112.5 ns | 15.72 ns |  22.04 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 RyuJit | .NET Core 2.2 | 2,504.8 ns |  7.30 ns |  10.47 ns |
|          Filter_MatchingFromSource | core31 RyuJit | .NET Core 3.1 | 2,389.8 ns | 31.82 ns |  42.49 ns |
|                  Logger_ForContext | core31 RyuJit | .NET Core 3.1 |   614.2 ns |  2.68 ns |   4.01 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 RyuJit | .NET Core 3.1 |   124.2 ns |  0.81 ns |   1.16 ns |
|          Filter_MatchingFromSource |  net48 RyuJit |      .NET 4.8 | 6,163.5 ns | 34.80 ns |  52.08 ns |
|                  Logger_ForContext |  net48 RyuJit |      .NET 4.8 | 3,080.6 ns | 15.17 ns |  22.71 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 RyuJit |      .NET 4.8 | 2,539.7 ns | 14.01 ns |  19.64 ns |
