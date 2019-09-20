``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.8.4010.0


```
|               Method |         Mean |      Error |      StdDev |       Median |  Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |-------------:|-----------:|------------:|-------------:|-------:|--------:|-------:|------:|------:|----------:|
| EmitLogAIgnoredEvent |     9.904 ns |  0.0382 ns |   0.0338 ns |     9.910 ns |   1.00 |    0.00 |      - |     - |     - |         - |
|         EmitLogEvent | 1,805.007 ns | 38.7911 ns | 114.3765 ns | 1,730.626 ns | 181.69 |    6.42 | 0.0687 |     - |     - |     216 B |
