```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-3720QM CPU 2.60GHz, ProcessorCount=8
Frequency=2533306 ticks, Resolution=394.7411 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1080.0

Type=LogContextEnrichmentBenchmark  Mode=Throughput  

```
               Method |      Median |     StdDev | Scaled | Scaled-SD |
--------------------- |------------ |----------- |------- |---------- |
                 Bare |  14.5935 ns |  0.2010 ns |   1.00 |      0.00 |
         PushProperty | 384.9070 ns |  2.4957 ns |  26.61 |      0.41 |
   PushPropertyNested | 778.8093 ns | 51.8017 ns |  55.46 |      3.59 |
 PushPropertyEnriched | 598.1650 ns | 12.3394 ns |  41.52 |      1.01 |
