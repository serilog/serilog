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
| BindZero | core31 | .NET Core 3.1 |  44.94 ns | 0.481 ns | 0.720 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | core31 | .NET Core 3.1 | 156.17 ns | 1.496 ns | 2.240 ns |  3.48 |    0.08 | 0.0229 |     - |     - |     144 B |
| BindFive | core31 | .NET Core 3.1 | 431.40 ns | 3.919 ns | 5.866 ns |  9.60 |    0.22 | 0.0687 |     - |     - |     432 B |
|          |        |               |           |          |          |       |         |        |       |       |           |
| BindZero |  net48 |      .NET 4.8 |  53.70 ns | 0.179 ns | 0.263 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net48 |      .NET 4.8 | 159.98 ns | 1.307 ns | 1.957 ns |  2.98 |    0.04 | 0.0253 |     - |     - |     160 B |
| BindFive |  net48 |      .NET 4.8 | 471.16 ns | 3.100 ns | 4.641 ns |  8.77 |    0.10 | 0.0706 |     - |     - |     449 B |
|          |        |               |           |          |          |       |         |        |       |       |           |
| BindZero |  net50 | .NET Core 5.0 |  35.34 ns | 0.297 ns | 0.445 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net50 | .NET Core 5.0 | 128.20 ns | 1.167 ns | 1.747 ns |  3.63 |    0.06 | 0.0229 |     - |     - |     144 B |
| BindFive |  net50 | .NET Core 5.0 | 374.33 ns | 2.406 ns | 3.602 ns | 10.60 |    0.16 | 0.0687 |     - |     - |     432 B |
