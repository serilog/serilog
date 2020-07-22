``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|         Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------:|----------:|----------:|------:|--------:|
|            Off | 2.249 ns | 0.0357 ns | 0.0334 ns |  1.00 |    0.00 |
| LevelSwitchOff | 3.219 ns | 0.0492 ns | 0.0460 ns |  1.43 |    0.03 |
| MinimumLevelOn | 9.743 ns | 0.1928 ns | 0.1803 ns |  4.33 |    0.11 |
|  LevelSwitchOn | 9.662 ns | 0.1781 ns | 0.1666 ns |  4.30 |    0.09 |
