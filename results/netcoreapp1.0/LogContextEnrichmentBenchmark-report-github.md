```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Windows
Processor=?, ProcessorCount=8
Frequency=2533306 ticks, Resolution=394.7411 ns, Timer=TSC
CLR=CORE, Arch=64-bit ? [RyuJIT]
GC=Concurrent Workstation
dotnet cli version: 1.0.0-preview2-003121

Type=LogContextEnrichmentBenchmark  Mode=Throughput  

```
               Method |      Median |     StdDev | Scaled | Scaled-SD |
--------------------- |------------ |----------- |------- |---------- |
                 Bare |  14.3441 ns |  0.2826 ns |   1.00 |      0.00 |
         PushProperty | 450.5621 ns |  3.2270 ns |  31.42 |      0.65 |
   PushPropertyNested | 920.1179 ns | 45.1624 ns |  63.81 |      3.30 |
 PushPropertyEnriched | 730.4018 ns |  4.5812 ns |  50.92 |      1.03 |
