``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.401
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT


```
|         Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------:|----------:|----------:|------:|--------:|
|            Off | 2.631 ns | 0.0771 ns | 0.1002 ns |  1.00 |    0.00 |
| LevelSwitchOff | 3.199 ns | 0.0765 ns | 0.0716 ns |  1.22 |    0.06 |
| MinimumLevelOn | 9.539 ns | 0.2093 ns | 0.2150 ns |  3.63 |    0.13 |
|  LevelSwitchOn | 9.973 ns | 0.2268 ns | 0.2122 ns |  3.80 |    0.19 |
