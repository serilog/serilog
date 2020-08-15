``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|               Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare |   9.776 ns | 0.2177 ns | 0.2037 ns |  1.00 |    0.00 |
|         PushProperty |  97.166 ns | 1.9264 ns | 3.2186 ns |  9.84 |    0.53 |
|   PushPropertyNested | 191.467 ns | 3.7985 ns | 5.6854 ns | 19.43 |    0.94 |
| PushPropertyEnriched | 163.913 ns | 3.2324 ns | 3.1747 ns | 16.80 |    0.50 |
