``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]          : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  core31 RyuJit   : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  net48 LegacyJit : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT
  net48 RyuJit    : .NET Framework 4.8 (4.8.4200.0), X64 RyuJIT

IterationCount=15  LaunchCount=2  WarmupCount=10  

```
|   Method |             Job |       Jit |       Runtime |      Mean |      Error |     StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |---------------- |---------- |-------------- |----------:|-----------:|-----------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  48.17 ns |   1.534 ns |   2.201 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 175.54 ns |   6.999 ns |  10.475 ns |  3.67 |    0.30 | 0.0229 |     - |     - |     144 B |
| BindFive |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 520.22 ns |  47.623 ns |  66.761 ns | 10.82 |    1.51 | 0.0687 |     - |     - |     432 B |
|          |                 |           |               |           |            |            |       |         |        |       |       |           |
| BindZero | net48 LegacyJit | LegacyJit |      .NET 4.8 |  58.75 ns |   1.205 ns |   1.728 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | net48 LegacyJit | LegacyJit |      .NET 4.8 | 179.06 ns |   6.146 ns |   8.814 ns |  3.05 |    0.16 | 0.0253 |     - |     - |     160 B |
| BindFive | net48 LegacyJit | LegacyJit |      .NET 4.8 | 646.05 ns | 113.432 ns | 162.680 ns | 11.00 |    2.74 | 0.0706 |     - |     - |     449 B |
|          |                 |           |               |           |            |            |       |         |        |       |       |           |
| BindZero |    net48 RyuJit |    RyuJit |      .NET 4.8 |  68.68 ns |   5.706 ns |   8.183 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |    net48 RyuJit |    RyuJit |      .NET 4.8 | 174.34 ns |   2.065 ns |   2.895 ns |  2.57 |    0.30 | 0.0253 |     - |     - |     160 B |
| BindFive |    net48 RyuJit |    RyuJit |      .NET 4.8 | 510.05 ns |  10.465 ns |  14.325 ns |  7.58 |    0.74 | 0.0706 |     - |     - |     449 B |
