using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Serilog.PerformanceTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //How to use console arguments => https://benchmarkdotnet.org/articles/guides/console-args.html
            //Running All Benchmark        -> dotnet run -c Release --framework netcoreapp3.1 --filter *
            //Running a specific Benchmark -> dotnet run -c Release --framework netcoreapp3.1 --filter Serilog.PerformanceTests.PipelineBenchmark.*

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, GetGlobalConfig());
        }

        static IConfig GetGlobalConfig() => DefaultConfig.Instance
                                                         .AddJob(Job.Default.WithRuntime(ClrRuntime.Net48).WithJit(Jit.RyuJit).WithId("net48RyuJit").AsDefault())
                                                         .AddJob(Job.Default.WithRuntime(ClrRuntime.Net48).WithJit(Jit.LegacyJit).WithId("net48LegacyJit"))
                                                         .AddJob(Job.Default.WithRuntime(CoreRuntime.Core31).WithJit(Jit.RyuJit).WithId("core31RyuJit"));
        
    }
}
