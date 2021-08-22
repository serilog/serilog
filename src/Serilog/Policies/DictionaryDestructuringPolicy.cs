// Copyright 2013-2021 Serilog Contributors
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

using System.Collections;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Policies
{
    class DictionaryDestructuringPolicy : IDestructuringPolicy
    {
        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
        {
            if (value is IDictionary dict)
            {
                var elements = new List<KeyValuePair<ScalarValue, LogEventPropertyValue>>();
                foreach (var key in dict.Keys)
                {
                    elements.Add(new KeyValuePair<ScalarValue, LogEventPropertyValue>(
                        (ScalarValue)propertyValueFactory.CreatePropertyValue(key),
                        propertyValueFactory.CreatePropertyValue(dict[key], destructureObjects: true)
                    ));
                }

                result = new DictionaryValue(elements);
                return true;
            }

            result = null;
            return false;
        }
    }
}
