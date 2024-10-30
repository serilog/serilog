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

namespace Serilog.Capturing;

// Values in Serilog are simplified down into a lowest-common-denominator internal
// type system so that there is a better chance of code written with one sink in
// mind working correctly with any other. This technique also makes the programmer
// writing a log event (roughly) in control of the cost of recording that event.
partial class PropertyValueConverter : ILogEventPropertyFactory, ILogEventPropertyValueFactory
{
    static readonly HashSet<Type> BuiltInScalarTypes = new()
    {
        typeof(decimal),
        typeof(string),
        typeof(DateTime), typeof(DateTimeOffset), typeof(TimeSpan),
        typeof(Guid), typeof(Uri),
#if FEATURE_DATE_AND_TIME_ONLY
            typeof(TimeOnly), typeof(DateOnly)
#endif
    };

    readonly IDestructuringPolicy[] _destructuringPolicies;
    readonly Type[] _dictionaryTypes;
    readonly IScalarConversionPolicy[] _scalarConversionPolicies;
    readonly DepthLimiter _depthLimiter;
    readonly int _maximumStringLength;
    readonly int _maximumCollectionCount;
    readonly bool _propagateExceptions;

    public PropertyValueConverter(
        int maximumDestructuringDepth,
        int maximumStringLength,
        int maximumCollectionCount,
        IEnumerable<Type> additionalScalarTypes,
        IEnumerable<Type> additionalDictionaryTypes,
        IEnumerable<IDestructuringPolicy> additionalDestructuringPolicies,
        bool propagateExceptions)
    {
        Guard.AgainstNull(additionalScalarTypes);
        Guard.AgainstNull(additionalDestructuringPolicies);
        if (maximumDestructuringDepth < 0) throw new ArgumentOutOfRangeException(nameof(maximumDestructuringDepth));
        if (maximumStringLength < 2) throw new ArgumentOutOfRangeException(nameof(maximumStringLength));
        if (maximumCollectionCount < 1) throw new ArgumentOutOfRangeException(nameof(maximumCollectionCount));

        _propagateExceptions = propagateExceptions;
        _maximumStringLength = maximumStringLength;
        _maximumCollectionCount = maximumCollectionCount;

        _scalarConversionPolicies = new IScalarConversionPolicy[]
        {
            new PrimitiveScalarConversionPolicy(),
            new SimpleScalarConversionPolicy(BuiltInScalarTypes.Concat(additionalScalarTypes)),
            new EnumScalarConversionPolicy(),
            new ByteArrayScalarConversionPolicy(),
#if FEATURE_SPAN
            new ByteMemoryScalarConversionPolicy(),
#endif
        };

        _destructuringPolicies = additionalDestructuringPolicies
            .Concat(new IDestructuringPolicy[]
            {
                new DelegateDestructuringPolicy(),
                new ReflectionTypesScalarDestructuringPolicy()
            })
            .ToArray();

        _dictionaryTypes = additionalDictionaryTypes.ToArray();
        _depthLimiter = new(maximumDestructuringDepth, this);
    }

    public LogEventProperty CreateProperty(string name, object? value, bool destructureObjects = false)
    {
        return new(name, CreatePropertyValue(value, destructureObjects));
    }

    public LogEventPropertyValue CreatePropertyValue(object? value, bool destructureObjects = false)
    {
        return CreatePropertyValue(value, destructureObjects, 1);
    }

    public LogEventPropertyValue CreatePropertyValue(object? value, Destructuring destructuring)
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

    LogEventPropertyValue CreatePropertyValue(object? value, bool destructureObjects, int depth)
    {
        return CreatePropertyValue(
            value,
            destructureObjects ?
                Destructuring.Destructure :
                Destructuring.Default,
            depth);
    }

    LogEventPropertyValue CreatePropertyValue(object? value, Destructuring destructuring, int depth)
    {
        if (value == null)
            return ScalarValue.Null;

        if (destructuring == Destructuring.Stringify)
        {
            return Stringify(value);
        }

        if (destructuring == Destructuring.Destructure)
        {
            if (value is string stringValue)
            {
                value = TruncateIfNecessary(stringValue);
            }
        }

        if (value is string)
            return new ScalarValue(value);

        foreach (var scalarConversionPolicy in _scalarConversionPolicies)
        {
            if (scalarConversionPolicy.TryConvertToScalar(value, out var converted))
                return converted;
        }

        DepthLimiter.SetCurrentDepth(depth);

        if (destructuring == Destructuring.Destructure)
        {
            foreach (var destructuringPolicy in _destructuringPolicies)
            {
                if (destructuringPolicy.TryDestructure(value, _depthLimiter, out var result))
                    return result;
            }
        }

        var type = value.GetType();
        if (TryConvertEnumerable(value, type, destructuring, out var enumerableResult))
            return enumerableResult;

        if (TryConvertValueTuple(value, type, destructuring, out var tupleResult))
            return tupleResult;

        if (TryConvertCompilerGeneratedType(value, type, destructuring, out var compilerGeneratedResult))
            return compilerGeneratedResult;

        return new ScalarValue(value.ToString() ?? "");
    }

