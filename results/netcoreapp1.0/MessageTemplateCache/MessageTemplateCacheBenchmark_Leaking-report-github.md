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
     Method | Items | OverflowCount | MaxDegreeOfParallelism |      Median |     StdDev | Scaled | Scaled-SD |
----------- |------ |-------------- |----------------------- |------------ |----------- |------- |---------- |
 **Dictionary** | **10000** |             **1** |                     **-1** |   **3.9183 ms** |  **0.1968 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |             1 |                     -1 |   2.1291 ms |  0.0444 ms |   0.55 |      0.03 |
 Concurrent | 10000 |             1 |                     -1 |  69.0551 ms |  5.2627 ms |  17.73 |      1.59 |
 **Dictionary** | **10000** |             **1** |                      **1** |   **2.3437 ms** |  **0.0091 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |             1 |                      1 |   2.4470 ms |  0.0135 ms |   1.04 |      0.01 |
 Concurrent | 10000 |             1 |                      1 | 167.0585 ms |  1.3400 ms |  71.17 |      0.62 |
 **Dictionary** | **10000** |            **10** |                     **-1** |   **3.9524 ms** |  **0.2059 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |            10 |                     -1 |   2.2075 ms |  0.0741 ms |   0.56 |      0.03 |
 Concurrent | 10000 |            10 |                     -1 |  77.3839 ms |  9.4562 ms |  19.32 |      2.57 |
 **Dictionary** | **10000** |            **10** |                      **1** |   **2.3351 ms** |  **0.0081 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |            10 |                      1 |   2.4537 ms |  0.0128 ms |   1.05 |      0.01 |
 Concurrent | 10000 |            10 |                      1 | 166.3683 ms |  1.7963 ms |  71.21 |      0.79 |
 **Dictionary** | **10000** |           **100** |                     **-1** |   **4.0189 ms** |  **0.1066 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |           100 |                     -1 |   2.4080 ms |  0.0545 ms |   0.60 |      0.02 |
 Concurrent | 10000 |           100 |                     -1 |  95.7208 ms | 12.9543 ms |  22.97 |      3.26 |
 **Dictionary** | **10000** |           **100** |                      **1** |   **2.3307 ms** |  **0.0147 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |           100 |                      1 |   2.4447 ms |  0.0132 ms |   1.05 |      0.01 |
 Concurrent | 10000 |           100 |                      1 | 165.2893 ms |  1.0848 ms |  70.90 |      0.63 |
 **Dictionary** | **10000** |          **1000** |                     **-1** |   **4.3837 ms** |  **0.1868 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |          1000 |                     -1 |   3.3281 ms |  0.1305 ms |   0.77 |      0.04 |
 Concurrent | 10000 |          1000 |                     -1 | 178.7427 ms | 34.4240 ms |  40.66 |      8.10 |
 **Dictionary** | **10000** |          **1000** |                      **1** |   **2.3404 ms** |  **0.0208 ms** |   **1.00** |      **0.00** |
  Hashtable | 10000 |          1000 |                      1 |   2.4727 ms |  0.0146 ms |   1.05 |      0.01 |
 Concurrent | 10000 |          1000 |                      1 | 165.6182 ms |  1.1273 ms |  70.66 |      0.77 |
