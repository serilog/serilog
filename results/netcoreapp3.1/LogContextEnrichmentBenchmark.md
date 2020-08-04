``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|               Method |      Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------- |----------:|---------:|---------:|------:|--------:|
|                 Bare |  10.50 ns | 0.179 ns | 0.168 ns |  1.00 |    0.00 |
|         PushProperty |  95.77 ns | 1.003 ns | 0.939 ns |  9.12 |    0.15 |
|   PushPropertyNested | 197.83 ns | 2.923 ns | 2.734 ns | 18.84 |    0.46 |
| PushPropertyEnriched | 167.84 ns | 3.052 ns | 2.855 ns | 15.98 |    0.39 |
