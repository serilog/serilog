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
|         Method |    Job |       Runtime |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|--------------- |------- |-------------- |----------:|----------:|----------:|----------:|------:|--------:|
|            Off | core31 | .NET Core 3.1 |  2.590 ns | 0.0631 ns | 0.0944 ns |  2.582 ns |  1.00 |    0.00 |
| LevelSwitchOff | core31 | .NET Core 3.1 |  2.919 ns | 0.0592 ns | 0.0887 ns |  2.904 ns |  1.13 |    0.07 |
| MinimumLevelOn | core31 | .NET Core 3.1 | 10.359 ns | 0.1397 ns | 0.2091 ns | 10.430 ns |  4.00 |    0.17 |
|  LevelSwitchOn | core31 | .NET Core 3.1 |  9.510 ns | 0.1276 ns | 0.1911 ns |  9.514 ns |  3.68 |    0.14 |
|                |        |               |           |           |           |           |       |         |
|            Off |  net48 |      .NET 4.8 |  2.378 ns | 0.0550 ns | 0.0807 ns |  2.351 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net48 |      .NET 4.8 |  2.595 ns | 0.0302 ns | 0.0424 ns |  2.598 ns |  1.09 |    0.04 |
| MinimumLevelOn |  net48 |      .NET 4.8 |  9.965 ns | 0.0534 ns | 0.0783 ns |  9.979 ns |  4.20 |    0.14 |
|  LevelSwitchOn |  net48 |      .NET 4.8 |  9.663 ns | 0.0570 ns | 0.0835 ns |  9.676 ns |  4.07 |    0.14 |
|                |        |               |           |           |           |           |       |         |
|            Off |  net50 | .NET Core 5.0 |  2.106 ns | 0.0342 ns | 0.0491 ns |  2.090 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net50 | .NET Core 5.0 |  2.826 ns | 0.2570 ns | 0.3768 ns |  2.501 ns |  1.34 |    0.19 |
| MinimumLevelOn |  net50 | .NET Core 5.0 |  9.049 ns | 0.0535 ns | 0.0750 ns |  9.062 ns |  4.30 |    0.10 |
|  LevelSwitchOn |  net50 | .NET Core 5.0 |  9.286 ns | 0.0433 ns | 0.0621 ns |  9.284 ns |  4.41 |    0.09 |
