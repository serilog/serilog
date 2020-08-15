``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]   : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT
  .NET 4.8 : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT


```
|                             Method |           Job |       Runtime |     Mean |     Error |    StdDev | Ratio | RatioSD |
|----------------------------------- |-------------- |-------------- |---------:|----------:|----------:|------:|--------:|
|          Filter_MatchingFromSource |      .NET 4.8 |      .NET 4.8 | 6.877 μs | 0.1374 μs | 0.1583 μs |     ? |       ? |
|          Filter_MatchingFromSource | .NET Core 2.1 | .NET Core 2.1 |       NA |        NA |        NA |     ? |       ? |
|          Filter_MatchingFromSource | .NET Core 3.1 | .NET Core 3.1 |       NA |        NA |        NA |     ? |       ? |
|                                    |               |               |          |           |           |       |         |
|                  Logger_ForContext |      .NET 4.8 |      .NET 4.8 | 3.632 μs | 0.0456 μs | 0.0404 μs |     ? |       ? |
|                  Logger_ForContext | .NET Core 2.1 | .NET Core 2.1 |       NA |        NA |        NA |     ? |       ? |
|                  Logger_ForContext | .NET Core 3.1 | .NET Core 3.1 |       NA |        NA |        NA |     ? |       ? |
|                                    |               |               |          |           |           |       |         |
| LevelOverrideMap_GetEffectiveLevel |      .NET 4.8 |      .NET 4.8 | 3.039 μs | 0.0509 μs | 0.0476 μs |     ? |       ? |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 2.1 | .NET Core 2.1 |       NA |        NA |        NA |     ? |       ? |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 3.1 | .NET Core 3.1 |       NA |        NA |        NA |     ? |       ? |

Benchmarks with issues:
  SourceContextMatchBenchmark.Filter_MatchingFromSource: .NET Core 2.1(Runtime=.NET Core 2.1)
  SourceContextMatchBenchmark.Filter_MatchingFromSource: .NET Core 3.1(Runtime=.NET Core 3.1)
  SourceContextMatchBenchmark.Logger_ForContext: .NET Core 2.1(Runtime=.NET Core 2.1)
  SourceContextMatchBenchmark.Logger_ForContext: .NET Core 3.1(Runtime=.NET Core 3.1)
  SourceContextMatchBenchmark.LevelOverrideMap_GetEffectiveLevel: .NET Core 2.1(Runtime=.NET Core 2.1)
  SourceContextMatchBenchmark.LevelOverrideMap_GetEffectiveLevel: .NET Core 3.1(Runtime=.NET Core 3.1)
