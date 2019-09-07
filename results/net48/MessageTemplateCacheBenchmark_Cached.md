``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.3815.0


```
|     Method | Items | MaxDegreeOfParallelism | Mean | Error | Ratio | RatioSD |
|----------- |------ |----------------------- |-----:|------:|------:|--------:|
| **Dictionary** |    **10** |                     **-1** |   **NA** |    **NA** |     **?** |       **?** |
|  Hashtable |    10 |                     -1 |   NA |    NA |     ? |       ? |
| Concurrent |    10 |                     -1 |   NA |    NA |     ? |       ? |
|            |       |                        |      |       |       |         |
| **Dictionary** |    **10** |                      **1** |   **NA** |    **NA** |     **?** |       **?** |
|  Hashtable |    10 |                      1 |   NA |    NA |     ? |       ? |
| Concurrent |    10 |                      1 |   NA |    NA |     ? |       ? |
|            |       |                        |      |       |       |         |
| **Dictionary** |    **20** |                     **-1** |   **NA** |    **NA** |     **?** |       **?** |
|  Hashtable |    20 |                     -1 |   NA |    NA |     ? |       ? |
| Concurrent |    20 |                     -1 |   NA |    NA |     ? |       ? |
|            |       |                        |      |       |       |         |
| **Dictionary** |    **20** |                      **1** |   **NA** |    **NA** |     **?** |       **?** |
|  Hashtable |    20 |                      1 |   NA |    NA |     ? |       ? |
| Concurrent |    20 |                      1 |   NA |    NA |     ? |       ? |
|            |       |                        |      |       |       |         |
| **Dictionary** |    **50** |                     **-1** |   **NA** |    **NA** |     **?** |       **?** |
|  Hashtable |    50 |                     -1 |   NA |    NA |     ? |       ? |
| Concurrent |    50 |                     -1 |   NA |    NA |     ? |       ? |
|            |       |                        |      |       |       |         |
| **Dictionary** |    **50** |                      **1** |   **NA** |    **NA** |     **?** |       **?** |
|  Hashtable |    50 |                      1 |   NA |    NA |     ? |       ? |
| Concurrent |    50 |                      1 |   NA |    NA |     ? |       ? |
|            |       |                        |      |       |       |         |
| **Dictionary** |   **100** |                     **-1** |   **NA** |    **NA** |     **?** |       **?** |
|  Hashtable |   100 |                     -1 |   NA |    NA |     ? |       ? |
| Concurrent |   100 |                     -1 |   NA |    NA |     ? |       ? |
|            |       |                        |      |       |       |         |
| **Dictionary** |   **100** |                      **1** |   **NA** |    **NA** |     **?** |       **?** |
|  Hashtable |   100 |                      1 |   NA |    NA |     ? |       ? |
| Concurrent |   100 |                      1 |   NA |    NA |     ? |       ? |
|            |       |                        |      |       |       |         |
| **Dictionary** |  **1000** |                     **-1** |   **NA** |    **NA** |     **?** |       **?** |
|  Hashtable |  1000 |                     -1 |   NA |    NA |     ? |       ? |
| Concurrent |  1000 |                     -1 |   NA |    NA |     ? |       ? |
|            |       |                        |      |       |       |         |
| **Dictionary** |  **1000** |                      **1** |   **NA** |    **NA** |     **?** |       **?** |
|  Hashtable |  1000 |                      1 |   NA |    NA |     ? |       ? |
| Concurrent |  1000 |                      1 |   NA |    NA |     ? |       ? |

Benchmarks with issues:
  MessageTemplateCacheBenchmark_Cached.Dictionary: DefaultJob [Items=10, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: DefaultJob [Items=10, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: DefaultJob [Items=10, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Dictionary: DefaultJob [Items=10, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: DefaultJob [Items=10, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: DefaultJob [Items=10, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Dictionary: DefaultJob [Items=20, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: DefaultJob [Items=20, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: DefaultJob [Items=20, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Dictionary: DefaultJob [Items=20, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: DefaultJob [Items=20, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: DefaultJob [Items=20, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Dictionary: DefaultJob [Items=50, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: DefaultJob [Items=50, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: DefaultJob [Items=50, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Dictionary: DefaultJob [Items=50, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: DefaultJob [Items=50, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: DefaultJob [Items=50, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Dictionary: DefaultJob [Items=100, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: DefaultJob [Items=100, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: DefaultJob [Items=100, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Dictionary: DefaultJob [Items=100, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: DefaultJob [Items=100, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: DefaultJob [Items=100, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Dictionary: DefaultJob [Items=1000, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: DefaultJob [Items=1000, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: DefaultJob [Items=1000, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Dictionary: DefaultJob [Items=1000, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: DefaultJob [Items=1000, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: DefaultJob [Items=1000, MaxDegreeOfParallelism=1]
