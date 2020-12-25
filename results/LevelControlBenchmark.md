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
|            Off | core31 | .NET Core 3.1 |  2.736 ns | 0.0348 ns | 0.0521 ns |  1.00 |    0.00 |
| LevelSwitchOff | core31 | .NET Core 3.1 |  2.603 ns | 0.0304 ns | 0.0455 ns |  0.95 |    0.02 |
| MinimumLevelOn | core31 | .NET Core 3.1 | 10.027 ns | 0.0634 ns | 0.0910 ns |  3.67 |    0.07 |
|  LevelSwitchOn | core31 | .NET Core 3.1 | 10.050 ns | 0.0786 ns | 0.1176 ns |  3.67 |    0.09 |
|                |        |               |           |           |           |       |         |
|            Off |  net48 |      .NET 4.8 |  2.476 ns | 0.0274 ns | 0.0410 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net48 |      .NET 4.8 |  2.751 ns | 0.0320 ns | 0.0479 ns |  1.11 |    0.03 |
| MinimumLevelOn |  net48 |      .NET 4.8 | 10.726 ns | 0.0647 ns | 0.0968 ns |  4.33 |    0.08 |
|  LevelSwitchOn |  net48 |      .NET 4.8 | 10.376 ns | 0.0746 ns | 0.1116 ns |  4.19 |    0.08 |
|                |        |               |           |           |           |       |         |
|            Off |  net50 | .NET Core 5.0 |  2.461 ns | 0.1666 ns | 0.2494 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net50 | .NET Core 5.0 |  3.342 ns | 0.0357 ns | 0.0534 ns |  1.37 |    0.14 |
| MinimumLevelOn |  net50 | .NET Core 5.0 | 10.312 ns | 0.0611 ns | 0.0915 ns |  4.23 |    0.44 |
|  LevelSwitchOn |  net50 | .NET Core 5.0 |  9.428 ns | 0.0629 ns | 0.0941 ns |  3.87 |    0.39 |
