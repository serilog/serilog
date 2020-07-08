``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|               Method |      Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------- |----------:|---------:|---------:|------:|--------:|
|                 Bare |  11.03 ns | 0.078 ns | 0.073 ns |  1.00 |    0.00 |
|         PushProperty | 102.64 ns | 1.814 ns | 1.608 ns |  9.31 |    0.19 |
|   PushPropertyNested | 203.14 ns | 4.053 ns | 5.410 ns | 18.53 |    0.54 |
| PushPropertyEnriched | 183.37 ns | 3.216 ns | 2.851 ns | 16.64 |    0.33 |
