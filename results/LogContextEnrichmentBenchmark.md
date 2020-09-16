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
|               Method |             Job |       Jit |       Runtime |       Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|--------------------- |---------------- |---------- |-------------- |-----------:|----------:|----------:|----------:|------:|--------:|
|                 Bare |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   9.979 ns | 0.3205 ns | 0.4698 ns |  10.35 ns |  1.00 |    0.00 |
|         PushProperty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  94.672 ns | 1.4407 ns | 2.1117 ns |  93.23 ns |  9.52 |    0.66 |
|   PushPropertyNested |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 181.635 ns | 2.7982 ns | 4.0131 ns | 180.96 ns | 18.25 |    0.52 |
| PushPropertyEnriched |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 158.363 ns | 0.4824 ns | 0.6918 ns | 158.28 ns | 15.93 |    0.71 |
|                      |                 |           |               |            |           |           |           |       |         |
|                 Bare | net48 LegacyJit | LegacyJit |      .NET 4.8 |  10.465 ns | 0.0586 ns | 0.0877 ns |  10.48 ns |  1.00 |    0.00 |
|         PushProperty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  75.758 ns | 0.5044 ns | 0.7234 ns |  75.57 ns |  7.23 |    0.08 |
|   PushPropertyNested | net48 LegacyJit | LegacyJit |      .NET 4.8 | 147.491 ns | 0.2430 ns | 0.3636 ns | 147.56 ns | 14.09 |    0.13 |
| PushPropertyEnriched | net48 LegacyJit | LegacyJit |      .NET 4.8 | 150.718 ns | 0.3145 ns | 0.4609 ns | 150.82 ns | 14.40 |    0.13 |
|                      |                 |           |               |            |           |           |           |       |         |
|                 Bare |    net48 RyuJit |    RyuJit |      .NET 4.8 |  10.348 ns | 0.0858 ns | 0.1284 ns |  10.32 ns |  1.00 |    0.00 |
|         PushProperty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  76.583 ns | 1.2447 ns | 1.7038 ns |  76.13 ns |  7.40 |    0.24 |
|   PushPropertyNested |    net48 RyuJit |    RyuJit |      .NET 4.8 | 147.399 ns | 0.3748 ns | 0.5254 ns | 147.60 ns | 14.24 |    0.19 |
| PushPropertyEnriched |    net48 RyuJit |    RyuJit |      .NET 4.8 | 150.541 ns | 0.3963 ns | 0.5683 ns | 150.65 ns | 14.55 |    0.19 |
