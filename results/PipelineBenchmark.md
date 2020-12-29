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
|                                                 Method |    Job |       Runtime |             Mean |          Error |         StdDev |      Ratio |   RatioSD |    Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|------------------------------------------------------- |------- |-------------- |-----------------:|---------------:|---------------:|-----------:|----------:|---------:|--------:|--------:|----------:|
|                                   EmitLogAIgnoredEvent | core31 | .NET Core 3.1 |        12.258 ns |      0.1194 ns |      0.1787 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent | core31 | .NET Core 3.1 |       584.571 ns |      7.9759 ns |     11.9379 ns |      47.71 |      1.44 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext | core31 | .NET Core 3.1 |       647.250 ns |      8.5104 ns |     12.4744 ns |      52.76 |      1.01 |   0.0668 |       - |       - |     424 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext | core31 | .NET Core 3.1 |       626.910 ns |      8.8290 ns |     13.2148 ns |      51.16 |      1.41 |   0.0582 |       - |       - |     368 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext | core31 | .NET Core 3.1 |     1,126.123 ns |     21.0808 ns |     31.5528 ns |      91.90 |      3.26 |   0.1564 |       - |       - |     984 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext | core31 | .NET Core 3.1 |     1,325.948 ns |     17.8084 ns |     26.6548 ns |     108.20 |      2.90 |   0.2041 |       - |       - |    1288 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext | core31 | .NET Core 3.1 |     6,222.577 ns |     70.3448 ns |    105.2888 ns |     507.74 |     10.33 |   1.2054 |  0.0076 |       - |    7584 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext | core31 | .NET Core 3.1 |    53,833.608 ns |    549.6814 ns |    822.7372 ns |   4,393.03 |    104.48 |  11.8408 |  1.0376 |       - |   74616 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | core31 | .NET Core 3.1 | 1,111,673.984 ns | 27,277.0218 ns | 40,826.9618 ns |  90,711.67 |  3,616.53 | 119.1406 | 54.6875 | 29.2969 |  735963 B |
|                                                        |        |               |                  |                |                |            |           |          |         |         |           |
|                                   EmitLogAIgnoredEvent |  net48 |      .NET 4.8 |        13.235 ns |      0.2454 ns |      0.3674 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |  net48 |      .NET 4.8 |       594.859 ns |      8.2992 ns |     12.4218 ns |      44.98 |      1.63 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net48 |      .NET 4.8 |       672.027 ns |      8.7593 ns |     13.1106 ns |      50.83 |      2.16 |   0.0687 |       - |       - |     433 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net48 |      .NET 4.8 |       647.910 ns |      7.5373 ns |     11.2814 ns |      48.99 |      1.40 |   0.0591 |       - |       - |     377 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net48 |      .NET 4.8 |     1,271.135 ns |     16.6191 ns |     24.8746 ns |      96.10 |      2.62 |   0.1602 |       - |       - |    1011 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net48 |      .NET 4.8 |     1,483.396 ns |     18.3766 ns |     27.5052 ns |     112.16 |      3.64 |   0.2079 |       - |       - |    1316 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net48 |      .NET 4.8 |     7,187.193 ns |     56.0759 ns |     83.9318 ns |     543.44 |     15.70 |   1.1826 |  0.0076 |       - |    7486 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net48 |      .NET 4.8 |    66,417.747 ns |  1,133.2314 ns |  1,696.1674 ns |   5,022.36 |    194.91 |  11.7188 |  0.8545 |       - |   73988 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net48 |      .NET 4.8 | 1,221,919.460 ns | 18,370.9836 ns | 27,496.8233 ns |  92,399.95 |  3,443.66 | 119.1406 | 56.6406 | 29.2969 |  737005 B |
|                                                        |        |               |                  |                |                |            |           |          |         |         |           |
|                                   EmitLogAIgnoredEvent |  net50 | .NET Core 5.0 |         7.274 ns |      0.1200 ns |      0.1795 ns |       1.00 |      0.00 |        - |       - |       - |         - |
|                                           EmitLogEvent |  net50 | .NET Core 5.0 |       346.047 ns |      4.3180 ns |      6.3293 ns |      47.63 |      1.53 |   0.0596 |       - |       - |     376 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |  net50 | .NET Core 5.0 |       391.466 ns |      4.5785 ns |      6.8529 ns |      53.85 |      1.82 |   0.0687 |       - |       - |     432 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |  net50 | .NET Core 5.0 |       389.930 ns |      6.0807 ns |      9.1013 ns |      53.63 |      1.67 |   0.0596 |       - |       - |     376 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |  net50 | .NET Core 5.0 |       774.065 ns |      9.0937 ns |     13.6111 ns |     106.48 |      3.40 |   0.1574 |       - |       - |     992 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |  net50 | .NET Core 5.0 |       958.493 ns |     15.5420 ns |     23.2625 ns |     131.85 |      4.70 |   0.2060 |       - |       - |    1296 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |  net50 | .NET Core 5.0 |     4,857.891 ns |     78.0343 ns |    116.7981 ns |     668.13 |     19.55 |   1.2054 |  0.0076 |       - |    7592 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  net50 | .NET Core 5.0 |    42,845.807 ns |    564.9952 ns |    828.1626 ns |   5,897.34 |    196.34 |  11.8408 |  0.9766 |       - |   74624 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext |  net50 | .NET Core 5.0 |   917,615.775 ns | 49,247.3616 ns | 70,629.0944 ns | 126,201.46 | 10,537.36 | 119.1406 | 44.9219 | 29.2969 |  735952 B |
