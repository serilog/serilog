``` ini

BenchmarkDotNet=v0.10.6, OS=Mac OS X 10.12
Processor=Intel Core i7-7567U CPU 3.50GHz (Kaby Lake), ProcessorCount=4
Frequency=1000000000 Hz, Resolution=1.0000 ns, Timer=UNKNOWN
dotnet cli version=2.1.4
  [Host]     : .NET Core 4.6.0.0, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.0.0, 64bit RyuJIT


```
 |         Method |     Mean |     Error |    StdDev |  Gen 0 | Allocated |
 |--------------- |---------:|----------:|----------:|-------:|----------:|
 | FormatToOutput | 1.386 us | 0.0237 us | 0.0222 us | 0.2327 |     488 B |
