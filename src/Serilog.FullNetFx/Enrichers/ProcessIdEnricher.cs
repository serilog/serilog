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

using System.Diagnostics;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Enrichers
{
    /// <summary>
    /// Enriches log events with a ProcessId property containing the current <see cref="Process.Id"/>.
    /// </summary>
    public class ProcessIdEnricher : ILogEventEnricher
    {
        LogEventProperty _cachedProperty;

        /// <summary>
        /// The name of the property the enricher provides.
        /// </summary>
        public const string PropertyName = "ProcessId";

        /// <summary>
        /// Enrich the log event.
        /// </summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory"></param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            _cachedProperty = _cachedProperty ?? propertyFactory.CreateProperty(PropertyName, Process.GetCurrentProcess().Id);
            logEvent.AddPropertyIfAbsent(_cachedProperty);
        }
    }
}
