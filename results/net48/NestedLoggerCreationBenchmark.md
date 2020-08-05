``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt | 70.08 ns | 1.384 ns | 1.750 ns | 0.0144 |     - |     - |      76 B |
| ForContextString | 42.01 ns | 0.766 ns | 0.716 ns | 0.0122 |     - |     - |      64 B |
|   ForContextType | 93.61 ns | 1.888 ns | 1.854 ns | 0.0122 |     - |     - |      64 B |
