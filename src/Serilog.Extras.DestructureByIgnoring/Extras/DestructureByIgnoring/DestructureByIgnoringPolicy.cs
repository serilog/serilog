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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;

namespace Serilog.Extras.DestructureByIgnoring
{
    class DestructureByIgnoringPolicy<TDestructure> : IDestructuringPolicy
    {
        private readonly IEnumerable<PropertyInfo> _propertiesToInclude;
        private readonly Type _destructureType;

        public DestructureByIgnoringPolicy(params Expression<Func<TDestructure, object>>[] ignoredProperties)
        {
            _destructureType = typeof(TDestructure);
            var namesOfPropertiesToIgnore = ignoredProperties.Select(GetNameOfPropertyToIgnore).ToArray();
            var runtimeProperties = _destructureType.GetRuntimeProperties();

            _propertiesToInclude = runtimeProperties.Where(p => !namesOfPropertiesToIgnore.Contains(p.Name)).ToArray();
        }

        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
        {
            if (value == null || value.GetType() != typeof(TDestructure))
            {
                result = null;
                return false;
            }
            
            result = BuildStructure(value, propertyValueFactory);

            return true;
        }

        private LogEventPropertyValue BuildStructure(object value, ILogEventPropertyValueFactory propertyValueFactory)
        {
            var structureProperties = new List<LogEventProperty>();
            foreach (var propertyInfo in _propertiesToInclude)
            {
                object propertyValue;
                try
                {
                    propertyValue = propertyInfo.GetValue(value);
                }
                catch (TargetInvocationException ex)
                {
                    SelfLog.WriteLine("The property accessor {0} threw exception {1}", propertyInfo, ex);
                    propertyValue = "The property accessor threw an exception: " + ex.InnerException.GetType().Name;
                }

                var logEventPropertyValue = BuildLogEventProperty(propertyValue, propertyValueFactory);

                structureProperties.Add(new LogEventProperty(propertyInfo.Name, logEventPropertyValue));
            }

            return new StructureValue(structureProperties, _destructureType.Name);
        }

        private static LogEventPropertyValue BuildLogEventProperty(object propertyValue, ILogEventPropertyValueFactory propertyValueFactory)
        {
            return propertyValue == null ? new ScalarValue(null) : propertyValueFactory.CreatePropertyValue(propertyValue, true);
        }

        private static string GetNameOfPropertyToIgnore(Expression<Func<TDestructure, object>> ignoredProperty)
        {
            return ignoredProperty.GetPropertyNameFromExpression();
        }
    }
}
