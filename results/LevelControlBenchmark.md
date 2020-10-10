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
|            Off |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.660 ns | 0.0672 ns | 0.1006 ns |  1.00 |    0.00 |
| LevelSwitchOff |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.889 ns | 0.0602 ns | 0.0902 ns |  1.09 |    0.06 |
| MinimumLevelOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10.603 ns | 0.1539 ns | 0.2207 ns |  3.99 |    0.17 |
|  LevelSwitchOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.723 ns | 0.1358 ns | 0.2033 ns |  3.66 |    0.15 |
|                |                 |           |               |           |           |           |       |         |
|            Off | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.426 ns | 0.0666 ns | 0.0997 ns |  1.00 |    0.00 |
| LevelSwitchOff | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.678 ns | 0.0669 ns | 0.1001 ns |  1.11 |    0.06 |
| MinimumLevelOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.365 ns | 0.2081 ns | 0.3051 ns |  4.27 |    0.21 |
|  LevelSwitchOn | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.010 ns | 0.1245 ns | 0.1864 ns |  4.13 |    0.21 |
|                |                 |           |               |           |           |           |       |         |
|            Off |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.414 ns | 0.0500 ns | 0.0732 ns |  1.00 |    0.00 |
| LevelSwitchOff |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.684 ns | 0.0592 ns | 0.0887 ns |  1.11 |    0.03 |
| MinimumLevelOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.300 ns | 0.1275 ns | 0.1908 ns |  4.27 |    0.15 |
|  LevelSwitchOn |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.023 ns | 0.1184 ns | 0.1772 ns |  4.15 |    0.11 |
