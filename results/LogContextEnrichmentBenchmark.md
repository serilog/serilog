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
|                 Bare | core31 | .NET Core 3.1 |  10.266 ns | 0.0748 ns | 0.1073 ns |  1.00 |    0.00 |
|         PushProperty | core31 | .NET Core 3.1 |  93.690 ns | 0.5644 ns | 0.8448 ns |  9.13 |    0.14 |
|   PushPropertyNested | core31 | .NET Core 3.1 | 183.930 ns | 2.2140 ns | 3.3138 ns | 17.90 |    0.29 |
| PushPropertyEnriched | core31 | .NET Core 3.1 | 159.307 ns | 1.7540 ns | 2.6253 ns | 15.53 |    0.32 |
|                      |        |               |            |           |           |       |         |
|                 Bare |  net48 |      .NET 4.8 |  10.484 ns | 0.0963 ns | 0.1442 ns |  1.00 |    0.00 |
|         PushProperty |  net48 |      .NET 4.8 |  77.149 ns | 0.6731 ns | 1.0075 ns |  7.36 |    0.16 |
|   PushPropertyNested |  net48 |      .NET 4.8 | 150.884 ns | 1.5014 ns | 2.2472 ns | 14.39 |    0.29 |
| PushPropertyEnriched |  net48 |      .NET 4.8 | 155.331 ns | 2.8073 ns | 4.2018 ns | 14.82 |    0.49 |
|                      |        |               |            |           |           |       |         |
|                 Bare |  net50 | .NET Core 5.0 |   9.399 ns | 0.0844 ns | 0.1263 ns |  1.00 |    0.00 |
|         PushProperty |  net50 | .NET Core 5.0 |  87.314 ns | 1.1988 ns | 1.7944 ns |  9.29 |    0.23 |
|   PushPropertyNested |  net50 | .NET Core 5.0 | 176.781 ns | 2.5765 ns | 3.8563 ns | 18.81 |    0.51 |
| PushPropertyEnriched |  net50 | .NET Core 5.0 | 149.029 ns | 1.2276 ns | 1.7606 ns | 15.85 |    0.28 |
