```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-4790 CPU 3.60GHz, ProcessorCount=8
Frequency=3507509 ticks, Resolution=285.1026 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=MessageTemplateCacheBenchmark_Cached  Mode=Throughput  

```
     Method | Items | MaxDegreeOfParallelism |        Median |     StdDev | Scaled | Scaled-SD |
----------- |------ |----------------------- |-------------- |----------- |------- |---------- |
 **Dictionary** |    **10** |                     **-1** |   **479.2330 us** |  **8.4171 us** |   **1.00** |      **0.00** |
  Hashtable |    10 |                     -1 |    31.3853 us |  0.9122 us |   0.07 |      0.00 |
 Concurrent |    10 |                     -1 |    30.8517 us |  0.5337 us |   0.06 |      0.00 |
 **Dictionary** |    **10** |                      **1** |   **118.6307 us** |  **2.1275 us** |   **1.00** |      **0.00** |
  Hashtable |    10 |                      1 |   101.8377 us |  3.3030 us |   0.86 |      0.03 |
 Concurrent |    10 |                      1 |    99.8312 us |  2.0072 us |   0.85 |      0.02 |
 **Dictionary** |    **20** |                     **-1** |   **814.2458 us** |  **4.7079 us** |   **1.00** |      **0.00** |
  Hashtable |    20 |                     -1 |    54.0605 us |  0.8628 us |   0.07 |      0.00 |
 Concurrent |    20 |                     -1 |    54.0682 us |  1.1269 us |   0.07 |      0.00 |
 **Dictionary** |    **20** |                      **1** |   **247.6677 us** |  **3.9224 us** |   **1.00** |      **0.00** |
  Hashtable |    20 |                      1 |   212.1726 us |  7.2594 us |   0.85 |      0.03 |
 Concurrent |    20 |                      1 |   201.6514 us |  7.5841 us |   0.81 |      0.03 |
 **Dictionary** |    **50** |                     **-1** | **1,522.8901 us** |  **7.7962 us** |   **1.00** |      **0.00** |
  Hashtable |    50 |                     -1 |   123.5203 us |  2.2916 us |   0.08 |      0.00 |
 Concurrent |    50 |                     -1 |   121.7588 us |  2.8585 us |   0.08 |      0.00 |
 **Dictionary** |    **50** |                      **1** |   **591.6731 us** |  **5.2648 us** |   **1.00** |      **0.00** |
  Hashtable |    50 |                      1 |   515.3277 us | 10.1826 us |   0.87 |      0.02 |
 Concurrent |    50 |                      1 |   493.4330 us |  4.9567 us |   0.83 |      0.01 |
 **Dictionary** |   **100** |                     **-1** | **2,469.4364 us** | **10.4088 us** |   **1.00** |      **0.00** |
  Hashtable |   100 |                     -1 |   237.0286 us |  2.6360 us |   0.10 |      0.00 |
 Concurrent |   100 |                     -1 |   238.5870 us |  3.0985 us |   0.10 |      0.00 |
 **Dictionary** |   **100** |                      **1** | **1,157.9635 us** |  **8.0010 us** |   **1.00** |      **0.00** |
  Hashtable |   100 |                      1 |   982.2426 us | 16.9287 us |   0.85 |      0.02 |
 Concurrent |   100 |                      1 |   964.2989 us | 19.8753 us |   0.84 |      0.02 |
 **Dictionary** |  **1000** |                     **-1** | **2,479.2089 us** | **10.0258 us** |   **1.00** |      **0.00** |
  Hashtable |  1000 |                     -1 |   244.9505 us |  3.0286 us |   0.10 |      0.00 |
 Concurrent |  1000 |                     -1 |   239.5800 us |  3.2452 us |   0.10 |      0.00 |
 **Dictionary** |  **1000** |                      **1** | **1,164.8552 us** |  **6.4559 us** |   **1.00** |      **0.00** |
  Hashtable |  1000 |                      1 | 1,002.4865 us | 10.4370 us |   0.86 |      0.01 |
 Concurrent |  1000 |                      1 |   973.6633 us | 13.2212 us |   0.84 |      0.01 |
