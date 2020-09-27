``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4220.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|         Method |             Job |       Jit |       Runtime |       Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |---------------- |---------- |-------------- |-----------:|---------:|---------:|-------:|------:|------:|----------:|
| FormatToOutput |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   881.8 ns |  6.84 ns |  9.81 ns | 0.0267 |     - |     - |     168 B |
| FormatToOutput | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,145.9 ns | 21.21 ns | 31.75 ns | 0.1049 |     - |     - |     666 B |
| FormatToOutput |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,125.7 ns |  9.27 ns | 13.87 ns | 0.1049 |     - |     - |     666 B |
