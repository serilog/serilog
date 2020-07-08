``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|         Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |----------:|----------:|----------:|------:|--------:|
|            Off |  2.207 ns | 0.0426 ns | 0.0398 ns |  1.00 |    0.00 |
| LevelSwitchOff |  2.748 ns | 0.0900 ns | 0.1552 ns |  1.22 |    0.10 |
| MinimumLevelOn | 10.734 ns | 0.1451 ns | 0.1358 ns |  4.86 |    0.09 |
|  LevelSwitchOn | 10.587 ns | 0.1508 ns | 0.1259 ns |  4.81 |    0.12 |