    bool TryConvertEnumerable(object value, Type type, Destructuring destructuring, [NotNullWhen(true)] out LogEventPropertyValue? result)
    {
        if (value is IEnumerable enumerable)
        {
            // Only dictionaries with 'scalar' keys are permitted, as
            // more complex keys may not serialize to unique values for
            // representation in sinks. This check strengthens the expectation
            // that resulting dictionary is representable in JSON as well
            // as richer formats (e.g. XML, .NET type-aware...).
            // Only actual dictionaries are supported, as arbitrary types
            // can implement multiple IDictionary interfaces and thus introduce
            // multiple different interpretations.
            if (TryGetDictionary(value, type, out var dictionary))
            {
                result = new DictionaryValue(MapToDictionaryElements(dictionary, destructuring));
                return true;

                IEnumerable<KeyValuePair<ScalarValue, LogEventPropertyValue>> MapToDictionaryElements(IDictionary dictionaryEntries, Destructuring destructure)
                {
                    var count = 0;
                    foreach (DictionaryEntry entry in dictionaryEntries)
                    {
                        if (++count > _maximumCollectionCount)
                        {
                            yield break;
                        }

                        var pair = new KeyValuePair<ScalarValue, LogEventPropertyValue>(
                            (ScalarValue)_depthLimiter.CreatePropertyValue(entry.Key, destructure),
                            _depthLimiter.CreatePropertyValue(entry.Value, destructure));

                        if (pair.Key.Value != null)
                            yield return pair;
                    }
                }
            }

            if (value is Array array && array.Rank > 1)
            {
                result = BuildArrayValue(array, new int[array.Rank], 0, destructuring);
                return true;
            }

            // Avoids allocation of two iterators - one from List and another one from MapToSequenceElements.
            // Allocation free for empty sequence.
            if (enumerable is IList list && list.Count <= _maximumCollectionCount)
            {
                if (list.Count == 0)
                {
                    result = SequenceValue.Empty;
                }
                else
                {
                    var valueArray = new LogEventPropertyValue[list.Count];
                    for (int i = 0; i < list.Count; ++i)
                        valueArray[i] = _depthLimiter.CreatePropertyValue(list[i], destructuring);
                    result = new SequenceValue(valueArray);
                }
            }
            else
            {
                result = new SequenceValue(MapToSequenceElements(enumerable, destructuring));
            }
            return true;

            IEnumerable<LogEventPropertyValue> MapToSequenceElements(IEnumerable sequence, Destructuring destructure)
            {
                var count = 0;
                foreach (var element in sequence)
                {
                    if (++count > _maximumCollectionCount)
                    {
                        yield break;
                    }

                    yield return _depthLimiter.CreatePropertyValue(element, destructure);
                }
            }
        }

        result = null;
        return false;
    }

    /// <summary>
    /// Recursively traverses a multidimensional array and constructs a nested SequenceValue representation.
    /// </summary>
    /// <param name="array">The multidimensional array to traverse.</param>
    /// <param name="indices">An array of indices representing the current position in each dimension.</param>
    /// <param name="dimension">The current dimension being processed.</param>
    /// <param name="destructuring">The destructuring strategy.</param>
    /// <returns>A LogEventPropertyValue representing the array's structure and elements.</returns>
    LogEventPropertyValue BuildArrayValue(Array array, int[] indices, int dimension, Destructuring destructuring)
    {
        if (dimension == array.Rank)
        {
            // Base case: get the value at the current indices
            object? value = array.GetValue(indices);
            return _depthLimiter.CreatePropertyValue(value, destructuring);
        }

        int length = array.GetLength(dimension);
        if (length == 0)
        {
            return SequenceValue.Empty;
        }

        var elements = new List<LogEventPropertyValue>(length);
        for (int i = 0; i < length; i++)
        {
            indices[dimension] = i;
            elements.Add(BuildArrayValue(array, indices, dimension + 1, destructuring));

            if (elements.Count >= _maximumCollectionCount)
            {
                break;
            }
        }
        return new SequenceValue(elements);
    }

#if FEATURE_ITUPLE

