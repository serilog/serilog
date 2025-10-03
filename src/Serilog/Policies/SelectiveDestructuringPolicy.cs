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

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Serilog.Policies;

/// <summary>
/// A destructuring policy that only includes specified properties when destructuring objects of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of objects this policy applies to.</typeparam>
public class SelectiveDestructuringPolicy<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T> : IDestructuringPolicy
{
  readonly HashSet<string> _selectedProperties;
  [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
  readonly Type _targetType;

  /// <summary>
  /// Creates a new selective destructuring policy.
  /// </summary>
  /// <param name="propertyNames">The names of the properties to include when destructuring.</param>
  public SelectiveDestructuringPolicy(params string[] propertyNames)
  {
    _targetType = typeof(T);
    _selectedProperties = new HashSet<string>(propertyNames ?? Array.Empty<string>(), StringComparer.Ordinal);
  }

  /// <summary>
  /// Try to destructure the provided object using selective property inclusion.
  /// </summary>
  /// <param name="value">The value to destructure.</param>
  /// <param name="propertyValueFactory">Factory to use for creating property values.</param>
  /// <param name="result">The destructured value if successful.</param>
  /// <returns>True if the value could be destructured by this policy, otherwise false.</returns>
  public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, [NotNullWhen(true)] out LogEventPropertyValue? result)
  {
    Guard.AgainstNull(value);

    if (value.GetType() != _targetType)
    {
      result = null;
      return false;
    }

    var properties = new List<LogEventProperty>();
    var typeInfo = _targetType.GetTypeInfo();

    foreach (var propertyName in _selectedProperties)
    {
      var property = typeInfo.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
      if (property != null && property.CanRead)
      {
        try
        {
          var propertyValue = property.GetValue(value);
          var logEventPropertyValue = propertyValueFactory.CreatePropertyValue(propertyValue, destructureObjects: true);
          properties.Add(new LogEventProperty(property.Name, logEventPropertyValue));
        }
        catch (TargetInvocationException)
        {
          // Skip properties that throw exceptions when accessed
        }
      }
    }

    result = new StructureValue(properties);
    return true;
  }
}