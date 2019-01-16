``` ini

BenchmarkDotNet=v0.10.6, OS=Mac OS X 10.12
Processor=Intel Core i7-7567U CPU 3.50GHz (Kaby Lake), ProcessorCount=4
Frequency=1000000000 Hz, Resolution=1.0000 ns, Timer=UNKNOWN
dotnet cli version=2.1.4
  [Host]     : .NET Core 4.6.0.0, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.0.0, 64bit RyuJIT


```
 |                       Method |       Mean |     Error |    StdDev | Scaled | ScaledSD |  Gen 0 | Allocated |
 |----------------------------- |-----------:|----------:|----------:|-------:|---------:|-------:|----------:|
 |                EmptyTemplate |   371.0 ns |  7.570 ns |  14.03 ns |   1.00 |     0.00 | 0.1259 |     264 B |
 | DefaultConsoleOutputTemplate | 2,912.1 ns | 66.661 ns | 190.19 ns |   7.86 |     0.58 | 1.0948 |    2296 B |
