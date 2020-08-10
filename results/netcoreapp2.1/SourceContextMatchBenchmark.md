``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]        : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  .NET 4.8      : .NET Framework 4.8 (4.8.4180.0), X64 RyuJIT
  .NET Core 2.1 : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  .NET Core 3.1 : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|                             Method |           Job |       Runtime |       Mean |     Error |   StdDev | Ratio | RatioSD |
|----------------------------------- |-------------- |-------------- |-----------:|----------:|---------:|------:|--------:|
|          Filter_MatchingFromSource |      .NET 4.8 |      .NET 4.8 | 6,550.0 ns |  48.97 ns | 45.81 ns |  1.12 |    0.02 |
|          Filter_MatchingFromSource | .NET Core 2.1 | .NET Core 2.1 | 5,857.0 ns | 105.22 ns | 98.43 ns |  1.00 |    0.00 |
|          Filter_MatchingFromSource | .NET Core 3.1 | .NET Core 3.1 | 2,560.2 ns |  50.11 ns | 86.43 ns |  0.44 |    0.02 |
|                                    |               |               |            |           |          |       |         |
|                  Logger_ForContext |      .NET 4.8 |      .NET 4.8 | 3,186.6 ns |  31.27 ns | 29.25 ns |  0.96 |    0.02 |
|                  Logger_ForContext | .NET Core 2.1 | .NET Core 2.1 | 3,322.3 ns |  41.26 ns | 38.60 ns |  1.00 |    0.00 |
|                  Logger_ForContext | .NET Core 3.1 | .NET Core 3.1 |   680.1 ns |  10.54 ns |  9.86 ns |  0.20 |    0.00 |
|                                    |               |               |            |           |          |       |         |
| LevelOverrideMap_GetEffectiveLevel |      .NET 4.8 |      .NET 4.8 | 2,611.6 ns |  45.46 ns | 42.52 ns |  0.95 |    0.02 |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 2.1 | .NET Core 2.1 | 2,756.6 ns |  50.81 ns | 49.90 ns |  1.00 |    0.00 |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 3.1 | .NET Core 3.1 |   129.4 ns |   2.58 ns |  2.76 ns |  0.05 |    0.00 |
