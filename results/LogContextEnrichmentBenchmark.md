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
|                 Bare | core31 | .NET Core 3.1 |  10.208 ns | 0.2588 ns | 0.3873 ns |  1.00 |    0.00 |
|         PushProperty | core31 | .NET Core 3.1 |  97.032 ns | 0.6189 ns | 0.9263 ns |  9.52 |    0.38 |
|   PushPropertyNested | core31 | .NET Core 3.1 | 189.936 ns | 2.0943 ns | 3.1346 ns | 18.64 |    0.95 |
| PushPropertyEnriched | core31 | .NET Core 3.1 | 170.859 ns | 2.9529 ns | 4.4198 ns | 16.75 |    0.37 |
|                      |        |               |            |           |           |       |         |
|                 Bare |  net48 |      .NET 4.8 |  10.871 ns | 0.0917 ns | 0.1373 ns |  1.00 |    0.00 |
|         PushProperty |  net48 |      .NET 4.8 |  79.924 ns | 0.5461 ns | 0.8174 ns |  7.35 |    0.11 |
|   PushPropertyNested |  net48 |      .NET 4.8 | 156.158 ns | 1.1136 ns | 1.6323 ns | 14.38 |    0.23 |
| PushPropertyEnriched |  net48 |      .NET 4.8 | 157.533 ns | 1.3442 ns | 2.0119 ns | 14.49 |    0.26 |
|                      |        |               |            |           |           |       |         |
|                 Bare |  net50 | .NET Core 5.0 |   9.593 ns | 0.0706 ns | 0.1056 ns |  1.00 |    0.00 |
|         PushProperty |  net50 | .NET Core 5.0 |  94.491 ns | 2.1897 ns | 3.2096 ns |  9.85 |    0.33 |
|   PushPropertyNested |  net50 | .NET Core 5.0 | 186.049 ns | 2.0819 ns | 3.1161 ns | 19.40 |    0.34 |
| PushPropertyEnriched |  net50 | .NET Core 5.0 | 157.084 ns | 1.5678 ns | 2.3466 ns | 16.38 |    0.29 |
