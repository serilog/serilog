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
| BindZero | core31 | .NET Core 3.1 |  45.22 ns | 0.346 ns | 0.508 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | core31 | .NET Core 3.1 | 154.73 ns | 1.874 ns | 2.805 ns |  3.43 |    0.09 | 0.0229 |     - |     - |     144 B |
| BindFive | core31 | .NET Core 3.1 | 435.49 ns | 2.262 ns | 3.385 ns |  9.63 |    0.12 | 0.0687 |     - |     - |     432 B |
|          |        |               |           |          |          |       |         |        |       |       |           |
| BindZero |  net48 |      .NET 4.8 |  54.02 ns | 0.246 ns | 0.368 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net48 |      .NET 4.8 | 159.69 ns | 1.304 ns | 1.952 ns |  2.96 |    0.04 | 0.0253 |     - |     - |     160 B |
| BindFive |  net48 |      .NET 4.8 | 477.56 ns | 3.662 ns | 5.481 ns |  8.84 |    0.12 | 0.0706 |     - |     - |     449 B |
|          |        |               |           |          |          |       |         |        |       |       |           |
| BindZero |  net50 | .NET Core 5.0 |  35.46 ns | 0.280 ns | 0.392 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |  net50 | .NET Core 5.0 | 137.28 ns | 1.390 ns | 2.080 ns |  3.88 |    0.09 | 0.0229 |     - |     - |     144 B |
| BindFive |  net50 | .NET Core 5.0 | 376.58 ns | 3.034 ns | 4.541 ns | 10.64 |    0.14 | 0.0687 |     - |     - |     432 B |
