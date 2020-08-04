``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  DefaultJob : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT


```
|         Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |----------:|----------:|----------:|------:|--------:|
|            Off |  2.599 ns | 0.0776 ns | 0.0894 ns |  1.00 |    0.00 |
| LevelSwitchOff |  2.834 ns | 0.0836 ns | 0.1116 ns |  1.09 |    0.05 |
| MinimumLevelOn | 11.035 ns | 0.1720 ns | 0.1609 ns |  4.25 |    0.19 |
|  LevelSwitchOn | 10.729 ns | 0.2193 ns | 0.2051 ns |  4.13 |    0.18 |
