``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.450 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4200.0), X86 LegacyJIT


```
|           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt | 69.87 ns | 1.367 ns | 1.729 ns | 0.0144 |     - |     - |      76 B |
| ForContextString | 41.27 ns | 0.813 ns | 0.760 ns | 0.0122 |     - |     - |      64 B |
|   ForContextType | 92.22 ns | 1.631 ns | 1.526 ns | 0.0122 |     - |     - |      64 B |
