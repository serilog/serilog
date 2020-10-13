``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.508 (2004/?/20H1)
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
| BindZero |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  42.80 ns | 0.152 ns | 0.222 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 147.09 ns | 0.797 ns | 1.143 ns |  3.44 |    0.03 | 0.0229 |     - |     - |     144 B |
| BindFive |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 414.56 ns | 1.647 ns | 2.466 ns |  9.68 |    0.09 | 0.0687 |     - |     - |     432 B |
|          |                 |           |               |           |          |          |       |         |        |       |       |           |
| BindZero | net48 LegacyJit | LegacyJit |      .NET 4.8 |  52.69 ns | 0.249 ns | 0.365 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | net48 LegacyJit | LegacyJit |      .NET 4.8 | 153.42 ns | 4.504 ns | 6.459 ns |  2.91 |    0.13 | 0.0253 |     - |     - |     160 B |
| BindFive | net48 LegacyJit | LegacyJit |      .NET 4.8 | 444.67 ns | 1.483 ns | 2.174 ns |  8.44 |    0.07 | 0.0710 |     - |     - |     449 B |
|          |                 |           |               |           |          |          |       |         |        |       |       |           |
| BindZero |    net48 RyuJit |    RyuJit |      .NET 4.8 |  52.55 ns | 0.186 ns | 0.279 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |    net48 RyuJit |    RyuJit |      .NET 4.8 | 151.50 ns | 0.758 ns | 1.134 ns |  2.88 |    0.03 | 0.0253 |     - |     - |     160 B |
| BindFive |    net48 RyuJit |    RyuJit |      .NET 4.8 | 443.25 ns | 1.063 ns | 1.490 ns |  8.44 |    0.05 | 0.0710 |     - |     - |     449 B |
