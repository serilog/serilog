``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]        : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  .NET 4.8      : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT
  .NET Core 2.1 : .NET Core 2.1.21 (CoreCLR 4.6.29130.01, CoreFX 4.6.29130.02), X64 RyuJIT
  .NET Core 3.1 : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|                             Method |           Job |       Runtime |       Mean |     Error |    StdDev | Ratio | RatioSD |
|----------------------------------- |-------------- |-------------- |-----------:|----------:|----------:|------:|--------:|
|          Filter_MatchingFromSource |      .NET 4.8 |      .NET 4.8 | 6,401.3 ns | 124.71 ns | 128.06 ns |  1.14 |    0.03 |
|          Filter_MatchingFromSource | .NET Core 2.1 | .NET Core 2.1 | 5,642.1 ns | 108.62 ns | 120.73 ns |  1.00 |    0.00 |
|          Filter_MatchingFromSource | .NET Core 3.1 | .NET Core 3.1 | 2,421.0 ns |  48.23 ns |  89.39 ns |  0.43 |    0.02 |
|                                    |               |               |            |           |           |       |         |
|                  Logger_ForContext |      .NET 4.8 |      .NET 4.8 | 3,120.3 ns |  56.42 ns |  52.77 ns |  0.96 |    0.01 |
|                  Logger_ForContext | .NET Core 2.1 | .NET Core 2.1 | 3,239.9 ns |  46.45 ns |  43.45 ns |  1.00 |    0.00 |
|                  Logger_ForContext | .NET Core 3.1 | .NET Core 3.1 |   622.5 ns |  11.84 ns |  14.10 ns |  0.19 |    0.00 |
|                                    |               |               |            |           |           |       |         |
| LevelOverrideMap_GetEffectiveLevel |      .NET 4.8 |      .NET 4.8 | 2,563.7 ns |  50.94 ns |  64.42 ns |  0.97 |    0.04 |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 2.1 | .NET Core 2.1 | 2,646.4 ns |  48.94 ns |  45.78 ns |  1.00 |    0.00 |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 3.1 | .NET Core 3.1 |   131.5 ns |   2.61 ns |   4.21 ns |  0.05 |    0.00 |
