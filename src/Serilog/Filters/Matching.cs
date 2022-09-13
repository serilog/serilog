// Copyright 2013-2020 Serilog Contributors
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

namespace Serilog.Filters;

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
        return WithProperty(Constants.SourceContextPropertyName, typeof(TSource).FullName!);
    }

    /// <summary>
    /// Matches events from the specified source type or namespace and
    /// nested types or namespaces.
    /// </summary>
    /// <param name="source">A dotted source type or namespace identifier.</param>
    /// <returns>A function that matches log events emitted by the source.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="source"/> is <code>null</code></exception>
    public static Func<LogEvent, bool> FromSource(string source)
    {
        Guard.AgainstNull(source);

        return WithProperty<string>(
            Constants.SourceContextPropertyName,
            s => s != null && s
#if FEATURE_SPAN
                .AsSpan()
#endif
                .StartsWith(source) && (s.Length == source.Length || s[source.Length] == '.'));
    }

    /// <summary>
    /// Matches events with the specified property attached,
    /// regardless of its value.
    /// </summary>
    /// <param name="propertyName">The name of the property to match.</param>
    /// <returns>A predicate for matching events.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="propertyName"/> is <code>null</code></exception>
    public static Func<LogEvent, bool> WithProperty(string propertyName)
    {
        Guard.AgainstNull(propertyName);

        return e => e.Properties.ContainsKey(propertyName);
    }

    /// <summary>
    /// Matches events with the specified property value.
    /// </summary>
    /// <param name="propertyName">The name of the property to match.</param>
    /// <param name="scalarValue">The property value to match; must be a scalar type.
    /// Null is allowed.</param>
    /// <returns>A predicate for matching events.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="propertyName"/> is <code>null</code></exception>
    public static Func<LogEvent, bool> WithProperty(string propertyName, object scalarValue)
    {
        Guard.AgainstNull(propertyName);

        var scalar = new ScalarValue(scalarValue);
        return e => e.Properties.TryGetValue(propertyName, out var propertyValue) &&
                    scalar.Equals(propertyValue);
    }

    /// <summary>
    /// Matches events with the specified property value.
    /// </summary>
    /// <param name="propertyName">The name of the property to match.</param>
    /// <param name="predicate">A predicate for testing </param>
    /// <typeparam name="TScalar">The type of scalar values to match.</typeparam>
    /// <returns>A predicate for matching events.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="propertyName"/> is <code>null</code></exception>
    /// <exception cref="ArgumentNullException">When <paramref name="predicate"/> is <code>null</code></exception>
    public static Func<LogEvent, bool> WithProperty<TScalar>(string propertyName, Func<TScalar, bool> predicate)
    {
        Guard.AgainstNull(propertyName);
        Guard.AgainstNull(predicate);

        return e =>
        {
            if (!e.Properties.TryGetValue(propertyName, out var propertyValue)) return false;

            return propertyValue is ScalarValue {Value: TScalar value} &&
                   predicate(value);
        };
    }
}
