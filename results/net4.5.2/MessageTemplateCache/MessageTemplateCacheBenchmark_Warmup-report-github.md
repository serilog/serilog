```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-4790 CPU 3.60GHz, ProcessorCount=8
Frequency=3507509 ticks, Resolution=285.1026 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=MessageTemplateCacheBenchmark_Warmup  Mode=Throughput  

```
     Method | Items | MaxDegreeOfParallelism |          Median |        StdDev | Scaled | Scaled-SD |
----------- |------ |----------------------- |---------------- |-------------- |------- |---------- |
 **Dictionary** |    **10** |                     **-1** |       **8.9109 us** |     **0.0617 us** |   **1.00** |      **0.00** |
  Hashtable |    10 |                     -1 |       9.8728 us |     1.2285 us |   1.14 |      0.14 |
 Concurrent |    10 |                     -1 |      21.8053 us |     1.1545 us |   2.43 |      0.13 |
 **Dictionary** |    **10** |                      **1** |       **4.2491 us** |     **0.1455 us** |   **1.00** |      **0.00** |
  Hashtable |    10 |                      1 |       4.3648 us |     0.1695 us |   1.02 |      0.05 |
 Concurrent |    10 |                      1 |      10.6738 us |     1.2851 us |   2.63 |      0.31 |
 **Dictionary** |   **100** |                     **-1** |     **102.2525 us** |     **1.4686 us** |   **1.00** |      **0.00** |
  Hashtable |   100 |                     -1 |     106.2933 us |    15.0920 us |   1.08 |      0.15 |
 Concurrent |   100 |                     -1 |     832.1438 us |    15.1188 us |   8.09 |      0.18 |
 **Dictionary** |   **100** |                      **1** |      **24.2769 us** |     **0.4100 us** |   **1.00** |      **0.00** |
  Hashtable |   100 |                      1 |      25.9633 us |     0.3417 us |   1.06 |      0.02 |
 Concurrent |   100 |                      1 |     382.6723 us |    70.3974 us |  15.89 |      2.90 |
 **Dictionary** |  **1000** |                     **-1** |     **850.1014 us** |     **6.2974 us** |   **1.00** |      **0.00** |
  Hashtable |  1000 |                     -1 |     838.4812 us |     8.0631 us |   0.99 |      0.01 |
 Concurrent |  1000 |                     -1 |  34,932.4300 us | 2,431.0637 us |  41.29 |      2.87 |
 **Dictionary** |  **1000** |                      **1** |     **243.0266 us** |     **1.8920 us** |   **1.00** |      **0.00** |
  Hashtable |  1000 |                      1 |     273.0008 us |     3.9633 us |   1.12 |      0.02 |
 Concurrent |  1000 |                      1 |  15,031.3941 us | 1,152.7616 us |  61.35 |      4.74 |
 **Dictionary** | **10000** |                     **-1** |   **4,649.2931 us** |   **189.7156 us** |   **1.00** |      **0.00** |
  Hashtable | 10000 |                     -1 |   4,395.5897 us |   157.3345 us |   0.95 |      0.05 |
 Concurrent | 10000 |                     -1 | 330,528.5888 us | 7,753.0369 us |  71.13 |      3.30 |
 **Dictionary** | **10000** |                      **1** |   **2,054.1521 us** |    **11.6541 us** |   **1.00** |      **0.00** |
  Hashtable | 10000 |                      1 |   2,099.7652 us |    12.9432 us |   1.02 |      0.01 |
 Concurrent | 10000 |                      1 | 176,623.3173 us | 1,099.8169 us |  85.92 |      0.71 |
