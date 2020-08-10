``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|         Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------:|----------:|----------:|------:|--------:|
|            Off | 2.469 ns | 0.0758 ns | 0.0986 ns |  1.00 |    0.00 |
| LevelSwitchOff | 2.701 ns | 0.0795 ns | 0.1005 ns |  1.09 |    0.07 |
| MinimumLevelOn | 9.731 ns | 0.2141 ns | 0.2102 ns |  3.94 |    0.13 |
|  LevelSwitchOn | 9.490 ns | 0.2166 ns | 0.2026 ns |  3.86 |    0.10 |
