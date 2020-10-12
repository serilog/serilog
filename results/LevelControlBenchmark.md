``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|         Method |             Job |       Jit |       Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------------- |---------- |-------------- |----------:|----------:|----------:|------:|--------:|
|            Off |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.241 ns | 0.0346 ns | 0.0485 ns |  1.00 |    0.00 |
| LevelSwitchOff |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.710 ns | 0.0441 ns | 0.0661 ns |  1.21 |    0.04 |
| MinimumLevelOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10.642 ns | 0.1192 ns | 0.1784 ns |  4.75 |    0.13 |
|  LevelSwitchOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.812 ns | 0.0954 ns | 0.1428 ns |  4.38 |    0.12 |
|                |                 |           |               |           |           |           |       |         |
|            Off | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3.227 ns | 0.0520 ns | 0.0779 ns |  1.00 |    0.00 |
| LevelSwitchOff | net48 LegacyJit | LegacyJit |      .NET 4.8 |  3.535 ns | 0.0676 ns | 0.1012 ns |  1.10 |    0.02 |
| MinimumLevelOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.701 ns | 0.1216 ns | 0.1782 ns |  3.32 |    0.10 |
|  LevelSwitchOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.607 ns | 0.0930 ns | 0.1393 ns |  3.29 |    0.09 |
|                |                 |           |               |           |           |           |       |         |
|            Off |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3.191 ns | 0.0469 ns | 0.0703 ns |  1.00 |    0.00 |
| LevelSwitchOff |    net48 RyuJit |    RyuJit |      .NET 4.8 |  3.500 ns | 0.0483 ns | 0.0723 ns |  1.10 |    0.03 |
| MinimumLevelOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.664 ns | 0.1128 ns | 0.1689 ns |  3.34 |    0.10 |
|  LevelSwitchOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.619 ns | 0.0868 ns | 0.1299 ns |  3.33 |    0.09 |
