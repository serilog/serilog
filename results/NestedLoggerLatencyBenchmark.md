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
|   RootLogger | core31 | .NET Core 3.1 | 10.05 ns | 0.068 ns | 0.099 ns |  1.00 |    0.00 |
| NestedLogger | core31 | .NET Core 3.1 | 35.64 ns | 0.411 ns | 0.615 ns |  3.54 |    0.07 |
|              |        |               |          |          |          |       |         |
|   RootLogger |  net48 |      .NET 4.8 | 10.17 ns | 0.095 ns | 0.139 ns |  1.00 |    0.00 |
| NestedLogger |  net48 |      .NET 4.8 | 40.04 ns | 0.298 ns | 0.447 ns |  3.94 |    0.07 |
|              |        |               |          |          |          |       |         |
|   RootLogger |  net50 | .NET Core 5.0 | 10.24 ns | 0.150 ns | 0.224 ns |  1.00 |    0.00 |
| NestedLogger |  net50 | .NET Core 5.0 | 31.78 ns | 0.534 ns | 0.800 ns |  3.11 |    0.14 |
