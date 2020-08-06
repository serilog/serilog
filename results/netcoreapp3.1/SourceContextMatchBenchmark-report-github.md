# Benchmark results

## Linux

``` ini

BenchmarkDotNet=v0.12.0, OS=debian 11
Intel Core i7-3840QM CPU 2.80GHz (Ivy Bridge), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.1.201
  [Host]     : .NET Core 3.1.3 (CoreCLR 4.700.20.11803, CoreFX 4.700.20.12001), X64 RyuJIT
  Job-CSERBI : .NET Core 2.1.16 (CoreCLR 4.6.28516.03, CoreFX 4.6.28516.10), X64 RyuJIT
  Job-GDNCBZ : .NET Core 3.1.3 (CoreCLR 4.700.20.11803, CoreFX 4.700.20.12001), X64 RyuJIT


```
|                             Method |       Runtime |       Mean |    Error |   StdDev | Ratio |
|----------------------------------- |-------------- |-----------:|---------:|---------:|------:|
|          Filter_MatchingFromSource | .NET Core 2.1 | 5,799.5 ns | 51.17 ns | 45.36 ns |  1.00 |
|          Filter_MatchingFromSource | .NET Core 3.1 | 3,564.5 ns | 37.43 ns | 35.01 ns |  0.61 |
|                                    |               |            |          |          |       |
|                  Logger_ForContext | .NET Core 2.1 | 2,519.0 ns | 15.37 ns | 13.62 ns |  1.00 |
|                  Logger_ForContext | .NET Core 3.1 |   961.8 ns |  4.67 ns |  4.36 ns |  0.38 |
|                                    |               |            |          |          |       |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 2.1 | 1,745.7 ns | 11.95 ns | 10.60 ns |  1.00 |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 3.1 |   193.8 ns |  2.35 ns |  2.20 ns |  0.11 |

## Windows

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-3840QM CPU 2.80GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.300
  [Host]     : .NET Core 3.1.4 (CoreCLR 4.700.20.20201, CoreFX 4.700.20.22101), X64 RyuJIT
  Job-KEWXME : .NET Core 2.1.16 (CoreCLR 4.6.28516.03, CoreFX 4.6.28516.10), X64 RyuJIT
  Job-KKEVOV : .NET Core 3.1.4 (CoreCLR 4.700.20.20201, CoreFX 4.700.20.22101), X64 RyuJIT


```
|                             Method |       Runtime |       Mean |     Error |    StdDev | Ratio |
|----------------------------------- |-------------- |-----------:|----------:|----------:|------:|
|          Filter_MatchingFromSource | .NET Core 2.1 | 8,088.7 ns | 120.48 ns | 112.70 ns |  1.00 |
|          Filter_MatchingFromSource | .NET Core 3.1 | 3,369.0 ns |  71.49 ns |  73.42 ns |  0.42 |
|                                    |               |            |           |           |       |
|                  Logger_ForContext | .NET Core 2.1 | 6,322.1 ns |  98.18 ns |  91.84 ns |  1.00 |
|                  Logger_ForContext | .NET Core 3.1 |   785.6 ns |   9.05 ns |   8.47 ns |  0.12 |
|                                    |               |            |           |           |       |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 2.1 | 5,619.1 ns |  20.98 ns |  18.60 ns |  1.00 |
| LevelOverrideMap_GetEffectiveLevel | .NET Core 3.1 |   185.4 ns |   1.63 ns |   1.44 ns |  0.03 |

