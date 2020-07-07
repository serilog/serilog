``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.2.402
  [Host]     : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT


```
|         Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------:|----------:|----------:|------:|--------:|
|            Off | 2.271 ns | 0.0235 ns | 0.0220 ns |  1.00 |    0.00 |
| LevelSwitchOff | 2.722 ns | 0.0383 ns | 0.0358 ns |  1.20 |    0.02 |
| MinimumLevelOn | 9.508 ns | 0.0645 ns | 0.0603 ns |  4.19 |    0.05 |
|  LevelSwitchOn | 9.229 ns | 0.0454 ns | 0.0402 ns |  4.07 |    0.05 |
