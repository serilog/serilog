```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Windows
Processor=?, ProcessorCount=8
Frequency=2533306 ticks, Resolution=394.7411 ns, Timer=TSC
CLR=CORE, Arch=64-bit ? [RyuJIT]
GC=Concurrent Workstation
dotnet cli version: 1.0.0-preview2-003121

Type=NestedLoggerLatencyBenchmark  Mode=Throughput  

```
       Method |     Median |    StdDev | Scaled | Scaled-SD |
------------- |----------- |---------- |------- |---------- |
   RootLogger | 14.0131 ns | 0.2212 ns |   1.00 |      0.00 |
 NestedLogger | 59.4502 ns | 1.3198 ns |   4.23 |      0.11 |
