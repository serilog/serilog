``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19041
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.301
  [Host]     : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT
  DefaultJob : .NET Core 3.1.5 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.27001), X64 RyuJIT


```
|               Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| EmitLogAIgnoredEvent |  13.79 ns | 0.304 ns | 0.473 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|         EmitLogEvent | 667.49 ns | 4.341 ns | 3.848 ns | 47.16 |    1.61 | 0.0582 |     - |     - |     368 B |
