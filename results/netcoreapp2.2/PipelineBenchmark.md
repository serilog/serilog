``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]     : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT


```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
| EmitLogAIgnoredEvent |  17.30 ns | 0.2108 ns | 0.1869 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|         EmitLogEvent | 725.57 ns | 4.8808 ns | 4.3267 ns | 41.96 |    0.48 | 0.1192 |     - |     - |     376 B |
