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
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Parameters;

namespace Serilog
{
    /// <summary>
    /// Configuration object for creating <see cref="ILogger"/> instances.
    /// </summary>
    public class LoggerConfiguration
    {
        readonly List<ILogEventSink> _logEventSinks = new List<ILogEventSink>();
        readonly List<ILogEventEnricher> _enrichers = new List<ILogEventEnricher>(); 
        readonly List<ILogEventFilter> _filters = new List<ILogEventFilter>();
        readonly List<Type> _additionalScalarTypes = new List<Type>();
        readonly List<IDestructuringPolicy> _additionalDestructuringPolicies = new List<IDestructuringPolicy>();
        
        LogEventLevel _minimumLevel = LogEventLevel.Information;

        /// <summary>
        /// Configures the sinks that log events will be emitted to.
        /// </summary>
        public LoggerSinkConfiguration WriteTo
        {
            get
            {
                return new LoggerSinkConfiguration(this, s => _logEventSinks.Add(s));
            }
        }

        /// <summary>
        /// Configures the minimum level at which events will be passed to sinks. If
        /// not specified, only events at the <see cref="LogEventLevel.Information"/>
        /// level and above will be passed through.
        /// </summary>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerMinimumLevelConfiguration MinimumLevel
        {
            get
            {
                return new LoggerMinimumLevelConfiguration(this, l => _minimumLevel = l);
            }
        }

        /// <summary>
        /// Configures enrichment of <see cref="LogEvent"/>s. Enrichers can add, remove and
        /// modify the properties associated with events.
        /// </summary>
        public LoggerEnrichmentConfiguration Enrich
        {
            get
            {
                return new LoggerEnrichmentConfiguration(this, e => _enrichers.Add(e), CreatePropertyValueConverter);
            }
        }

        /// <summary>
        /// Configures global filtering of <see cref="LogEvent"/>s.
        /// </summary>
        public LoggerFilterConfiguration Filter
        {
            get
            {
                return new LoggerFilterConfiguration(this, f => _filters.Add(f));
            }
        }

        /// <summary>
        /// Configures destructuring of message template parameters.
        /// </summary>
        public LoggerDestructuringConfiguration Destructure
        {
            get
            {
                return new LoggerDestructuringConfiguration(this, _additionalScalarTypes.Add, _additionalDestructuringPolicies.Add);
            }
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

        PropertyValueConverter CreatePropertyValueConverter()
        {
            return new PropertyValueConverter(_additionalScalarTypes, _additionalDestructuringPolicies);
        }
    }
}
