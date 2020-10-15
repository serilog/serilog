``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.402
  [Host]          : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.8 (CoreCLR 4.700.20.41105, CoreFX 4.700.20.41903), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4250.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|   Method |             Job |       Jit |       Runtime |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |---------------- |---------- |-------------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  41.37 ns | 0.153 ns | 0.224 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 146.19 ns | 0.917 ns | 1.372 ns |  3.54 |    0.04 | 0.0229 |     - |     - |     144 B |
| BindFive |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 413.88 ns | 2.344 ns | 3.286 ns | 10.01 |    0.09 | 0.0687 |     - |     - |     432 B |
|          |                 |           |               |           |          |          |       |         |        |       |       |           |
| BindZero | net48 LegacyJit | LegacyJit |      .NET 4.8 |  50.47 ns | 0.201 ns | 0.301 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | net48 LegacyJit | LegacyJit |      .NET 4.8 | 153.66 ns | 5.337 ns | 7.823 ns |  3.04 |    0.16 | 0.0253 |     - |     - |     160 B |
| BindFive | net48 LegacyJit | LegacyJit |      .NET 4.8 | 464.98 ns | 1.299 ns | 1.821 ns |  9.22 |    0.06 | 0.0710 |     - |     - |     449 B |
|          |                 |           |               |           |          |          |       |         |        |       |       |           |
| BindZero |    net48 RyuJit |    RyuJit |      .NET 4.8 |  50.55 ns | 0.209 ns | 0.307 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |    net48 RyuJit |    RyuJit |      .NET 4.8 | 150.85 ns | 1.379 ns | 2.021 ns |  2.98 |    0.04 | 0.0253 |     - |     - |     160 B |
| BindFive |    net48 RyuJit |    RyuJit |      .NET 4.8 | 455.61 ns | 6.120 ns | 9.160 ns |  9.02 |    0.19 | 0.0710 |     - |     - |     449 B |
