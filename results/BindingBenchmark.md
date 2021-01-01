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
| BindZero | core31 | .NET Core 3.1 |  43.17 ns | 0.622 ns | 0.931 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | core31 | .NET Core 3.1 | 147.94 ns | 2.108 ns | 3.090 ns |  3.43 |    0.11 | 0.0229 |     - |     - |     144 B |
| BindFive | core31 | .NET Core 3.1 | 415.61 ns | 3.624 ns | 5.425 ns |  9.63 |    0.24 | 0.0687 |     - |     - |     432 B |
|          |        |               |           |          |          |       |         |        |       |       |           |
| BindZero |  net48 |      .NET 4.8 |  52.01 ns | 0.368 ns | 0.550 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net48 |      .NET 4.8 | 154.83 ns | 2.187 ns | 3.273 ns |  2.98 |    0.07 | 0.0253 |     - |     - |     160 B |
| BindFive |  net48 |      .NET 4.8 | 454.62 ns | 6.563 ns | 9.824 ns |  8.74 |    0.20 | 0.0710 |     - |     - |     449 B |
|          |        |               |           |          |          |       |         |        |       |       |           |
| BindZero |  net50 | .NET Core 5.0 |  34.84 ns | 0.628 ns | 0.940 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net50 | .NET Core 5.0 | 129.34 ns | 3.966 ns | 5.937 ns |  3.71 |    0.14 | 0.0229 |     - |     - |     144 B |
| BindFive |  net50 | .NET Core 5.0 | 358.26 ns | 4.425 ns | 6.623 ns | 10.29 |    0.38 | 0.0687 |     - |     - |     432 B |
