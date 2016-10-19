```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Windows
Processor=?, ProcessorCount=8
Frequency=3507509 ticks, Resolution=285.1026 ns, Timer=TSC
CLR=CORE, Arch=64-bit ? [RyuJIT]
GC=Concurrent Workstation
dotnet cli version: 1.0.0-preview2-003131

Type=MessageTemplateCacheBenchmark_Warmup  Mode=Throughput  

```
     Method | Items | MaxDegreeOfParallelism |          Median |        StdDev | Scaled | Scaled-SD |
----------- |------ |----------------------- |---------------- |-------------- |------- |---------- |
 **Dictionary** |    **10** |                     **-1** |       **9.6902 us** |     **0.0664 us** |   **1.00** |      **0.00** |
  Hashtable |    10 |                     -1 |       7.2262 us |     0.1284 us |   0.74 |      0.01 |
 Concurrent |    10 |                     -1 |       9.5027 us |     0.6200 us |   1.00 |      0.06 |
 **Dictionary** |    **10** |                      **1** |       **3.3790 us** |     **0.0236 us** |   **1.00** |      **0.00** |
  Hashtable |    10 |                      1 |       3.4664 us |     0.0544 us |   1.03 |      0.02 |
 Concurrent |    10 |                      1 |       4.7689 us |     0.3187 us |   1.47 |      0.09 |
 **Dictionary** |   **100** |                     **-1** |     **126.1621 us** |    **19.4774 us** |   **1.00** |      **0.00** |
  Hashtable |   100 |                     -1 |     101.6898 us |     8.7118 us |   0.81 |      0.10 |
 Concurrent |   100 |                     -1 |     404.3201 us |    33.8885 us |   3.08 |      0.39 |
 **Dictionary** |   **100** |                      **1** |      **26.9025 us** |     **0.1860 us** |   **1.00** |      **0.00** |
  Hashtable |   100 |                      1 |      29.0055 us |     0.4187 us |   1.07 |      0.02 |
 Concurrent |   100 |                      1 |     107.1666 us |    14.1945 us |   4.07 |      0.53 |
 **Dictionary** |  **1000** |                     **-1** |     **833.5471 us** |    **11.3689 us** |   **1.00** |      **0.00** |
  Hashtable |  1000 |                     -1 |     830.9745 us |    11.1378 us |   0.99 |      0.02 |
 Concurrent |  1000 |                     -1 |  15,259.1376 us | 1,588.6467 us |  18.34 |      1.91 |
 **Dictionary** |  **1000** |                      **1** |     **257.6129 us** |     **1.8266 us** |   **1.00** |      **0.00** |
  Hashtable |  1000 |                      1 |     293.7693 us |     2.1159 us |   1.14 |      0.01 |
 Concurrent |  1000 |                      1 |   6,819.1691 us | 1,207.1580 us |  27.51 |      4.68 |
 **Dictionary** | **10000** |                     **-1** |   **4,318.6810 us** |   **112.2715 us** |   **1.00** |      **0.00** |
  Hashtable | 10000 |                     -1 |   4,112.6765 us |    54.6069 us |   0.95 |      0.03 |
 Concurrent | 10000 |                     -1 | 324,148.5624 us | 9,763.5470 us |  75.11 |      2.91 |
 **Dictionary** | **10000** |                      **1** |   **2,343.6103 us** |    **11.5641 us** |   **1.00** |      **0.00** |
  Hashtable | 10000 |                      1 |   2,484.4611 us |     6.8263 us |   1.06 |      0.01 |
 Concurrent | 10000 |                      1 | 165,379.1908 us | 1,588.0393 us |  70.54 |      0.74 |
