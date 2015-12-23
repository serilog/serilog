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
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Filters
{
    /// <summary>
    /// Predicates applied to log events that can be used
    /// </summary>
    public static class Matching
    {
        /// <summary>
        /// Matches events from the specified source type.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <returns>A predicate for matching events.</returns>
        public static Func<LogEvent, bool> FromSource<TSource>()
        {
            return WithProperty(Constants.SourceContextPropertyName, typeof(TSource).FullName);
        }

        /// <summary>
        /// Matches events from the specified source type or namespace and
        /// nested types or namespaces.
        /// </summary>
        /// <param name="source">A dotted source type or namespace identifier.</param>
        /// <returns>A function that matches log events emitted by the source.</returns>
        public static Func<LogEvent, bool> FromSource(string source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var sourcePrefix = source + ".";
            return WithProperty<string>(Constants.SourceContextPropertyName, s => s != null && (s == source || s.StartsWith(sourcePrefix)));
        }

        /// <summary>
        /// Matches events with the specified property attached,
        /// regardless of its value.
        /// </summary>
        /// <param name="propertyName">The name of the property to match.</param>
        /// <returns>A predicate for matching events.</returns>
        public static Func<LogEvent, bool> WithProperty(string propertyName)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            return e => e.Properties.ContainsKey(propertyName);
        }

        /// <summary>
        /// Matches events with the specified property value.
        /// </summary>
        /// <param name="propertyName">The name of the property to match.</param>
        /// <param name="scalarValue">The property value to match; must be a scalar type.
        /// Null is allowed.</param>
        /// <returns>A predicate for matching events.</returns>
        public static Func<LogEvent, bool> WithProperty(string propertyName, object scalarValue)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            var scalar = new ScalarValue(scalarValue);
            return e =>
            {
                LogEventPropertyValue propertyValue;
                return e.Properties.TryGetValue(propertyName, out propertyValue) &&
                    scalar.Equals(propertyValue);
            };
        }

        /// <summary>
        /// Matches events with the specified property value.
        /// </summary>
        /// <param name="propertyName">The name of the property to match.</param>
        /// <param name="predicate">A predicate for testing </param>
        /// <typeparam name="TScalar">The type of scalar values to match.</typeparam>
        /// <returns>A predicate for matching events.</returns>
        public static Func<LogEvent, bool> WithProperty<TScalar>(string propertyName, Func<TScalar, bool> predicate)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return e =>
            {
                LogEventPropertyValue propertyValue;
                if (!e.Properties.TryGetValue(propertyName, out propertyValue)) return false;

                var s = propertyValue as ScalarValue;
                if (s == null) return false;

                return (s.Value is TScalar) &&
                       predicate((TScalar)s.Value);
            };
        }
    }
}
