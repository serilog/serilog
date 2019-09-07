``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]     : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT


```
|               Method |      Mean |      Error |      StdDev |    Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|-----------:|------------:|----------:|------:|--------:|-------:|------:|------:|----------:|
| EmitLogAIgnoredEvent |  21.76 ns |  0.4625 ns |   0.6922 ns |  21.71 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|         EmitLogEvent | 953.61 ns | 50.6144 ns | 145.2223 ns | 906.43 ns | 39.24 |    2.66 | 0.1183 |     - |     - |     376 B |
