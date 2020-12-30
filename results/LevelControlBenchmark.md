``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.101
  [Host] : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
  core31 : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48  : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net50  : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT

Jit=RyuJit  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
|         Method |    Job |       Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |------- |-------------- |----------:|----------:|----------:|------:|--------:|
|            Off | core31 | .NET Core 3.1 |  2.637 ns | 0.0551 ns | 0.0824 ns |  1.00 |    0.00 |
| LevelSwitchOff | core31 | .NET Core 3.1 |  2.931 ns | 0.0574 ns | 0.0859 ns |  1.11 |    0.04 |
| MinimumLevelOn | core31 | .NET Core 3.1 | 10.377 ns | 0.1309 ns | 0.1919 ns |  3.95 |    0.12 |
|  LevelSwitchOn | core31 | .NET Core 3.1 |  9.562 ns | 0.1314 ns | 0.1967 ns |  3.63 |    0.15 |
|                |        |               |           |           |           |       |         |
|            Off |  net48 |      .NET 4.8 |  2.411 ns | 0.0539 ns | 0.0807 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net48 |      .NET 4.8 |  2.678 ns | 0.0670 ns | 0.1003 ns |  1.11 |    0.06 |
| MinimumLevelOn |  net48 |      .NET 4.8 | 10.280 ns | 0.1164 ns | 0.1742 ns |  4.27 |    0.16 |
|  LevelSwitchOn |  net48 |      .NET 4.8 |  9.973 ns | 0.1051 ns | 0.1573 ns |  4.14 |    0.16 |
|                |        |               |           |           |           |       |         |
|            Off |  net50 | .NET Core 5.0 |  2.164 ns | 0.0252 ns | 0.0370 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net50 | .NET Core 5.0 |  3.293 ns | 0.0983 ns | 0.1471 ns |  1.52 |    0.08 |
| MinimumLevelOn |  net50 | .NET Core 5.0 | 10.275 ns | 0.1260 ns | 0.1847 ns |  4.75 |    0.11 |
|  LevelSwitchOn |  net50 | .NET Core 5.0 |  9.520 ns | 0.1326 ns | 0.1985 ns |  4.40 |    0.13 |
