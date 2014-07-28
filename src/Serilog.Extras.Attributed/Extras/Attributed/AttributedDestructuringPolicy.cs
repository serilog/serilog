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
using System.Reflection;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using System.Threading;
using CachedFunc = System.Func<object, Serilog.Core.ILogEventPropertyValueFactory, Serilog.Events.LogEventPropertyValue>;

namespace Serilog.Extras.Attributed
{

    class AttributedDestructuringPolicy : IDestructuringPolicy
    {

        ReaderWriterLockSlim _locker =new ReaderWriterLockSlim();
        readonly Dictionary<Type, CachedFunc> _cache = new Dictionary<Type, CachedFunc>();

        CachedFunc GetOrAddFunc(object value)
        {
            var t = value.GetType();
            try
            {
                _locker.EnterUpgradeableReadLock();
                CachedFunc func;
                if (_cache.TryGetValue(t, out func))
                {
                    return func;
                }
                func = GetValueToCache(value);
                _locker.EnterWriteLock();
                try
                {
                    _cache[t] = func;
                }
                finally
                {
                    _locker.ExitWriteLock();   
                }
                return func;
            }
            finally
            {
                _locker.ExitUpgradeableReadLock();
            }
        }

        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
        {
            var func = GetOrAddFunc(value);

            if (func == null)
            {
                result = null;
                return false;
            }
            result = func(value, propertyValueFactory);
            return true;
        }


        static CachedFunc GetValueToCache(object value)
        {
            var type = value.GetType();
            var ti = type.GetTypeInfo();

            var logAsScalar = ti.GetCustomAttribute<LogAsScalarAttribute>();
            if (logAsScalar != null)
            {
                return (o, f) => MakeScalar(o, logAsScalar.IsMutable);
            }
            var properties = GetProperties(ti)
                .ToList();
            if (properties.Any(pi =>
                pi.GetCustomAttribute<LogAsScalarAttribute>() != null ||
                pi.GetCustomAttribute<NotLoggedAttribute>() != null))
            {
                var loggedProperties = properties
                    .Where(pi => pi.GetCustomAttribute<NotLoggedAttribute>() == null)
                    .ToList();

                var scalars = loggedProperties
                    .Where(pi => pi.GetCustomAttribute<LogAsScalarAttribute>() != null)
                    .ToDictionary(pi => pi, pi => pi.GetCustomAttribute<LogAsScalarAttribute>().IsMutable);

                return (o, f) => MakeStructure(value, loggedProperties, scalars, f, type);
            }
            return null;
        }

        static LogEventPropertyValue MakeStructure(object value, IEnumerable<PropertyInfo> loggedProperties, Dictionary<PropertyInfo, bool> scalars, ILogEventPropertyValueFactory propertyValueFactory, Type type)
        {
            var structureProperties = new List<LogEventProperty>();
            foreach (var pi in loggedProperties)
            {
                object propValue;
                try
                {
                    propValue = pi.GetValue(value);
                }
                catch (TargetInvocationException ex)
                {
                    SelfLog.WriteLine("The property accessor {0} threw exception {1}", pi, ex);
                    propValue = "The property accessor threw an exception: " + ex.InnerException.GetType().Name;
                }

                LogEventPropertyValue pv;
                bool stringify;

                if (propValue == null)
                {
                    pv = new ScalarValue(null);
                }
                else if (scalars.TryGetValue(pi, out stringify))
                {
                    pv = MakeScalar(propValue, stringify);
                }
                else
                {
                    pv = propertyValueFactory.CreatePropertyValue(propValue, true);
                }

                structureProperties.Add(new LogEventProperty(pi.Name, pv));
            }
            return new StructureValue(structureProperties, type.Name);
        }

        static ScalarValue MakeScalar(object value, bool stringify)
        {
            return new ScalarValue(stringify ? value.ToString() : value);
        }

        static IEnumerable<PropertyInfo> GetProperties(TypeInfo ti)
        {
            var seenNames = new HashSet<string>();

            var valueType = ti;
            while (valueType.AsType() != typeof(object))
            {
                var props = valueType.DeclaredProperties.Where(p => p.CanRead &&
                                                                    p.GetMethod.IsPublic &&
                                                                    !p.GetMethod.IsStatic);

                foreach (var prop in props)
                {
                    if (seenNames.Contains(prop.Name))
                        continue;

                    seenNames.Add(prop.Name);
                    yield return prop;
                }
                
                valueType = valueType.BaseType.GetTypeInfo();
            }
        }
    }
}
