``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|           Method |     Mean |    Error |   StdDev |   Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|---------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt | 70.88 ns | 1.429 ns | 1.908 ns | 72.09 ns | 0.0144 |     - |     - |      76 B |
| ForContextString | 41.81 ns | 0.768 ns | 0.719 ns | 41.75 ns | 0.0122 |     - |     - |      64 B |
|   ForContextType | 97.10 ns | 1.378 ns | 1.289 ns | 97.12 ns | 0.0122 |     - |     - |      64 B |
