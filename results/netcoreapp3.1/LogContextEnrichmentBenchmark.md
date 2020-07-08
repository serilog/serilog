``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|               Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare |   9.755 ns | 0.1332 ns | 0.1246 ns |  1.00 |    0.00 |
|         PushProperty | 109.710 ns | 2.1093 ns | 2.1660 ns | 11.24 |    0.23 |
|   PushPropertyNested | 224.586 ns | 3.4286 ns | 3.2072 ns | 23.02 |    0.34 |
| PushPropertyEnriched | 172.298 ns | 3.2599 ns | 3.2016 ns | 17.65 |    0.33 |
