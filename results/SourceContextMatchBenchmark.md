``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]        : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core22 RyuJit : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  core31 RyuJit : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 RyuJit  : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT

Jit=RyuJit  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
|                             Method |           Job |       Runtime |       Mean |    Error |   StdDev |
|----------------------------------- |-------------- |-------------- |-----------:|---------:|---------:|
|          Filter_MatchingFromSource | core22 RyuJit | .NET Core 2.2 | 5,800.6 ns | 45.21 ns | 66.26 ns |
|                  Logger_ForContext | core22 RyuJit | .NET Core 2.2 | 3,418.7 ns | 36.73 ns | 52.67 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 RyuJit | .NET Core 2.2 | 2,756.3 ns | 34.00 ns | 49.83 ns |
|          Filter_MatchingFromSource | core31 RyuJit | .NET Core 3.1 | 2,413.3 ns | 12.60 ns | 18.86 ns |
|                  Logger_ForContext | core31 RyuJit | .NET Core 3.1 |   712.1 ns |  5.44 ns |  8.14 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 RyuJit | .NET Core 3.1 |   137.0 ns |  3.36 ns |  5.03 ns |
|          Filter_MatchingFromSource |  net48 RyuJit |      .NET 4.8 | 6,347.8 ns | 36.64 ns | 54.84 ns |
|                  Logger_ForContext |  net48 RyuJit |      .NET 4.8 | 3,350.4 ns | 21.77 ns | 32.59 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 RyuJit |      .NET 4.8 | 2,683.4 ns | 16.58 ns | 24.82 ns |
