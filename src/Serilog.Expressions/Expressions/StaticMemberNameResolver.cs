// Copyright © Serilog Contributors
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

namespace Serilog.Expressions;

/// <summary>
/// A <see cref="NameResolver"/> that matches public static members of a class by name.
/// </summary>
public class StaticMemberNameResolver : NameResolver
{
    readonly IReadOnlyDictionary<string, MethodInfo> _methods;

    /// <summary>
    /// Create a <see cref="StaticMemberNameResolver"/> that returns members of the specified <see cref="Type"/>.
    /// </summary>
    /// <param name="type">A <see cref="Type"/> with public static members implementing runtime functions.</param>
    public StaticMemberNameResolver(
#if NET6_0_OR_GREATER
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
#endif
        Type type)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));

        _methods = type
            .GetTypeInfo()
            .GetMethods(BindingFlags.Static | BindingFlags.Public)
            .ToDictionary(m => m.Name, StringComparer.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public override bool TryResolveFunctionName(string name, [MaybeNullWhen(false)] out MethodInfo implementation)
    {
        return _methods.TryGetValue(name, out implementation);
    }
}