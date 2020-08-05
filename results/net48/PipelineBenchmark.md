``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |         Mean |      Error |     StdDev |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |-------------:|-----------:|-----------:|-------:|--------:|-------:|------:|------:|----------:|
| EmitLogAIgnoredEvent |     9.452 ns |  0.1805 ns |  0.1688 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|         EmitLogEvent | 1,533.801 ns | 26.0145 ns | 24.3340 ns | 162.32 |    3.84 | 0.0401 |     - |     - |     216 B |
