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
|                 Bare | core31 | .NET Core 3.1 |  10.273 ns | 0.1364 ns | 0.2041 ns |  1.00 |    0.00 |
|         PushProperty | core31 | .NET Core 3.1 |  94.590 ns | 1.2954 ns | 1.9389 ns |  9.21 |    0.31 |
|   PushPropertyNested | core31 | .NET Core 3.1 | 185.845 ns | 2.2189 ns | 3.3212 ns | 18.10 |    0.42 |
| PushPropertyEnriched | core31 | .NET Core 3.1 | 165.161 ns | 4.2143 ns | 6.3077 ns | 16.09 |    0.76 |
|                      |        |               |            |           |           |       |         |
|                 Bare |  net48 |      .NET 4.8 |  10.428 ns | 0.1287 ns | 0.1927 ns |  1.00 |    0.00 |
|         PushProperty |  net48 |      .NET 4.8 |  77.650 ns | 0.9898 ns | 1.4815 ns |  7.45 |    0.18 |
|   PushPropertyNested |  net48 |      .NET 4.8 | 149.741 ns | 1.9308 ns | 2.8899 ns | 14.36 |    0.32 |
| PushPropertyEnriched |  net48 |      .NET 4.8 | 152.414 ns | 2.3360 ns | 3.4241 ns | 14.63 |    0.43 |
|                      |        |               |            |           |           |       |         |
|                 Bare |  net50 | .NET Core 5.0 |   9.624 ns | 0.1506 ns | 0.2254 ns |  1.00 |    0.00 |
|         PushProperty |  net50 | .NET Core 5.0 |  88.645 ns | 1.1491 ns | 1.7199 ns |  9.22 |    0.37 |
|   PushPropertyNested |  net50 | .NET Core 5.0 | 182.442 ns | 2.1069 ns | 3.1535 ns | 18.97 |    0.69 |
| PushPropertyEnriched |  net50 | .NET Core 5.0 | 151.947 ns | 2.3082 ns | 3.4548 ns | 15.80 |    0.49 |
