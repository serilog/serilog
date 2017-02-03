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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Serilog.Settings.KeyValuePairs
{
    class SettingValueConversions
    {
        static Dictionary<Type, Func<string, object>> ExtendedTypeConversions = new Dictionary<Type, Func<string, object>>
            {
                { typeof(Uri), s => new Uri(s) },
                { typeof(TimeSpan), s => TimeSpan.Parse(s) }
            };

        public static object ConvertToType(string value, Type toType)
        {
            var toTypeInfo = toType.GetTypeInfo();
            if (toTypeInfo.IsGenericType && toType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (string.IsNullOrEmpty(value))
                    return null;

                // unwrap Nullable<> type since we're not handling null situations
                toType = toTypeInfo.GenericTypeArguments[0];
                toTypeInfo = toType.GetTypeInfo();
            }

            if (toTypeInfo.IsEnum)
                return Enum.Parse(toType, value);

            var convertor = ExtendedTypeConversions
                .Where(t => t.Key.GetTypeInfo().IsAssignableFrom(toTypeInfo))
                .Select(t => t.Value)
                .FirstOrDefault();

            if (convertor != null)
                return convertor(value);

            if (toTypeInfo.IsInterface && !string.IsNullOrWhiteSpace(value))
            {
                var type = Type.GetType(value.Trim(), throwOnError: false);
                if (type != null)
                {
                    var ctor = type.GetTypeInfo().DeclaredConstructors.FirstOrDefault(ci =>
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

            return Convert.ChangeType(value, toType);
        }
    }
}
