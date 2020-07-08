``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|         Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------:|----------:|----------:|------:|--------:|
|            Off | 2.790 ns | 0.0660 ns | 0.0617 ns |  1.00 |    0.00 |
| LevelSwitchOff | 2.922 ns | 0.0669 ns | 0.0626 ns |  1.05 |    0.02 |
| MinimumLevelOn | 9.950 ns | 0.1238 ns | 0.1158 ns |  3.57 |    0.09 |
|  LevelSwitchOn | 9.686 ns | 0.1341 ns | 0.1254 ns |  3.47 |    0.11 |
