using System;
using Serilog;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Configuration;

namespace TestDummies
{
    public static class DummyLoggerConfigurationExtensions
    {
        public static LoggerConfiguration WithDummyThreadId(this LoggerEnrichmentConfiguration enrich)
        {
            return enrich.With(new DummyThreadIdEnricher());
        }

        public static LoggerConfiguration DummyRollingFile(
            LoggerSinkConfiguration loggerSinkConfiguration,
            string pathFormat,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            string outputTemplate = null,
            IFormatProvider formatProvider = null)
        {
            return null;
        }

        public static LoggerConfiguration DummyRollingFile(
            LoggerSinkConfiguration loggerSinkConfiguration,
            ITextFormatter formatter,
            string pathFormat,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
        {
            return null;
        }
    }
}