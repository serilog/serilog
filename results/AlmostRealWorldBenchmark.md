``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.101
  [Host]           : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
  core22 RyuJit    : .NET Core 2.2.8 (CoreCLR 4.6.28207.03, CoreFX 4.6.28208.02), X64 RyuJIT
  core31 RyuJit    : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT
  net462 LegacyJit : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net462 RyuJit    : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net472 LegacyJit : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net472 RyuJit    : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net48 LegacyJit  : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net48 RyuJit     : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  net50 RyuJit     : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT

IterationCount=3  LaunchCount=1  WarmupCount=3  

```
|                     Method |              Job |       Jit |       Runtime |        Mean |        Error |      StdDev |  Ratio | RatioSD |     Gen 0 |    Gen 1 | Gen 2 |   Allocated |
|--------------------------- |----------------- |---------- |-------------- |------------:|-------------:|------------:|-------:|--------:|----------:|---------:|------:|------------:|
| SimulateAAppWithoutSerilog |    core22 RyuJit |    RyuJit | .NET Core 2.2 |    143.7 μs |     14.78 μs |     0.81 μs |   1.00 |    0.00 |    6.3477 |   0.4883 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |    core22 RyuJit |    RyuJit | .NET Core 2.2 |  1,497.9 μs |    172.92 μs |     9.48 μs |  10.42 |    0.05 |  439.4531 |  48.8281 |     - |  2702.13 KB |
|  SimulateAAppWithSerilogOn |    core22 RyuJit |    RyuJit | .NET Core 2.2 | 55,361.5 μs |  7,775.47 μs |   426.20 μs | 385.19 |    3.79 | 7777.7778 | 111.1111 |     - | 48456.45 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |    core31 RyuJit |    RyuJit | .NET Core 3.1 |    143.5 μs |     38.77 μs |     2.13 μs |   1.00 |    0.00 |    6.3477 |   0.7324 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |    core31 RyuJit |    RyuJit | .NET Core 3.1 |  1,547.5 μs |    242.34 μs |    13.28 μs |  10.78 |    0.07 |  439.4531 |  54.6875 |     - |  2702.13 KB |
|  SimulateAAppWithSerilogOn |    core31 RyuJit |    RyuJit | .NET Core 3.1 | 52,502.6 μs | 20,130.35 μs | 1,103.41 μs | 365.98 |   12.92 | 7800.0000 | 100.0000 |     - | 47987.03 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog | net462 LegacyJit | LegacyJit |    .NET 4.6.2 |    194.1 μs |     51.96 μs |     2.85 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff | net462 LegacyJit | LegacyJit |    .NET 4.6.2 |  1,429.2 μs |    306.49 μs |    16.80 μs |   7.36 |    0.05 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn | net462 LegacyJit | LegacyJit |    .NET 4.6.2 | 57,660.4 μs |  7,506.23 μs |   411.44 μs | 297.08 |    5.87 | 8000.0000 | 222.2222 |     - | 49552.88 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |    net462 RyuJit |    RyuJit |    .NET 4.6.2 |    197.3 μs |     15.93 μs |     0.87 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |    net462 RyuJit |    RyuJit |    .NET 4.6.2 |  1,449.2 μs |     96.98 μs |     5.32 μs |   7.35 |    0.04 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |    net462 RyuJit |    RyuJit |    .NET 4.6.2 | 58,587.4 μs | 12,787.43 μs |   700.92 μs | 296.96 |    4.57 | 8000.0000 | 222.2222 |     - | 49552.85 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog | net472 LegacyJit | LegacyJit |    .NET 4.7.2 |    198.0 μs |     59.98 μs |     3.29 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff | net472 LegacyJit | LegacyJit |    .NET 4.7.2 |  1,444.5 μs |    336.92 μs |    18.47 μs |   7.30 |    0.16 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn | net472 LegacyJit | LegacyJit |    .NET 4.7.2 | 56,758.4 μs | 14,675.86 μs |   804.43 μs | 286.79 |    8.15 | 8000.0000 | 222.2222 |     - | 49552.85 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |    net472 RyuJit |    RyuJit |    .NET 4.7.2 |    195.7 μs |     14.08 μs |     0.77 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |    net472 RyuJit |    RyuJit |    .NET 4.7.2 |  1,436.1 μs |    348.71 μs |    19.11 μs |   7.34 |    0.08 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |    net472 RyuJit |    RyuJit |    .NET 4.7.2 | 58,131.8 μs |  8,498.80 μs |   465.85 μs | 297.04 |    3.55 | 8000.0000 | 222.2222 |     - | 49552.93 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |  net48 LegacyJit | LegacyJit |      .NET 4.8 |    196.3 μs |     29.02 μs |     1.59 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |  net48 LegacyJit | LegacyJit |      .NET 4.8 |  1,442.7 μs |    402.95 μs |    22.09 μs |   7.35 |    0.10 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |  net48 LegacyJit | LegacyJit |      .NET 4.8 | 57,542.8 μs | 10,911.34 μs |   598.09 μs | 293.09 |    4.13 | 8000.0000 | 222.2222 |     - | 49552.99 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |     net48 RyuJit |    RyuJit |      .NET 4.8 |    209.0 μs |     36.29 μs |     1.99 μs |   1.00 |    0.00 |   20.7520 |   3.4180 |     - |   128.52 KB |
| SimulateAAppWithSerilogOff |     net48 RyuJit |    RyuJit |      .NET 4.8 |  1,449.2 μs |    371.22 μs |    20.35 μs |   6.94 |    0.16 |  326.1719 |  54.6875 |     - |  2015.72 KB |
|  SimulateAAppWithSerilogOn |     net48 RyuJit |    RyuJit |      .NET 4.8 | 57,746.1 μs | 29,908.11 μs | 1,639.36 μs | 276.37 |    8.75 | 8000.0000 | 222.2222 |     - | 49552.11 KB |
|                            |                  |           |               |             |              |             |        |         |           |          |       |             |
| SimulateAAppWithoutSerilog |     net50 RyuJit |    RyuJit | .NET Core 5.0 |    139.3 μs |      5.61 μs |     0.31 μs |   1.00 |    0.00 |    6.3477 |   0.7324 |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |     net50 RyuJit |    RyuJit | .NET Core 5.0 |  1,508.5 μs |    571.28 μs |    31.31 μs |  10.83 |    0.20 |  439.4531 |  54.6875 |     - |  2702.13 KB |
|  SimulateAAppWithSerilogOn |     net50 RyuJit |    RyuJit | .NET Core 5.0 | 39,964.1 μs |  8,328.14 μs |   456.49 μs | 286.83 |    2.67 | 7846.1538 | 153.8462 |     - | 48456.28 KB |
