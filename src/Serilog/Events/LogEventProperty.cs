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

namespace Serilog.Events;

/// <summary>
/// A property associated with a <see cref="LogEvent"/>.
/// </summary>
public class LogEventProperty
{
    /// <summary>
    /// Construct a <see cref="LogEventProperty"/> with the specified name and value.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="value">The value of the property.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="name"/> is <c>null</c></exception>
    /// <exception cref="ArgumentException">When <paramref name="name"/> is empty or only contains whitespace</exception>
    /// <exception cref="ArgumentNullException">When <paramref name="value"/> is <c>null</c></exception>
    public LogEventProperty(string name, LogEventPropertyValue value)
    {
        Guard.AgainstNull(value);
        EnsureValidName(name);

        Name = name;
        Value = value;
    }

    /// <summary>
    /// Construct a <see cref="LogEventProperty"/> from an existing <see cref="EventProperty"/> instance.
    /// </summary>
    /// <param name="property">The existing property.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="property"/> is <c>default</c></exception>
    internal LogEventProperty(EventProperty property)
    {
        if (property.Equals(EventProperty.None)) throw new ArgumentNullException(nameof(property));

        Name = property.Name;
        Value = property.Value;
    }

    /// <summary>
    /// The name of the property.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The value of the property.
    /// </summary>
    public LogEventPropertyValue Value { get; }

    /// <summary>
    /// Test <paramref name="name" /> to determine if it is a valid property name.
    /// </summary>
    /// <param name="name">The name to check.</param>
    /// <returns><see langword="true"/> if the name is valid; otherwise, <see langword="false"/>.</returns>
    public static bool IsValidName([NotNullWhen(true)] string? name) => !string.IsNullOrWhiteSpace(name);

    /// <summary>Ensures that provided name is valid.</summary>
    /// <exception cref="ArgumentNullException">When <paramref name="name"/> is <c>null</c></exception>
    /// <exception cref="ArgumentException">When <paramref name="name"/> is empty or only contains whitespace</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void EnsureValidName(string name)
    {
        Guard.AgainstNull(name);
        if (!IsValidName(name)) throw new ArgumentException($"Property {nameof(name)} must not be empty or whitespace.", nameof(name));
    }
}
