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
|                                                 Method |    Job |       Runtime |             Mean |          Error |         StdDev |           Median |      Ratio |  RatioSD |    Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|------------------------------------------------------- |------- |-------------- |-----------------:|---------------:|---------------:|-----------------:|-----------:|---------:|---------:|--------:|--------:|----------:|
|                                   EmitLogAIgnoredEvent | core31 | .NET Core 3.1 |        12.055 ns |      0.4199 ns |      0.5886 ns |        12.482 ns |       1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent | core31 | .NET Core 3.1 |       614.207 ns |      4.8482 ns |      6.9531 ns |       613.851 ns |      51.09 |     2.62 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | core31 | .NET Core 3.1 |       692.438 ns |      4.9842 ns |      7.4601 ns |       693.929 ns |      57.54 |     2.89 |   0.0668 |       - |       - |     424 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | core31 | .NET Core 3.1 |       642.482 ns |      5.0691 ns |      7.5872 ns |       642.201 ns |      53.38 |     2.75 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | core31 | .NET Core 3.1 |     1,172.675 ns |      9.4425 ns |     14.1331 ns |     1,171.986 ns |      97.52 |     4.93 |   0.1564 |       - |       - |     984 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | core31 | .NET Core 3.1 |     1,409.307 ns |     10.7302 ns |     16.0604 ns |     1,410.680 ns |     117.25 |     5.86 |   0.2041 |       - |       - |    1288 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | core31 | .NET Core 3.1 |     6,482.084 ns |     45.4754 ns |     68.0654 ns |     6,482.333 ns |     539.07 |    25.39 |   1.2054 |  0.0076 |       - |    7584 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | core31 | .NET Core 3.1 |    56,656.172 ns |    309.4956 ns |    463.2384 ns |    56,783.676 ns |   4,712.12 |   231.43 |  11.8408 |  1.0376 |       - |   74616 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | core31 | .NET Core 3.1 | 1,176,281.309 ns | 16,993.0349 ns | 25,434.3745 ns | 1,177,996.582 ns |  97,816.19 | 5,108.52 | 119.1406 | 52.7344 | 29.2969 |  735963 B |
|                                                        |        |               |                  |                |                |                  |            |          |          |         |         |           |
|                                   EmitLogAIgnoredEvent |  net48 |      .NET 4.8 |        14.178 ns |      0.2343 ns |      0.3507 ns |        14.210 ns |       1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |  net48 |      .NET 4.8 |       626.186 ns |      5.0818 ns |      7.6061 ns |       625.203 ns |      44.19 |     1.27 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net48 |      .NET 4.8 |       696.208 ns |      4.7775 ns |      7.1508 ns |       694.963 ns |      49.12 |     1.00 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net48 |      .NET 4.8 |       680.631 ns |      6.6157 ns |      9.9020 ns |       680.311 ns |      48.03 |     1.08 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net48 |      .NET 4.8 |     1,313.742 ns |      8.9940 ns |     13.4617 ns |     1,314.332 ns |      92.70 |     2.16 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net48 |      .NET 4.8 |     1,545.127 ns |     11.7338 ns |     17.5626 ns |     1,543.621 ns |     109.02 |     2.16 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net48 |      .NET 4.8 |     7,465.173 ns |     36.2506 ns |     54.2582 ns |     7,466.070 ns |     526.79 |    11.96 |   1.1826 |  0.0076 |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net48 |      .NET 4.8 |    69,382.373 ns |    631.6677 ns |    945.4504 ns |    69,145.978 ns |   4,897.22 |   165.61 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net48 |      .NET 4.8 | 1,266,538.340 ns | 14,799.1732 ns | 22,150.7057 ns | 1,268,770.410 ns |  89,375.52 | 2,503.06 | 119.1406 | 56.6406 | 29.2969 |  737005 B |
|                                                        |        |               |                  |                |                |                  |            |          |          |         |         |           |
|                                   EmitLogAIgnoredEvent |  net50 | .NET Core 5.0 |         7.586 ns |      0.0726 ns |      0.1064 ns |         7.567 ns |       1.00 |     0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |  net50 | .NET Core 5.0 |       349.597 ns |      2.8707 ns |      4.2967 ns |       349.854 ns |      46.07 |     0.79 |   0.0596 |       - |       - |     376 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net50 | .NET Core 5.0 |       400.196 ns |      2.4111 ns |      3.6088 ns |       401.009 ns |      52.78 |     0.81 |   0.0687 |       - |       - |     432 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net50 | .NET Core 5.0 |       397.589 ns |      3.1955 ns |      4.7828 ns |       397.363 ns |      52.40 |     1.06 |   0.0596 |       - |       - |     376 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net50 | .NET Core 5.0 |       806.949 ns |     12.3735 ns |     18.1369 ns |       801.855 ns |     106.38 |     2.10 |   0.1574 |       - |       - |     992 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net50 | .NET Core 5.0 |       985.917 ns |     12.1924 ns |     17.8715 ns |       981.797 ns |     130.00 |     3.32 |   0.2060 |       - |       - |    1296 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net50 | .NET Core 5.0 |     5,081.107 ns |     45.6860 ns |     68.3807 ns |     5,058.711 ns |     669.51 |     8.32 |   1.2054 |  0.0076 |       - |    7592 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net50 | .NET Core 5.0 |    44,888.550 ns |    300.2689 ns |    449.4284 ns |    44,861.346 ns |   5,921.35 |   108.98 |  11.8408 |  0.9766 |       - |   74624 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net50 | .NET Core 5.0 |   958,171.198 ns | 47,696.9562 ns | 71,390.5582 ns |   956,299.316 ns | 125,862.04 | 8,669.24 | 119.1406 | 56.6406 | 29.2969 |  735952 B |
