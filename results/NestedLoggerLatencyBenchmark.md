``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.404
  [Host]          : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|       Method |             Job |       Jit |       Runtime |     Mean |    Error |   StdDev | Ratio | RatioSD |
|------------- |---------------- |---------- |-------------- |---------:|---------:|---------:|------:|--------:|
|   RootLogger |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10.77 ns | 0.084 ns | 0.126 ns |  1.00 |    0.00 |
| NestedLogger |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 36.71 ns | 0.387 ns | 0.579 ns |  3.41 |    0.06 |
|              |                 |           |               |          |          |          |       |         |
|   RootLogger | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.28 ns | 0.099 ns | 0.149 ns |  1.00 |    0.00 |
| NestedLogger | net48 LegacyJit | LegacyJit |      .NET 4.8 | 40.65 ns | 0.270 ns | 0.395 ns |  3.96 |    0.07 |
|              |                 |           |               |          |          |          |       |         |
|   RootLogger |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.25 ns | 0.093 ns | 0.139 ns |  1.00 |    0.00 |
| NestedLogger |    net48 RyuJit |    RyuJit |      .NET 4.8 | 41.33 ns | 0.338 ns | 0.495 ns |  4.04 |    0.08 |
