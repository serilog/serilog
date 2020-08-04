``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]        : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  .NET Core 2.1 : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  .NET Core 3.1 : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|                             Method |           Job |       Runtime |       Mean |     Error |    StdDev | Ratio | RatioSD |
|----------------------------------- |-------------- |-------------- |-----------:|----------:|----------:|------:|--------:|
|          Filter_MatchingFromSource | .NET Core 2.1 | .NET Core 2.1 | 5,795.4 ns | 113.23 ns | 143.20 ns |  1.00 |    0.00 |
|          Filter_MatchingFromSource | .NET Core 3.1 | .NET Core 3.1 | 2,479.1 ns |  49.48 ns |  72.53 ns |  0.43 |    0.02 |
|                                    |               |               |            |           |           |       |         |
|                  Logger_ForContext | .NET Core 2.1 | .NET Core 2.1 | 3,155.9 ns |  39.34 ns |  36.80 ns |  1.00 |    0.00 |
|                  Logger_ForContext | .NET Core 3.1 | .NET Core 3.1 |   624.2 ns |  12.03 ns |  11.82 ns |  0.20 |    0.00 |
|                                    |               |               |            |           |           |       |         |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 2.1 | .NET Core 2.1 | 2,633.8 ns |  48.88 ns |  45.72 ns |  1.00 |    0.00 |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 3.1 | .NET Core 3.1 |   130.6 ns |   2.55 ns |   3.23 ns |  0.05 |    0.00 |
