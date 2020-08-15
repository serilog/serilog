``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]        : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  .NET 4.8      : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT
  .NET Core 3.1 : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|                             Method |           Job |       Runtime |       Mean |     Error |    StdDev | Ratio | RatioSD |
|----------------------------------- |-------------- |-------------- |-----------:|----------:|----------:|------:|--------:|
|          Filter_MatchingFromSource |      .NET 4.8 |      .NET 4.8 | 6,310.0 ns | 113.58 ns | 106.24 ns |     ? |       ? |
|          Filter_MatchingFromSource | .NET Core 2.1 | .NET Core 2.1 |         NA |        NA |        NA |     ? |       ? |
|          Filter_MatchingFromSource | .NET Core 3.1 | .NET Core 3.1 | 2,510.4 ns |  49.14 ns |  67.26 ns |     ? |       ? |
|                                    |               |               |            |           |           |       |         |
|                  Logger_ForContext |      .NET 4.8 |      .NET 4.8 | 3,153.9 ns |  31.05 ns |  29.05 ns |     ? |       ? |
|                  Logger_ForContext | .NET Core 2.1 | .NET Core 2.1 |         NA |        NA |        NA |     ? |       ? |
|                  Logger_ForContext | .NET Core 3.1 | .NET Core 3.1 |   674.6 ns |  12.87 ns |  12.04 ns |     ? |       ? |
|                                    |               |               |            |           |           |       |         |
| LevelOverrideMap_GetEffectiveLevel |      .NET 4.8 |      .NET 4.8 | 2,585.1 ns |  44.84 ns |  41.95 ns |     ? |       ? |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 2.1 | .NET Core 2.1 |         NA |        NA |        NA |     ? |       ? |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 3.1 | .NET Core 3.1 |   128.4 ns |   2.59 ns |   3.71 ns |     ? |       ? |

Benchmarks with issues:
  SourceContextMatchBenchmark.Filter_MatchingFromSource: .NET Core 2.1(Runtime=.NET Core 2.1)
  SourceContextMatchBenchmark.Logger_ForContext: .NET Core 2.1(Runtime=.NET Core 2.1)
  SourceContextMatchBenchmark.LevelOverrideMap_GetEffectiveLevel: .NET Core 2.1(Runtime=.NET Core 2.1)
