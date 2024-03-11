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

namespace Serilog.Configuration;

/// <summary>
/// Controls template parameter destructuring configuration.
/// </summary>
public class LoggerDestructuringConfiguration
{
    readonly LoggerConfiguration _loggerConfiguration;
    readonly Action<Type> _addScalar;
    readonly Action<Type> _addDictionaryType;
    readonly Action<IDestructuringPolicy> _addPolicy;
    readonly Action<int> _setMaximumDepth;
    readonly Action<int> _setMaximumStringLength;
    readonly Action<int> _setMaximumCollectionCount;
    readonly Action<Type, DestructuringFallback> _addFallbackDestructuring;

    internal LoggerDestructuringConfiguration(
        LoggerConfiguration loggerConfiguration,
        Action<Type> addScalar,
        Action<Type> addDictionaryType,
        Action<IDestructuringPolicy> addPolicy,
        Action<int> setMaximumDepth,
        Action<int> setMaximumStringLength,
        Action<int> setMaximumCollectionCount,
        Action<Type, DestructuringFallback> addFallbackDestructuring)
    {
        _loggerConfiguration = Guard.AgainstNull(loggerConfiguration);
        _addScalar = Guard.AgainstNull(addScalar);
        _addDictionaryType = Guard.AgainstNull(addDictionaryType);
        _addPolicy = Guard.AgainstNull(addPolicy);
        _setMaximumDepth = Guard.AgainstNull(setMaximumDepth);
        _setMaximumStringLength = Guard.AgainstNull(setMaximumStringLength);
        _setMaximumCollectionCount = Guard.AgainstNull(setMaximumCollectionCount);
        _addFallbackDestructuring = Guard.AgainstNull(addFallbackDestructuring);
    }

    /// <summary>
    /// Treat objects of the specified type as scalar values, i.e., don't break
    /// them down into properties even when destructuring complex types.
    /// </summary>
    /// <param name="scalarType">Type to treat as scalar.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="scalarType"/> is <code>null</code></exception>
    public LoggerConfiguration AsScalar(Type scalarType)
    {
        Guard.AgainstNull(scalarType);

        _addScalar(scalarType);
        return _loggerConfiguration;
    }

    /// <summary>
    /// Treat objects of the specified type as scalar values, i.e., don't break
    /// them down into properties even when destructuring complex types.
    /// </summary>
    /// <typeparam name="TScalar">Type to treat as scalar.</typeparam>
    /// <returns>Configuration object allowing method chaining.</returns>
    public LoggerConfiguration AsScalar<TScalar>() => AsScalar(typeof(TScalar));

    /// <summary>
    /// When destructuring objects, transform instances with the provided policies.
    /// </summary>
    /// <param name="destructuringPolicies">Policies to apply when destructuring.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="destructuringPolicies"/> is <code>null</code></exception>
    /// <exception cref="ArgumentException">When any element of <paramref name="destructuringPolicies"/> is <code>null</code></exception>
    // ReSharper disable once MemberCanBePrivate.Global
    public LoggerConfiguration With(params IDestructuringPolicy[] destructuringPolicies)
    {
        Guard.AgainstNull(destructuringPolicies);

        foreach (var destructuringPolicy in destructuringPolicies)
        {
            if (destructuringPolicy == null) throw new ArgumentException("Null policy is not allowed.");

            _addPolicy(destructuringPolicy);
        }
        return _loggerConfiguration;
    }

    /// <summary>
    /// When destructuring objects, transform instances with the provided policy.
    /// </summary>
    /// <typeparam name="TDestructuringPolicy">Policy to apply when destructuring.</typeparam>
    /// <returns>Configuration object allowing method chaining.</returns>
    public LoggerConfiguration With<TDestructuringPolicy>()
        where TDestructuringPolicy : IDestructuringPolicy, new()
    {
        return With(new TDestructuringPolicy());
    }

    /// <summary>
    /// Capture instances of the specified type as dictionaries.
    /// By default, only concrete instantiations of are considered dictionary-like.
    /// </summary>
    /// <typeparam name="T">Type of dictionary.</typeparam>
    /// <returns>Configuration object allowing method chaining.</returns>
    public LoggerConfiguration AsDictionary<T>()
        where T : IDictionary
    {
        _addDictionaryType(typeof(T));
        return _loggerConfiguration;
    }

