using BenchmarkDotNet.Running;

namespace Serilog.PerformanceTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //How to use console arguments => https://benchmarkdotnet.org/articles/guides/console-args.html
            //Running All Benchmark        -> dotnet run -c Release --framework net5.0 --filter *
            //Running a specific Benchmark -> dotnet run -c Release --framework net5.0 --filter Serilog.PerformanceTests.PipelineBenchmark.*

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
