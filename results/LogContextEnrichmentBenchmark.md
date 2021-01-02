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
|               Method |    Job |       Runtime |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |------- |-------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare | core31 | .NET Core 3.1 |   9.599 ns | 0.0865 ns | 0.1295 ns |  1.00 |    0.00 |
|         PushProperty | core31 | .NET Core 3.1 |  91.957 ns | 0.7064 ns | 1.0573 ns |  9.58 |    0.18 |
|   PushPropertyNested | core31 | .NET Core 3.1 | 183.758 ns | 1.5646 ns | 2.3419 ns | 19.15 |    0.39 |
| PushPropertyEnriched | core31 | .NET Core 3.1 | 161.603 ns | 1.6076 ns | 2.4062 ns | 16.84 |    0.36 |
|                      |        |               |            |           |           |       |         |
|                 Bare |  net48 |      .NET 4.8 |   9.875 ns | 0.1068 ns | 0.1599 ns |  1.00 |    0.00 |
|         PushProperty |  net48 |      .NET 4.8 |  77.416 ns | 0.7673 ns | 1.1485 ns |  7.84 |    0.18 |
|   PushPropertyNested |  net48 |      .NET 4.8 | 147.726 ns | 1.5648 ns | 2.3421 ns | 14.96 |    0.35 |
| PushPropertyEnriched |  net48 |      .NET 4.8 | 152.427 ns | 1.6779 ns | 2.5114 ns | 15.44 |    0.33 |
|                      |        |               |            |           |           |       |         |
|                 Bare |  net50 | .NET Core 5.0 |   9.194 ns | 0.0906 ns | 0.1356 ns |  1.00 |    0.00 |
|         PushProperty |  net50 | .NET Core 5.0 |  87.511 ns | 0.8647 ns | 1.2675 ns |  9.53 |    0.18 |
|   PushPropertyNested |  net50 | .NET Core 5.0 | 178.632 ns | 1.5394 ns | 2.3041 ns | 19.43 |    0.41 |
| PushPropertyEnriched |  net50 | .NET Core 5.0 | 150.330 ns | 1.7566 ns | 2.6292 ns | 16.35 |    0.33 |
