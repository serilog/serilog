using System;
using Serilog.Events;
using Serilog.Sinks.CouchDB;

namespace Serilog
{
    /// <summary>
    /// Adds the WithCouchDBSink() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationHttpExtensions
    {
        /// <summary>
        /// Adds a sink that writes log events as documents to a CouchDB database.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="databaseUrl">The URL of a created CouchDB database that log events will be written to.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        public static LoggerConfiguration WithCouchDBSink(this LoggerConfiguration loggerConfiguration, string databaseUrl, LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
            if (databaseUrl == null) throw new ArgumentNullException("databaseUrl");
            return loggerConfiguration.WithSink(new CouchDBSink(databaseUrl, loggerConfiguration.ParsedMessageTemplateCache), restrictedToMinimumLevel);
        }
    }
}
