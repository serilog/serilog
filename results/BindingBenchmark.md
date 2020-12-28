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
| BindZero | core31 | .NET Core 3.1 |  43.69 ns | 0.313 ns | 0.468 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | core31 | .NET Core 3.1 | 151.88 ns | 1.722 ns | 2.524 ns |  3.48 |    0.06 | 0.0229 |     - |     - |     144 B |
| BindFive | core31 | .NET Core 3.1 | 429.00 ns | 3.510 ns | 5.254 ns |  9.82 |    0.16 | 0.0687 |     - |     - |     432 B |
|          |        |               |           |          |          |       |         |        |       |       |           |
| BindZero |  net48 |      .NET 4.8 |  52.07 ns | 0.319 ns | 0.448 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net48 |      .NET 4.8 | 157.86 ns | 1.517 ns | 2.271 ns |  3.03 |    0.06 | 0.0253 |     - |     - |     160 B |
| BindFive |  net48 |      .NET 4.8 | 469.76 ns | 4.366 ns | 6.535 ns |  9.03 |    0.12 | 0.0706 |     - |     - |     449 B |
|          |        |               |           |          |          |       |         |        |       |       |           |
| BindZero |  net50 | .NET Core 5.0 |  36.99 ns | 0.362 ns | 0.530 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net50 | .NET Core 5.0 | 134.69 ns | 1.509 ns | 2.259 ns |  3.64 |    0.10 | 0.0229 |     - |     - |     144 B |
| BindFive |  net50 | .NET Core 5.0 | 371.43 ns | 4.408 ns | 6.461 ns | 10.04 |    0.22 | 0.0687 |     - |     - |     432 B |
