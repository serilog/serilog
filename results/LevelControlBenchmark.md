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
|            Off | core31 | .NET Core 3.1 |  2.728 ns | 0.0370 ns | 0.0553 ns |  1.00 |    0.00 |
| LevelSwitchOff | core31 | .NET Core 3.1 |  2.589 ns | 0.0232 ns | 0.0340 ns |  0.95 |    0.02 |
| MinimumLevelOn | core31 | .NET Core 3.1 | 10.102 ns | 0.0791 ns | 0.1159 ns |  3.70 |    0.09 |
|  LevelSwitchOn | core31 | .NET Core 3.1 | 10.041 ns | 0.0627 ns | 0.0938 ns |  3.68 |    0.07 |
|                |        |               |           |           |           |       |         |
|            Off |  net48 |      .NET 4.8 |  2.481 ns | 0.0227 ns | 0.0339 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net48 |      .NET 4.8 |  2.757 ns | 0.0321 ns | 0.0481 ns |  1.11 |    0.03 |
| MinimumLevelOn |  net48 |      .NET 4.8 | 10.756 ns | 0.0560 ns | 0.0838 ns |  4.34 |    0.06 |
|  LevelSwitchOn |  net48 |      .NET 4.8 | 10.365 ns | 0.0562 ns | 0.0841 ns |  4.18 |    0.06 |
|                |        |               |           |           |           |       |         |
|            Off |  net50 | .NET Core 5.0 |  2.717 ns | 0.0298 ns | 0.0446 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net50 | .NET Core 5.0 |  3.152 ns | 0.1232 ns | 0.1806 ns |  1.16 |    0.07 |
| MinimumLevelOn |  net50 | .NET Core 5.0 | 10.259 ns | 0.0784 ns | 0.1173 ns |  3.78 |    0.07 |
|  LevelSwitchOn |  net50 | .NET Core 5.0 |  9.890 ns | 0.0775 ns | 0.1160 ns |  3.64 |    0.08 |
