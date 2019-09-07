``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]     : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |----------:|----------:|----------:|------:|--------:|
|                 Bare |  11.52 ns | 0.2575 ns | 0.2965 ns |  1.00 |    0.00 |
|         PushProperty | 106.93 ns | 2.0987 ns | 2.2456 ns |  9.29 |    0.36 |
|   PushPropertyNested | 216.04 ns | 4.2368 ns | 5.7994 ns | 18.85 |    0.68 |
| PushPropertyEnriched | 192.12 ns | 3.8611 ns | 4.5963 ns | 16.68 |    0.53 |
