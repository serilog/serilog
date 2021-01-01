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
|   RootLogger | core31 | .NET Core 3.1 | 10.173 ns | 0.0644 ns | 0.0903 ns |  1.00 |    0.00 |
| NestedLogger | core31 | .NET Core 3.1 | 36.856 ns | 0.7688 ns | 1.1025 ns |  3.62 |    0.12 |
|              |        |               |           |           |           |       |         |
|   RootLogger |  net48 |      .NET 4.8 |  9.703 ns | 0.0453 ns | 0.0678 ns |  1.00 |    0.00 |
| NestedLogger |  net48 |      .NET 4.8 | 41.076 ns | 0.8493 ns | 1.2450 ns |  4.23 |    0.12 |
|              |        |               |           |           |           |       |         |
|   RootLogger |  net50 | .NET Core 5.0 |  8.920 ns | 0.0494 ns | 0.0740 ns |  1.00 |    0.00 |
| NestedLogger |  net50 | .NET Core 5.0 | 31.794 ns | 0.5030 ns | 0.7214 ns |  3.56 |    0.09 |
