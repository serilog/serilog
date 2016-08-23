```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Windows
Processor=?, ProcessorCount=8
Frequency=2533306 ticks, Resolution=394.7411 ns, Timer=TSC
CLR=CORE, Arch=64-bit ? [RyuJIT]
GC=Concurrent Workstation
dotnet cli version: 1.0.0-preview2-003121

Type=NestedLoggerCreationBenchmark  Mode=Throughput  

```
           Method |      Median |    StdDev |
----------------- |------------ |---------- |
 ForContextString |  92.1957 ns | 0.8786 ns |
   ForContextType | 155.5276 ns | 0.8364 ns |
