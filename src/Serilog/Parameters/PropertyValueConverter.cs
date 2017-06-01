// Copyright 2013-2015 Serilog Contributors
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
using System.Runtime.CompilerServices;

namespace Serilog.Parameters
{
    // Values in Serilog are simplified down into a lowest-common-denominator internal
    // type system so that there is a better chance of code written with one sink in
    // mind working correctly with any other. This technique also makes the programmer
    // writing a log event (roughly) in control of the cost of recording that event.
    partial class PropertyValueConverter : ILogEventPropertyFactory, ILogEventPropertyValueFactory
    {
        static readonly HashSet<Type> BuiltInScalarTypes = new HashSet<Type>
        {
            typeof(bool),
            typeof(char),
            typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint),
            typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal),
            typeof(string),
            typeof(DateTime), typeof(DateTimeOffset), typeof(TimeSpan),
            typeof(Guid), typeof(Uri)
        };

        readonly IDestructuringPolicy[] _destructuringPolicies;
        readonly IScalarConversionPolicy[] _scalarConversionPolicies;
        readonly int _maximumDestructuringDepth;
        readonly int _maximumStringLength;
        readonly int _maximumCollectionCount;
        readonly bool _propagateExceptions;

        public PropertyValueConverter(
            int maximumDestructuringDepth, 
            int maximumStringLength,
            int maximumCollectionCount,
            IEnumerable<Type> additionalScalarTypes,
            IEnumerable<IDestructuringPolicy> additionalDestructuringPolicies,
            bool propagateExceptions)
        {
            if (additionalScalarTypes == null) throw new ArgumentNullException(nameof(additionalScalarTypes));
            if (additionalDestructuringPolicies == null) throw new ArgumentNullException(nameof(additionalDestructuringPolicies));
            if (maximumDestructuringDepth < 0) throw new ArgumentOutOfRangeException(nameof(maximumDestructuringDepth));
            if (maximumStringLength < 2) throw new ArgumentOutOfRangeException(nameof(maximumStringLength));
            if (maximumCollectionCount < 1) throw new ArgumentOutOfRangeException(nameof(maximumCollectionCount));

            _maximumDestructuringDepth = maximumDestructuringDepth;
            _propagateExceptions = propagateExceptions;
            _maximumStringLength = maximumStringLength;
            _maximumCollectionCount = maximumCollectionCount;

            _scalarConversionPolicies = new IScalarConversionPolicy[]
            {
                new SimpleScalarConversionPolicy(BuiltInScalarTypes.Concat(additionalScalarTypes)),
                new NullableScalarConversionPolicy(),
                new EnumScalarConversionPolicy(),
                new ByteArrayScalarConversionPolicy()
            };

            _destructuringPolicies = additionalDestructuringPolicies
                .Concat(new IDestructuringPolicy []
                {
                    new DelegateDestructuringPolicy(),
                    new ReflectionTypesScalarDestructuringPolicy()
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
            try
            {
                return CreatePropertyValue(value, destructuring, 1);
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine("Exception caught while converting property value: {0}", ex);

                if (_propagateExceptions)
                    throw;

                return new ScalarValue("Capturing the property value threw an exception: " + ex.GetType().Name);
            }
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
            {
                return Stringify(value);
            }

            var valueType = value.GetType();
            var limiter = new DepthLimiter(depth, _maximumDestructuringDepth, this);

            if (destructuring == Destructuring.Destructure)
            {
                var stringValue = value as string;
                if (stringValue != null)
                {
                    value = TruncateIfNecessary(stringValue);
                }
            }

            foreach (var scalarConversionPolicy in _scalarConversionPolicies)
            {            
                ScalarValue converted;
                if (scalarConversionPolicy.TryConvertToScalar(value, limiter, out converted))
                    return converted;
            }

            if (destructuring == Destructuring.Destructure)
            {
                foreach (var destructuringPolicy in _destructuringPolicies)
                {
                    LogEventPropertyValue result;
                    if (destructuringPolicy.TryDestructure(value, limiter, out result))
                        return result;
                }
            }

            if (TryConvertEnumerable(value, destructuring, valueType, limiter, out var enumerableResult))
                return enumerableResult;

            if (TryConvertValueTuple(value, destructuring, valueType, limiter, out var tupleResult))
                return tupleResult;

            if (destructuring == Destructuring.Destructure)
            {
                var type = value.GetType();
                var typeTag = type.Name;
                if (typeTag.Length <= 0 || IsCompilerGeneratedType(type))
                {
                    typeTag = null;
                }

                return new StructureValue(GetProperties(value, limiter), typeTag);
            }

            return new ScalarValue(value.ToString());
        }

        bool TryConvertEnumerable(object value, Destructuring destructuring, Type valueType, DepthLimiter limiter, out LogEventPropertyValue result)
        {
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
                if (IsValueTypeDictionary(valueType))
                {
                    var typeInfo = typeof(KeyValuePair<,>).MakeGenericType(valueType.GenericTypeArguments).GetTypeInfo();
                    var keyProperty = typeInfo.GetDeclaredProperty("Key");
                    var valueProperty = typeInfo.GetDeclaredProperty("Value");

                    result = new DictionaryValue(enumerable
                        .Cast<object>()
                        .Take(_maximumCollectionCount)
                        .Select(kvp => new KeyValuePair<ScalarValue, LogEventPropertyValue>(
                            (ScalarValue) limiter.CreatePropertyValue(keyProperty.GetValue(kvp), destructuring),
                            limiter.CreatePropertyValue(valueProperty.GetValue(kvp), destructuring)))
                        .Where(kvp => kvp.Key.Value != null));
                    return true;
                }

                result = new SequenceValue(enumerable
                    .Cast<object>()
                    .Take(_maximumCollectionCount)
                    .Select(o => limiter.CreatePropertyValue(o, destructuring)));

                return true;
            }

            result = null;
            return false;
        }

        static bool TryConvertValueTuple(object value, Destructuring destructuring, Type valueType, DepthLimiter limiter, out LogEventPropertyValue result)
        {
            if (!(value is IStructuralEquatable && valueType.IsConstructedGenericType))
            {
                result = null;
                return false;
            }

            var definition = valueType.GetGenericTypeDefinition();

            // Ignore the 8+ value case for now.
#if VALUETUPLE
            if (definition == typeof(ValueTuple<>) || definition == typeof(ValueTuple<,>) ||
                definition == typeof(ValueTuple<,,>) || definition == typeof(ValueTuple<,,,>) ||
                definition == typeof(ValueTuple<,,,,>) || definition == typeof(ValueTuple<,,,,,>) ||
                definition == typeof(ValueTuple<,,,,,,>))
#else
            var defn = definition.FullName;
            if (defn == "System.ValueTuple`1" || defn == "System.ValueTuple`2" ||
                defn == "System.ValueTuple`3" || defn == "System.ValueTuple`4" ||
                defn == "System.ValueTuple`5" || defn == "System.ValueTuple`6" ||
                defn == "System.ValueTuple`7")
#endif
            {
                var elements = new List<LogEventPropertyValue>();
                foreach (var field in valueType.GetTypeInfo().DeclaredFields)
                {
                    if (field.IsPublic && !field.IsStatic)
                    {
                        var fieldValue = field.GetValue(value);
                        var propertyValue = limiter.CreatePropertyValue(fieldValue, destructuring);
                        elements.Add(propertyValue);
                    }
                }

                result = new SequenceValue(elements);
                return true;
            }

            result = null;
            return false;
        }

        LogEventPropertyValue Stringify(object value)
        {
            var stringified = value.ToString();
            var truncated = TruncateIfNecessary(stringified);
            return new ScalarValue(truncated);
        }

        string TruncateIfNecessary(string text)
        {
            if (text.Length > _maximumStringLength)
            {
                return text.Substring(0, _maximumStringLength - 1) + "…";
            }

            return text;
        }

        bool IsValueTypeDictionary(Type valueType)
        {
            return valueType.IsConstructedGenericType &&
                   valueType.GetGenericTypeDefinition() == typeof (Dictionary<,>) &&
                   IsValidDictionaryKeyType(valueType.GenericTypeArguments[0]);
        }

        bool IsValidDictionaryKeyType(Type valueType)
        {
            return BuiltInScalarTypes.Contains(valueType) ||
                   valueType.GetTypeInfo().IsEnum;
        }

        IEnumerable<LogEventProperty> GetProperties(object value, ILogEventPropertyValueFactory recursive)
        {
            foreach (var prop in value.GetType().GetPropertiesRecursive())
            {
                object propValue;
                try
                {
                    propValue = prop.GetValue(value);
                }
                catch (TargetParameterCountException)
                {
                    // These properties would ideally be ignored; since they never produce values they're not
                    // of concern to auditing and exceptions can be suppressed.
                    SelfLog.WriteLine("The property accessor {0} is a non-default indexer", prop);
                    continue;
                }
                catch (TargetInvocationException ex)
                {
                    SelfLog.WriteLine("The property accessor {0} threw exception: {1}", prop, ex);

                    if (_propagateExceptions)
                        throw;

                    propValue = "The property accessor threw an exception: " + ex.InnerException.GetType().Name;
                }
                yield return new LogEventProperty(prop.Name, recursive.CreatePropertyValue(propValue, true));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsCompilerGeneratedType(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            var typeName = type.Name;

            //C# Anonymous types always start with "<>" and VB's start with "VB$"
            return typeInfo.IsGenericType && typeInfo.IsSealed && typeInfo.IsNotPublic && type.Namespace == null
                && (typeName[0] == '<'
                    || (typeName.Length > 2 && typeName[0] == 'V' && typeName[1] == 'B' && typeName[2] == '$'));
        }
    }
}

