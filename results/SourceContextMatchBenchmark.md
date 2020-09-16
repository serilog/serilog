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
|                             Method |           Job |       Runtime |       Mean |     Error |    StdDev |     Median |
|----------------------------------- |-------------- |-------------- |-----------:|----------:|----------:|-----------:|
|          Filter_MatchingFromSource | core22 RyuJit | .NET Core 2.2 | 5,806.4 ns | 106.71 ns | 159.72 ns | 5,767.8 ns |
|                  Logger_ForContext | core22 RyuJit | .NET Core 2.2 | 3,163.8 ns |  24.36 ns |  35.71 ns | 3,156.8 ns |
| LevelOverrideMap_GetEffectiveLevel | core22 RyuJit | .NET Core 2.2 | 2,629.2 ns |  12.48 ns |  18.67 ns | 2,632.8 ns |
|          Filter_MatchingFromSource | core31 RyuJit | .NET Core 3.1 | 2,405.6 ns |  36.68 ns |  54.91 ns | 2,404.5 ns |
|                  Logger_ForContext | core31 RyuJit | .NET Core 3.1 |   616.7 ns |   3.81 ns |   5.34 ns |   620.0 ns |
| LevelOverrideMap_GetEffectiveLevel | core31 RyuJit | .NET Core 3.1 |   129.0 ns |   0.44 ns |   0.65 ns |   129.0 ns |
|          Filter_MatchingFromSource |  net48 RyuJit |      .NET 4.8 | 6,243.3 ns |  52.84 ns |  77.46 ns | 6,252.6 ns |
|                  Logger_ForContext |  net48 RyuJit |      .NET 4.8 | 3,113.4 ns |   9.03 ns |  13.52 ns | 3,113.0 ns |
| LevelOverrideMap_GetEffectiveLevel |  net48 RyuJit |      .NET 4.8 | 2,548.9 ns |  11.20 ns |  16.76 ns | 2,553.3 ns |
