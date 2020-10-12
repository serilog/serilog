``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|               Method |             Job |       Jit |       Runtime |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |---------------- |---------- |-------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   9.660 ns | 0.1221 ns | 0.1827 ns |  1.00 |    0.00 |
|         PushProperty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  95.450 ns | 1.3082 ns | 1.9580 ns |  9.88 |    0.27 |
|   PushPropertyNested |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 189.329 ns | 6.3722 ns | 9.1388 ns | 19.61 |    1.03 |
| PushPropertyEnriched |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 163.889 ns | 2.4281 ns | 3.6343 ns | 16.97 |    0.54 |
|                      |                 |           |               |            |           |           |       |         |
|                 Bare | net48 LegacyJit | LegacyJit |      .NET 4.8 |  10.541 ns | 0.1285 ns | 0.1923 ns |  1.00 |    0.00 |
|         PushProperty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  77.178 ns | 1.1529 ns | 1.7256 ns |  7.32 |    0.19 |
|   PushPropertyNested | net48 LegacyJit | LegacyJit |      .NET 4.8 | 150.692 ns | 2.4578 ns | 3.6788 ns | 14.30 |    0.44 |
| PushPropertyEnriched | net48 LegacyJit | LegacyJit |      .NET 4.8 | 152.510 ns | 2.7469 ns | 4.1114 ns | 14.47 |    0.48 |
|                      |                 |           |               |            |           |           |       |         |
|                 Bare |    net48 RyuJit |    RyuJit |      .NET 4.8 |  10.712 ns | 0.1585 ns | 0.2373 ns |  1.00 |    0.00 |
|         PushProperty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  77.353 ns | 1.2535 ns | 1.8762 ns |  7.23 |    0.24 |
|   PushPropertyNested |    net48 RyuJit |    RyuJit |      .NET 4.8 | 150.845 ns | 2.5623 ns | 3.8351 ns | 14.09 |    0.51 |
| PushPropertyEnriched |    net48 RyuJit |    RyuJit |      .NET 4.8 | 156.054 ns | 5.2656 ns | 7.8813 ns | 14.58 |    0.85 |
