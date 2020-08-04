``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  DefaultJob : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT


```
|               Method |      Mean |    Error |   StdDev | Ratio | RatioSD |
|--------------------- |----------:|---------:|---------:|------:|--------:|
|                 Bare |  10.95 ns | 0.163 ns | 0.153 ns |  1.00 |    0.00 |
|         PushProperty |  89.53 ns | 1.693 ns | 1.584 ns |  8.18 |    0.21 |
|   PushPropertyNested | 175.18 ns | 3.366 ns | 3.306 ns | 16.00 |    0.39 |
| PushPropertyEnriched | 162.20 ns | 3.121 ns | 3.205 ns | 14.82 |    0.29 |
