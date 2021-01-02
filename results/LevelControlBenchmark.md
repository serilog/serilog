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
|            Off | core31 | .NET Core 3.1 |  2.181 ns | 0.0547 ns | 0.0819 ns |  1.00 |    0.00 |
| LevelSwitchOff | core31 | .NET Core 3.1 |  2.525 ns | 0.0506 ns | 0.0758 ns |  1.16 |    0.06 |
| MinimumLevelOn | core31 | .NET Core 3.1 | 10.172 ns | 0.1124 ns | 0.1683 ns |  4.67 |    0.20 |
|  LevelSwitchOn | core31 | .NET Core 3.1 |  9.568 ns | 0.0967 ns | 0.1447 ns |  4.39 |    0.16 |
|                |        |               |           |           |           |       |         |
|            Off |  net48 |      .NET 4.8 |  2.416 ns | 0.0413 ns | 0.0618 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net48 |      .NET 4.8 |  2.623 ns | 0.0392 ns | 0.0586 ns |  1.09 |    0.03 |
| MinimumLevelOn |  net48 |      .NET 4.8 | 10.130 ns | 0.0992 ns | 0.1485 ns |  4.20 |    0.13 |
|  LevelSwitchOn |  net48 |      .NET 4.8 |  9.876 ns | 0.1053 ns | 0.1577 ns |  4.09 |    0.11 |
|                |        |               |           |           |           |       |         |
|            Off |  net50 | .NET Core 5.0 |  2.124 ns | 0.0172 ns | 0.0257 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net50 | .NET Core 5.0 |  2.520 ns | 0.0475 ns | 0.0711 ns |  1.19 |    0.04 |
| MinimumLevelOn |  net50 | .NET Core 5.0 | 10.104 ns | 0.0951 ns | 0.1423 ns |  4.76 |    0.09 |
|  LevelSwitchOn |  net50 | .NET Core 5.0 |  9.118 ns | 0.1029 ns | 0.1540 ns |  4.29 |    0.09 |
