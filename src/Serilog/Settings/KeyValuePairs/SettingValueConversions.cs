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

namespace Serilog.Settings.KeyValuePairs;

class SettingValueConversions
{
    // should match "The.NameSpace.TypeName::MemberName" optionally followed by
    // usual assembly qualifiers like :
    // ", MyAssembly, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    static Regex StaticMemberAccessorRegex = new("^(?<shortTypeName>[^:]+)::(?<memberName>[A-Za-z][A-Za-z0-9]*)(?<typeNameExtraQualifiers>[^:]*)$");

    static Dictionary<Type, Func<string, object>> ExtendedTypeConversions = new()
    {
        { typeof(Uri), s => new Uri(s) },
        { typeof(TimeSpan), s => TimeSpan.Parse(s) },
        { typeof(Type),
            // Suppress this trimming warning. We'll annotate all the users of this dictionary instead
#pragma warning disable IL2057
            s => Type.GetType(s, throwOnError: true)!
#pragma warning restore IL2057
        },
    };

    [RequiresUnreferencedCode("Finds accessors by name")]
    [RequiresDynamicCode("Creates arrays of unknown element type")]
    public static object? ConvertToType(string value, Type toType)
    {
        if (toType.IsGenericType && toType.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            if (value == string.Empty)
                return null;

            // unwrap Nullable<> type since we're not handling null situations
            toType = toType.GenericTypeArguments[0];
        }

        if (toType.IsEnum)
            return Enum.Parse(toType, value);

        var convertor = ExtendedTypeConversions
            .Where(t => t.Key.IsAssignableFrom(toType))
            .Select(t => t.Value)
            .FirstOrDefault();

        if (convertor != null)
            return convertor(value);

        if ((toType.IsInterface || toType.IsAbstract) && !string.IsNullOrWhiteSpace(value))
        {
            // check if value looks like a static property or field directive
            // like "Namespace.TypeName::StaticProperty, AssemblyName"
            if (TryParseStaticMemberAccessor(value, out var accessorTypeName, out var memberName))
            {
                var accessorType = Type.GetType(accessorTypeName, throwOnError: true)!;
                // is there a public static property with that name ?
                var publicStaticPropertyInfo = accessorType
                    .GetProperties(BindingFlags.Static | BindingFlags.Public)
                    .FirstOrDefault(x => x.Name == memberName &&
                                         x.GetMethod != null);

                if (publicStaticPropertyInfo != null)
                {
                    // static property, no instance to pass
                    return publicStaticPropertyInfo.GetValue(null);
                }

                // no property ? look for a public static field
                var publicStaticFieldInfo = accessorType
                    .GetFields(BindingFlags.Static | BindingFlags.Public)
                    .FirstOrDefault(x => x.Name == memberName);

                if (publicStaticFieldInfo != null)
                {
                    // static field, no instance to pass
                    return publicStaticFieldInfo.GetValue(null);
                }

                throw new InvalidOperationException($"Could not find a public static property or field with name `{memberName}` on type `{accessorTypeName}`");
            }

            // maybe it's the assembly-qualified type name of a concrete implementation
            // with a default constructor
            var type = Type.GetType(value.Trim(), throwOnError: false);
            if (type != null)
            {
                var ctor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(ci =>
                    {
                        var parameters = ci.GetParameters();
                        return parameters.Length == 0 || parameters.All(pi => pi.HasDefaultValue);
                    });

                if (ctor == null)
                    throw new InvalidOperationException($"A default constructor was not found on {type.FullName}.");

                var call = ctor.GetParameters().Select(pi => pi.DefaultValue).ToArray();
                return ctor.Invoke(call);
            }
        }

        if (toType.IsArray && toType.GetArrayRank() == 1)
        {
            var elementType = toType.GetElementType()!;

            if (string.IsNullOrEmpty(value))
            {
                return Array.CreateInstance(elementType, 0);
            }

            var items = value.Split(',');
            var length = items.Length;
            var result = Array.CreateInstance(elementType, items.Length);
            for (var i = 0; i < length; ++i)
            {
                var item = items[i];
                var element = ConvertToType(item, elementType);
                result.SetValue(element, i);
            }
            return result;
        }

        return Convert.ChangeType(value, toType);
    }

    internal static bool TryParseStaticMemberAccessor(string input, [NotNullWhen(true)] out string? accessorTypeName, [NotNullWhen(true)] out string? memberName)
    {
        if (StaticMemberAccessorRegex.IsMatch(input))
        {
            var match = StaticMemberAccessorRegex.Match(input);
            var shortAccessorTypeName = match.Groups["shortTypeName"].Value;
            var rawMemberName = match.Groups["memberName"].Value;
            var extraQualifiers = match.Groups["typeNameExtraQualifiers"].Value;

            memberName = rawMemberName.Trim();
            accessorTypeName = shortAccessorTypeName.Trim() + extraQualifiers.TrimEnd();
            return true;
        }
        accessorTypeName = null;
        memberName = null;
        return false;
    }
}
