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
| BindZero | core31 | .NET Core 3.1 |  43.05 ns | 0.575 ns | 0.843 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | core31 | .NET Core 3.1 | 150.31 ns | 2.279 ns | 3.411 ns |  3.49 |    0.13 | 0.0229 |     - |     - |     144 B |
| BindFive | core31 | .NET Core 3.1 | 417.54 ns | 3.924 ns | 5.752 ns |  9.70 |    0.21 | 0.0687 |     - |     - |     432 B |
|          |        |               |           |          |          |       |         |        |       |       |           |
| BindZero |  net48 |      .NET 4.8 |  51.28 ns | 0.373 ns | 0.559 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net48 |      .NET 4.8 | 152.55 ns | 2.051 ns | 3.070 ns |  2.97 |    0.06 | 0.0253 |     - |     - |     160 B |
| BindFive |  net48 |      .NET 4.8 | 458.06 ns | 3.893 ns | 5.827 ns |  8.93 |    0.13 | 0.0710 |     - |     - |     449 B |
|          |        |               |           |          |          |       |         |        |       |       |           |
| BindZero |  net50 | .NET Core 5.0 |  33.91 ns | 0.458 ns | 0.686 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net50 | .NET Core 5.0 | 126.80 ns | 1.788 ns | 2.676 ns |  3.74 |    0.12 | 0.0229 |     - |     - |     144 B |
| BindFive |  net50 | .NET Core 5.0 | 355.90 ns | 4.918 ns | 7.361 ns | 10.50 |    0.35 | 0.0687 |     - |     - |     432 B |