    bool TryConvertValueTuple(object value, Type type, Destructuring destructuring, [NotNullWhen(true)] out LogEventPropertyValue? result)
    {
        if (value is not ITuple tuple)
        {
            result = null;
            return false;
        }

        var elements = new LogEventPropertyValue[tuple.Length];
        for (var i = 0; i < tuple.Length; i++)
        {
            var fieldValue = tuple[i];
            elements[i] = _depthLimiter.CreatePropertyValue(fieldValue, destructuring);
        }

        result = new SequenceValue(elements);
        return true;
    }

#else

    bool TryConvertValueTuple(object value, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] Type type, Destructuring destructuring, [NotNullWhen(true)] out LogEventPropertyValue? result)
    {
        if (!(value is IStructuralEquatable && type.IsConstructedGenericType))
        {
            result = null;
            return false;
        }

        var definition = type.GetGenericTypeDefinition();

        // Ignore the 8+ value case for now.
        if (definition == typeof(ValueTuple<>) || definition == typeof(ValueTuple<,>) ||
            definition == typeof(ValueTuple<,,>) || definition == typeof(ValueTuple<,,,>) ||
            definition == typeof(ValueTuple<,,,,>) || definition == typeof(ValueTuple<,,,,,>) ||
            definition == typeof(ValueTuple<,,,,,,>))
        {
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            var elements = new LogEventPropertyValue[fields.Length];
            for (var index = 0; index < fields.Length; index++)
            {
                var field = fields[index];
                var fieldValue = field.GetValue(value);
                var propertyValue = _depthLimiter.CreatePropertyValue(fieldValue, destructuring);
                elements[index] = propertyValue;
            }

            result = new SequenceValue(elements);
            return true;
        }

        result = null;
        return false;
    }

#endif

    bool TryConvertCompilerGeneratedType(
        object value,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] Type type,
        Destructuring destructuring,
        [NotNullWhen(true)] out LogEventPropertyValue? result)
    {
        if (destructuring == Destructuring.Destructure)
        {
            var typeTag = type.Name;
            if (typeTag.Length <= 0 || IsCompilerGeneratedType(type))
            {
                typeTag = null;
            }

            result = new StructureValue(GetProperties(value, type), typeTag);
            return true;
        }

        result = null;
        return false;
    }

    LogEventPropertyValue Stringify(object value)
    {
        var stringified = value.ToString();
        var truncated = stringified == null ? "" : TruncateIfNecessary(stringified);
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

    bool TryGetDictionary(object value, Type valueType, [NotNullWhen(true)] out IDictionary? dictionary)
    {
        if (value is IDictionary idictionary)
        {
            if (_dictionaryTypes.Contains(valueType))
            {
                dictionary = idictionary;
                return true;
            }

            if (valueType.IsConstructedGenericType)
            {
                var definition = valueType.GetGenericTypeDefinition();
                if ((definition == typeof(Dictionary<,>) || definition == typeof(System.Collections.ObjectModel.ReadOnlyDictionary<,>)) &&
                    IsValidDictionaryKeyType(valueType.GenericTypeArguments[0]))
                {
                    dictionary = idictionary;
                    return true;
                }
            }
        }

        dictionary = null;
        return false;
    }

    static bool IsValidDictionaryKeyType(Type valueType)
    {
        return valueType.IsPrimitive ||
               BuiltInScalarTypes.Contains(valueType) ||
               valueType.IsEnum;
    }

    IEnumerable<LogEventProperty> GetProperties(object value, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] Type type)
    {
        foreach (var prop in type.GetPropertiesRecursive())
        {
            object? propValue;
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

                propValue = "The property accessor threw an exception: " + ex.InnerException?.GetType().Name;
            }
            catch (NotSupportedException)
            {
                SelfLog.WriteLine("The property accessor {0} is not supported via Reflection API", prop);

                if (_propagateExceptions)
                    throw;

                propValue = "Accessing this property is not supported via Reflection API";
            }
            yield return new(prop.Name, _depthLimiter.CreatePropertyValue(propValue, Destructuring.Destructure));
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static bool IsCompilerGeneratedType(Type type)
    {
        if (!type.IsGenericType || !type.IsSealed || type.Namespace != null)
        {
            return false;
        }

        // C# Anonymous types always start with "<>" and VB's start with "VB$"
        var name = type.Name;
        return name[0] == '<'
               || (name.Length > 2 && name[0] == 'V' && name[1] == 'B' && name[2] == '$');
    }
}
