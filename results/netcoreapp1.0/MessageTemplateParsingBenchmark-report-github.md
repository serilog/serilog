```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Windows
Processor=?, ProcessorCount=8
Frequency=2533306 ticks, Resolution=394.7411 ns, Timer=TSC
CLR=CORE, Arch=64-bit ? [RyuJIT]
GC=Concurrent Workstation
dotnet cli version: 1.0.0-preview2-003121

Type=MessageTemplateParsingBenchmark  Mode=Throughput  

```
                       Method |        Median |     StdDev | Scaled | Scaled-SD |
----------------------------- |-------------- |----------- |------- |---------- |
                EmptyTemplate |   188.2760 ns |  2.4773 ns |   1.00 |      0.00 |
 DefaultConsoleOutputTemplate | 2,753.6243 ns | 22.7107 ns |  14.54 |      0.22 |
