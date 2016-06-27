// Copyright 2013-2016 Serilog Contributors
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
using System.Reflection;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Policies
{
    class ReflectionTypesScalarDestructuringPolicy : IDestructuringPolicy
    {
        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
        {
            // These types and their subclasses are property-laden and deep;
            // most sinks will convert them to strings.
            if (value is Type || value is MemberInfo)
            {
                result = new ScalarValue(value);
                return true;
            }

            result = null;
            return false;
        }
    }
}
