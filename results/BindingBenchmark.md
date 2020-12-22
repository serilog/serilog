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
|   Method |             Job |       Jit |       Runtime |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |---------------- |---------- |-------------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  45.38 ns | 0.651 ns | 0.974 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 156.05 ns | 1.854 ns | 2.775 ns |  3.44 |    0.09 | 0.0229 |     - |     - |     144 B |
| BindFive |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 438.47 ns | 3.011 ns | 4.507 ns |  9.67 |    0.23 | 0.0687 |     - |     - |     432 B |
|          |                 |           |               |           |          |          |       |         |        |       |       |           |
| BindZero | net48 LegacyJit | LegacyJit |      .NET 4.8 |  54.49 ns | 0.502 ns | 0.752 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | net48 LegacyJit | LegacyJit |      .NET 4.8 | 162.15 ns | 1.697 ns | 2.540 ns |  2.98 |    0.06 | 0.0253 |     - |     - |     160 B |
| BindFive | net48 LegacyJit | LegacyJit |      .NET 4.8 | 478.07 ns | 4.262 ns | 6.379 ns |  8.78 |    0.17 | 0.0710 |     - |     - |     449 B |
|          |                 |           |               |           |          |          |       |         |        |       |       |           |
| BindZero |    net48 RyuJit |    RyuJit |      .NET 4.8 |  54.30 ns | 0.294 ns | 0.439 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne |    net48 RyuJit |    RyuJit |      .NET 4.8 | 162.41 ns | 1.631 ns | 2.442 ns |  2.99 |    0.05 | 0.0253 |     - |     - |     160 B |
| BindFive |    net48 RyuJit |    RyuJit |      .NET 4.8 | 475.07 ns | 2.662 ns | 3.985 ns |  8.75 |    0.11 | 0.0710 |     - |     - |     449 B |
