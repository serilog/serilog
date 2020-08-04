``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]        : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  .NET Core 2.1 : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  .NET Core 3.1 : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|                             Method |           Job |       Runtime |       Mean |     Error |   StdDev | Ratio |
|----------------------------------- |-------------- |-------------- |-----------:|----------:|---------:|------:|
|          Filter_MatchingFromSource | .NET Core 2.1 | .NET Core 2.1 | 5,864.5 ns | 102.01 ns | 95.42 ns |  1.00 |
|          Filter_MatchingFromSource | .NET Core 3.1 | .NET Core 3.1 | 2,482.0 ns |  48.90 ns | 63.58 ns |  0.42 |
|                                    |               |               |            |           |          |       |
|                  Logger_ForContext | .NET Core 2.1 | .NET Core 2.1 | 3,154.1 ns |  57.46 ns | 53.75 ns |  1.00 |
|                  Logger_ForContext | .NET Core 3.1 | .NET Core 3.1 |   622.8 ns |  12.44 ns | 12.78 ns |  0.20 |
|                                    |               |               |            |           |          |       |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 2.1 | .NET Core 2.1 | 2,659.6 ns |  51.82 ns | 50.90 ns |  1.00 |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 3.1 | .NET Core 3.1 |   132.3 ns |   2.66 ns |  3.82 ns |  0.05 |
