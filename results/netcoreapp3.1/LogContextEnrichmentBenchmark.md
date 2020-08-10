``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|               Method |      Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------- |----------:|---------:|---------:|------:|--------:|
|                 Bare |  10.52 ns | 0.168 ns | 0.158 ns |  1.00 |    0.00 |
|         PushProperty |  89.81 ns | 1.711 ns | 1.601 ns |  8.53 |    0.15 |
|   PushPropertyNested | 178.59 ns | 3.390 ns | 3.481 ns | 16.97 |    0.47 |
| PushPropertyEnriched | 164.06 ns | 3.152 ns | 2.948 ns | 15.59 |    0.38 |
