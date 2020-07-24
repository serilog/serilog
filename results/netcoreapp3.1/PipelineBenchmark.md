``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|               Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| EmitLogAIgnoredEvent |  11.86 ns | 0.059 ns | 0.055 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|         EmitLogEvent | 668.84 ns | 2.377 ns | 2.224 ns | 56.40 |    0.32 | 0.0582 |     - |     - |     368 B |
