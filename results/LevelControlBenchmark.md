``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|         Method |             Job |       Jit |       Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------------- |---------- |-------------- |----------:|----------:|----------:|------:|--------:|
|            Off |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.574 ns | 0.0147 ns | 0.0216 ns |  1.00 |    0.00 |
| LevelSwitchOff |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.889 ns | 0.0152 ns | 0.0217 ns |  1.12 |    0.01 |
| MinimumLevelOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10.278 ns | 0.0348 ns | 0.0509 ns |  3.99 |    0.03 |
|  LevelSwitchOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.452 ns | 0.0310 ns | 0.0465 ns |  3.67 |    0.03 |
|                |                 |           |               |           |           |           |       |         |
|            Off | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.376 ns | 0.0137 ns | 0.0201 ns |  1.00 |    0.00 |
| LevelSwitchOff | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.622 ns | 0.0138 ns | 0.0203 ns |  1.10 |    0.01 |
| MinimumLevelOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.116 ns | 0.0387 ns | 0.0567 ns |  4.26 |    0.04 |
|  LevelSwitchOn | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.869 ns | 0.0550 ns | 0.0806 ns |  4.15 |    0.04 |
|                |                 |           |               |           |           |           |       |         |
|            Off |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.374 ns | 0.0102 ns | 0.0140 ns |  1.00 |    0.00 |
| LevelSwitchOff |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.627 ns | 0.0157 ns | 0.0225 ns |  1.11 |    0.01 |
| MinimumLevelOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.133 ns | 0.0357 ns | 0.0524 ns |  4.27 |    0.03 |
|  LevelSwitchOn |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.878 ns | 0.0354 ns | 0.0497 ns |  4.16 |    0.03 |
