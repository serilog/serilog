``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|         Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |----------:|----------:|----------:|------:|--------:|
|            Off |  2.202 ns | 0.0307 ns | 0.0273 ns |  1.00 |    0.00 |
| LevelSwitchOff |  3.214 ns | 0.0869 ns | 0.1099 ns |  1.46 |    0.07 |
| MinimumLevelOn |  9.825 ns | 0.1854 ns | 0.1734 ns |  4.47 |    0.08 |
|  LevelSwitchOn | 10.002 ns | 0.2129 ns | 0.1992 ns |  4.55 |    0.10 |
