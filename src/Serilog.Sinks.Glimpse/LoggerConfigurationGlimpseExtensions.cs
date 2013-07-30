using System;
using Glimpse.Core.Framework;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.Glimpse;

namespace Serilog
{
    /// <summary>
    /// Adds the WithGlimpseSink() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationGlimpseExtensions
    {
        /// <summary>
        /// Write log events to Glimpse
        /// </summary>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration Glimpse(this LoggerSinkConfiguration loggerConfiguration, LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum, IFormatProvider formatProvider = null)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
            
#pragma warning disable 618
            return loggerConfiguration.Sink(new GlimpseSink(GlimpseConfiguration.GetConfiguredMessageBroker, formatProvider), restrictedToMinimumLevel);
#pragma warning restore 618
        }
    }
}
