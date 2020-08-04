``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|         Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------- |---------:|----------:|----------:|------:|--------:|
|            Off | 2.947 ns | 0.0816 ns | 0.1002 ns |  1.00 |    0.00 |
| LevelSwitchOff | 3.420 ns | 0.0614 ns | 0.0575 ns |  1.16 |    0.05 |
| MinimumLevelOn | 9.714 ns | 0.1738 ns | 0.1626 ns |  3.30 |    0.14 |
|  LevelSwitchOn | 9.744 ns | 0.1780 ns | 0.1665 ns |  3.31 |    0.14 |
