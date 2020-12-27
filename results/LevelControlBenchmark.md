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
|            Off | core31 | .NET Core 3.1 |  2.621 ns | 0.0549 ns | 0.0821 ns |  1.00 |    0.00 |
| LevelSwitchOff | core31 | .NET Core 3.1 |  2.484 ns | 0.0464 ns | 0.0694 ns |  0.95 |    0.04 |
| MinimumLevelOn | core31 | .NET Core 3.1 |  9.606 ns | 0.0687 ns | 0.1028 ns |  3.67 |    0.12 |
|  LevelSwitchOn | core31 | .NET Core 3.1 |  9.567 ns | 0.0978 ns | 0.1464 ns |  3.65 |    0.13 |
|                |        |               |           |           |           |       |         |
|            Off |  net48 |      .NET 4.8 |  2.368 ns | 0.0362 ns | 0.0542 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net48 |      .NET 4.8 |  2.616 ns | 0.0438 ns | 0.0655 ns |  1.11 |    0.03 |
| MinimumLevelOn |  net48 |      .NET 4.8 | 10.106 ns | 0.0975 ns | 0.1460 ns |  4.27 |    0.11 |
|  LevelSwitchOn |  net48 |      .NET 4.8 |  9.849 ns | 0.0968 ns | 0.1448 ns |  4.16 |    0.11 |
|                |        |               |           |           |           |       |         |
|            Off |  net50 | .NET Core 5.0 |  2.616 ns | 0.0563 ns | 0.0843 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net50 | .NET Core 5.0 |  3.026 ns | 0.1177 ns | 0.1762 ns |  1.16 |    0.08 |
| MinimumLevelOn |  net50 | .NET Core 5.0 |  9.514 ns | 0.3456 ns | 0.5173 ns |  3.64 |    0.23 |
|  LevelSwitchOn |  net50 | .NET Core 5.0 |  9.060 ns | 0.0964 ns | 0.1442 ns |  3.47 |    0.13 |
