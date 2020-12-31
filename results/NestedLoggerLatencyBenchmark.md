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
|   RootLogger | core31 | .NET Core 3.1 |  8.399 ns | 0.1114 ns | 0.1633 ns |  1.00 |    0.00 |
| NestedLogger | core31 | .NET Core 3.1 | 35.578 ns | 1.0201 ns | 1.5268 ns |  4.23 |    0.21 |
|              |        |               |           |           |           |       |         |
|   RootLogger |  net48 |      .NET 4.8 |  8.395 ns | 0.1439 ns | 0.2153 ns |  1.00 |    0.00 |
| NestedLogger |  net48 |      .NET 4.8 | 42.487 ns | 0.8949 ns | 1.2545 ns |  5.06 |    0.25 |
|              |        |               |           |           |           |       |         |
|   RootLogger |  net50 | .NET Core 5.0 |  7.726 ns | 0.4172 ns | 0.6245 ns |  1.00 |    0.00 |
| NestedLogger |  net50 | .NET Core 5.0 | 31.654 ns | 0.9854 ns | 1.4132 ns |  4.11 |    0.46 |
