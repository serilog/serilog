``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.403
  [Host]          : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|         Method |             Job |       Jit |       Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------------- |---------- |-------------- |----------:|----------:|----------:|------:|--------:|
|            Off |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.236 ns | 0.1015 ns | 0.1519 ns |  1.00 |    0.00 |
| LevelSwitchOff |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  2.756 ns | 0.2169 ns | 0.3246 ns |  1.25 |    0.22 |
| MinimumLevelOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10.295 ns | 0.1226 ns | 0.1758 ns |  4.63 |    0.27 |
|  LevelSwitchOn |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.415 ns | 0.0345 ns | 0.0516 ns |  4.23 |    0.28 |
|                |                 |           |               |           |           |           |       |         |
|            Off | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.596 ns | 0.0175 ns | 0.0251 ns |  1.00 |    0.00 |
| LevelSwitchOff | net48 LegacyJit | LegacyJit |      .NET 4.8 |  2.863 ns | 0.0248 ns | 0.0355 ns |  1.10 |    0.02 |
| MinimumLevelOn | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.970 ns | 0.0446 ns | 0.0654 ns |  3.84 |    0.05 |
|  LevelSwitchOn | net48 LegacyJit | LegacyJit |      .NET 4.8 |  9.774 ns | 0.0564 ns | 0.0826 ns |  3.77 |    0.04 |
|                |                 |           |               |           |           |           |       |         |
|            Off |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.596 ns | 0.0156 ns | 0.0234 ns |  1.00 |    0.00 |
| LevelSwitchOff |    net48 RyuJit |    RyuJit |      .NET 4.8 |  2.864 ns | 0.0149 ns | 0.0219 ns |  1.10 |    0.01 |
| MinimumLevelOn |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.996 ns | 0.0642 ns | 0.0900 ns |  3.85 |    0.05 |
|  LevelSwitchOn |    net48 RyuJit |    RyuJit |      .NET 4.8 |  9.704 ns | 0.0391 ns | 0.0573 ns |  3.74 |    0.04 |
