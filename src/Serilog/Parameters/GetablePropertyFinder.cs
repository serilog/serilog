using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Serilog.Parameters
{
    static class GetablePropertyFinder
    {
        internal static IEnumerable<PropertyInfo> GetPropertiesRecursive(this Type type)
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
                    yield return propertyInfo;
                }

                currentTypeInfo = currentTypeInfo.BaseType.GetTypeInfo();
            }
        }
    }
}