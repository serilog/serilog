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

using Serilog.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Serilog.Settings.KeyValuePairs
{
    static class CallableConfigurationMethodFinder
    {
        internal static LoggerConfiguration FromLogContext(LoggerEnrichmentConfiguration loggerEnrichmentConfiguration)
        {
            return loggerEnrichmentConfiguration.FromLogContext();
        }

        static readonly MethodInfo SurrogateFromLogContextConfigurationMethod = typeof(CallableConfigurationMethodFinder).GetTypeInfo().DeclaredMethods.Single(m => m.Name == nameof(FromLogContext));

        internal static IList<MethodInfo> FindConfigurationMethods(IEnumerable<Assembly> configurationAssemblies, Type configType)
        {
            var methods = configurationAssemblies
                .SelectMany(a => a.ExportedTypes
                    .Select(t => t.GetTypeInfo())
                    .Where(t => t.IsSealed && t.IsAbstract && !t.IsNested))
                .SelectMany(t => t.DeclaredMethods)
                .Where(m => m.IsStatic && m.IsPublic && m.IsDefined(typeof(ExtensionAttribute), false))
                .Where(m => m.GetParameters()[0].ParameterType == configType)
                .ToList();

            // Unlike the other configuration methods, FromLogContext is an instance method rather than an extension. This
            // hack ensures we find it.
            if (configType == typeof(LoggerEnrichmentConfiguration))
                methods.Add(SurrogateFromLogContextConfigurationMethod);

            return methods;
        }
    }
}
