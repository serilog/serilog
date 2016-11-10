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
using Serilog.Core;
using Serilog.Policies;

namespace Serilog.Configuration
{
    /// <summary>
    /// Controls template parameter destructuring configuration.
    /// </summary>
    public class LoggerDestructuringConfiguration
    {
        readonly LoggerConfiguration _loggerConfiguration;
        readonly Action<Type> _addScalar;
        readonly Action<IDestructuringPolicy> _addPolicy;
        readonly Action<int> _setMaximumDepth;
        readonly Action<int> _setMaximumStringLength;
        readonly Action<int> _setMaximumCollectionCount;

        internal LoggerDestructuringConfiguration(
            LoggerConfiguration loggerConfiguration,
            Action<Type> addScalar,
            Action<IDestructuringPolicy> addPolicy,
            Action<int> setMaximumDepth,
            Action<int> setMaximumStringLength,
            Action<int> setMaximumCollectionCount)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException(nameof(loggerConfiguration));
            if (addScalar == null) throw new ArgumentNullException(nameof(addScalar));
            if (addPolicy == null) throw new ArgumentNullException(nameof(addPolicy));
            if (setMaximumDepth == null) throw new ArgumentNullException(nameof(setMaximumDepth));
            if (setMaximumStringLength == null) throw new ArgumentNullException(nameof(setMaximumStringLength));
            if (setMaximumCollectionCount == null) throw new ArgumentNullException(nameof(setMaximumCollectionCount));
            _loggerConfiguration = loggerConfiguration;
            _addScalar = addScalar;
            _addPolicy = addPolicy;
            _setMaximumDepth = setMaximumDepth;
            _setMaximumStringLength = setMaximumStringLength;
            _setMaximumCollectionCount = setMaximumCollectionCount;
        }

        /// <summary>
        /// Treat objects of the specified type as scalar values, i.e., don't break
        /// them down into properties event when destructuring complex types.
        /// </summary>
        /// <param name="scalarType">Type to treat as scalar.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration AsScalar(Type scalarType)
        {
            if (scalarType == null) throw new ArgumentNullException(nameof(scalarType));
            _addScalar(scalarType);
            return _loggerConfiguration;
        }

        /// <summary>
        /// Treat objects of the specified type as scalar values, i.e., don't break
        /// them down into properties event when destructuring complex types.
        /// </summary>
        /// <typeparam name="TScalar">Type to treat as scalar.</typeparam>
        /// <returns>Configuration object allowing method chaining.</returns>
        public LoggerConfiguration AsScalar<TScalar>()
        {
            return AsScalar(typeof(TScalar));
        }

        /// <summary>
        /// When destructuring objects, transform instances with the provided policies.
        /// </summary>
        /// <param name="destructuringPolicies">Policies to apply when destructuring.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        // ReSharper disable once MemberCanBePrivate.Global
        public LoggerConfiguration With(params IDestructuringPolicy[] destructuringPolicies)
        {
            if (destructuringPolicies == null) throw new ArgumentNullException(nameof(destructuringPolicies));
            foreach (var destructuringPolicy in destructuringPolicies)
            {
                if (destructuringPolicy == null)
                    throw new ArgumentException("Null policy is not allowed.");
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
        /// When destructuring objects, transform instances of the specified type with
        /// the provided function.
        /// </summary>
        /// <param name="transformation">Function mapping instances of <typeparamref name="TValue"/>
        /// to an alternative representation.</param>
        /// <typeparam name="TValue">Type of values to transform.</typeparam>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public LoggerConfiguration ByTransforming<TValue>(Func<TValue, object> transformation)
        {
            if (transformation == null) throw new ArgumentNullException(nameof(transformation));
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
        /// <exception cref="ArgumentNullException"></exception>
        public LoggerConfiguration ByTransformingWhere<TValue>(
            Func<Type, bool> predicate,
            Func<TValue, object> transformation)
        {
            if (transformation == null) throw new ArgumentNullException(nameof(transformation));
            var policy = new ProjectedDestructuringPolicy(predicate,
                                                          o => transformation((TValue)o));
            return With(policy);
        }

        /// <summary>
        /// When destructuring objects, depth will be limited to 5 property traversals deep to
        /// guard against ballooning space when recursive/cyclic structures are accidentally passed. To
        /// increase this limit pass a higher value.
        /// </summary>
        /// <param name="maximumDestructuringDepth">The maximum depth to use.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public LoggerConfiguration ToMaximumDepth(int maximumDestructuringDepth)
        {
            if (maximumDestructuringDepth < 0) throw new ArgumentOutOfRangeException(nameof(maximumDestructuringDepth));
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
        /// <exception cref="ArgumentOutOfRangeException">When passed length is less than 2</exception>
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
        /// <exception cref="ArgumentOutOfRangeException">When passed length is less than 1</exception>
        public LoggerConfiguration ToMaximumCollectionCount(int maximumCollectionCount)
        {
            if (maximumCollectionCount < 1) throw new ArgumentOutOfRangeException(nameof(maximumCollectionCount), maximumCollectionCount, "Maximum collection length must be at least one.");
            _setMaximumCollectionCount(maximumCollectionCount);
            return _loggerConfiguration;
        }
    }
}

