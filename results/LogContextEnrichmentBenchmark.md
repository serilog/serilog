``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.101
  [Host] : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
  core31 : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48  : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net50  : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT

Jit=RyuJit  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
|               Method |    Job |       Runtime |       Mean |     Error |     StdDev |     Median | Ratio | RatioSD |
|--------------------- |------- |-------------- |-----------:|----------:|-----------:|-----------:|------:|--------:|
|                 Bare | core31 | .NET Core 3.1 |  10.213 ns | 0.0724 ns |  0.1083 ns |  10.219 ns |  1.00 |    0.00 |
|         PushProperty | core31 | .NET Core 3.1 |  93.120 ns | 0.7894 ns |  1.1571 ns |  93.094 ns |  9.12 |    0.16 |
|   PushPropertyNested | core31 | .NET Core 3.1 | 182.201 ns | 2.2869 ns |  3.3521 ns | 181.525 ns | 17.84 |    0.38 |
| PushPropertyEnriched | core31 | .NET Core 3.1 | 169.246 ns | 1.2699 ns |  1.9007 ns | 169.737 ns | 16.57 |    0.25 |
|                      |        |               |            |           |            |            |       |         |
|                 Bare |  net48 |      .NET 4.8 |  10.211 ns | 0.0767 ns |  0.1148 ns |  10.228 ns |  1.00 |    0.00 |
|         PushProperty |  net48 |      .NET 4.8 |  77.428 ns | 0.6080 ns |  0.9101 ns |  77.722 ns |  7.58 |    0.11 |
|   PushPropertyNested |  net48 |      .NET 4.8 | 150.394 ns | 1.6899 ns |  2.4236 ns | 150.355 ns | 14.74 |    0.31 |
| PushPropertyEnriched |  net48 |      .NET 4.8 | 169.642 ns | 9.1176 ns | 13.0761 ns | 159.904 ns | 16.63 |    1.27 |
|                      |        |               |            |           |            |            |       |         |
|                 Bare |  net50 | .NET Core 5.0 |   9.964 ns | 0.1787 ns |  0.2620 ns |   9.959 ns |  1.00 |    0.00 |
|         PushProperty |  net50 | .NET Core 5.0 |  87.946 ns | 0.7883 ns |  1.1798 ns |  87.679 ns |  8.83 |    0.18 |
|   PushPropertyNested |  net50 | .NET Core 5.0 | 186.345 ns | 1.6202 ns |  2.4250 ns | 186.226 ns | 18.72 |    0.41 |
| PushPropertyEnriched |  net50 | .NET Core 5.0 | 153.783 ns | 1.7963 ns |  2.6886 ns | 153.811 ns | 15.45 |    0.62 |
