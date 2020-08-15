``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|         Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |----------:|----------:|----------:|------:|--------:|
|            Off |  2.659 ns | 0.0739 ns | 0.0691 ns |  1.00 |    0.00 |
| LevelSwitchOff |  2.934 ns | 0.0840 ns | 0.0899 ns |  1.10 |    0.03 |
| MinimumLevelOn | 10.346 ns | 0.1530 ns | 0.1432 ns |  3.89 |    0.13 |
|  LevelSwitchOn |  9.698 ns | 0.1818 ns | 0.1700 ns |  3.65 |    0.07 |
