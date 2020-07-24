``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host] : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

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
  MessageTemplateCacheBenchmark_Cached.Dictionary: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=10, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=10, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=10, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Dictionary: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=10, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=10, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=10, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Dictionary: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=20, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=20, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=20, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Dictionary: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=20, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=20, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=20, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Dictionary: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=50, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=50, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=50, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Dictionary: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=50, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=50, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=50, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Dictionary: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=100, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=100, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=100, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Dictionary: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=100, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=100, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=100, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Dictionary: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=1000, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=1000, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=1000, MaxDegreeOfParallelism=-1]
  MessageTemplateCacheBenchmark_Cached.Dictionary: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=1000, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Hashtable: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=1000, MaxDegreeOfParallelism=1]
  MessageTemplateCacheBenchmark_Cached.Concurrent: ShortRun(IterationCount=3, LaunchCount=1, WarmupCount=3) [Items=1000, MaxDegreeOfParallelism=1]
