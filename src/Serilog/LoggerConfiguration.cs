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
using System.Collections.Generic;
using System.Linq;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.DumpFile;
using Serilog.Sinks.File;
using Serilog.Sinks.SystemConsole;
using Serilog.Sinks.Trace;

namespace Serilog
{
    /// <summary>
    /// Configuration object for creating <see cref="ILogger"/> instances.
    /// </summary>
    public class LoggerConfiguration
    {
        readonly List<ILogEventSink> _logEventSinks = new List<ILogEventSink>();
        readonly List<ILogEventEnricher> _enrichers = new List<ILogEventEnricher>(); 
        readonly IMessageTemplateCache _parsedMessageTemplateCache = new MessageTemplateCache();

        LogEventLevel _minimumLevel = LogEventLevel.Information;

        /// <summary>
        /// Used by extenders; provides access to a shared cache of parsed message templates.
        /// </summary>
        public IMessageTemplateCache ParsedMessageTemplateCache { get { return _parsedMessageTemplateCache; } }

        /// <summary>
        /// Writes log events to <see cref="System.Console"/>.
        /// </summary>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration WithConsoleSink(LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum)
        {
            const string defaultOutputTemplate = "{TimeStamp} [{Level}] {Message:l}{NewLine:l}{Exception:l}";
            var formatter = new MessageTemplateTextFormatter(defaultOutputTemplate, ParsedMessageTemplateCache);
            return WithSink(new ConsoleSink(formatter), restrictedToMinimumLevel);
        }

        /// <summary>
        /// Write log events to the specified <see cref="ILogEventSink"/>.
        /// </summary>
        /// <param name="logEventSink">The sink.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration WithSink(ILogEventSink logEventSink, LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum)
        {
            var sink = logEventSink;
            if (restrictedToMinimumLevel > LogEventLevel.Minimum)
                sink = new RestrictedSink(sink, restrictedToMinimumLevel);

            _logEventSinks.Add(sink);
            return this;
        }

        /// <summary>
        /// Sets the minimum level at which events will be passed to sinks. If
        /// not specified, only events at the <see cref="LogEventLevel.Information"/>
        /// level and above will be passed through.
        /// </summary>
        /// <param name="minimumLevel">The minimum level to set.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration MinimumLevel(LogEventLevel minimumLevel)
        {
            _minimumLevel = minimumLevel;
            return this;
        }

        /// <summary>
        /// Specificies one or more enrichers that may add properties dynamically to
        /// log events.
        /// </summary>
        /// <param name="enrichers">Enrichers to apply to all events passing through
        /// the logger.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration EnrichedBy(params ILogEventEnricher[] enrichers)
        {
            if (enrichers == null) throw new ArgumentNullException("enrichers");
            _enrichers.AddRange(enrichers);
            return this;
        }
        
        /// <summary>
        /// Create a logger using the configured sinks, enrichers and minimum level.
        /// </summary>
        /// <returns>The logger.</returns>
        /// <remarks>To free resources held by sinks ahead of program shutdown,
        /// the returned logger may be cast to <see cref="IDisposable"/> and
        /// disposed.</remarks>
        public ILogger CreateLogger()
        {
            Action dispose = () =>
            {
                foreach (var disp in _logEventSinks.OfType<IDisposable>())
                    disp.Dispose();
            };

            return new Logger(_parsedMessageTemplateCache, _minimumLevel, new SafeAggregateSink(_logEventSinks), _enrichers, dispose);
        }

        /// <summary>
        /// Write log events in a simple text dump format to the specified file.
        /// </summary>
        /// <param name="path">Path to the dump file.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration WithDumpFileSink(string path, LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum)
        {
            return WithSink(new DumpFileSink(path), restrictedToMinimumLevel);
        }

        /// <summary>
        /// Write log events to the specified file.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration WithFileSink(string path, LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum)
        {
            const string defaultOutputTemplate = "{TimeStamp} [{Level}] {Message:l}{NewLine:l}{Exception:l}";
            var formatter = new MessageTemplateTextFormatter(defaultOutputTemplate, ParsedMessageTemplateCache);
            return WithSink(new FileSink(path, formatter), restrictedToMinimumLevel);
        }

        /// <summary>
        /// Write log events to the <see cref="System.Diagnostics.Trace"/>.
        /// </summary>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration WithDiagnosticTraceSink(LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum)
        {
            const string defaultOutputTemplate = "[{Level}] {Message:l}{NewLine:l}{Exception:l}";
            var formatter = new MessageTemplateTextFormatter(defaultOutputTemplate, ParsedMessageTemplateCache);
            return WithSink(new DiagnosticTraceSink(formatter), restrictedToMinimumLevel);
        }

        /// <summary>
        /// Include the specified property value in all events logged to the logger.
        /// </summary>
        /// <param name="propertyName">The name of the property to add.</param>
        /// <param name="value">The property value to add.</param>
        /// <param name="destructureObjects">If true, objects of unknown type will be logged as structures; otherwise they will be converted using <see cref="Object.ToString"/>.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration WithFixedProperty(string propertyName, object value, bool destructureObjects = false)
        {
            return EnrichedBy(new FixedPropertyEnricher(new[] { LogEventProperty.For(propertyName, value, destructureObjects) }));
        }
    }
}
