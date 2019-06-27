using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Capturing
{
    partial class PropertyValueConverter
    {
        readonly struct FastBuildersLookup
        {
            readonly int _length;
            readonly Type[] _keys;
            readonly Delegate[] _builders;
            const int MaxHops = 4;

            public FastBuildersLookup(PropertyValueConverter converter, Type[] scalarTypes)
            {
                var fastScalarTypes = scalarTypes.Where(t => t != typeof(string)).ToArray();
                var fastBuilders = new List<Delegate>
                {
                    converter.BuildString()
                };

                var methods = typeof(PropertyValueConverter).GetRuntimeMethods().ToList();
                var buildScalar = methods.Single(m => m.Name.StartsWith(nameof(BuildScalarValueType)));
                var buildNullableScalar = methods.Single(m => m.Name.StartsWith(nameof(BuildScalarNullableValueType)));
                var buildRef = methods.Single(m => m.Name.StartsWith(nameof(BuildScalarRefType)));

                Delegate InvokeBuilder(MethodInfo method, Type type) => (Delegate)method.MakeGenericMethod(type).Invoke(method.IsStatic ? null : converter, null);

                foreach (var scalarType in fastScalarTypes)
                {
                    var isValueType = scalarType.GetTypeInfo().IsValueType;

                    if (isValueType)
                    {
                        fastBuilders.Add(InvokeBuilder(buildScalar, scalarType));
                        fastBuilders.Add(InvokeBuilder(buildNullableScalar, scalarType));
                    }
                    else
                    {
                        fastBuilders.Add(InvokeBuilder(buildRef, scalarType));
                    }
                }

                var buildersDict = fastBuilders.ToDictionary(b => b.GetType().GetTypeInfo().GenericTypeArguments[0]);

                var size = Build(buildersDict, out var keys, out var values);

                _keys = keys;
                _builders = values;
                _length = size;
            }

            static int Build(Dictionary<Type, Delegate> buildersDict, out Type[] keys, out Delegate[] values)
            {
                var size = buildersDict.Count; // make it double to reduce collisions
                bool success;
                do
                {
                    success = true;
                    size *= 2;

                    keys = new Type[size];
                    values = new Delegate[size];
                    var hops = new int[size];

                    foreach (var kvp in buildersDict)
                    {
                        if (HopscotchInsert(kvp.Key, kvp.Value, keys, values, hops) == false)
                        {
                            success = false;
                            break;
                        }
                    }

                } while (success == false);

                return size;
            }

            static bool HopscotchInsert(Type key, Delegate value, Type[] keys, Delegate[] values, int[] hops)
            {
                var length = keys.Length;
                var index = GetHashCode(key);

                // find distance to the first empty position
                var distance = 0;
                int position;
                for (var i = 0; i < length; i++)
                {
                    position = (index + i) % length;
                    if (keys[position] == null)
                    {
                        distance = i;
                        break;
                    }
                }

                while (distance >= MaxHops)
                {
                    position = (index + distance) % length;
                    var prevPosition = (index + distance - 1) % length;

                    if (hops[prevPosition] < MaxHops - 1)
                    {
                        keys[position] = keys[prevPosition];
                        values[position] = values[prevPosition];
                        hops[position] = hops[prevPosition] + 1;

                        // clear prev
                        keys[prevPosition] = default;
                        values[prevPosition] = default;
                        hops[prevPosition] = default;

                        distance -= 1;
                    }
                    else
                    {
                        return false;
                    }
                }

                // distance within MaxHops, just put it
                position = (index + distance) % length;

                keys[position] = key;
                values[position] = value;
                hops[position] = distance;

                return true;
            }

            static int GetHashCode(Type key)
            {
                var hash = key.GetHashCode();
                return hash ^ (hash >> 16);
            }

            public bool TryFindFastBuilder<T>(out PropertyValueBuilder<T> builder)
            {
                var hash = GetHashCode(typeof(T));

                for (var i = 0; i < MaxHops; i++)
                {
                    var position = (hash + i) % _length;

                    if (_keys[position] == typeof(T))
                    {
                        builder = (PropertyValueBuilder<T>)_builders[position];
                        return true;
                    }
                }

                builder = null;
                return false;
            }
        }

        delegate LogEventPropertyValue PropertyValueBuilder<TValue>(TValue value, Destructuring destructuring, int depth);

        PropertyValueBuilder<string> BuildString()
        {
            return (value, destructuring, depth) =>
            {
                return value == null ? new ScalarValue(null) : new ScalarValue(TruncateIfNecessary(value));
            };
        }

        static PropertyValueBuilder<TValue> BuildScalarValueType<TValue>()
            where TValue : struct
        {
            return (value, destructuring, depth) =>
            {
                return new ScalarValue<TValue>(value);
            };
        }

        static PropertyValueBuilder<TValue?> BuildScalarNullableValueType<TValue>()
            where TValue : struct
        {
            return (value, destructuring, depth) =>
            {
                if (value == null)
                {
                    return new ScalarValue(null);
                }

                return new ScalarValue<TValue>(value.Value);
            };
        }

        static PropertyValueBuilder<TValue> BuildScalarRefType<TValue>()
            where TValue : class
        {
            return (value, destructuring, depth) =>
            {
                return value == null ? new ScalarValue(null) : new ScalarValue(value);
            };
        }

    }
}
