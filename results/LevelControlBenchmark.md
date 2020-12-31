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
|         Method |    Job |       Runtime |     Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |------- |-------------- |---------:|----------:|----------:|------:|--------:|
|            Off | core31 | .NET Core 3.1 | 1.687 ns | 0.0276 ns | 0.0413 ns |  1.00 |    0.00 |
| LevelSwitchOff | core31 | .NET Core 3.1 | 1.328 ns | 0.0310 ns | 0.0464 ns |  0.79 |    0.03 |
| MinimumLevelOn | core31 | .NET Core 3.1 | 8.282 ns | 0.1502 ns | 0.2248 ns |  4.91 |    0.16 |
|  LevelSwitchOn | core31 | .NET Core 3.1 | 8.247 ns | 0.1223 ns | 0.1831 ns |  4.89 |    0.16 |
|                |        |               |          |           |           |       |         |
|            Off |  net48 |      .NET 4.8 | 1.493 ns | 0.0418 ns | 0.0626 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net48 |      .NET 4.8 | 1.479 ns | 0.0310 ns | 0.0465 ns |  0.99 |    0.03 |
| MinimumLevelOn |  net48 |      .NET 4.8 | 7.883 ns | 0.1446 ns | 0.2165 ns |  5.29 |    0.25 |
|  LevelSwitchOn |  net48 |      .NET 4.8 | 8.369 ns | 0.1511 ns | 0.2262 ns |  5.61 |    0.27 |
|                |        |               |          |           |           |       |         |
|            Off |  net50 | .NET Core 5.0 | 1.477 ns | 0.0310 ns | 0.0465 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net50 | .NET Core 5.0 | 1.534 ns | 0.0349 ns | 0.0522 ns |  1.04 |    0.05 |
| MinimumLevelOn |  net50 | .NET Core 5.0 | 8.103 ns | 0.1446 ns | 0.2164 ns |  5.49 |    0.22 |
|  LevelSwitchOn |  net50 | .NET Core 5.0 | 8.394 ns | 0.1180 ns | 0.1767 ns |  5.69 |    0.22 |
