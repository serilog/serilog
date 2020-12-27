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
|                                                 Method |    Job |       Runtime |           Mean |         Error |        StdDev |         Median |     Ratio |  RatioSD |   Gen 0 |   Gen 1 | Gen 2 | Allocated |
|------------------------------------------------------- |------- |-------------- |---------------:|--------------:|--------------:|---------------:|----------:|---------:|--------:|--------:|------:|----------:|
|                                   EmitLogAIgnoredEvent | core31 | .NET Core 3.1 |      14.957 ns |     1.1670 ns |     1.6737 ns |      15.983 ns |      1.00 |     0.00 |       - |       - |     - |         - |
|                                           EmitLogEvent | core31 | .NET Core 3.1 |     638.612 ns |     9.0238 ns |    13.2270 ns |     638.972 ns |     43.12 |     4.32 |  0.0582 |       - |     - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | core31 | .NET Core 3.1 |     721.033 ns |     7.6940 ns |    11.5160 ns |     718.053 ns |     48.71 |     5.00 |  0.0620 |       - |     - |     392 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | core31 | .NET Core 3.1 |     708.560 ns |     6.4500 ns |     9.6541 ns |     709.396 ns |     48.01 |     5.76 |  0.0582 |       - |     - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | core31 | .NET Core 3.1 |   1,227.727 ns |    15.9953 ns |    23.9410 ns |   1,225.167 ns |     83.29 |    10.70 |  0.1507 |       - |     - |     952 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | core31 | .NET Core 3.1 |   1,367.017 ns |    44.5841 ns |    66.7314 ns |   1,363.347 ns |     91.85 |     6.83 |  0.1717 |       - |     - |    1088 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | core31 | .NET Core 3.1 |   6,263.486 ns |    49.5843 ns |    74.2155 ns |   6,254.723 ns |    423.35 |    45.72 |  0.9613 |  0.0076 |     - |    6032 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | core31 | .NET Core 3.1 |  53,622.610 ns |   265.6252 ns |   397.5753 ns |  53,652.643 ns |  3,627.36 |   411.45 |  8.9111 |  0.6714 |     - |   55921 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | core31 | .NET Core 3.1 | 554,900.438 ns | 3,921.8009 ns | 5,748.5249 ns | 554,183.398 ns | 37,542.82 | 4,187.59 | 86.9141 | 34.1797 |     - |  550834 B |
|                                                        |        |               |                |               |               |                |           |          |         |         |       |           |
|                                   EmitLogAIgnoredEvent |  net48 |      .NET 4.8 |      14.744 ns |     0.1503 ns |     0.2203 ns |      14.782 ns |      1.00 |     0.00 |       - |       - |     - |         - |
|                                           EmitLogEvent |  net48 |      .NET 4.8 |     625.170 ns |     5.0542 ns |     7.5649 ns |     626.601 ns |     42.39 |     0.79 |  0.0591 |       - |     - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net48 |      .NET 4.8 |     713.336 ns |     5.2104 ns |     7.7987 ns |     715.488 ns |     48.37 |     0.85 |  0.0629 |       - |     - |     401 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net48 |      .NET 4.8 |     690.969 ns |     5.7612 ns |     8.4446 ns |     694.050 ns |     46.87 |     0.91 |  0.0591 |       - |     - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net48 |      .NET 4.8 |   1,329.916 ns |    10.7256 ns |    16.0535 ns |   1,331.788 ns |     90.25 |     1.54 |  0.1545 |       - |     - |     979 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net48 |      .NET 4.8 |   1,481.713 ns |    11.3212 ns |    16.9451 ns |   1,481.232 ns |    100.48 |     2.09 |  0.1755 |       - |     - |    1115 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net48 |      .NET 4.8 |   7,089.427 ns |    55.8604 ns |    80.1134 ns |   7,088.899 ns |    481.14 |    10.57 |  0.9384 |       - |     - |    5929 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net48 |      .NET 4.8 |  64,349.206 ns |   650.6394 ns |   973.8464 ns |  64,228.583 ns |  4,367.49 |   121.00 |  8.6670 |  0.8545 |     - |   55241 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net48 |      .NET 4.8 | 689,907.474 ns | 5,317.5522 ns | 7,959.0617 ns | 691,283.838 ns | 46,798.22 |   889.25 | 86.9141 | 27.3438 |     - |  551390 B |
|                                                        |        |               |                |               |               |                |           |          |         |         |       |           |
|                                   EmitLogAIgnoredEvent |  net50 | .NET Core 5.0 |       7.709 ns |     0.5028 ns |     0.7525 ns |       7.710 ns |      1.00 |     0.00 |       - |       - |     - |         - |
|                                           EmitLogEvent |  net50 | .NET Core 5.0 |     392.312 ns |    17.8753 ns |    26.2014 ns |     374.982 ns |     51.55 |     8.46 |  0.0596 |       - |     - |     376 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net50 | .NET Core 5.0 |     412.258 ns |     2.4565 ns |     3.6767 ns |     413.123 ns |     53.97 |     5.18 |  0.0634 |       - |     - |     400 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net50 | .NET Core 5.0 |     405.886 ns |     3.6225 ns |     5.4219 ns |     405.569 ns |     53.09 |     4.64 |  0.0596 |       - |     - |     376 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net50 | .NET Core 5.0 |     790.985 ns |     8.7187 ns |    13.0498 ns |     788.934 ns |    103.47 |     9.18 |  0.1526 |       - |     - |     960 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net50 | .NET Core 5.0 |     903.728 ns |     5.0539 ns |     7.4080 ns |     903.355 ns |    117.98 |    11.15 |  0.1745 |       - |     - |    1096 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net50 | .NET Core 5.0 |   4,826.823 ns |    43.1512 ns |    64.5868 ns |   4,835.307 ns |    632.25 |    65.10 |  0.9613 |  0.0076 |     - |    6040 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net50 | .NET Core 5.0 |  41,153.531 ns |   377.2448 ns |   564.6423 ns |  41,229.269 ns |  5,389.71 |   548.21 |  8.9111 |  0.6714 |     - |   55928 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net50 | .NET Core 5.0 | 428,891.851 ns | 2,657.5058 ns | 3,895.3375 ns | 429,429.883 ns | 55,997.87 | 5,391.99 | 87.4023 | 34.6680 |     - |  550832 B |
