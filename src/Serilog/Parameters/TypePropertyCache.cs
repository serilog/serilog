// Copyright 2014 Serilog Contributors
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

namespace Serilog.Parameters
{
    using System.Linq.Expressions;
    using Serilog.Core;

    static class TypePropertyCache
    {
        static ThreadSafeDictionary<RuntimeTypeHandle, List<PropertyAccessor>> _cache = new ThreadSafeDictionary<RuntimeTypeHandle, List<PropertyAccessor>>();

        public static List<PropertyAccessor> GetCachedProperties(this Type type)
        {
            return _cache.GetOrAdd(type.TypeHandle, () => GetPropertiesRecursive(type).ToList());
        }

        static IEnumerable<PropertyAccessor> GetPropertiesRecursive(Type type)
        {
            var seenNames = new HashSet<string>();

            var currentTypeInfo = type.GetTypeInfo();

            while (currentTypeInfo.AsType() != typeof(object))
            {
                var unseenProperties = currentTypeInfo.DeclaredProperties.Where(p => p.CanRead &&
                                                                    p.GetMethod.IsPublic &&
                                                                    !p.GetMethod.IsStatic &&
                                                                    (p.Name != "Item" || p.GetIndexParameters().Length == 0) &&
                                                                    !seenNames.Contains(p.Name));

                foreach (var propertyInfo in unseenProperties)
                {
                    seenNames.Add(propertyInfo.Name);
                    var accessor = new PropertyAccessor
                    {
                        Name = propertyInfo.Name,
                        GetDelegate = GetGetMethodByExpression(propertyInfo)
                    };
                    yield return accessor;
                }

                currentTypeInfo = currentTypeInfo.BaseType.GetTypeInfo();
            }
        }


        public static Func<object, object> GetGetMethodByExpression(PropertyInfo propertyInfo)
        {
            var getMethodInfo = propertyInfo.GetMethod;
            var instance = Expression.Parameter(typeof(object), "instance");

            var instanceCast = Expression.Convert(instance, propertyInfo.DeclaringType);
            
            var callExpression = Expression.Convert(Expression.Call(instanceCast, getMethodInfo), typeof(object));
            return Expression.Lambda<Func<object, object>>(callExpression, instance)
                .Compile();
        }

    }
}