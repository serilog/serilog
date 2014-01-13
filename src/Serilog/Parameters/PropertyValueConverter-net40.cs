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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Policies;

namespace Serilog.Parameters
{
    // Values in Serilog are simplified down into a lowest-common-denominator internal
    // type system so that there is a better chance of code written with one sink in
    // mind working correctly with any other. This techniqe also makes the programmer
    // writing a log event (roughly) in control of the cost of recording that event.
    partial class PropertyValueConverter : ILogEventPropertyFactory, ILogEventPropertyValueFactory
    {
        static readonly HashSet<Type> BuiltInScalarTypes = new HashSet<Type>
        {
            typeof(bool),
            typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint),
                typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal),
            typeof(string),
            typeof(DateTime), typeof(DateTimeOffset), typeof(TimeSpan),
            typeof(Guid), typeof(Uri)
        };

        readonly IDestructuringPolicy[] _destructuringPolicies; 
        readonly IScalarConversionPolicy[] _scalarConversionPolicies; 

        public PropertyValueConverter(IEnumerable<Type> additionalScalarTypes, IEnumerable<IDestructuringPolicy> additionalDestructuringPolicies)
        {
            if (additionalScalarTypes == null) throw new ArgumentNullException("additionalScalarTypes");
            if (additionalDestructuringPolicies == null) throw new ArgumentNullException("additionalDestructuringPolicies");

            _scalarConversionPolicies = new IScalarConversionPolicy[]
            {
                new SimpleScalarConversionPolicy(BuiltInScalarTypes.Concat(additionalScalarTypes)),
                new NullableScalarConversionPolicy(),
                new EnumScalarConversionPolicy(),
                new ByteArrayScalarConversionPolicy(),
                new ReflectionTypesScalarConversionPolicy()
            };

            _destructuringPolicies = additionalDestructuringPolicies
                .Concat(new []
                {
                    new DelegateDestructuringPolicy() 
                })
                .ToArray();
        }

        public LogEventProperty CreateProperty(string name, object value, bool destructureObjects = false)
        {
            return new LogEventProperty(name, CreatePropertyValue(value, destructureObjects));
        }

        public LogEventPropertyValue CreatePropertyValue(object value, bool destructureObjects = false)
        {
            return CreatePropertyValue(value, destructureObjects, 1);
        }

        public LogEventPropertyValue CreatePropertyValue(object value, Destructuring destructuring)
        {
            return CreatePropertyValue(value, destructuring, 1);
        }

        LogEventPropertyValue CreatePropertyValue(object value, bool destructureObjects, int depth)
        {
            return CreatePropertyValue(
                value,
                destructureObjects ?
                    Destructuring.Destructure :
                    Destructuring.Default,
                depth);
        }

        LogEventPropertyValue CreatePropertyValue(object value, Destructuring destructuring, int depth)
        {
            if (value == null)
                return new ScalarValue(null);

            if (destructuring == Destructuring.Stringify)
                return new ScalarValue(value.ToString());

            var valueType = value.GetType();
            var limiter = new DepthLimiter(depth, this);

            foreach (var scalarConversionPolicy in _scalarConversionPolicies)
            {
                ScalarValue converted;
                if (scalarConversionPolicy.TryConvertToScalar(value, limiter, out converted))
                    return converted;
            }

            var enumerable = value as IEnumerable;
            if (enumerable != null)
            {
                // Only dictionaries with 'scalar' keys are permitted, as
                // more complex keys may not serialize to unique values for
                // representation in sinks. This check strengthens the expectation
                // that resulting dictionary is representable in JSON as well
                // as richer formats (e.g. XML, .NET type-aware...).
                // Only actual dictionaries are supported, as arbitrary types
                // can implement multiple IDictionary interfaces and thus introduce
                // multiple different interpretations.
                if (valueType.IsGenericType &&
                    valueType.GetGenericTypeDefinition() == typeof(Dictionary<,>) &&
                    IsValidDictionaryKeyType(valueType.GetGenericArguments()[0]))
                {
                    return new DictionaryValue(enumerable.Cast<dynamic>()
                        .Select(kvp => new KeyValuePair<ScalarValue, LogEventPropertyValue>(
                            (ScalarValue)limiter.CreatePropertyValue(kvp.Key, destructuring),
                            limiter.CreatePropertyValue(kvp.Value, destructuring)))
                        .Where(kvp => kvp.Key != null));
                }

                return new SequenceValue(
                    enumerable.Cast<object>().Select(o => limiter.CreatePropertyValue(o, destructuring)));
            }

            // Unknown types

            if (destructuring == Destructuring.Destructure)
            {
                foreach (var destructuringPolicy in _destructuringPolicies)
                {
                    LogEventPropertyValue result;
                    if (destructuringPolicy.TryDestructure(value, limiter, out result))
                        return result;
                }

                var typeTag = value.GetType().Name;
                if (typeTag.Length <= 0 || !char.IsLetter(typeTag[0]))
                    typeTag = null;

                return new StructureValue(GetProperties(value, limiter), typeTag);
            }

            return new ScalarValue(value.ToString());
        }

        bool IsValidDictionaryKeyType(Type valueType)
        {
            return BuiltInScalarTypes.Contains(valueType) ||
                   valueType.IsEnum;
        }

        static IEnumerable<LogEventProperty> GetProperties(object value, ILogEventPropertyValueFactory recursive)
        {
            var valueType = value.GetType();
            var props = valueType.GetProperties().Where(p => p.CanRead &&
                                                            p.GetGetMethod().IsPublic &&
                                                            !p.GetGetMethod().IsStatic);

            foreach (var prop in props)
            {
                object propValue;
                try
                {
                    propValue = prop.GetValue(value, null);
                }
                catch (TargetInvocationException ex)
                {
                    SelfLog.WriteLine("The property accessor {0} threw exception {1}", prop, ex);
                    propValue = "The property accessor threw an exception: " + ex.InnerException.GetType().Name;
                }
                yield return new LogEventProperty(prop.Name, recursive.CreatePropertyValue(propValue, true));
            }
        }
    }
}
