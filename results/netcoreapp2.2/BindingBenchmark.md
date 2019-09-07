``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.401
  [Host]     : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.6 (CoreCLR 4.6.27817.03, CoreFX 4.6.27818.02), 64bit RyuJIT


```
|   Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
| BindZero |  65.46 ns |  1.379 ns |  2.227 ns |  1.00 |    0.00 |      - |     - |     - |         - |
|  BindOne | 234.15 ns | 10.502 ns | 29.792 ns |  3.59 |    0.43 | 0.0455 |     - |     - |     144 B |
| BindFive | 602.77 ns | 11.822 ns | 16.182 ns |  9.22 |    0.42 | 0.1364 |     - |     - |     432 B |
