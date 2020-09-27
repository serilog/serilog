``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|       Method |             Job |       Jit |       Runtime |     Mean |    Error |   StdDev | Ratio | RatioSD |
|------------- |---------------- |---------- |-------------- |---------:|---------:|---------:|------:|--------:|
|   RootLogger |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 10.17 ns | 0.061 ns | 0.091 ns |  1.00 |    0.00 |
| NestedLogger |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 35.38 ns | 0.436 ns | 0.639 ns |  3.48 |    0.07 |
|              |                 |           |               |          |          |          |       |         |
|   RootLogger | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.54 ns | 0.058 ns | 0.085 ns |  1.00 |    0.00 |
| NestedLogger | net48 LegacyJit | LegacyJit |      .NET 4.8 | 43.92 ns | 0.499 ns | 0.732 ns |  4.17 |    0.08 |
|              |                 |           |               |          |          |          |       |         |
|   RootLogger |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.54 ns | 0.074 ns | 0.111 ns |  1.00 |    0.00 |
| NestedLogger |    net48 RyuJit |    RyuJit |      .NET 4.8 | 43.87 ns | 0.225 ns | 0.323 ns |  4.16 |    0.05 |
