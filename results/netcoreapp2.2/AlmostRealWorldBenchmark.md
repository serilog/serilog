``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]   : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  ShortRun : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|                  Method |      Mean |      Error |    StdDev | Ratio | RatioSD |      Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------ |----------:|-----------:|----------:|------:|--------:|-----------:|------:|------:|----------:|
|            SimulateAApp |  1.318 ms |  0.9339 ms | 0.0512 ms |  1.00 |    0.00 |   876.9531 |     - |     - |   2.63 MB |
| SimulateAAppWithSerilog | 60.504 ms | 30.4718 ms | 1.6703 ms | 45.95 |    2.29 | 15222.2222 |     - |     - |   45.9 MB |
