``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 2.1.21 (CoreCLR 4.6.29130.01, CoreFX 4.6.29130.02), X64 RyuJIT
  DefaultJob : .NET Core 2.1.21 (CoreCLR 4.6.29130.01, CoreFX 4.6.29130.02), X64 RyuJIT


```
|               Method |      Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------- |----------:|---------:|---------:|------:|--------:|
|                 Bare |  10.98 ns | 0.215 ns | 0.201 ns |  1.00 |    0.00 |
|         PushProperty |  99.14 ns | 1.515 ns | 1.417 ns |  9.03 |    0.17 |
|   PushPropertyNested | 180.05 ns | 3.171 ns | 2.966 ns | 16.41 |    0.37 |
| PushPropertyEnriched | 165.24 ns | 3.222 ns | 3.309 ns | 15.04 |    0.40 |
