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
|           Method |    Job |       Runtime |     Mean |    Error |   StdDev |   Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |------- |-------------- |---------:|---------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt | core31 | .NET Core 3.1 | 81.53 ns | 0.720 ns | 1.078 ns | 82.14 ns | 0.0242 |     - |     - |     152 B |
| ForContextString | core31 | .NET Core 3.1 | 50.77 ns | 0.310 ns | 0.454 ns | 50.78 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType | core31 | .NET Core 3.1 | 86.56 ns | 0.861 ns | 1.288 ns | 86.48 ns | 0.0204 |     - |     - |     128 B |
|    ForContextInt |  net48 |      .NET 4.8 | 78.85 ns | 0.688 ns | 1.030 ns | 79.09 ns | 0.0242 |     - |     - |     152 B |
| ForContextString |  net48 |      .NET 4.8 | 50.73 ns | 0.891 ns | 1.334 ns | 50.64 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType |  net48 |      .NET 4.8 | 97.77 ns | 0.771 ns | 1.154 ns | 97.86 ns | 0.0204 |     - |     - |     128 B |
|    ForContextInt |  net50 | .NET Core 5.0 | 65.99 ns | 0.951 ns | 1.424 ns | 66.12 ns | 0.0242 |     - |     - |     152 B |
| ForContextString |  net50 | .NET Core 5.0 | 37.21 ns | 0.209 ns | 0.307 ns | 37.16 ns | 0.0204 |     - |     - |     128 B |
|   ForContextType |  net50 | .NET Core 5.0 | 73.54 ns | 0.713 ns | 1.067 ns | 73.52 ns | 0.0204 |     - |     - |     128 B |
