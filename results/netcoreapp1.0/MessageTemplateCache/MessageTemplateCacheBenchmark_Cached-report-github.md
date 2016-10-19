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
     Method | Items | MaxDegreeOfParallelism |      Median |     StdDev | Scaled | Scaled-SD |
----------- |------ |----------------------- |------------ |----------- |------- |---------- |
 **Dictionary** |    **10** |                     **-1** |   **5.4201 us** |  **0.0512 us** |   **1.00** |      **0.00** |
  Hashtable |    10 |                     -1 |   4.9545 us |  0.0347 us |   0.91 |      0.01 |
 Concurrent |    10 |                     -1 |   4.9253 us |  0.0159 us |   0.91 |      0.01 |
 **Dictionary** |    **10** |                      **1** |   **2.0893 us** |  **0.0253 us** |   **1.00** |      **0.00** |
  Hashtable |    10 |                      1 |   1.9488 us |  0.0386 us |   0.93 |      0.02 |
 Concurrent |    10 |                      1 |   1.9073 us |  0.0197 us |   0.91 |      0.01 |
 **Dictionary** |    **20** |                     **-1** |   **9.7756 us** |  **0.2778 us** |   **1.00** |      **0.00** |
  Hashtable |    20 |                     -1 |   6.0307 us |  0.0230 us |   0.61 |      0.02 |
 Concurrent |    20 |                     -1 |   5.9965 us |  0.0173 us |   0.60 |      0.02 |
 **Dictionary** |    **20** |                      **1** |   **3.4780 us** |  **0.0507 us** |   **1.00** |      **0.00** |
  Hashtable |    20 |                      1 |   3.2690 us |  0.0749 us |   0.94 |      0.02 |
 Concurrent |    20 |                      1 |   3.0224 us |  0.0822 us |   0.87 |      0.03 |
 **Dictionary** |    **50** |                     **-1** |  **29.9933 us** |  **1.9805 us** |   **1.00** |      **0.00** |
  Hashtable |    50 |                     -1 |   7.3773 us |  0.0626 us |   0.25 |      0.02 |
 Concurrent |    50 |                     -1 |   7.2335 us |  0.0518 us |   0.24 |      0.02 |
 **Dictionary** |    **50** |                      **1** |   **7.4501 us** |  **0.0993 us** |   **1.00** |      **0.00** |
  Hashtable |    50 |                      1 |   7.0496 us |  0.1044 us |   0.94 |      0.02 |
 Concurrent |    50 |                      1 |   6.3908 us |  0.0815 us |   0.86 |      0.02 |
 **Dictionary** |   **100** |                     **-1** |  **66.0669 us** | **10.1367 us** |   **1.00** |      **0.00** |
  Hashtable |   100 |                     -1 |   9.9072 us |  0.1425 us |   0.15 |      0.02 |
 Concurrent |   100 |                     -1 |   9.6922 us |  0.1097 us |   0.15 |      0.02 |
 **Dictionary** |   **100** |                      **1** |  **14.1209 us** |  **0.2217 us** |   **1.00** |      **0.00** |
  Hashtable |   100 |                      1 |  13.1885 us |  0.1276 us |   0.93 |      0.02 |
 Concurrent |   100 |                      1 |  12.0479 us |  0.2229 us |   0.85 |      0.02 |
 **Dictionary** |  **1000** |                     **-1** | **477.3456 us** | **15.2028 us** |   **1.00** |      **0.00** |
  Hashtable |  1000 |                     -1 |  31.2365 us |  0.3421 us |   0.07 |      0.00 |
 Concurrent |  1000 |                     -1 |  30.6801 us |  0.2434 us |   0.06 |      0.00 |
 **Dictionary** |  **1000** |                      **1** | **134.5746 us** |  **1.0971 us** |   **1.00** |      **0.00** |
  Hashtable |  1000 |                      1 | 126.6081 us |  0.8346 us |   0.94 |      0.01 |
 Concurrent |  1000 |                      1 | 114.9060 us |  1.3409 us |   0.85 |      0.01 |
