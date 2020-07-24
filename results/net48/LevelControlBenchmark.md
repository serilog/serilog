``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|         Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------:|----------:|----------:|------:|--------:|
|            Off | 2.749 ns | 0.0948 ns | 0.0887 ns |  1.00 |    0.00 |
| LevelSwitchOff | 2.939 ns | 0.0244 ns | 0.0204 ns |  1.07 |    0.03 |
| MinimumLevelOn | 9.946 ns | 0.1277 ns | 0.1195 ns |  3.62 |    0.11 |
|  LevelSwitchOn | 9.753 ns | 0.1055 ns | 0.0986 ns |  3.55 |    0.10 |
