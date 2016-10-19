```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Windows
Processor=?, ProcessorCount=8
Frequency=3507509 ticks, Resolution=285.1026 ns, Timer=TSC
CLR=CORE, Arch=64-bit ? [RyuJIT]
GC=Concurrent Workstation
dotnet cli version: 1.0.0-preview2-003131

Type=MessageTemplateCacheBenchmark_Leaking  Mode=Throughput  

```
     Method | Items | MaxDegreeOfParallelism |      Median |     StdDev | Scaled | Scaled-SD |
----------- |------ |----------------------- |------------ |----------- |------- |---------- |
 **Dictionary** | **10000** |                     **-1** |   **4.0866 ms** |  **0.1704 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |                     -1 |   2.3930 ms |  0.1114 ms |   0.59 |      0.04 |
 Concurrent | 10000 |                     -1 |  94.0910 ms | 13.6877 ms |  22.44 |      3.46 |
 **Dictionary** | **10000** |                      **1** |   **2.3378 ms** |  **0.0076 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |                      1 |   2.4907 ms |  0.0108 ms |   1.07 |      0.01 |
 Concurrent | 10000 |                      1 | 165.0691 ms |  1.1892 ms |  70.74 |      0.54 |
