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
|               Method |    Job |       Runtime |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |------- |-------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare | core31 | .NET Core 3.1 |   7.760 ns | 0.1122 ns | 0.1680 ns |  1.00 |    0.00 |
|         PushProperty | core31 | .NET Core 3.1 |  93.035 ns | 2.2001 ns | 3.2930 ns | 11.99 |    0.47 |
|   PushPropertyNested | core31 | .NET Core 3.1 | 180.968 ns | 2.4428 ns | 3.6563 ns | 23.33 |    0.77 |
| PushPropertyEnriched | core31 | .NET Core 3.1 | 162.222 ns | 3.5543 ns | 5.3198 ns | 20.91 |    0.76 |
|                      |        |               |            |           |           |       |         |
|                 Bare |  net48 |      .NET 4.8 |   8.072 ns | 0.1582 ns | 0.2319 ns |  1.00 |    0.00 |
|         PushProperty |  net48 |      .NET 4.8 |  78.340 ns | 0.9537 ns | 1.4275 ns |  9.71 |    0.36 |
|   PushPropertyNested |  net48 |      .NET 4.8 | 150.416 ns | 2.0403 ns | 3.0539 ns | 18.66 |    0.72 |
| PushPropertyEnriched |  net48 |      .NET 4.8 | 150.499 ns | 2.4351 ns | 3.6448 ns | 18.68 |    0.76 |
|                      |        |               |            |           |           |       |         |
|                 Bare |  net50 | .NET Core 5.0 |   7.816 ns | 0.2665 ns | 0.3988 ns |  1.00 |    0.00 |
|         PushProperty |  net50 | .NET Core 5.0 |  87.950 ns | 1.1061 ns | 1.6555 ns | 11.28 |    0.57 |
|   PushPropertyNested |  net50 | .NET Core 5.0 | 181.128 ns | 2.0638 ns | 3.0890 ns | 23.24 |    1.34 |
| PushPropertyEnriched |  net50 | .NET Core 5.0 | 147.627 ns | 2.4712 ns | 3.6988 ns | 18.94 |    1.18 |
