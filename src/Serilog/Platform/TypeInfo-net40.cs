// Copyright 2015 Serilog Contributors
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
using System.Reflection;

#if NET40
namespace Serilog.Platform
{
    /// <summary>
    /// Backport of .NET 4.5 TypeInfo concept
    /// </summary>
    internal class TypeInfo
    {
        readonly Type _type;

        public TypeInfo(Type type)
        {
            _type = type;
        }

        public Type AsType()
        {
            return _type;
        }

        public Type BaseType
        {
            get
            {
                return _type.BaseType;
            }
        }

        public bool IsGenericType
        {
            get { return _type.IsGenericType; }
        }

        public Type[] GenericTypeArguments
        {
            get { return _type.GetGenericArguments(); }
        }

        public bool IsEnum
        {
            get { return _type.IsEnum; }
        }

        public Assembly Assembly
        {
            get { return _type.Assembly; }
        }

        public bool IsSealed
        {
            get { return _type.IsSealed; }
        }

        public bool IsAbstract
        {
            get { return _type.IsAbstract; }
        }

        public bool IsNested
        {
            get { return _type.IsNested; }
        }

        public IEnumerable<MethodInfo> DeclaredMethods
        {
            get { return _type.GetMethods(); }
        }

        public IEnumerable<PropertyInfo> DeclaredProperties
        {
            get { return _type.GetProperties(); }
        }

        public bool IsAssignableFrom(TypeInfo targetType)
        {
            return _type.IsAssignableFrom(targetType._type);
        }
    }
}
#endif
