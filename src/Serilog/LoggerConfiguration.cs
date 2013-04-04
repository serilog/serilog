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
using Serilog.Parameters;
using Serilog.Policies;
using Serilog.Sinks.DumpFile;
using Serilog.Sinks.File;
using Serilog.Sinks.RollingFile;
using Serilog.Sinks.SystemConsole;
using Serilog.Sinks.Trace;

namespace Serilog
{
    /// <summary>
    /// Configuration object for creating <see cref="ILogger"/> instances.
    /// </summary>
    public class LoggerConfiguration
    {
        const string DefaultOutputTemplate = "{TimeStamp} [{Level}] {Message:l}{NewLine:l}{Exception:l}";

        readonly List<ILogEventSink> _logEventSinks = new List<ILogEventSink>();
        readonly List<ILogEventEnricher> _enrichers = new List<ILogEventEnricher>(); 
        readonly List<ILogEventFilter> _filters = new List<ILogEventFilter>();
        readonly List<Type> _additionalScalarTypes = new List<Type>();
        readonly List<IDestructuringPolicy> _additionalDestructuringPolicies = new List<IDestructuringPolicy>();
        
        LogEventLevel _minimumLevel = LogEventLevel.Information;

        /// <summary>
        /// Writes log events to <see cref="System.Console"/>.
        /// </summary>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{TimeStamp} [{Level}] {Message:l}{NewLine:l}{Exception:l}".</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration WithConsoleSink(
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum,
            string outputTemplate = DefaultOutputTemplate)
        {
            var formatter = new MessageTemplateTextFormatter(outputTemplate);
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

            var sink = new SafeAggregateSink(_logEventSinks);
            
            if (_filters.Any())
                sink = new SafeAggregateSink(new[] { new FilteringSink(sink, _filters) });

            var converter = CreatePropertyValueConverter();
            var processor = new MessageTemplateProcessor(converter);

            return new Logger(processor, _minimumLevel, sink, _enrichers, dispose);
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
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{TimeStamp} [{Level}] {Message:l}{NewLine:l}{Exception:l}".</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration WithFileSink(
            string path,
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum,
            string outputTemplate = DefaultOutputTemplate)
        {
            var formatter = new MessageTemplateTextFormatter(outputTemplate);
            return WithSink(new FileSink(path, formatter), restrictedToMinimumLevel);
        }
        /// <summary>
        /// Write log events to a series of files. Each file will be named according to
        /// the date of the first log entry written to it. Only simple date-based rolling is
        /// currently supported.
        /// </summary>
        /// <param name="pathFormat">.NET format string describing the location of the log files,
        /// with {0} in the place of the file date. E.g. "Logs\myapp-{0}.log" will result in log
        /// files such as "Logs\myapp-2013-10-20.log", "Logs\myapp-2013-10-21.log" and so on.</param>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{TimeStamp} [{Level}] {Message:l}{NewLine:l}{Exception:l}".</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration WithRollingFileSink(
            string pathFormat,
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum,
            string outputTemplate = DefaultOutputTemplate)
        {
            var formatter = new MessageTemplateTextFormatter(outputTemplate);
            return WithSink(new RollingFileSink(pathFormat, formatter), restrictedToMinimumLevel);
        }

        /// <summary>
        /// Write log events to the <see cref="System.Diagnostics.Trace"/>.
        /// </summary>
        /// <param name="restrictedToMinimumLevel">The minimum level for
        /// events passed through the sink.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{TimeStamp} [{Level}] {Message:l}{NewLine:l}{Exception:l}".</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration WithDiagnosticTraceSink(
            LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum,
            string outputTemplate = DefaultOutputTemplate)
        {
            var formatter = new MessageTemplateTextFormatter(outputTemplate);
            return WithSink(new DiagnosticTraceSink(formatter), restrictedToMinimumLevel);
        }

        /// <summary>
        /// Include the specified property value in all events logged to the logger.
        /// </summary>
        /// <param name="propertyName">The name of the property to add.</param>
        /// <param name="value">The property value to add.</param>
        /// <param name="destructureObjects">If true, objects of unknown type will be logged as structures; otherwise they will be converted using <see cref="Object.ToString"/>.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration EnrichedWithProperty(string propertyName, object value, bool destructureObjects = false)
        {
            var propertyValueConverter = CreatePropertyValueConverter();
            return EnrichedBy(new FixedPropertyEnricher(propertyValueConverter.CreateProperty(propertyName, value, destructureObjects)));
        }

        /// <summary>
        /// Filter out log events from the stream based on the provided filter.
        /// </summary>
        /// <param name="filter">The filter to apply.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration FilteredBy(ILogEventFilter filter)
        {
            if (filter == null) throw new ArgumentNullException("filter");
            _filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Treat objects of the specified type as scalar values, i.e., don't break
        /// them down into properties event when destructuring complex types.
        /// </summary>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration TreatingAsScalar(Type scalarType)
        {
            if (scalarType == null) throw new ArgumentNullException("scalarType");
            _additionalScalarTypes.Add(scalarType);
            return this;
        }

        /// <summary>
        /// When destructuring objects, transform instances with the provided policy.
        /// </summary>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration TransformingValuesWith(IDestructuringPolicy destructuringPolicy)
        {
            if (destructuringPolicy == null) throw new ArgumentNullException("destructuringPolicy");
            _additionalDestructuringPolicies.Add(destructuringPolicy);
            return this;
        }

        /// <summary>
        /// When destructuring objects, transform instances of the specified type with
        /// the provided function.
        /// </summary>
        /// <param name="transformation">Function mapping instances of <typeparamref name="TValue"/>
        /// to an alternative representation.</param>
        /// <typeparam name="TValue">Type of values to transform.</typeparam>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public LoggerConfiguration TransformingValuesOf<TValue>(Func<TValue, object> transformation)
        {
            if (transformation == null) throw new ArgumentNullException("transformation");
            var policy = new ProjectedDestructuringPolicy(t => t == typeof(TValue),
                                                          o => transformation((TValue)o));
            return TransformingValuesWith(policy);
        }

        PropertyValueConverter CreatePropertyValueConverter()
        {
            return new PropertyValueConverter(_additionalScalarTypes, _additionalDestructuringPolicies);
        }
    }
}
