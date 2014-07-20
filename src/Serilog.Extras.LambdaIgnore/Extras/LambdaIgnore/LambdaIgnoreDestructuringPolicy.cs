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

namespace Serilog.Extras.LambdaIgnore.Extras.LambdaIgnore
{
    class LambdaIgnoreDestructuringPolicy<TDestructureType> : IDestructuringPolicy
    {
        private readonly IEnumerable<PropertyInfo> _propertiesToInclude;
        private readonly Type _destructureType;

        public LambdaIgnoreDestructuringPolicy(params Expression<Func<TDestructureType, object>>[] ignored)
        {
            _destructureType = typeof(TDestructureType);
            var namesOfPropertyToIgnore = ignored.Select(GetNameOfPropertyToIgnore);
            _propertiesToInclude = _destructureType.GetProperties().Where(p => !namesOfPropertyToIgnore.Contains(p.Name)).ToList();
        }

        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
        {
            if (value == null || value.GetType() != typeof(TDestructureType))
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
                object value;
                try
                {
                    value = propertyInfo.GetValue(value);
                }
                catch (TargetInvocationException ex)
                {
                    SelfLog.WriteLine("The property accessor {0} threw exception {1}", propertyInfo, ex);
                    value = "The property accessor threw an exception: " + ex.InnerException.GetType().Name;
                }

                LogEventPropertyValue propertyValue;

                if (value == null)
                {
                    propertyValue = new ScalarValue(null);
                }
                else
                {
                    propertyValue = propertyValueFactory.CreatePropertyValue(value, true);
                }

                structureProperties.Add(new LogEventProperty(propertyInfo.Name, propertyValue));
            }

            return new StructureValue(structureProperties, _destructureType.Name);
        }

        private string GetNameOfPropertyToIgnore(Expression<Func<TDestructureType, object>> ignored)
        {

            var bodyOfExpression = ignored.Body as MemberExpression;

            if (bodyOfExpression == null)
            {
                var unaryExpression = (UnaryExpression)ignored.Body;
                bodyOfExpression = unaryExpression.Operand as MemberExpression;
            }

            if (bodyOfExpression != null) 
                return bodyOfExpression.Member.Name;

            return string.Empty;
        }
    }
}
