// Copyright 2014 Serilog Contributors
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
using Serilog.Events;

namespace Serilog.Core.Enrichers
{
    /// <summary>
    /// Adds a new property encricher to the log event.
    /// </summary>
    public class PropertyEnricher : ILogEventEnricher
    {
        readonly string _name;
        readonly object _value;
        readonly bool _destructureObjects;

        /// <summary>
        /// Create a new property encricher.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="value">The value of the property.</param>
        /// <param name="destructureObjects">False (default) if the property is scalar. True if property should be destructured.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public PropertyEnricher(string name, object value, bool destructureObjects = false)
        {
            if (name == null) throw new ArgumentNullException("name");
            _name = name;
            _value = value;
            _destructureObjects = destructureObjects;
        }

        /// <summary>
        /// Enrich the log event.
        /// </summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            if (propertyFactory == null) throw new ArgumentNullException("propertyFactory");
            var property = propertyFactory.CreateProperty(_name, _value, _destructureObjects);
            logEvent.AddPropertyIfAbsent(property);
        }
    }
}
