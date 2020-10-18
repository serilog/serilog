``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.403
  [Host]          : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.9 (CoreCLR 4.700.20.47201, CoreFX 4.700.20.47203), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|               Method |             Job |       Jit |       Runtime |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD |
|--------------------- |---------------- |---------- |-------------- |-----------:|----------:|----------:|-----------:|------:|--------:|
|                 Bare |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  10.401 ns | 0.0534 ns | 0.0782 ns |  10.375 ns |  1.00 |    0.00 |
|         PushProperty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  91.034 ns | 0.3864 ns | 0.5664 ns |  91.089 ns |  8.75 |    0.09 |
|   PushPropertyNested |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 179.049 ns | 3.0620 ns | 4.2925 ns | 176.106 ns | 17.21 |    0.45 |
| PushPropertyEnriched |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 162.108 ns | 3.0662 ns | 4.2983 ns | 159.726 ns | 15.58 |    0.43 |
|                      |                 |           |               |            |           |           |            |       |         |
|                 Bare | net48 LegacyJit | LegacyJit |      .NET 4.8 |   9.888 ns | 0.0382 ns | 0.0536 ns |   9.904 ns |  1.00 |    0.00 |
|         PushProperty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  74.559 ns | 0.3222 ns | 0.4621 ns |  74.625 ns |  7.54 |    0.06 |
|   PushPropertyNested | net48 LegacyJit | LegacyJit |      .NET 4.8 | 144.311 ns | 0.6142 ns | 0.8407 ns | 144.419 ns | 14.59 |    0.13 |
| PushPropertyEnriched | net48 LegacyJit | LegacyJit |      .NET 4.8 | 148.326 ns | 0.7013 ns | 1.0496 ns | 148.247 ns | 15.00 |    0.13 |
|                      |                 |           |               |            |           |           |            |       |         |
|                 Bare |    net48 RyuJit |    RyuJit |      .NET 4.8 |   9.932 ns | 0.0330 ns | 0.0473 ns |   9.942 ns |  1.00 |    0.00 |
|         PushProperty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  74.873 ns | 0.3224 ns | 0.4725 ns |  74.776 ns |  7.54 |    0.06 |
|   PushPropertyNested |    net48 RyuJit |    RyuJit |      .NET 4.8 | 145.049 ns | 0.5411 ns | 0.7761 ns | 145.081 ns | 14.60 |    0.11 |
| PushPropertyEnriched |    net48 RyuJit |    RyuJit |      .NET 4.8 | 148.288 ns | 0.5172 ns | 0.7581 ns | 148.404 ns | 14.93 |    0.09 |
