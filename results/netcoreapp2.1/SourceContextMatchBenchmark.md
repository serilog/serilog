``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]        : .NET Core 2.1.21 (CoreCLR 4.6.29130.01, CoreFX 4.6.29130.02), X64 RyuJIT
  .NET 4.8      : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT
  .NET Core 2.1 : .NET Core 2.1.21 (CoreCLR 4.6.29130.01, CoreFX 4.6.29130.02), X64 RyuJIT
  .NET Core 3.1 : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|                             Method |           Job |       Runtime |       Mean |     Error |    StdDev | Ratio | RatioSD |
|----------------------------------- |-------------- |-------------- |-----------:|----------:|----------:|------:|--------:|
|          Filter_MatchingFromSource |      .NET 4.8 |      .NET 4.8 | 6,355.4 ns |  98.78 ns |  92.40 ns |  1.12 |    0.04 |
|          Filter_MatchingFromSource | .NET Core 2.1 | .NET Core 2.1 | 5,682.5 ns | 113.55 ns | 147.65 ns |  1.00 |    0.00 |
|          Filter_MatchingFromSource | .NET Core 3.1 | .NET Core 3.1 | 2,458.7 ns |  48.67 ns |  79.96 ns |  0.43 |    0.02 |
|                                    |               |               |            |           |           |       |         |
|                  Logger_ForContext |      .NET 4.8 |      .NET 4.8 | 3,146.8 ns |  48.03 ns |  44.93 ns |  0.99 |    0.01 |
|                  Logger_ForContext | .NET Core 2.1 | .NET Core 2.1 | 3,166.7 ns |  61.88 ns |  60.77 ns |  1.00 |    0.00 |
|                  Logger_ForContext | .NET Core 3.1 | .NET Core 3.1 |   624.7 ns |  12.19 ns |  12.52 ns |  0.20 |    0.01 |
|                                    |               |               |            |           |           |       |         |
| LevelOverrideMap_GetEffectiveLevel |      .NET 4.8 |      .NET 4.8 | 2,571.4 ns |  50.83 ns |  58.53 ns |  0.99 |    0.02 |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 2.1 | .NET Core 2.1 | 2,614.7 ns |  51.55 ns |  50.63 ns |  1.00 |    0.00 |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 3.1 | .NET Core 3.1 |   129.5 ns |   2.22 ns |   3.03 ns |  0.05 |    0.00 |
