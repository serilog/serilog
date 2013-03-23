// Copyright 2013 Nicholas Blumhardt
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Serilog.Core;
using Serilog.Events;

namespace Serilog
{
    /// <summary>
    /// An optional static entry point for logging that can be easily referenced
    /// by different parts of an application. To configure the <see cref="Log"/>
    /// set the Logger static property to a logger instance.
    /// </summary>
    /// <example>
    /// Log.Logger = new LoggerConfiguration()
    ///     .WithConsoleSink()
    ///     .CreateLogger();
    /// 
    /// var thing = "World";
    /// Log.Logger.Information("Hello, {Thing}!", thing);
    /// </example>
    /// <remarks>
    /// The methods on <see cref="Log"/> (and its dynamic sibling <see cref="ILogger"/>) are guaranteed
    /// never to throw exceptions. Methods on all other types may.
    /// </remarks>
    public static class Log
    {
        static ILogger _logger = new SilentLogger();

        /// <summary>
        /// The globally-shared logger.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static ILogger Logger
        {
            get { return _logger; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _logger = value;
            }
        }

        /// <summary>
        /// Create a logger that enriches log events with additional information
        /// via specified properties or enricher objects.
        /// </summary>
        /// <param name="enrichers">Enrichers that apply in the context.</param>
        /// <param name="fixedProperties">Properties set in the context.</param>
        /// <returns>A logger that will enrich log events as specified.</returns>
        public static ILogger ForContext(ILogEventEnricher[] enrichers, params LogEventProperty[] fixedProperties)
        {
            return Logger.ForContext(enrichers, fixedProperties);
        }

        /// <summary>
        /// Create a logger that enriches log events with additional information
        /// via specified properties.
        /// </summary>
        /// <param name="fixedProperties">Properties set in the context.</param>
        /// <returns>A logger that will enrich log events as specified.</returns>
        public static ILogger ForContext(params LogEventProperty[] fixedProperties)
        {
            return Logger.ForContext(fixedProperties);
        }

        /// <summary>
        /// Create a logger that enriches log events with additional information
        /// via specified properties or enricher objects.
        /// </summary>
        /// <param name="enrichers">Enrichers that apply in the context.</param>
        /// <param name="fixedProperties">Properties set in the context.</param>
        /// <typeparam name="TSource">Type generating log messages in the context.</typeparam>
        /// <returns>A logger that will enrich log events as specified.</returns>
        public static ILogger ForContext<TSource>(ILogEventEnricher[] enrichers, params LogEventProperty[] fixedProperties)
        {
            return Logger.ForContext<TSource>(enrichers, fixedProperties);
        }

        /// <summary>
        /// Create a logger that enriches log events with additional information
        /// via specified properties or enricher objects.
        /// </summary>
        /// <param name="fixedProperties">Properties set in the context.</param>
        /// <typeparam name="TSource">Type generating log messages in the context.</typeparam>
        /// <returns>A logger that will enrich log events as specified.</returns>
        public static ILogger ForContext<TSource>(params LogEventProperty[] fixedProperties)
        {
            return Logger.ForContext<TSource>(fixedProperties);
        }

        /// <summary>
        /// Write a log event with the specified level.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="messageTemplate"></param>
        /// <param name="propertyValues"></param>
        public static void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            Logger.Write(level, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Write a log event with the specified level and associated exception.
        /// </summary>
        /// <param name="level">The level of the event.</param>
        /// <param name="exception">Exception related to the event.</param>
        /// <param name="messageTemplate"></param>
        /// <param name="propertyValues"></param>
        public static void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Write(level, exception, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Determine if events at the specified level will be passed through
        /// to the log sinks.
        /// </summary>
        /// <param name="level">Level to check.</param>
        /// <returns>True if the level is enabled; otherwise, false.</returns>
        public static bool IsEnabled(LogEventLevel level)
        {
            return Logger.IsEnabled(level);
        }

        public static void Verbose(string messageTemplate, params object[] propertyValues)
        {
            Logger.Verbose(messageTemplate, propertyValues);
        }

        public static void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Verbose(exception, messageTemplate, propertyValues);
        }

        public static void Debug(string messageTemplate, params object[] propertyValues)
        {
            Logger.Debug(messageTemplate, propertyValues);
        }

        public static void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Debug(exception, messageTemplate, propertyValues);
        }

        public static void Information(string messageTemplate, params object[] propertyValues)
        {
            Logger.Information(messageTemplate, propertyValues);
        }

        public static void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Information(exception, messageTemplate, propertyValues);
        }

        public static void Warning(string messageTemplate, params object[] propertyValues)
        {
            Logger.Warning(messageTemplate, propertyValues);
        }

        public static void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Warning(exception, messageTemplate, propertyValues);
        }

        public static void Error(string messageTemplate, params object[] propertyValues)
        {
            Logger.Error(messageTemplate, propertyValues);
        }

        public static void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Error(exception, messageTemplate, propertyValues);
        }

        public static void Fatal(string messageTemplate, params object[] propertyValues)
        {
            Logger.Fatal(messageTemplate, propertyValues);
        }

        public static void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            Logger.Fatal(exception, messageTemplate, propertyValues);
        }
    }
}
