``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
| EmitLogAIgnoredEvent |  12.67 ns |  0.272 ns |  0.344 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|         EmitLogEvent | 639.96 ns | 12.075 ns | 11.295 ns | 50.63 |    1.53 | 0.0582 |     - |     - |     368 B |
