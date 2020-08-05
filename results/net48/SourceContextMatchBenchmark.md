``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]        : .NET Framework 4.8 (4.8.4180.0), X64 RyuJIT
  .NET 4.8      : .NET Framework 4.8 (4.8.4180.0), X64 RyuJIT
  .NET Core 2.1 : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  .NET Core 3.1 : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|                             Method |           Job |       Runtime |       Mean |     Error |    StdDev | Ratio | RatioSD |
|----------------------------------- |-------------- |-------------- |-----------:|----------:|----------:|------:|--------:|
|          Filter_MatchingFromSource |      .NET 4.8 |      .NET 4.8 | 6,422.8 ns | 124.50 ns | 143.38 ns |  1.09 |    0.02 |
|          Filter_MatchingFromSource | .NET Core 2.1 | .NET Core 2.1 | 5,845.7 ns |  97.53 ns |  86.46 ns |  1.00 |    0.00 |
|          Filter_MatchingFromSource | .NET Core 3.1 | .NET Core 3.1 | 2,470.0 ns |  44.58 ns |  41.70 ns |  0.42 |    0.01 |
|                                    |               |               |            |           |           |       |         |
|                  Logger_ForContext |      .NET 4.8 |      .NET 4.8 | 3,308.9 ns |  53.50 ns |  50.05 ns |  0.96 |    0.01 |
|                  Logger_ForContext | .NET Core 2.1 | .NET Core 2.1 | 3,460.0 ns |  23.63 ns |  19.73 ns |  1.00 |    0.00 |
|                  Logger_ForContext | .NET Core 3.1 | .NET Core 3.1 |   697.3 ns |  13.72 ns |  22.55 ns |  0.20 |    0.01 |
|                                    |               |               |            |           |           |       |         |
| LevelOverrideMap_GetEffectiveLevel |      .NET 4.8 |      .NET 4.8 | 2,691.0 ns |  53.44 ns |  52.49 ns |  0.97 |    0.02 |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 2.1 | .NET Core 2.1 | 2,781.9 ns |  48.00 ns |  44.90 ns |  1.00 |    0.00 |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 3.1 | .NET Core 3.1 |   147.0 ns |   2.92 ns |   4.63 ns |  0.05 |    0.00 |
