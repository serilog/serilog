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
| BindZero | core31 | .NET Core 3.1 |  43.05 ns | 0.383 ns | 0.573 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | core31 | .NET Core 3.1 | 146.19 ns | 1.881 ns | 2.816 ns |  3.40 |    0.08 | 0.0229 |     - |     - |     144 B |
| BindFive | core31 | .NET Core 3.1 | 415.90 ns | 4.414 ns | 6.471 ns |  9.66 |    0.19 | 0.0687 |     - |     - |     432 B |
|          |        |               |           |          |          |       |         |        |       |       |           |
| BindZero |  net48 |      .NET 4.8 |  50.41 ns | 0.319 ns | 0.477 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net48 |      .NET 4.8 | 151.45 ns | 1.591 ns | 2.381 ns |  3.00 |    0.06 | 0.0253 |     - |     - |     160 B |
| BindFive |  net48 |      .NET 4.8 | 452.73 ns | 2.609 ns | 3.905 ns |  8.98 |    0.11 | 0.0710 |     - |     - |     449 B |
|          |        |               |           |          |          |       |         |        |       |       |           |
| BindZero |  net50 | .NET Core 5.0 |  35.65 ns | 0.354 ns | 0.519 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net50 | .NET Core 5.0 | 131.12 ns | 1.565 ns | 2.343 ns |  3.68 |    0.08 | 0.0229 |     - |     - |     144 B |
| BindFive |  net50 | .NET Core 5.0 | 366.74 ns | 6.347 ns | 9.499 ns | 10.31 |    0.27 | 0.0687 |     - |     - |     432 B |
