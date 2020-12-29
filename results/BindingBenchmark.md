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
| BindZero | core31 | .NET Core 3.1 |  43.27 ns | 0.402 ns | 0.602 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | core31 | .NET Core 3.1 | 149.60 ns | 2.262 ns | 3.386 ns |  3.46 |    0.09 | 0.0229 |     - |     - |     144 B |
| BindFive | core31 | .NET Core 3.1 | 416.41 ns | 4.315 ns | 6.459 ns |  9.63 |    0.19 | 0.0687 |     - |     - |     432 B |
|          |        |               |           |          |          |       |         |        |       |       |           |
| BindZero |  net48 |      .NET 4.8 |  51.90 ns | 0.426 ns | 0.637 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net48 |      .NET 4.8 | 154.54 ns | 2.310 ns | 3.458 ns |  2.98 |    0.07 | 0.0253 |     - |     - |     160 B |
| BindFive |  net48 |      .NET 4.8 | 453.47 ns | 3.565 ns | 5.113 ns |  8.74 |    0.17 | 0.0710 |     - |     - |     449 B |
|          |        |               |           |          |          |       |         |        |       |       |           |
| BindZero |  net50 | .NET Core 5.0 |  34.43 ns | 0.721 ns | 1.079 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net50 | .NET Core 5.0 | 127.26 ns | 3.162 ns | 4.635 ns |  3.69 |    0.11 | 0.0229 |     - |     - |     144 B |
| BindFive |  net50 | .NET Core 5.0 | 364.40 ns | 3.852 ns | 5.765 ns | 10.60 |    0.38 | 0.0687 |     - |     - |     432 B |
