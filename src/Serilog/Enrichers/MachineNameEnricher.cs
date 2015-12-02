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
using Serilog.Events;

#if !PROFILE259
namespace Serilog.Enrichers
{
    /// <summary>
    /// Enriches log events with a MachineName property containing <see cref="Environment.MachineName"/>.
    /// </summary>
    public class MachineNameEnricher : ILogEventEnricher
    {
        LogEventProperty _cachedProperty;

        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        public const string MachineNamePropertyName = "MachineName";

        /// <summary>
        /// Enrich the log event.
        /// </summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
#if !DOTNET5_4
            _cachedProperty = _cachedProperty ?? propertyFactory.CreateProperty(MachineNamePropertyName, Environment.MachineName);
#else
            _cachedProperty = _cachedProperty ?? propertyFactory.CreateProperty(MachineNamePropertyName, Environment.GetEnvironmentVariable("COMPUTERNAME"));
#endif
            logEvent.AddPropertyIfAbsent(_cachedProperty);
        }
    }
}
#endif
