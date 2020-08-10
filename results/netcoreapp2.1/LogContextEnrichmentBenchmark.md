``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  DefaultJob : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT


```
|               Method |      Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------- |----------:|---------:|---------:|------:|--------:|
|                 Bare |  11.46 ns | 0.123 ns | 0.115 ns |  1.00 |    0.00 |
|         PushProperty |  98.11 ns | 1.708 ns | 1.597 ns |  8.56 |    0.18 |
|   PushPropertyNested | 178.56 ns | 2.420 ns | 2.264 ns | 15.59 |    0.29 |
| PushPropertyEnriched | 171.38 ns | 3.262 ns | 3.051 ns | 14.96 |    0.37 |
