// Copyright 2013-2015 Serilog Contributors
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
using Serilog.Core.Enrichers;
using Serilog.Enrichers;

namespace Serilog.Configuration
{
    /// <summary>
    /// Controls enrichment configuration.
    /// </summary>
    public class LoggerEnrichmentConfiguration
    {
        readonly LoggerConfiguration _loggerConfiguration;
        readonly Action<ILogEventEnricher> _addEnricher;

        internal LoggerEnrichmentConfiguration(
            LoggerConfiguration loggerConfiguration,
            Action<ILogEventEnricher> addEnricher)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException(nameof(loggerConfiguration));
            if (addEnricher == null) throw new ArgumentNullException(nameof(addEnricher));
            _loggerConfiguration = loggerConfiguration;
            _addEnricher = addEnricher;
        }

        /// <summary>
        /// Specifies one or more enrichers that may add properties dynamically to
        /// log events.
        /// </summary>
        /// <param name="enrichers">Enrichers to apply to all events passing through
        /// the logger.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration With(params ILogEventEnricher[] enrichers)
        {
            if (enrichers == null) throw new ArgumentNullException(nameof(enrichers));
            foreach (var logEventEnricher in enrichers)
            {
                if (logEventEnricher == null)
                    throw new ArgumentException("Null enricher is not allowed.");
                _addEnricher(logEventEnricher);
            }
            return _loggerConfiguration;
        }

        /// <summary>
        /// Specifies an enricher that may add properties dynamically to
        /// log events.
        /// </summary>
        /// <typeparam name="TEnricher">Enricher type to apply to all events passing through
        /// the logger.</typeparam>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration With<TEnricher>()
            where TEnricher : ILogEventEnricher, new()
        {
            return With(new TEnricher());
        }

        /// <summary>
        /// Include the specified property value in all events logged to the logger.
        /// </summary>
        /// <param name="name">The name of the property to add.</param>
        /// <param name="value">The property value to add.</param>
        /// <param name="destructureObjects">If true, objects of unknown type will be logged as structures; otherwise they will be converted using <see cref="Object.ToString"/>.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration WithProperty(string name, object value, bool destructureObjects = false)
        {
            return With(new PropertyEnricher(name, value, destructureObjects));
        }

        /// <summary>
        /// Enrich log events with properties from <see cref="Context.LogContext"/>.
        /// </summary>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public LoggerConfiguration FromLogContext()
        {
            return With<LogContextEnricher>();
        }
    }
}
