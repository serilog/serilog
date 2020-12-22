``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.404
  [Host]          : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|         Method |             Job |       Jit |       Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------------- |---------- |-------------- |----------:|----------:|----------:|------:|--------:|
|            Off |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.746 ns | 0.0383 ns | 0.0573 ns |  1.00 |    0.00 |
| LevelSwitchOff |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  3.028 ns | 0.0372 ns | 0.0556 ns |  1.10 |    0.02 |
| MinimumLevelOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10.732 ns | 0.1046 ns | 0.1534 ns |  3.91 |    0.09 |
|  LevelSwitchOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10.028 ns | 0.0777 ns | 0.1114 ns |  3.65 |    0.08 |
|                |                 |           |               |           |           |           |       |         |
|            Off | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.495 ns | 0.0295 ns | 0.0442 ns |  1.00 |    0.00 |
| LevelSwitchOff | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.773 ns | 0.0366 ns | 0.0548 ns |  1.11 |    0.02 |
| MinimumLevelOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.857 ns | 0.0652 ns | 0.0976 ns |  4.35 |    0.08 |
|  LevelSwitchOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.467 ns | 0.0822 ns | 0.1230 ns |  4.20 |    0.09 |
|                |                 |           |               |           |           |           |       |         |
|            Off |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.498 ns | 0.0405 ns | 0.0607 ns |  1.00 |    0.00 |
| LevelSwitchOff |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.760 ns | 0.0311 ns | 0.0456 ns |  1.11 |    0.03 |
| MinimumLevelOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.832 ns | 0.0785 ns | 0.1175 ns |  4.34 |    0.12 |
|  LevelSwitchOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.448 ns | 0.0774 ns | 0.1158 ns |  4.18 |    0.10 |
