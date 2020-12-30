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
|         Method |    Job |       Runtime |       Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |------- |-------------- |-----------:|---------:|---------:|-------:|------:|------:|----------:|
| FormatToOutput | core31 | .NET Core 3.1 |   871.2 ns |  8.33 ns | 12.47 ns | 0.0315 |     - |     - |     200 B |
| FormatToOutput |  net48 |      .NET 4.8 | 1,137.2 ns | 13.30 ns | 19.91 ns | 0.1106 |     - |     - |     698 B |
| FormatToOutput |  net50 | .NET Core 5.0 |   809.7 ns |  8.39 ns | 12.55 ns | 0.0315 |     - |     - |     200 B |
