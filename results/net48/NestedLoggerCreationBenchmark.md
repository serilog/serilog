``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
|    ForContextInt | 76.48 ns | 1.243 ns | 1.163 ns | 0.0144 |     - |     - |      76 B |
| ForContextString | 44.54 ns | 1.009 ns | 1.162 ns | 0.0122 |     - |     - |      64 B |
|   ForContextType | 97.24 ns | 0.874 ns | 0.818 ns | 0.0122 |     - |     - |      64 B |
