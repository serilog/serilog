``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.302
  [Host]     : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT
  DefaultJob : .NET Core 2.1.20 (CoreCLR 4.6.29017.01, CoreFX 4.6.29018.12), X64 RyuJIT


```
|               Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
| EmitLogAIgnoredEvent |  13.52 ns | 0.135 ns | 0.126 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|         EmitLogEvent | 633.89 ns | 9.787 ns | 8.676 ns | 46.88 |    0.77 | 0.0591 |     - |     - |     376 B |