    /// <summary>
    /// When destructuring objects, transform instances of the specified type with
    /// the provided function.
    /// </summary>
    /// <param name="transformation">Function mapping instances of <typeparamref name="TValue"/>
    /// to an alternative representation.</param>
    /// <typeparam name="TValue">Type of values to transform.</typeparam>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="transformation"/> is <code>null</code></exception>
    public LoggerConfiguration ByTransforming<TValue>(Func<TValue, object> transformation)
    {
        Guard.AgainstNull(transformation);

        var policy = new ProjectedDestructuringPolicy(t => t == typeof(TValue),
            o => transformation((TValue)o));
        return With(policy);
    }

    /// <summary>
    /// When destructuring objects, transform instances of the specified type with
    /// the provided function, if the predicate returns true. Be careful to avoid any
    /// intensive work in the predicate, as it can slow down the pipeline significantly.
    /// </summary>
    /// <param name="predicate">A predicate used to determine if the transform applies to
    /// a specific type of value</param>
    /// <param name="transformation">Function mapping instances of <typeparamref name="TValue"/>
    /// to an alternative representation.</param>
    /// <typeparam name="TValue">Type of values to transform.</typeparam>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="predicate"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="transformation"/> is <code>null</code></exception>
    public LoggerConfiguration ByTransformingWhere<TValue>(
        Func<Type, bool> predicate,
        Func<TValue, object> transformation)
    {
        Guard.AgainstNull(predicate);
        Guard.AgainstNull(transformation);

        var policy = new ProjectedDestructuringPolicy(predicate,
            o => transformation((TValue)o));
        return With(policy);
    }

    /// <summary>
    /// When destructuring objects, depth will be limited to 10 property traversals deep to
    /// guard against ballooning space when recursive/cyclic structures are accidentally passed. To
    /// change this limit pass a new maximum depth.
    /// </summary>
    /// <param name="maximumDestructuringDepth">The maximum depth to use.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="ArgumentOutOfRangeException">When <paramref name="maximumDestructuringDepth"/> is negative</exception>
    public LoggerConfiguration ToMaximumDepth(int maximumDestructuringDepth)
    {
        if (maximumDestructuringDepth < 0) throw new ArgumentOutOfRangeException(nameof(maximumDestructuringDepth), "Maximum destructuring depth must be positive.");

        _setMaximumDepth(maximumDestructuringDepth);
        return _loggerConfiguration;
    }

    /// <summary>
    /// When destructuring objects, string values can be restricted to specified length
    /// thus avoiding bloating payload. Limit is applied to each value separately,
    /// sum of length of strings can exceed limit.
    /// </summary>
    /// <param name="maximumStringLength">The maximum string length.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="ArgumentOutOfRangeException">When <paramref name="maximumStringLength"/> is less than 2</exception>
    public LoggerConfiguration ToMaximumStringLength(int maximumStringLength)
    {
        if (maximumStringLength < 2) throw new ArgumentOutOfRangeException(nameof(maximumStringLength), maximumStringLength, "Maximum string length must be at least two.");

        _setMaximumStringLength(maximumStringLength);
        return _loggerConfiguration;
    }

    /// <summary>
    /// When destructuring objects, collections be restricted to specified count
    /// thus avoiding bloating payload. Limit is applied to each collection separately,
    /// sum of length of collection can exceed limit.
    /// Applies limit to all <see cref="IEnumerable"/> including dictionaries.
    /// </summary>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="ArgumentOutOfRangeException">When <paramref name="maximumCollectionCount"/> is less than 1</exception>
    public LoggerConfiguration ToMaximumCollectionCount(int maximumCollectionCount)
    {
        if (maximumCollectionCount < 1) throw new ArgumentOutOfRangeException(nameof(maximumCollectionCount), maximumCollectionCount, "Maximum collection length must be at least one.");

        _setMaximumCollectionCount(maximumCollectionCount);
        return _loggerConfiguration;
    }

    /// <summary>
    /// If no explicit destructuring hint was given for the property, use the given
    /// destructuring as fallback.
    /// </summary>
    /// <param name="destructuringFallback">The fallback destructuring.</param>
    /// <param name="type">Type to define the fallback for.</typeparam>
    /// <returns>Configuration object allowing method chaining.</returns>
    public LoggerConfiguration WhenNoOperator(Type type, DestructuringFallback destructuringFallback)
    {
        _addFallbackDestructuring(type, destructuringFallback);
        return _loggerConfiguration;
    }

    /// <summary>
    /// If no explicit destructuring hint was given for the property, use the given
    /// destructuring as fallback.
    /// </summary>
    /// <param name="destructuringFallback">The fallback destructuring.</param>
    /// <typeparam name="T">Type to define the fallback for.</typeparam>
    /// <returns>Configuration object allowing method chaining.</returns>
    public LoggerConfiguration WhenNoOperator<T>(DestructuringFallback destructuringFallback) => WhenNoOperator(typeof(T), destructuringFallback);
}
