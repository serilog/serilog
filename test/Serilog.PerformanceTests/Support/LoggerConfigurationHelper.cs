using Serilog.Configuration;

namespace Serilog.PerformanceTests.Support
{
    static class LoggerConfigurationHelper
    {
        public static LoggerConfiguration AddManyProperties(this LoggerConfiguration config, int numOfProps)
        {
            for (var i = 0; i < numOfProps; i++)
            {
                config = config.Enrich.WithProperty($"ConfigEnrichProp{i}", $"Value{i}");
            }
            return config;
        }

        public static LoggerConfiguration AddManySink(this LoggerConfiguration config, int numOfSinks)
        {
            for (var i = 0; i < numOfSinks; i++)
            {
                config = config.WriteTo.Sink(i % 2 == 0 ? new NullSink() : new DisposableNullSink());
            }
            return config;
        }

        public static LoggerConfiguration AddManyAuditSink(this LoggerConfiguration config, int numOfSinks)
        {
            for (var i = 0; i < numOfSinks; i++)
            {
                config = config.AuditTo.Sink(i % 2 == 0 ? new NullSink() : new DisposableNullSink());
            }
            return config;
        }

        public static LoggerConfiguration AddManyFilters(this LoggerConfiguration config, int numOfSinks)
        {
            for (var i = 0; i < numOfSinks; i++)
            {
                config = config.Filter.ByIncludingOnly(_ => true);
            }
            return config;
        }
    }
}
