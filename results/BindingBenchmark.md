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
|   Method |    Job |       Runtime |      Mean |    Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |------- |-------------- |----------:|---------:|----------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero | core31 | .NET Core 3.1 |  43.39 ns | 0.609 ns |  0.912 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | core31 | .NET Core 3.1 | 146.32 ns | 2.501 ns |  3.743 ns |  3.37 |    0.12 | 0.0229 |     - |     - |     144 B |
| BindFive | core31 | .NET Core 3.1 | 422.28 ns | 4.712 ns |  7.053 ns |  9.74 |    0.25 | 0.0687 |     - |     - |     432 B |
|          |        |               |           |          |           |       |         |        |       |       |           |
| BindZero |  net48 |      .NET 4.8 |  52.08 ns | 0.617 ns |  0.923 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net48 |      .NET 4.8 | 155.05 ns | 2.038 ns |  3.050 ns |  2.98 |    0.08 | 0.0253 |     - |     - |     160 B |
| BindFive |  net48 |      .NET 4.8 | 461.12 ns | 7.036 ns | 10.531 ns |  8.86 |    0.29 | 0.0710 |     - |     - |     449 B |
|          |        |               |           |          |           |       |         |        |       |       |           |
| BindZero |  net50 | .NET Core 5.0 |  34.35 ns | 0.597 ns |  0.893 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net50 | .NET Core 5.0 | 125.95 ns | 2.061 ns |  3.085 ns |  3.67 |    0.14 | 0.0229 |     - |     - |     144 B |
| BindFive |  net50 | .NET Core 5.0 | 361.54 ns | 5.593 ns |  8.199 ns | 10.53 |    0.36 | 0.0687 |     - |     - |     432 B |
