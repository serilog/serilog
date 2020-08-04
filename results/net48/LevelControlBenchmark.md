``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|         Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------:|----------:|----------:|------:|--------:|
|            Off | 2.555 ns | 0.0782 ns | 0.0869 ns |  1.00 |    0.00 |
| LevelSwitchOff | 2.780 ns | 0.0821 ns | 0.1008 ns |  1.09 |    0.05 |
| MinimumLevelOn | 9.530 ns | 0.2026 ns | 0.1895 ns |  3.72 |    0.17 |
|  LevelSwitchOn | 9.225 ns | 0.1997 ns | 0.1868 ns |  3.60 |    0.09 |
