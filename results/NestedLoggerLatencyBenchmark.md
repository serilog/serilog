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
|       Method |    Job |       Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD |
|------------- |------- |-------------- |----------:|----------:|----------:|------:|--------:|
|   RootLogger | core31 | .NET Core 3.1 |  9.712 ns | 0.0977 ns | 0.1462 ns |  1.00 |    0.00 |
| NestedLogger | core31 | .NET Core 3.1 | 38.773 ns | 1.0192 ns | 1.5256 ns |  3.99 |    0.16 |
|              |        |               |           |           |           |       |         |
|   RootLogger |  net48 |      .NET 4.8 | 10.082 ns | 0.0791 ns | 0.1160 ns |  1.00 |    0.00 |
| NestedLogger |  net48 |      .NET 4.8 | 47.316 ns | 0.9614 ns | 1.4091 ns |  4.69 |    0.16 |
|              |        |               |           |           |           |       |         |
|   RootLogger |  net50 | .NET Core 5.0 |  9.848 ns | 0.0720 ns | 0.1078 ns |  1.00 |    0.00 |
| NestedLogger |  net50 | .NET Core 5.0 | 35.141 ns | 1.1380 ns | 1.7033 ns |  3.57 |    0.18 |
