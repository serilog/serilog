``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=2.2.402
  [Host]     : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.7 (CoreCLR 4.6.28008.02, CoreFX 4.6.28008.03), 64bit RyuJIT


```
|               Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |-----------:|----------:|----------:|------:|--------:|
|                 Bare |   9.715 ns | 0.0357 ns | 0.0334 ns |  1.00 |    0.00 |
|         PushProperty |  89.458 ns | 0.9460 ns | 0.8386 ns |  9.21 |    0.07 |
|   PushPropertyNested | 174.451 ns | 1.8966 ns | 1.6813 ns | 17.96 |    0.18 |
| PushPropertyEnriched | 154.993 ns | 1.3390 ns | 1.2525 ns | 15.95 |    0.11 |
