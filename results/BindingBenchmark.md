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
| BindZero | core31 | .NET Core 3.1 |  43.31 ns | 0.765 ns | 1.146 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | core31 | .NET Core 3.1 | 162.89 ns | 2.190 ns | 3.277 ns |  3.76 |    0.07 | 0.0229 |     - |     - |     144 B |
| BindFive | core31 | .NET Core 3.1 | 453.95 ns | 2.397 ns | 3.588 ns | 10.49 |    0.28 | 0.0687 |     - |     - |     432 B |
|          |        |               |           |          |          |       |         |        |       |       |           |
| BindZero |  net48 |      .NET 4.8 |  51.70 ns | 0.301 ns | 0.451 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net48 |      .NET 4.8 | 166.70 ns | 1.232 ns | 1.844 ns |  3.22 |    0.05 | 0.0253 |     - |     - |     160 B |
| BindFive |  net48 |      .NET 4.8 | 508.15 ns | 4.065 ns | 6.085 ns |  9.83 |    0.16 | 0.0706 |     - |     - |     449 B |
|          |        |               |           |          |          |       |         |        |       |       |           |
| BindZero |  net50 | .NET Core 5.0 |  34.17 ns | 0.297 ns | 0.445 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net50 | .NET Core 5.0 | 132.23 ns | 2.501 ns | 3.744 ns |  3.87 |    0.13 | 0.0229 |     - |     - |     144 B |
| BindFive |  net50 | .NET Core 5.0 | 368.88 ns | 2.586 ns | 3.870 ns | 10.80 |    0.19 | 0.0687 |     - |     - |     432 B |
