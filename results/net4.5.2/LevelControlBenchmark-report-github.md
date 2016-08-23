```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-3720QM CPU 2.60GHz, ProcessorCount=8
Frequency=2533306 ticks, Resolution=394.7411 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1080.0

Type=LevelControlBenchmark  Mode=Throughput  

```
         Method |     Median |    StdDev | Scaled | Scaled-SD |
--------------- |----------- |---------- |------- |---------- |
            Off |  4.1028 ns | 0.0534 ns |   1.00 |      0.00 |
 LevelSwitchOff |  4.9870 ns | 0.0728 ns |   1.21 |      0.02 |
 MinimumLevelOn | 14.6336 ns | 0.2279 ns |   3.58 |      0.07 |
  LevelSwitchOn | 14.0402 ns | 0.2001 ns |   3.42 |      0.07 |
