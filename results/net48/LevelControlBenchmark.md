``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT


```
|         Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------:|----------:|----------:|------:|--------:|
|            Off | 2.945 ns | 0.0817 ns | 0.0941 ns |  1.00 |    0.00 |
| LevelSwitchOff | 3.416 ns | 0.0919 ns | 0.0944 ns |  1.16 |    0.06 |
| MinimumLevelOn | 9.719 ns | 0.1905 ns | 0.1782 ns |  3.31 |    0.13 |
|  LevelSwitchOn | 9.757 ns | 0.1945 ns | 0.1819 ns |  3.32 |    0.12 |
