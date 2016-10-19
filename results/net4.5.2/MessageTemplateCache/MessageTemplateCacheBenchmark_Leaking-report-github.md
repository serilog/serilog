```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-4790 CPU 3.60GHz, ProcessorCount=8
Frequency=3507509 ticks, Resolution=285.1026 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=MessageTemplateCacheBenchmark_Leaking  Mode=Throughput  

```
     Method | Items | MaxDegreeOfParallelism |      Median |    StdDev | Scaled | Scaled-SD |
----------- |------ |----------------------- |------------ |---------- |------- |---------- |
 **Dictionary** | **10000** |                     **-1** |   **4.2101 ms** | **0.1586 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |                     -1 |   3.2149 ms | 0.0669 ms |   0.76 |      0.03 |
 Concurrent | 10000 |                     -1 | 114.5999 ms | 9.5658 ms |  26.59 |      2.44 |
 **Dictionary** | **10000** |                      **1** |   **1.9974 ms** | **0.0107 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |                      1 |   2.0848 ms | 0.0108 ms |   1.04 |      0.01 |
 Concurrent | 10000 |                      1 | 176.0598 ms | 0.9919 ms |  88.22 |      0.67 |
