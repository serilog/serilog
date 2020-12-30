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
|                 Bare | core31 | .NET Core 3.1 |   9.894 ns | 0.2756 ns | 0.4126 ns |  1.00 |    0.00 |
|         PushProperty | core31 | .NET Core 3.1 |  92.300 ns | 1.8051 ns | 2.7018 ns |  9.34 |    0.29 |
|   PushPropertyNested | core31 | .NET Core 3.1 | 183.670 ns | 2.9377 ns | 4.3970 ns | 18.60 |    1.10 |
| PushPropertyEnriched | core31 | .NET Core 3.1 | 164.790 ns | 2.8412 ns | 4.2525 ns | 16.68 |    0.69 |
|                      |        |               |            |           |           |       |         |
|                 Bare |  net48 |      .NET 4.8 |  10.400 ns | 0.1260 ns | 0.1886 ns |  1.00 |    0.00 |
|         PushProperty |  net48 |      .NET 4.8 |  78.164 ns | 1.0593 ns | 1.5193 ns |  7.52 |    0.18 |
|   PushPropertyNested |  net48 |      .NET 4.8 | 150.239 ns | 2.3379 ns | 3.4992 ns | 14.45 |    0.44 |
| PushPropertyEnriched |  net48 |      .NET 4.8 | 153.557 ns | 2.3386 ns | 3.5003 ns | 14.77 |    0.43 |
|                      |        |               |            |           |           |       |         |
|                 Bare |  net50 | .NET Core 5.0 |   9.700 ns | 0.1365 ns | 0.2042 ns |  1.00 |    0.00 |
|         PushProperty |  net50 | .NET Core 5.0 |  88.961 ns | 0.9557 ns | 1.4008 ns |  9.17 |    0.29 |
|   PushPropertyNested |  net50 | .NET Core 5.0 | 184.506 ns | 2.4167 ns | 3.6171 ns | 19.03 |    0.61 |
| PushPropertyEnriched |  net50 | .NET Core 5.0 | 154.530 ns | 2.5591 ns | 3.8303 ns | 15.94 |    0.54 |
