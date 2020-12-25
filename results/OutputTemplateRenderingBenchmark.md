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
| FormatToOutput | core31 | .NET Core 3.1 |   906.8 ns |  5.27 ns |  7.89 ns | 0.0315 |     - |     - |     200 B |
| FormatToOutput |  net48 |      .NET 4.8 | 1,193.2 ns | 12.49 ns | 18.31 ns | 0.1106 |     - |     - |     698 B |
| FormatToOutput |  net50 | .NET Core 5.0 |   846.2 ns |  6.77 ns | 10.13 ns | 0.0315 |     - |     - |     200 B |
