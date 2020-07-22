``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|               Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare |   9.509 ns | 0.1874 ns | 0.1753 ns |  1.00 |    0.00 |
|         PushProperty |  95.402 ns | 1.6101 ns | 1.3445 ns | 10.02 |    0.27 |
|   PushPropertyNested | 193.328 ns | 3.7486 ns | 4.1666 ns | 20.38 |    0.68 |
| PushPropertyEnriched | 185.329 ns | 3.1546 ns | 2.9508 ns | 19.50 |    0.52 |
