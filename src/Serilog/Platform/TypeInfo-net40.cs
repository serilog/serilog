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

#if NET40

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Serilog.Platform
{
    /// <summary>
    /// Backport of .NET 4.5 TypeInfo concept
    /// </summary>
    internal class TypeInfo
    {
        public TypeInfo(Type type)
        {
            Type = type;
        }

        public Type AsType() => Type;

        public Type Type { get; }

        public Type BaseType => Type.BaseType;

        public bool IsGenericType => Type.IsGenericType;

        public Type[] GenericTypeArguments => Type.GetGenericArguments();

        public bool IsEnum => Type.IsEnum;

        public Assembly Assembly => Type.Assembly;

        public bool IsSealed => Type.IsSealed;

        public bool IsAbstract => Type.IsAbstract;

        public bool IsNested => Type.IsNested;

        public IEnumerable<MethodInfo> DeclaredMethods => Type.GetMethods();

        public IEnumerable<PropertyInfo> DeclaredProperties => Type.GetProperties();

        public bool IsAssignableFrom(TypeInfo targetType) => Type.IsAssignableFrom(targetType.Type);

        public bool IsNotPublic => Type.IsNotPublic;
    }
}
#endif
