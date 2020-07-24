``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|               Method |      Mean |    Error |    StdDev |    Median | Ratio | RatioSD |
|--------------------- |----------:|---------:|----------:|----------:|------:|--------:|
|                 Bare |  10.98 ns | 0.189 ns |  0.158 ns |  11.01 ns |  1.00 |    0.00 |
|         PushProperty | 113.08 ns | 3.955 ns | 11.538 ns | 106.67 ns | 11.45 |    1.31 |
|   PushPropertyNested | 202.72 ns | 3.355 ns |  3.139 ns | 202.19 ns | 18.43 |    0.41 |
| PushPropertyEnriched | 174.68 ns | 2.182 ns |  1.934 ns | 175.35 ns | 15.90 |    0.31 |
