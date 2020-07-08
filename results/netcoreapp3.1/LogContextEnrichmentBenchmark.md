``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|               Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare |   9.889 ns | 0.1268 ns | 0.1186 ns |  1.00 |    0.00 |
|         PushProperty | 101.207 ns | 1.2439 ns | 1.1636 ns | 10.24 |    0.13 |
|   PushPropertyNested | 233.181 ns | 3.6294 ns | 3.3949 ns | 23.58 |    0.39 |
| PushPropertyEnriched | 182.832 ns | 3.5100 ns | 3.6045 ns | 18.53 |    0.26 |
