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
|            Off | core31 | .NET Core 3.1 |  2.207 ns | 0.0171 ns | 0.0240 ns |  2.212 ns |  1.00 |    0.00 |
| LevelSwitchOff | core31 | .NET Core 3.1 |  2.934 ns | 0.2233 ns | 0.3273 ns |  3.143 ns |  1.34 |    0.15 |
| MinimumLevelOn | core31 | .NET Core 3.1 | 10.128 ns | 0.2134 ns | 0.3194 ns | 10.130 ns |  4.60 |    0.15 |
|  LevelSwitchOn | core31 | .NET Core 3.1 |  9.575 ns | 0.0831 ns | 0.1219 ns |  9.615 ns |  4.34 |    0.07 |
|                |        |               |           |           |           |           |       |         |
|            Off |  net48 |      .NET 4.8 |  2.460 ns | 0.0358 ns | 0.0536 ns |  2.463 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net48 |      .NET 4.8 |  2.707 ns | 0.0378 ns | 0.0566 ns |  2.689 ns |  1.10 |    0.02 |
| MinimumLevelOn |  net48 |      .NET 4.8 | 10.422 ns | 0.0891 ns | 0.1306 ns | 10.424 ns |  4.24 |    0.11 |
|  LevelSwitchOn |  net48 |      .NET 4.8 | 10.147 ns | 0.1089 ns | 0.1629 ns | 10.162 ns |  4.13 |    0.11 |
|                |        |               |           |           |           |           |       |         |
|            Off |  net50 | .NET Core 5.0 |  2.187 ns | 0.0192 ns | 0.0288 ns |  2.192 ns |  1.00 |    0.00 |
| LevelSwitchOff |  net50 | .NET Core 5.0 |  2.574 ns | 0.0335 ns | 0.0502 ns |  2.573 ns |  1.18 |    0.03 |
| MinimumLevelOn |  net50 | .NET Core 5.0 | 10.333 ns | 0.0928 ns | 0.1390 ns | 10.328 ns |  4.73 |    0.09 |
|  LevelSwitchOn |  net50 | .NET Core 5.0 |  9.267 ns | 0.1091 ns | 0.1599 ns |  9.325 ns |  4.24 |    0.10 |
