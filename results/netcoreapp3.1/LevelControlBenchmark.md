``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|         Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |----------:|----------:|----------:|------:|--------:|
|            Off |  2.432 ns | 0.0681 ns | 0.0637 ns |  1.00 |    0.00 |
| LevelSwitchOff |  3.398 ns | 0.0750 ns | 0.0664 ns |  1.40 |    0.05 |
| MinimumLevelOn | 10.286 ns | 0.1769 ns | 0.1655 ns |  4.23 |    0.12 |
|  LevelSwitchOn | 10.230 ns | 0.1129 ns | 0.1056 ns |  4.21 |    0.11 |
