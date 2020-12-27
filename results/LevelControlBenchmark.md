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
|            Off | core31 | .NET Core 3.1 |  2.584 ns | 0.0288 ns | 0.0430 ns |  1.00 |    0.00 |
| LevelSwitchOff | core31 | .NET Core 3.1 |  2.925 ns | 0.0323 ns | 0.0473 ns |  1.13 |    0.03 |
| MinimumLevelOn | core31 | .NET Core 3.1 | 10.335 ns | 0.0723 ns | 0.1083 ns |  4.00 |    0.08 |
|  LevelSwitchOn | core31 | .NET Core 3.1 |  9.556 ns | 0.0827 ns | 0.1238 ns |  3.70 |    0.08 |
|                |        |               |           |           |           |       |         |
|            Off |  net48 |      .NET 4.8 |  3.090 ns | 0.0433 ns | 0.0649 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net48 |      .NET 4.8 |  3.362 ns | 0.0445 ns | 0.0667 ns |  1.09 |    0.03 |
| MinimumLevelOn |  net48 |      .NET 4.8 | 10.204 ns | 0.0806 ns | 0.1207 ns |  3.30 |    0.09 |
|  LevelSwitchOn |  net48 |      .NET 4.8 | 10.172 ns | 0.0709 ns | 0.1061 ns |  3.29 |    0.07 |
|                |        |               |           |           |           |       |         |
|            Off |  net50 | .NET Core 5.0 |  2.607 ns | 0.0352 ns | 0.0527 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net50 | .NET Core 5.0 |  2.539 ns | 0.0424 ns | 0.0634 ns |  0.97 |    0.04 |
| MinimumLevelOn |  net50 | .NET Core 5.0 |  9.446 ns | 0.0799 ns | 0.1195 ns |  3.63 |    0.08 |
|  LevelSwitchOn |  net50 | .NET Core 5.0 |  9.568 ns | 0.0907 ns | 0.1357 ns |  3.67 |    0.09 |
