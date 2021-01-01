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
|                 Bare | core31 | .NET Core 3.1 |   9.565 ns | 0.0408 ns | 0.0598 ns |  1.00 |    0.00 |
|         PushProperty | core31 | .NET Core 3.1 |  88.911 ns | 0.9107 ns | 1.3349 ns |  9.30 |    0.14 |
|   PushPropertyNested | core31 | .NET Core 3.1 | 176.014 ns | 0.8598 ns | 1.2331 ns | 18.40 |    0.19 |
| PushPropertyEnriched | core31 | .NET Core 3.1 | 160.979 ns | 0.8159 ns | 1.1959 ns | 16.83 |    0.18 |
|                      |        |               |            |           |           |       |         |
|                 Bare |  net48 |      .NET 4.8 |  10.177 ns | 0.0560 ns | 0.0804 ns |  1.00 |    0.00 |
|         PushProperty |  net48 |      .NET 4.8 |  75.953 ns | 0.4055 ns | 0.5943 ns |  7.46 |    0.09 |
|   PushPropertyNested |  net48 |      .NET 4.8 | 146.940 ns | 0.7974 ns | 1.1936 ns | 14.44 |    0.18 |
| PushPropertyEnriched |  net48 |      .NET 4.8 | 148.863 ns | 0.6357 ns | 0.9317 ns | 14.63 |    0.16 |
|                      |        |               |            |           |           |       |         |
|                 Bare |  net50 | .NET Core 5.0 |   9.391 ns | 0.0441 ns | 0.0659 ns |  1.00 |    0.00 |
|         PushProperty |  net50 | .NET Core 5.0 |  86.080 ns | 0.3769 ns | 0.5406 ns |  9.17 |    0.10 |
|   PushPropertyNested |  net50 | .NET Core 5.0 | 178.609 ns | 0.8696 ns | 1.2747 ns | 19.02 |    0.21 |
| PushPropertyEnriched |  net50 | .NET Core 5.0 | 147.602 ns | 0.8531 ns | 1.2504 ns | 15.72 |    0.17 |
