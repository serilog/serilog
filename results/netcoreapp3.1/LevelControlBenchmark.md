``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|         Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------:|----------:|----------:|------:|--------:|
|            Off | 2.287 ns | 0.0441 ns | 0.0391 ns |  1.00 |    0.00 |
| LevelSwitchOff | 3.229 ns | 0.0917 ns | 0.0941 ns |  1.42 |    0.04 |
| MinimumLevelOn | 9.810 ns | 0.2125 ns | 0.2087 ns |  4.30 |    0.10 |
|  LevelSwitchOn | 9.742 ns | 0.1684 ns | 0.1575 ns |  4.26 |    0.09 |
