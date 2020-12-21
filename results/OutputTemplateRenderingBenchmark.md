``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.404
  [Host]          : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|         Method |             Job |       Jit |       Runtime |       Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |---------------- |---------- |-------------- |-----------:|---------:|---------:|-------:|------:|------:|----------:|
| FormatToOutput |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   926.0 ns |  8.28 ns | 12.39 ns | 0.0315 |     - |     - |     200 B |
| FormatToOutput | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,204.6 ns | 11.20 ns | 16.77 ns | 0.1106 |     - |     - |     698 B |
| FormatToOutput |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,204.5 ns |  8.53 ns | 12.50 ns | 0.1106 |     - |     - |     698 B |
