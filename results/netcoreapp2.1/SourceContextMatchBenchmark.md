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
|          Filter_MatchingFromSource |      .NET 4.8 |      .NET 4.8 | 6,580.5 ns | 100.29 ns | 88.90 ns |  1.13 |    0.02 |
|          Filter_MatchingFromSource | .NET Core 2.1 | .NET Core 2.1 | 5,821.3 ns |  62.39 ns | 58.36 ns |  1.00 |    0.00 |
|          Filter_MatchingFromSource | .NET Core 3.1 | .NET Core 3.1 | 2,584.2 ns |  50.44 ns | 80.00 ns |  0.45 |    0.02 |
|                                    |               |               |            |           |          |       |         |
|                  Logger_ForContext |      .NET 4.8 |      .NET 4.8 | 3,354.4 ns |  64.56 ns | 60.39 ns |  0.97 |    0.03 |
|                  Logger_ForContext | .NET Core 2.1 | .NET Core 2.1 | 3,464.7 ns |  65.03 ns | 69.58 ns |  1.00 |    0.00 |
|                  Logger_ForContext | .NET Core 3.1 | .NET Core 3.1 |   696.4 ns |   7.30 ns |  6.83 ns |  0.20 |    0.00 |
|                                    |               |               |            |           |          |       |         |
| LevelOverrideMap_GetEffectiveLevel |      .NET 4.8 |      .NET 4.8 | 2,723.9 ns |  44.77 ns | 37.39 ns |  0.96 |    0.02 |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 2.1 | .NET Core 2.1 | 2,827.6 ns |  54.45 ns | 50.93 ns |  1.00 |    0.00 |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 3.1 | .NET Core 3.1 |   143.4 ns |   2.85 ns |  3.60 ns |  0.05 |    0.00 |
