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
using System.Reflection;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using System.Threading;
using System.Linq;
using Serilog.Parameters;
using CachedFunc = System.Func<object, Serilog.Core.ILogEventPropertyValueFactory, Serilog.Events.LogEventPropertyValue>;

namespace Serilog.Extras.Attributed
{

    class AttributedDestructuringPolicy : IDestructuringPolicy
    {
        ReaderWriterLockSlim _locker =new ReaderWriterLockSlim();
        readonly Dictionary<RuntimeTypeHandle, CachedFunc> _cache = new Dictionary<RuntimeTypeHandle, CachedFunc>();

        CachedFunc GetOrAddFunc(object value)
        {
            var t = value.GetType();
            try
            {
                _locker.EnterUpgradeableReadLock();
                CachedFunc func;
                if (_cache.TryGetValue(t.TypeHandle, out func))
                {
                    return func;
                }
                func = GetValueToCache(value);
                _locker.EnterWriteLock();
                try
                {
                    _cache[t.TypeHandle] = func;
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
            var properties = type.GetPropertiesRecursive()
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
                    .ToDictionary(pi => pi.Name, pi => pi.GetCustomAttribute<LogAsScalarAttribute>().IsMutable);

                return (o, f) => MakeStructure(value, loggedProperties.Select(x => x.GetPropertyAccessor()), scalars, f, type);
            }
            return null;
        }

        static LogEventPropertyValue MakeStructure(object value, IEnumerable<PropertyAccessor> loggedProperties, Dictionary<string, bool> scalars, ILogEventPropertyValueFactory propertyValueFactory, Type type)
        {
            var structureProperties = new List<LogEventProperty>();
            foreach (var propertyAccessor in loggedProperties)
            {
                object propValue;
                try
                {
                    propValue = propertyAccessor.GetDelegate(value);
                }
                catch (Exception ex)
                {
                    SelfLog.WriteLine("The property accessor {0} threw exception {1}", propertyAccessor.Name, ex);
                    propValue = "The property accessor threw an exception: " + ex.InnerException.GetType().Name;
                }

                LogEventPropertyValue pv;
                bool stringify;

                if (propValue == null)
                {
                    pv = new ScalarValue(null);
                }
                else if (scalars.TryGetValue(propertyAccessor.Name, out stringify))
                {
                    pv = MakeScalar(propValue, stringify);
                }
                else
                {
                    pv = propertyValueFactory.CreatePropertyValue(propValue, true);
                }

                structureProperties.Add(new LogEventProperty(propertyAccessor.Name, pv));
            }
            return new StructureValue(structureProperties, type.Name);
        }

        static ScalarValue MakeScalar(object value, bool stringify)
        {
            return new ScalarValue(stringify ? value.ToString() : value);
        }

    }
}
