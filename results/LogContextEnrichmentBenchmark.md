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
|                 Bare | core31 | .NET Core 3.1 |  10.793 ns | 0.0767 ns | 0.1148 ns |  1.00 |    0.00 |
|         PushProperty | core31 | .NET Core 3.1 |  96.193 ns | 1.0874 ns | 1.6276 ns |  8.91 |    0.20 |
|   PushPropertyNested | core31 | .NET Core 3.1 | 189.030 ns | 1.8444 ns | 2.7606 ns | 17.52 |    0.31 |
| PushPropertyEnriched | core31 | .NET Core 3.1 | 164.581 ns | 1.5360 ns | 2.2989 ns | 15.25 |    0.27 |
|                      |        |               |            |           |           |       |         |
|                 Bare |  net48 |      .NET 4.8 |  10.109 ns | 0.0762 ns | 0.1140 ns |  1.00 |    0.00 |
|         PushProperty |  net48 |      .NET 4.8 |  79.028 ns | 1.0544 ns | 1.5782 ns |  7.82 |    0.18 |
|   PushPropertyNested |  net48 |      .NET 4.8 | 153.148 ns | 2.2992 ns | 3.3702 ns | 15.16 |    0.36 |
| PushPropertyEnriched |  net48 |      .NET 4.8 | 156.377 ns | 1.4813 ns | 2.1713 ns | 15.48 |    0.28 |
|                      |        |               |            |           |           |       |         |
|                 Bare |  net50 | .NET Core 5.0 |   9.417 ns | 0.0734 ns | 0.1098 ns |  1.00 |    0.00 |
|         PushProperty |  net50 | .NET Core 5.0 |  88.921 ns | 0.8952 ns | 1.3398 ns |  9.44 |    0.19 |
|   PushPropertyNested |  net50 | .NET Core 5.0 | 184.869 ns | 1.6611 ns | 2.4863 ns | 19.63 |    0.30 |
| PushPropertyEnriched |  net50 | .NET Core 5.0 | 152.968 ns | 1.5828 ns | 2.3690 ns | 16.25 |    0.33 |
