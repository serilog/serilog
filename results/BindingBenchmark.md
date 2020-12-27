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
|   Method |    Job |       Runtime |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |------- |-------------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero | core31 | .NET Core 3.1 |  42.90 ns | 0.397 ns | 0.570 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | core31 | .NET Core 3.1 | 147.60 ns | 1.376 ns | 2.060 ns |  3.44 |    0.07 | 0.0229 |     - |     - |     144 B |
| BindFive | core31 | .NET Core 3.1 | 417.44 ns | 4.099 ns | 6.136 ns |  9.74 |    0.14 | 0.0687 |     - |     - |     432 B |
|          |        |               |           |          |          |       |         |        |       |       |           |
| BindZero |  net48 |      .NET 4.8 |  52.10 ns | 0.352 ns | 0.527 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net48 |      .NET 4.8 | 154.03 ns | 1.331 ns | 1.993 ns |  2.96 |    0.06 | 0.0253 |     - |     - |     160 B |
| BindFive |  net48 |      .NET 4.8 | 450.04 ns | 1.928 ns | 2.885 ns |  8.64 |    0.10 | 0.0710 |     - |     - |     449 B |
|          |        |               |           |          |          |       |         |        |       |       |           |
| BindZero |  net50 | .NET Core 5.0 |  33.77 ns | 0.401 ns | 0.600 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net50 | .NET Core 5.0 | 125.85 ns | 1.715 ns | 2.567 ns |  3.73 |    0.12 | 0.0229 |     - |     - |     144 B |
| BindFive |  net50 | .NET Core 5.0 | 361.20 ns | 4.429 ns | 6.491 ns | 10.70 |    0.27 | 0.0687 |     - |     - |     432 B |
