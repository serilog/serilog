using System;
using System.Collections.Generic;
using System.Reflection;

namespace Serilog.Settings.KeyValuePairs
{
    /// <summary>
    /// Backport of .Net 4.5 TypeInfo concept
    /// </summary>
    static class TypeExtensions
    {
        internal static TypeInfo GetTypeInfo(this Type type)
        {
            return new TypeInfo(type);
        }

        internal class TypeInfo
        {
            readonly Type type;

            public TypeInfo(Type type)
            {
                this.type = type;
            }

            public Type Type
            {
                get { return type; }
            }

            public bool IsGenericType
            {
                get { return type.IsGenericType; }
            }

            public Type[] GenericTypeArguments
            {
                get { return type.GetGenericArguments(); }
            }

            public bool IsEnum
            {
                get { return type.IsEnum; }
            }

            public Assembly Assembly
            {
                get { return type.Assembly; }
            }

            public bool IsSealed
            {
                get { return type.IsSealed; }
            }

            public bool IsAbstract
            {
                get { return type.IsAbstract; }
            }

            public bool IsNested
            {
                get { return type.IsNested; }
            }

            public IEnumerable<MethodInfo> DeclaredMethods
            {
                get { return type.GetMethods(); }
            }

            public bool IsAssignableFrom(TypeInfo targetType)
            {
                return type.IsAssignableFrom(targetType.Type);
            }
        }
    }
}