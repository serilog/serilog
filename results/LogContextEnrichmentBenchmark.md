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
|               Method |             Job |       Jit |       Runtime |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD |
|--------------------- |---------------- |---------- |-------------- |-----------:|----------:|----------:|-----------:|------:|--------:|
|                 Bare |   core31 RyuJit |    RyuJit | .NET Core 3.1 |   9.685 ns | 0.2574 ns | 0.3773 ns |   9.477 ns |  1.00 |    0.00 |
|         PushProperty |   core31 RyuJit |    RyuJit | .NET Core 3.1 |  90.700 ns | 1.0353 ns | 1.5175 ns |  90.793 ns |  9.37 |    0.23 |
|   PushPropertyNested |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 178.536 ns | 0.9791 ns | 1.3725 ns | 178.113 ns | 18.52 |    0.82 |
| PushPropertyEnriched |   core31 RyuJit |    RyuJit | .NET Core 3.1 | 161.115 ns | 1.8152 ns | 2.6607 ns | 161.757 ns | 16.65 |    0.41 |
|                      |                 |           |               |            |           |           |            |       |         |
|                 Bare | net48 LegacyJit | LegacyJit |      .NET 4.8 |  10.222 ns | 0.0861 ns | 0.1235 ns |  10.215 ns |  1.00 |    0.00 |
|         PushProperty | net48 LegacyJit | LegacyJit |      .NET 4.8 |  75.043 ns | 0.2085 ns | 0.2784 ns |  75.079 ns |  7.35 |    0.10 |
|   PushPropertyNested | net48 LegacyJit | LegacyJit |      .NET 4.8 | 146.301 ns | 0.4812 ns | 0.7053 ns | 146.181 ns | 14.32 |    0.19 |
| PushPropertyEnriched | net48 LegacyJit | LegacyJit |      .NET 4.8 | 148.081 ns | 0.4669 ns | 0.6843 ns | 148.036 ns | 14.49 |    0.17 |
|                      |                 |           |               |            |           |           |            |       |         |
|                 Bare |    net48 RyuJit |    RyuJit |      .NET 4.8 |  10.304 ns | 0.2536 ns | 0.3795 ns |  10.175 ns |  1.00 |    0.00 |
|         PushProperty |    net48 RyuJit |    RyuJit |      .NET 4.8 |  75.328 ns | 0.3442 ns | 0.4936 ns |  75.202 ns |  7.32 |    0.28 |
|   PushPropertyNested |    net48 RyuJit |    RyuJit |      .NET 4.8 | 146.415 ns | 0.6450 ns | 0.9042 ns | 146.550 ns | 14.21 |    0.52 |
| PushPropertyEnriched |    net48 RyuJit |    RyuJit |      .NET 4.8 | 148.218 ns | 0.5169 ns | 0.7413 ns | 148.125 ns | 14.39 |    0.52 |
