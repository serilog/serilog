``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
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
|                 Bare |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   7.684 ns | 0.0428 ns | 0.0641 ns |  1.00 |    0.00 |
|         PushProperty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  87.362 ns | 0.4751 ns | 0.7112 ns | 11.37 |    0.10 |
|   PushPropertyNested |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 176.692 ns | 0.7620 ns | 1.1405 ns | 23.00 |    0.26 |
| PushPropertyEnriched |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 155.676 ns | 0.8061 ns | 1.2066 ns | 20.26 |    0.20 |
|                      |                 |           |               |            |           |           |       |         |
|                 Bare | net48 LegacyJit | LegacyJit |      .NET 4.8 |   7.707 ns | 0.0416 ns | 0.0622 ns |  1.00 |    0.00 |
|         PushProperty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  74.847 ns | 0.2994 ns | 0.4294 ns |  9.71 |    0.10 |
|   PushPropertyNested | net48 LegacyJit | LegacyJit |      .NET 4.8 | 147.213 ns | 0.8765 ns | 1.2847 ns | 19.10 |    0.19 |
| PushPropertyEnriched | net48 LegacyJit | LegacyJit |      .NET 4.8 | 145.951 ns | 0.5675 ns | 0.8319 ns | 18.93 |    0.18 |
|                      |                 |           |               |            |           |           |       |         |
|                 Bare |    net48 RyuJit |    RyuJit |      .NET 4.8 |   7.718 ns | 0.0521 ns | 0.0763 ns |  1.00 |    0.00 |
|         PushProperty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  74.648 ns | 0.2793 ns | 0.4180 ns |  9.67 |    0.11 |
|   PushPropertyNested |    net48 RyuJit |    RyuJit |      .NET 4.8 | 146.464 ns | 0.6240 ns | 0.9146 ns | 18.98 |    0.26 |
| PushPropertyEnriched |    net48 RyuJit |    RyuJit |      .NET 4.8 | 146.379 ns | 0.8070 ns | 1.1574 ns | 18.96 |    0.25 |
