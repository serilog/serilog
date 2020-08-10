``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]        : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  .NET 4.8      : .NET Framework 4.8 (4.8.4180.0), X64 RyuJIT
  .NET Core 3.1 : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|                             Method |           Job |       Runtime |       Mean |     Error |    StdDev | Ratio | RatioSD |
|----------------------------------- |-------------- |-------------- |-----------:|----------:|----------:|------:|--------:|
|          Filter_MatchingFromSource |      .NET 4.8 |      .NET 4.8 | 6,518.3 ns | 116.72 ns | 109.18 ns |     ? |       ? |
|          Filter_MatchingFromSource | .NET Core 2.1 | .NET Core 2.1 |         NA |        NA |        NA |     ? |       ? |
|          Filter_MatchingFromSource | .NET Core 3.1 | .NET Core 3.1 | 2,522.4 ns |  49.54 ns |  62.65 ns |     ? |       ? |
|                                    |               |               |            |           |           |       |         |
|                  Logger_ForContext |      .NET 4.8 |      .NET 4.8 | 3,155.2 ns |  61.92 ns |  60.81 ns |     ? |       ? |
|                  Logger_ForContext | .NET Core 2.1 | .NET Core 2.1 |         NA |        NA |        NA |     ? |       ? |
|                  Logger_ForContext | .NET Core 3.1 | .NET Core 3.1 |   674.8 ns |  13.06 ns |  12.21 ns |     ? |       ? |
|                                    |               |               |            |           |           |       |         |
| LevelOverrideMap_GetEffectiveLevel |      .NET 4.8 |      .NET 4.8 | 2,531.2 ns |  49.49 ns |  48.61 ns |     ? |       ? |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 2.1 | .NET Core 2.1 |         NA |        NA |        NA |     ? |       ? |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 3.1 | .NET Core 3.1 |   129.0 ns |   2.56 ns |   3.15 ns |     ? |       ? |

Benchmarks with issues:
  SourceContextMatchBenchmark.Filter_MatchingFromSource: .NET Core 2.1(Runtime=.NET Core 2.1)
  SourceContextMatchBenchmark.Logger_ForContext: .NET Core 2.1(Runtime=.NET Core 2.1)
  SourceContextMatchBenchmark.LevelOverrideMap_GetEffectiveLevel: .NET Core 2.1(Runtime=.NET Core 2.1)
