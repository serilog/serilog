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
|               Method |    Job |       Runtime |      Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------- |------- |-------------- |----------:|---------:|---------:|------:|--------:|
|                 Bare | core31 | .NET Core 3.1 |  10.18 ns | 0.250 ns | 0.374 ns |  1.00 |    0.00 |
|         PushProperty | core31 | .NET Core 3.1 |  98.05 ns | 0.567 ns | 0.849 ns |  9.64 |    0.38 |
|   PushPropertyNested | core31 | .NET Core 3.1 | 189.76 ns | 1.679 ns | 2.514 ns | 18.66 |    0.57 |
| PushPropertyEnriched | core31 | .NET Core 3.1 | 170.37 ns | 2.000 ns | 2.994 ns | 16.75 |    0.45 |
|                      |        |               |           |          |          |       |         |
|                 Bare |  net48 |      .NET 4.8 |  10.89 ns | 0.070 ns | 0.105 ns |  1.00 |    0.00 |
|         PushProperty |  net48 |      .NET 4.8 |  80.35 ns | 0.760 ns | 1.137 ns |  7.38 |    0.12 |
|   PushPropertyNested |  net48 |      .NET 4.8 | 155.62 ns | 1.539 ns | 2.207 ns | 14.29 |    0.28 |
| PushPropertyEnriched |  net48 |      .NET 4.8 | 160.37 ns | 2.580 ns | 3.862 ns | 14.72 |    0.43 |
|                      |        |               |           |          |          |       |         |
|                 Bare |  net50 | .NET Core 5.0 |  10.18 ns | 0.297 ns | 0.444 ns |  1.00 |    0.00 |
|         PushProperty |  net50 | .NET Core 5.0 |  91.66 ns | 0.773 ns | 1.109 ns |  9.05 |    0.39 |
|   PushPropertyNested |  net50 | .NET Core 5.0 | 190.62 ns | 1.931 ns | 2.890 ns | 18.75 |    0.60 |
| PushPropertyEnriched |  net50 | .NET Core 5.0 | 152.48 ns | 1.721 ns | 2.576 ns | 15.01 |    0.75 |
