``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]   : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  ShortRun : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method |        Mean |        Error |       StdDev |  Ratio | RatioSD |      Gen 0 | Gen 1 | Gen 2 |   Allocated |
|--------------------------- |------------:|-------------:|-------------:|-------:|--------:|-----------:|------:|------:|------------:|
| SimulateAAppWithoutSerilog |    145.6 us |     30.27 us |     1.659 us |   1.00 |    0.00 |    12.4512 |     - |     - |    39.16 KB |
| SimulateAAppWithSerilogOff |  1,763.5 us |    363.64 us |    19.932 us |  12.11 |    0.22 |   880.8594 |     - |     - |  2710.91 KB |
|  SimulateAAppWithSerilogOn | 65,089.8 us | 50,055.49 us | 2,743.710 us | 447.05 |   16.49 | 15500.0000 |     - |     - | 47702.96 KB |
