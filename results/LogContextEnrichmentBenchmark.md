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
|               Method |             Job |       Jit |       Runtime |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|--------------------- |---------------- |---------- |-------------- |----------:|----------:|----------:|----------:|------:|--------:|
|                 Bare |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  11.07 ns |  0.404 ns |  0.580 ns |  10.75 ns |  1.00 |    0.00 |
|         PushProperty |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 144.49 ns | 14.097 ns | 20.218 ns | 138.21 ns | 13.08 |    1.93 |
|   PushPropertyNested |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 281.84 ns | 25.206 ns | 36.150 ns | 270.92 ns | 25.40 |    2.28 |
| PushPropertyEnriched |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 224.24 ns | 12.706 ns | 18.624 ns | 218.90 ns | 20.38 |    2.32 |
|                      |                 |           |               |           |           |           |           |       |         |
|                 Bare | net48 LegacyJit | LegacyJit |      .NET 4.8 |  12.69 ns |  0.380 ns |  0.520 ns |  12.72 ns |  1.00 |    0.00 |
|         PushProperty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  98.08 ns |  5.130 ns |  7.519 ns |  97.26 ns |  7.80 |    0.70 |
|   PushPropertyNested | net48 LegacyJit | LegacyJit |      .NET 4.8 | 178.19 ns |  3.523 ns |  4.938 ns | 176.73 ns | 14.07 |    0.69 |
| PushPropertyEnriched | net48 LegacyJit | LegacyJit |      .NET 4.8 | 177.56 ns |  3.388 ns |  4.965 ns | 177.47 ns | 13.98 |    0.61 |
|                      |                 |           |               |           |           |           |           |       |         |
|                 Bare |    net48 RyuJit |    RyuJit |      .NET 4.8 |  11.60 ns |  0.176 ns |  0.252 ns |  11.53 ns |  1.00 |    0.00 |
|         PushProperty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  89.97 ns |  1.911 ns |  2.741 ns |  89.36 ns |  7.76 |    0.24 |
|   PushPropertyNested |    net48 RyuJit |    RyuJit |      .NET 4.8 | 173.87 ns |  1.961 ns |  2.875 ns | 172.82 ns | 15.00 |    0.45 |
| PushPropertyEnriched |    net48 RyuJit |    RyuJit |      .NET 4.8 | 172.84 ns |  1.763 ns |  2.354 ns | 172.76 ns | 14.93 |    0.32 |
