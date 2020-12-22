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
|               Method |             Job |       Jit |       Runtime |      Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------- |---------------- |---------- |-------------- |----------:|---------:|---------:|------:|--------:|
|                 Bare |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  10.05 ns | 0.023 ns | 0.033 ns |  1.00 |    0.00 |
|         PushProperty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  95.68 ns | 0.407 ns | 0.596 ns |  9.52 |    0.06 |
|   PushPropertyNested |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 192.16 ns | 2.134 ns | 3.195 ns | 19.12 |    0.31 |
| PushPropertyEnriched |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 171.19 ns | 2.208 ns | 3.236 ns | 17.03 |    0.31 |
|                      |                 |           |               |           |          |          |       |         |
|                 Bare | net48 LegacyJit | LegacyJit |      .NET 4.8 |  10.96 ns | 0.028 ns | 0.039 ns |  1.00 |    0.00 |
|         PushProperty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  81.25 ns | 0.303 ns | 0.444 ns |  7.41 |    0.05 |
|   PushPropertyNested | net48 LegacyJit | LegacyJit |      .NET 4.8 | 158.28 ns | 0.520 ns | 0.762 ns | 14.43 |    0.08 |
| PushPropertyEnriched | net48 LegacyJit | LegacyJit |      .NET 4.8 | 159.75 ns | 0.424 ns | 0.608 ns | 14.57 |    0.08 |
|                      |                 |           |               |           |          |          |       |         |
|                 Bare |    net48 RyuJit |    RyuJit |      .NET 4.8 |  10.96 ns | 0.038 ns | 0.055 ns |  1.00 |    0.00 |
|         PushProperty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  81.20 ns | 0.266 ns | 0.381 ns |  7.41 |    0.05 |
|   PushPropertyNested |    net48 RyuJit |    RyuJit |      .NET 4.8 | 158.01 ns | 0.627 ns | 0.919 ns | 14.42 |    0.12 |
| PushPropertyEnriched |    net48 RyuJit |    RyuJit |      .NET 4.8 | 160.45 ns | 0.466 ns | 0.698 ns | 14.64 |    0.10 |
