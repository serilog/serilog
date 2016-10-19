```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Windows
Processor=?, ProcessorCount=8
Frequency=3507509 ticks, Resolution=285.1026 ns, Timer=TSC
CLR=CORE, Arch=64-bit ? [RyuJIT]
GC=Concurrent Workstation
dotnet cli version: 1.0.0-preview2-003131

Type=MessageTemplateCacheBenchmark_Cached  Mode=Throughput  

```
     Method | Items | MaxDegreeOfParallelism |        Median |     StdDev | Scaled | Scaled-SD |
----------- |------ |----------------------- |-------------- |----------- |------- |---------- |
 **Dictionary** |    **10** |                     **-1** |   **471.8337 us** | **12.2518 us** |   **1.00** |      **0.00** |
  Hashtable |    10 |                     -1 |    30.1689 us |  0.6795 us |   0.06 |      0.00 |
 Concurrent |    10 |                     -1 |    29.0805 us |  0.1960 us |   0.06 |      0.00 |
 **Dictionary** |    **10** |                      **1** |   **130.2107 us** |  **3.1653 us** |   **1.00** |      **0.00** |
  Hashtable |    10 |                      1 |   122.2643 us |  2.4861 us |   0.94 |      0.03 |
 Concurrent |    10 |                      1 |   111.4388 us |  1.5693 us |   0.86 |      0.02 |
 **Dictionary** |    **20** |                     **-1** |   **807.0063 us** |  **2.6482 us** |   **1.00** |      **0.00** |
  Hashtable |    20 |                     -1 |    52.2398 us |  1.0080 us |   0.06 |      0.00 |
 Concurrent |    20 |                     -1 |    51.2831 us |  0.3521 us |   0.06 |      0.00 |
 **Dictionary** |    **20** |                      **1** |   **259.5397 us** |  **7.4273 us** |   **1.00** |      **0.00** |
  Hashtable |    20 |                      1 |   245.7154 us |  7.1712 us |   0.94 |      0.04 |
 Concurrent |    20 |                      1 |   222.5680 us |  2.6695 us |   0.85 |      0.02 |
 **Dictionary** |    **50** |                     **-1** | **1,296.5404 us** | **21.9341 us** |   **1.00** |      **0.00** |
  Hashtable |    50 |                     -1 |   121.7466 us |  1.9856 us |   0.09 |      0.00 |
 Concurrent |    50 |                     -1 |   117.7603 us |  0.9613 us |   0.09 |      0.00 |
 **Dictionary** |    **50** |                      **1** |   **650.9873 us** |  **8.5592 us** |   **1.00** |      **0.00** |
  Hashtable |    50 |                      1 |   615.8303 us | 11.6568 us |   0.94 |      0.02 |
 Concurrent |    50 |                      1 |   557.0763 us |  6.8188 us |   0.86 |      0.02 |
 **Dictionary** |   **100** |                     **-1** | **2,330.0390 us** | **10.0342 us** |   **1.00** |      **0.00** |
  Hashtable |   100 |                     -1 |   235.0997 us |  3.6531 us |   0.10 |      0.00 |
 Concurrent |   100 |                     -1 |   227.1950 us |  2.0706 us |   0.10 |      0.00 |
 **Dictionary** |   **100** |                      **1** | **1,306.6364 us** | **60.9756 us** |   **1.00** |      **0.00** |
  Hashtable |   100 |                      1 | 1,214.4314 us | 18.8564 us |   0.92 |      0.04 |
 Concurrent |   100 |                      1 | 1,113.6760 us |  8.7944 us |   0.84 |      0.04 |
 **Dictionary** |  **1000** |                     **-1** | **2,338.3248 us** | **20.7148 us** |   **1.00** |      **0.00** |
  Hashtable |  1000 |                     -1 |   238.6618 us |  1.8611 us |   0.10 |      0.00 |
 Concurrent |  1000 |                     -1 |   232.2733 us |  1.5743 us |   0.10 |      0.00 |
 **Dictionary** |  **1000** |                      **1** | **1,311.6107 us** | **12.7315 us** |   **1.00** |      **0.00** |
  Hashtable |  1000 |                      1 | 1,246.3194 us |  8.1155 us |   0.95 |      0.01 |
 Concurrent |  1000 |                      1 | 1,136.7721 us | 15.0574 us |   0.87 |      0.01 |
