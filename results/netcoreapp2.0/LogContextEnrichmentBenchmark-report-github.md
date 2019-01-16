``` ini

BenchmarkDotNet=v0.10.6, OS=Mac OS X 10.12
Processor=Intel Core i7-7567U CPU 3.50GHz (Kaby Lake), ProcessorCount=4
Frequency=1000000000 Hz, Resolution=1.0000 ns, Timer=UNKNOWN
dotnet cli version=2.1.4
  [Host]     : .NET Core 4.6.0.0, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.0.0, 64bit RyuJIT


```
 |               Method |      Mean |     Error |     StdDev | Scaled | ScaledSD |
 |--------------------- |----------:|----------:|-----------:|-------:|---------:|
 |                 Bare |  12.05 ns | 0.3210 ns |  0.5789 ns |   1.00 |     0.00 |
 |         PushProperty | 149.67 ns | 2.9527 ns |  3.2820 ns |  12.45 |     0.62 |
 |   PushPropertyNested | 296.44 ns | 6.7011 ns | 11.0101 ns |  24.65 |     1.43 |
 | PushPropertyEnriched | 304.18 ns | 5.7287 ns |  8.3971 ns |  25.29 |     1.33 |
