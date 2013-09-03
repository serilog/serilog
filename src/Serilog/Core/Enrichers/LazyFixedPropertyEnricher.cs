// Copyright 2013 Serilog Contributors
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
    class LazyFixedPropertyEnricher : ILogEventEnricher
    {
        readonly string _name;
        readonly object _value;
        readonly bool _destructureObjects;

        public LazyFixedPropertyEnricher(string name, object value, bool destructureObjects)
        {
            if (name == null) throw new ArgumentNullException("name");
            _name = name;
            _value = value;
            _destructureObjects = destructureObjects;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            if (propertyFactory == null) throw new ArgumentNullException("propertyFactory");
            var property = propertyFactory.CreateProperty(_name, _value, _destructureObjects);
            logEvent.AddPropertyIfAbsent(property);
        }
    }
}
