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
     Method | Items | MaxDegreeOfParallelism |      Median |     StdDev | Scaled | Scaled-SD |
----------- |------ |----------------------- |------------ |----------- |------- |---------- |
 **Dictionary** |    **10** |                     **-1** |   **5.1555 us** |  **0.0507 us** |   **1.00** |      **0.00** |
  Hashtable |    10 |                     -1 |   4.4010 us |  0.0434 us |   0.85 |      0.01 |
 Concurrent |    10 |                     -1 |   4.4323 us |  0.0677 us |   0.86 |      0.02 |
 **Dictionary** |    **10** |                      **1** |   **2.9110 us** |  **0.1116 us** |   **1.00** |      **0.00** |
  Hashtable |    10 |                      1 |   2.8101 us |  0.1591 us |   0.97 |      0.07 |
 Concurrent |    10 |                      1 |   2.8076 us |  0.1195 us |   0.98 |      0.05 |
 **Dictionary** |    **20** |                     **-1** |   **9.5123 us** |  **0.1870 us** |   **1.00** |      **0.00** |
  Hashtable |    20 |                     -1 |   5.7014 us |  0.0338 us |   0.60 |      0.01 |
 Concurrent |    20 |                     -1 |   5.7452 us |  0.0450 us |   0.60 |      0.01 |
 **Dictionary** |    **20** |                      **1** |   **4.4900 us** |  **0.2552 us** |   **1.00** |      **0.00** |
  Hashtable |    20 |                      1 |   3.9176 us |  0.2345 us |   0.89 |      0.07 |
 Concurrent |    20 |                      1 |   3.8974 us |  0.2235 us |   0.88 |      0.07 |
 **Dictionary** |    **50** |                     **-1** |  **27.0377 us** |  **1.2542 us** |   **1.00** |      **0.00** |
  Hashtable |    50 |                     -1 |   7.5979 us |  0.0627 us |   0.28 |      0.01 |
 Concurrent |    50 |                     -1 |   7.6123 us |  0.0427 us |   0.28 |      0.01 |
 **Dictionary** |    **50** |                      **1** |   **8.0037 us** |  **0.5410 us** |   **1.00** |      **0.00** |
  Hashtable |    50 |                      1 |   7.1405 us |  0.5055 us |   0.90 |      0.08 |
 Concurrent |    50 |                      1 |   7.2504 us |  0.5599 us |   0.90 |      0.09 |
 **Dictionary** |   **100** |                     **-1** |  **53.3566 us** |  **7.5268 us** |   **1.00** |      **0.00** |
  Hashtable |   100 |                     -1 |  10.1725 us |  0.0903 us |   0.19 |      0.02 |
 Concurrent |   100 |                     -1 |  10.1390 us |  0.1140 us |   0.19 |      0.02 |
 **Dictionary** |   **100** |                      **1** |  **13.4704 us** |  **0.0694 us** |   **1.00** |      **0.00** |
  Hashtable |   100 |                      1 |  11.7881 us |  0.6524 us |   0.88 |      0.05 |
 Concurrent |   100 |                      1 |  12.0876 us |  0.8071 us |   0.91 |      0.06 |
 **Dictionary** |  **1000** |                     **-1** | **482.1057 us** | **17.5977 us** |   **1.00** |      **0.00** |
  Hashtable |  1000 |                     -1 |  33.1846 us |  0.3355 us |   0.07 |      0.00 |
 Concurrent |  1000 |                     -1 |  32.8517 us |  0.3190 us |   0.07 |      0.00 |
 **Dictionary** |  **1000** |                      **1** | **124.2795 us** |  **0.7927 us** |   **1.00** |      **0.00** |
  Hashtable |  1000 |                      1 | 105.6937 us |  1.1198 us |   0.85 |      0.01 |
 Concurrent |  1000 |                      1 | 103.0948 us |  0.8157 us |   0.83 |      0.01 |
