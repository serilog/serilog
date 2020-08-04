``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host] : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|                             Method |           Job |       Runtime | Mean | Error | Ratio | RatioSD |
|----------------------------------- |-------------- |-------------- |-----:|------:|------:|--------:|
|          Filter_MatchingFromSource | .NET Core 2.1 | .NET Core 2.1 |   NA |    NA |     ? |       ? |
|          Filter_MatchingFromSource | .NET Core 3.1 | .NET Core 3.1 |   NA |    NA |     ? |       ? |
|                                    |               |               |      |       |       |         |
|                  Logger_ForContext | .NET Core 2.1 | .NET Core 2.1 |   NA |    NA |     ? |       ? |
|                  Logger_ForContext | .NET Core 3.1 | .NET Core 3.1 |   NA |    NA |     ? |       ? |
|                                    |               |               |      |       |       |         |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 2.1 | .NET Core 2.1 |   NA |    NA |     ? |       ? |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 3.1 | .NET Core 3.1 |   NA |    NA |     ? |       ? |

Benchmarks with issues:
  SourceContextMatchBenchmark.Filter_MatchingFromSource: .NET Core 2.1(Runtime=.NET Core 2.1)
  SourceContextMatchBenchmark.Filter_MatchingFromSource: .NET Core 3.1(Runtime=.NET Core 3.1)
  SourceContextMatchBenchmark.Logger_ForContext: .NET Core 2.1(Runtime=.NET Core 2.1)
  SourceContextMatchBenchmark.Logger_ForContext: .NET Core 3.1(Runtime=.NET Core 3.1)
  SourceContextMatchBenchmark.LevelOverrideMap_GetEffectiveLevel: .NET Core 2.1(Runtime=.NET Core 2.1)
  SourceContextMatchBenchmark.LevelOverrideMap_GetEffectiveLevel: .NET Core 3.1(Runtime=.NET Core 3.1)
