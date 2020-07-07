``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0


```
|           Method |     Mean |     Error |    StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|----------:|----------:|-------:|------:|------:|----------:|
|    ForContextInt | 70.19 ns | 0.5814 ns | 0.5439 ns | 0.0144 |     - |     - |      76 B |
| ForContextString | 42.08 ns | 0.8558 ns | 0.8788 ns | 0.0122 |     - |     - |      64 B |
|   ForContextType | 98.75 ns | 0.7026 ns | 0.6228 ns | 0.0122 |     - |     - |      64 B |
