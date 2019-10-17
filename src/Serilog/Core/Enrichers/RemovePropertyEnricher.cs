// Copyright 2019 Serilog Contributors
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
    class RemovePropertyEnricher : ILogEventEnricher
    {
        readonly string _propertyName;
        readonly bool _retainWhenInMessage;

        public RemovePropertyEnricher(string propertyName, bool retainWhenInMessage)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentException("Property name must not be null or empty.", nameof(propertyName));
            _propertyName = propertyName;
            _retainWhenInMessage = retainWhenInMessage;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (!_retainWhenInMessage)
            {
                logEvent.RemovePropertyIfPresent(_propertyName);
                return;
            }

            if (logEvent.Properties.ContainsKey(_propertyName))
            {
                for (var i = 0; i < logEvent.MessageTemplate.NamedProperties.Length; ++i)
                {
                    if (logEvent.MessageTemplate.NamedProperties[i].PropertyName == _propertyName)
                        return;
                }

                logEvent.RemovePropertyIfPresent(_propertyName);
            }
        }
    }
}
