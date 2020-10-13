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
|            Off |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.571 ns | 0.0200 ns | 0.0299 ns |  1.00 |    0.00 |
| LevelSwitchOff |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.441 ns | 0.0158 ns | 0.0227 ns |  0.95 |    0.01 |
| MinimumLevelOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.499 ns | 0.0538 ns | 0.0754 ns |  3.69 |    0.05 |
|  LevelSwitchOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.711 ns | 0.3076 ns | 0.4605 ns |  3.78 |    0.18 |
|                |                 |           |               |           |           |           |       |         |
|            Off | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.346 ns | 0.0152 ns | 0.0219 ns |  1.00 |    0.00 |
| LevelSwitchOff | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.594 ns | 0.0155 ns | 0.0222 ns |  1.11 |    0.01 |
| MinimumLevelOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.024 ns | 0.0392 ns | 0.0563 ns |  4.27 |    0.05 |
|  LevelSwitchOn | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.742 ns | 0.0348 ns | 0.0499 ns |  4.15 |    0.04 |
|                |                 |           |               |           |           |           |       |         |
|            Off |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.340 ns | 0.0118 ns | 0.0169 ns |  1.00 |    0.00 |
| LevelSwitchOff |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.599 ns | 0.0159 ns | 0.0233 ns |  1.11 |    0.01 |
| MinimumLevelOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.001 ns | 0.0354 ns | 0.0496 ns |  4.27 |    0.04 |
|  LevelSwitchOn |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.728 ns | 0.0401 ns | 0.0562 ns |  4.16 |    0.04 |
