``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]   : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  ShortRun : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                     Method |      Mean |      Error |    StdDev | Ratio | RatioSD |      Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------------- |----------:|-----------:|----------:|------:|--------:|-----------:|------:|------:|----------:|
| SimulateAAppWithoutSerilog |  1.468 ms |  0.4999 ms | 0.0274 ms |  1.00 |    0.00 |   876.9531 |     - |     - |   2.63 MB |
| SimulateAAppWithSerilogOff |  2.026 ms |  2.9769 ms | 0.1632 ms |  1.38 |    0.13 |   878.9063 |     - |     - |   2.65 MB |
|  SimulateAAppWithSerilogOn | 73.363 ms | 83.1556 ms | 4.5580 ms | 50.00 |    3.77 | 15428.5714 |     - |     - |  46.58 MB |
