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
|       Method |             Job |       Jit |       Runtime |     Mean |    Error |   StdDev |   Median | Ratio | RatioSD |
|------------- |---------------- |---------- |-------------- |---------:|---------:|---------:|---------:|------:|--------:|
|   RootLogger |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10.32 ns | 0.020 ns | 0.028 ns | 10.32 ns |  1.00 |    0.00 |
| NestedLogger |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 35.89 ns | 0.467 ns | 0.685 ns | 35.41 ns |  3.47 |    0.07 |
|              |                 |           |               |          |          |          |          |       |         |
|   RootLogger | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.74 ns | 0.037 ns | 0.053 ns | 10.75 ns |  1.00 |    0.00 |
| NestedLogger | net48 LegacyJit | LegacyJit |      .NET 4.8 | 42.20 ns | 0.114 ns | 0.167 ns | 42.21 ns |  3.93 |    0.02 |
|              |                 |           |               |          |          |          |          |       |         |
|   RootLogger |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.69 ns | 0.024 ns | 0.032 ns | 10.69 ns |  1.00 |    0.00 |
| NestedLogger |    net48 RyuJit |    RyuJit |      .NET 4.8 | 42.19 ns | 0.083 ns | 0.122 ns | 42.18 ns |  3.95 |    0.01 |
