using System;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;

namespace Serilog.PerformanceTests
{
    enum MyConfigs
    {
        Default,
        ShortRun,
        SpanCompare
    }

    [AttributeUsage(AttributeTargets.Class)]
    class MyBenchmarkRun : Attribute, IConfigSource
    {
        public IConfig Config { get; }

        public MyBenchmarkRun(MyConfigs theConfig = MyConfigs.Default)
        {
            Config = theConfig switch
            {
                MyConfigs.Default => new MyDefaultConfig(),
                MyConfigs.ShortRun => new MyShortRunConfig(),
                MyConfigs.SpanCompare => new MySpanCompareConfig(),
                _ => throw new ArgumentOutOfRangeException(nameof(theConfig), theConfig, null)
            };
        }

        class MyDefaultConfig : ManualConfig
        {
            public MyDefaultConfig()
            {
                AddJob(Job.MediumRun.WithRuntime(ClrRuntime.Net48).WithJit(Jit.RyuJit).WithId("net48 RyuJit"));
                AddJob(Job.MediumRun.WithRuntime(ClrRuntime.Net48).WithJit(Jit.LegacyJit).WithId("net48 LegacyJit"));
                AddJob(Job.MediumRun.WithRuntime(CoreRuntime.Core31).WithJit(Jit.RyuJit).WithId("core31 RyuJit"));
            }
        }
        class MyShortRunConfig : ManualConfig
        {
            public MyShortRunConfig()
            {
                AddJob(Job.ShortRun.WithRuntime(ClrRuntime.Net48).WithJit(Jit.RyuJit).WithId("net48 RyuJit"));
                AddJob(Job.ShortRun.WithRuntime(ClrRuntime.Net48).WithJit(Jit.LegacyJit).WithId("net48 LegacyJit"));
                AddJob(Job.ShortRun.WithRuntime(CoreRuntime.Core31).WithJit(Jit.RyuJit).WithId("core31 RyuJit"));
            }
        }
        class MySpanCompareConfig : ManualConfig
        {
            public MySpanCompareConfig()
            {
                AddJob(Job.MediumRun.WithRuntime(ClrRuntime.Net48).WithJit(Jit.RyuJit).WithId("net48 RyuJit"));
                AddJob(Job.MediumRun.WithRuntime(CoreRuntime.Core22).WithJit(Jit.RyuJit).WithId("core22 RyuJit"));
                AddJob(Job.MediumRun.WithRuntime(CoreRuntime.Core31).WithJit(Jit.RyuJit).WithId("core31 RyuJit"));
            }
        }
    }
}
