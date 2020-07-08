``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT


```
|               Method |         Mean |      Error |     StdDev |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |-------------:|-----------:|-----------:|-------:|--------:|-------:|------:|------:|----------:|
| EmitLogAIgnoredEvent |     9.087 ns |  0.1046 ns |  0.0978 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|         EmitLogEvent | 1,644.937 ns | 13.6623 ns | 12.7797 ns | 181.05 |    2.91 | 0.0401 |     - |     - |     216 B |
