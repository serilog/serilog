``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |         Mean |      Error |     StdDev |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |-------------:|-----------:|-----------:|-------:|--------:|-------:|------:|------:|----------:|
| EmitLogAIgnoredEvent |     8.981 ns |  0.1944 ns |  0.1819 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|         EmitLogEvent | 1,556.905 ns | 24.6087 ns | 23.0190 ns | 173.43 |    4.48 | 0.0401 |     - |     - |     216 B |
