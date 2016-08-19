```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-3720QM CPU 2.60GHz, ProcessorCount=8
Frequency=2533306 ticks, Resolution=394.7411 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1080.0

Type=MessageTemplateParsingBenchmark  Mode=Throughput  

```
                       Method |        Median |     StdDev | Scaled | Scaled-SD |
----------------------------- |-------------- |----------- |------- |---------- |
                EmptyTemplate |   183.3209 ns |  3.9928 ns |   1.00 |      0.00 |
 DefaultConsoleOutputTemplate | 2,636.2172 ns | 77.3304 ns |  14.40 |      0.50 |
