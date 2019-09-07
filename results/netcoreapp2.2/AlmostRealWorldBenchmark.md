``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]   : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
  ShortRun : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
|      Method |     Mean |    Error |   StdDev | Ratio |      Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------ |---------:|---------:|---------:|------:|-----------:|------:|------:|----------:|
| LogLikeAApp | 70.16 ms | 95.74 ms | 5.248 ms |  1.00 | 15500.0000 |     - |     - |  46.58 MB |
