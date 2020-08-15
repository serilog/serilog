``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|               Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare |   9.972 ns | 0.2058 ns | 0.1926 ns |  1.00 |    0.00 |
|         PushProperty |  95.971 ns | 1.9252 ns | 2.6988 ns |  9.62 |    0.39 |
|   PushPropertyNested | 194.756 ns | 3.8784 ns | 4.7630 ns | 19.43 |    0.68 |
| PushPropertyEnriched | 160.276 ns | 2.9678 ns | 2.7760 ns | 16.08 |    0.37 |
