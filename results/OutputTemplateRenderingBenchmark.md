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
|         Method |             Job |       Jit |       Runtime |       Mean |   Error |  StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |---------------- |---------- |-------------- |-----------:|--------:|--------:|-------:|------:|------:|----------:|
| FormatToOutput |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   859.9 ns | 4.17 ns | 6.24 ns | 0.0315 |     - |     - |     200 B |
| FormatToOutput | net48 LegacyJit | LegacyJit |      .NET 4.8 | 1,127.1 ns | 2.67 ns | 3.91 ns | 0.1106 |     - |     - |     698 B |
| FormatToOutput |    net48 RyuJit |    RyuJit |      .NET 4.8 | 1,118.2 ns | 2.63 ns | 3.93 ns | 0.1106 |     - |     - |     698 B |
