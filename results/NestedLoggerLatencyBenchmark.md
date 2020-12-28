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
|   RootLogger | core31 | .NET Core 3.1 | 10.45 ns | 0.084 ns | 0.126 ns |  1.00 |    0.00 |
| NestedLogger | core31 | .NET Core 3.1 | 40.03 ns | 1.240 ns | 1.855 ns |  3.83 |    0.19 |
|              |        |               |          |          |          |       |         |
|   RootLogger |  net48 |      .NET 4.8 | 10.18 ns | 0.091 ns | 0.133 ns |  1.00 |    0.00 |
| NestedLogger |  net48 |      .NET 4.8 | 47.49 ns | 1.365 ns | 2.043 ns |  4.67 |    0.24 |
|              |        |               |          |          |          |       |         |
|   RootLogger |  net50 | .NET Core 5.0 | 10.22 ns | 0.086 ns | 0.128 ns |  1.00 |    0.00 |
| NestedLogger |  net50 | .NET Core 5.0 | 35.96 ns | 1.161 ns | 1.738 ns |  3.52 |    0.19 |
