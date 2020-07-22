``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt | 77.72 ns | 1.571 ns | 3.138 ns | 0.0144 |     - |     - |      76 B |
| ForContextString | 43.58 ns | 0.874 ns | 0.775 ns | 0.0122 |     - |     - |      64 B |
|   ForContextType | 94.04 ns | 1.697 ns | 1.504 ns | 0.0122 |     - |     - |      64 B |
