```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Windows
Processor=?, ProcessorCount=8
Frequency=2533306 ticks, Resolution=394.7411 ns, Timer=TSC
CLR=CORE, Arch=64-bit ? [RyuJIT]
GC=Concurrent Workstation
dotnet cli version: 1.0.0-preview2-003121

Type=LevelControlBenchmark  Mode=Throughput  

```
         Method |     Median |    StdDev | Scaled | Scaled-SD |
--------------- |----------- |---------- |------- |---------- |
            Off |  4.1095 ns | 0.1013 ns |   1.00 |      0.00 |
 LevelSwitchOff |  4.9570 ns | 0.0807 ns |   1.20 |      0.03 |
 MinimumLevelOn | 14.5884 ns | 0.1821 ns |   3.54 |      0.10 |
  LevelSwitchOn | 13.9769 ns | 0.1802 ns |   3.38 |      0.09 |
