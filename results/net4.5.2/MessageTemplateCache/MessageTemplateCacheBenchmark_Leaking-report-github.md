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
     Method | Items | OverflowCount | MaxDegreeOfParallelism |      Median |     StdDev | Scaled | Scaled-SD |
----------- |------ |-------------- |----------------------- |------------ |----------- |------- |---------- |
 **Dictionary** | **10000** |             **1** |                     **-1** |   **3.9319 ms** |  **0.1209 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |             1 |                     -1 |   3.3042 ms |  0.1371 ms |   0.83 |      0.04 |
 Concurrent | 10000 |             1 |                     -1 |  92.5456 ms |  6.2102 ms |  23.77 |      1.73 |
 **Dictionary** | **10000** |             **1** |                      **1** |   **2.0104 ms** |  **0.0072 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |             1 |                      1 |   2.0956 ms |  0.0115 ms |   1.04 |      0.01 |
 Concurrent | 10000 |             1 |                      1 | 176.4643 ms |  1.1110 ms |  87.81 |      0.62 |
 **Dictionary** | **10000** |            **10** |                     **-1** |   **4.0999 ms** |  **0.1572 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |            10 |                     -1 |   3.2378 ms |  0.0898 ms |   0.79 |      0.04 |
 Concurrent | 10000 |            10 |                     -1 |  99.3250 ms |  7.0231 ms |  24.21 |      1.93 |
 **Dictionary** | **10000** |            **10** |                      **1** |   **2.0057 ms** |  **0.0085 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |            10 |                      1 |   2.0805 ms |  0.0107 ms |   1.04 |      0.01 |
 Concurrent | 10000 |            10 |                      1 | 176.0989 ms |  0.9909 ms |  87.83 |      0.60 |
 **Dictionary** | **10000** |           **100** |                     **-1** |   **4.2080 ms** |  **0.1332 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |           100 |                     -1 |   3.1794 ms |  0.1479 ms |   0.77 |      0.04 |
 Concurrent | 10000 |           100 |                     -1 | 116.0221 ms |  8.9935 ms |  27.49 |      2.28 |
 **Dictionary** | **10000** |           **100** |                      **1** |   **2.0018 ms** |  **0.0070 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |           100 |                      1 |   2.0884 ms |  0.0116 ms |   1.04 |      0.01 |
 Concurrent | 10000 |           100 |                      1 | 177.2720 ms |  0.9531 ms |  88.50 |      0.55 |
 **Dictionary** | **10000** |          **1000** |                     **-1** |   **4.4081 ms** |  **0.1243 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |          1000 |                     -1 |   3.7401 ms |  0.2270 ms |   0.87 |      0.06 |
 Concurrent | 10000 |          1000 |                     -1 | 201.6415 ms | 28.0265 ms |  45.39 |      6.50 |
 **Dictionary** | **10000** |          **1000** |                      **1** |   **2.0047 ms** |  **0.0061 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |          1000 |                      1 |   2.0889 ms |  0.0116 ms |   1.04 |      0.01 |
 Concurrent | 10000 |          1000 |                      1 | 176.6271 ms |  1.3462 ms |  88.05 |      0.70 |
