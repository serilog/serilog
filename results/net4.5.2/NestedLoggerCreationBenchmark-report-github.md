```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-3720QM CPU 2.60GHz, ProcessorCount=8
Frequency=2533306 ticks, Resolution=394.7411 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1080.0

Type=NestedLoggerCreationBenchmark  Mode=Throughput  

```
           Method |      Median |    StdDev |
----------------- |------------ |---------- |
 ForContextString |  91.0471 ns | 0.5325 ns |
   ForContextType | 159.5909 ns | 1.3952 ns |
