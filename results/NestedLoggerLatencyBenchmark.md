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
|       Method |    Job |       Runtime |     Mean |    Error |   StdDev | Ratio | RatioSD |
|------------- |------- |-------------- |---------:|---------:|---------:|------:|--------:|
|   RootLogger | core31 | .NET Core 3.1 | 10.79 ns | 0.073 ns | 0.109 ns |  1.00 |    0.00 |
| NestedLogger | core31 | .NET Core 3.1 | 36.18 ns | 0.504 ns | 0.739 ns |  3.35 |    0.08 |
|              |        |               |          |          |          |       |         |
|   RootLogger |  net48 |      .NET 4.8 | 10.55 ns | 0.076 ns | 0.111 ns |  1.00 |    0.00 |
| NestedLogger |  net48 |      .NET 4.8 | 41.57 ns | 0.293 ns | 0.430 ns |  3.94 |    0.06 |
|              |        |               |          |          |          |       |         |
|   RootLogger |  net50 | .NET Core 5.0 | 10.50 ns | 0.065 ns | 0.098 ns |  1.00 |    0.00 |
| NestedLogger |  net50 | .NET Core 5.0 | 31.40 ns | 0.298 ns | 0.447 ns |  2.99 |    0.05 |
