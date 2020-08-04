``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]        : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  .NET 4.8      : .NET Framework 4.8 (4.8.4180.0), X64 RyuJIT
  .NET Core 2.1 : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  .NET Core 3.1 : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|                             Method |           Job |       Runtime |       Mean |    Error |   StdDev | Ratio | RatioSD |
|----------------------------------- |-------------- |-------------- |-----------:|---------:|---------:|------:|--------:|
|          Filter_MatchingFromSource |      .NET 4.8 |      .NET 4.8 | 6,449.9 ns | 79.93 ns | 70.86 ns |  1.08 |    0.02 |
|          Filter_MatchingFromSource | .NET Core 2.1 | .NET Core 2.1 | 5,970.1 ns | 42.27 ns | 37.47 ns |  1.00 |    0.00 |
|          Filter_MatchingFromSource | .NET Core 3.1 | .NET Core 3.1 | 2,684.7 ns | 50.34 ns | 49.44 ns |  0.45 |    0.01 |
|                                    |               |               |            |          |          |       |         |
|                  Logger_ForContext |      .NET 4.8 |      .NET 4.8 | 3,328.4 ns | 20.68 ns | 19.34 ns |  0.97 |    0.01 |
|                  Logger_ForContext | .NET Core 2.1 | .NET Core 2.1 | 3,432.6 ns | 18.99 ns | 15.86 ns |  1.00 |    0.00 |
|                  Logger_ForContext | .NET Core 3.1 | .NET Core 3.1 |   692.0 ns |  8.68 ns |  8.12 ns |  0.20 |    0.00 |
|                                    |               |               |            |          |          |       |         |
| LevelOverrideMap_GetEffectiveLevel |      .NET 4.8 |      .NET 4.8 | 2,725.4 ns | 17.63 ns | 16.49 ns |  0.99 |    0.01 |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 2.1 | .NET Core 2.1 | 2,757.9 ns | 28.42 ns | 26.59 ns |  1.00 |    0.00 |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 3.1 | .NET Core 3.1 |   142.7 ns |  2.49 ns |  2.08 ns |  0.05 |    0.00 |
