``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]          : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|         Method |             Job |       Jit |       Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------------- |---------- |-------------- |----------:|----------:|----------:|------:|--------:|
|            Off |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.722 ns | 0.0456 ns | 0.0682 ns |  1.00 |    0.00 |
| LevelSwitchOff |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.597 ns | 0.0421 ns | 0.0631 ns |  0.95 |    0.04 |
| MinimumLevelOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.919 ns | 0.0905 ns | 0.1355 ns |  3.65 |    0.10 |
|  LevelSwitchOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10.155 ns | 0.0913 ns | 0.1366 ns |  3.73 |    0.11 |
|                |                 |           |               |           |           |           |       |         |
|            Off | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.474 ns | 0.0424 ns | 0.0635 ns |  1.00 |    0.00 |
| LevelSwitchOff | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.859 ns | 0.1129 ns | 0.1655 ns |  1.16 |    0.07 |
| MinimumLevelOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 11.530 ns | 0.5289 ns | 0.7752 ns |  4.66 |    0.33 |
|  LevelSwitchOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 11.021 ns | 0.2492 ns | 0.3652 ns |  4.46 |    0.19 |
|                |                 |           |               |           |           |           |       |         |
|            Off |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.617 ns | 0.0476 ns | 0.0713 ns |  1.00 |    0.00 |
| LevelSwitchOff |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.881 ns | 0.0678 ns | 0.0929 ns |  1.10 |    0.05 |
| MinimumLevelOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 13.428 ns | 1.7213 ns | 2.5230 ns |  5.13 |    0.94 |
|  LevelSwitchOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.694 ns | 0.1454 ns | 0.1890 ns |  4.10 |    0.13 |
