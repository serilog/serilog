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
|            Off | core31 | .NET Core 3.1 |  2.617 ns | 0.0605 ns | 0.0906 ns |  1.00 |    0.00 |
| LevelSwitchOff | core31 | .NET Core 3.1 |  2.937 ns | 0.0595 ns | 0.0891 ns |  1.12 |    0.07 |
| MinimumLevelOn | core31 | .NET Core 3.1 | 10.379 ns | 0.1104 ns | 0.1652 ns |  3.97 |    0.16 |
|  LevelSwitchOn | core31 | .NET Core 3.1 |  9.542 ns | 0.1173 ns | 0.1756 ns |  3.65 |    0.14 |
|                |        |               |           |           |           |       |         |
|            Off |  net48 |      .NET 4.8 |  2.413 ns | 0.0614 ns | 0.0920 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net48 |      .NET 4.8 |  2.661 ns | 0.0660 ns | 0.0987 ns |  1.10 |    0.07 |
| MinimumLevelOn |  net48 |      .NET 4.8 | 10.233 ns | 0.1305 ns | 0.1953 ns |  4.25 |    0.18 |
|  LevelSwitchOn |  net48 |      .NET 4.8 |  9.943 ns | 0.1206 ns | 0.1767 ns |  4.12 |    0.20 |
|                |        |               |           |           |           |       |         |
|            Off |  net50 | .NET Core 5.0 |  2.624 ns | 0.0686 ns | 0.1027 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net50 | .NET Core 5.0 |  2.897 ns | 0.0622 ns | 0.0931 ns |  1.11 |    0.06 |
| MinimumLevelOn |  net50 | .NET Core 5.0 |  9.577 ns | 0.3078 ns | 0.4608 ns |  3.65 |    0.23 |
|  LevelSwitchOn |  net50 | .NET Core 5.0 |  9.071 ns | 0.1499 ns | 0.2244 ns |  3.46 |    0.16 |
