``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]          : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|       Method |             Job |       Jit |       Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD |
|------------- |---------------- |---------- |-------------- |----------:|----------:|----------:|------:|--------:|
|   RootLogger |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  9.824 ns | 0.0947 ns | 0.1417 ns |  1.00 |    0.00 |
| NestedLogger |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 38.172 ns | 1.4092 ns | 2.0655 ns |  3.88 |    0.22 |
|              |                 |           |               |           |           |           |       |         |
|   RootLogger | net48 LegacyJit | LegacyJit |      .NET 4.8 | 10.192 ns | 0.0892 ns | 0.1280 ns |  1.00 |    0.00 |
| NestedLogger | net48 LegacyJit | LegacyJit |      .NET 4.8 | 42.399 ns | 1.4609 ns | 2.0952 ns |  4.16 |    0.21 |
|              |                 |           |               |           |           |           |       |         |
|   RootLogger |    net48 RyuJit |    RyuJit |      .NET 4.8 | 10.188 ns | 0.0943 ns | 0.1411 ns |  1.00 |    0.00 |
| NestedLogger |    net48 RyuJit |    RyuJit |      .NET 4.8 | 43.402 ns | 2.0793 ns | 3.1122 ns |  4.26 |    0.32 |
