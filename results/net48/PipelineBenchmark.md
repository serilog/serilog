``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT


```
|                                                 Method |           Mean |         Error |        StdDev |     Ratio | RatioSD |   Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------------------------------- |---------------:|--------------:|--------------:|----------:|--------:|--------:|-------:|------:|----------:|
|                                   EmitLogAIgnoredEvent |       8.559 ns |     0.1895 ns |     0.1946 ns |      1.00 |    0.00 |       - |      - |     - |         - |
|                                           EmitLogEvent |   1,528.397 ns |    30.0047 ns |    28.0664 ns |    178.59 |    4.94 |  0.0401 |      - |     - |     216 B |
|          EmitLogEventWith1Enrich0ForContext0LogContext |   1,592.943 ns |    30.5411 ns |    28.5681 ns |    186.16 |    6.25 |  0.0458 |      - |     - |     244 B |
|          EmitLogEventWith0Enrich1ForContext0LogContext |   1,575.094 ns |    19.0266 ns |    17.7975 ns |    184.06 |    5.16 |  0.0401 |      - |     - |     216 B |
|          EmitLogEventWith0Enrich0ForContext1LogContext |   2,181.253 ns |    43.2914 ns |    64.7965 ns |    252.87 |    8.55 |  0.1068 |      - |     - |     565 B |
|          EmitLogEventWith1Enrich1ForContext1LogContext |   2,293.900 ns |    35.2724 ns |    32.9938 ns |    268.06 |    8.00 |  0.1106 |      - |     - |     593 B |
|       EmitLogEventWith10Enrich10ForContext10LogContext |   7,792.760 ns |   154.5485 ns |   216.6554 ns |    911.89 |   31.33 |  0.8240 |      - |     - |    4374 B |
|    EmitLogEventWith100Enrich100ForContext100LogContext |  15,009.949 ns |   294.4940 ns |   393.1410 ns |  1,754.74 |   73.54 |  1.9836 | 0.0305 |     - |   10439 B |
| EmitLogEventWith1000Enrich1000ForContext1000LogContext | 150,379.133 ns | 2,960.3514 ns | 3,951.9840 ns | 17,594.29 |  401.18 | 19.0430 | 1.9531 |     - |  101430 B |
