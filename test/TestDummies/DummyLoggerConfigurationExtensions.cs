using System;
using System.Collections.Generic;
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
            this LoggerSinkConfiguration loggerSinkConfiguration,
            string pathFormat,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            string outputTemplate = null,
            IFormatProvider formatProvider = null)
        {
            return loggerSinkConfiguration.Sink(new DummyRollingFileSink(), restrictedToMinimumLevel);
        }

        public static LoggerConfiguration DummyRollingFile(
            this LoggerSinkConfiguration loggerSinkConfiguration,
            ITextFormatter formatter,
            string pathFormat,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum)
        {
            return loggerSinkConfiguration.Sink(new DummyRollingFileSink(), restrictedToMinimumLevel);
        }

        public static LoggerConfiguration DummyRollingFile(
            this LoggerAuditSinkConfiguration loggerSinkConfiguration,
            string pathFormat,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            string outputTemplate = null,
            IFormatProvider formatProvider = null)
        {
            return loggerSinkConfiguration.Sink(new DummyRollingFileAuditSink(), restrictedToMinimumLevel);
        }
    }
}