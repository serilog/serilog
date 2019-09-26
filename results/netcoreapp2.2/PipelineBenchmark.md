``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.2.402
  [Host]     : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
| EmitLogAIgnoredEvent |  14.09 ns | 0.1431 ns | 0.1338 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|         EmitLogEvent | 623.39 ns | 5.9385 ns | 5.5549 ns | 44.24 |    0.64 | 0.0591 |     - |     - |     376 B |
