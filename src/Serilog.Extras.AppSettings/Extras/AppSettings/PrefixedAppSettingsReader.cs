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
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.RollingFile;
using System.ComponentModel;

namespace Serilog.Extras.AppSettings
{
    class PrefixedAppSettingsReader
    {
        const string UsingDirective = "serilog:using";
        const string WriteToDirective = "serilog:write-to";
        const string MinimumLevelDirective = "serilog:minimum-level";
        const string EnrichWithPropertyDirective = "serilog:enrich:with-property";

        const string UsingDirectiveFullFormPrefix = "serilog:using:";
        const string EnrichWithPropertyDirectivePrefix = "serilog:enrich:with-property:";

        const string WriteToDirectiveRegex = @"^serilog:write-to:(?<name>([A-Za-z0-9]*:)?)(?<method>[A-Za-z0-9]*)(\.(?<argument>[A-Za-z0-9]*)){0,1}$";

        public static void ConfigureLogger(LoggerConfiguration loggerConfiguration)
        {
            ConfigureLogger(loggerConfiguration, ConfigurationManager.AppSettings);
        }

        internal static void ConfigureLogger(LoggerConfiguration loggerConfiguration, NameValueCollection settings)
        {
            var supportedDirectives = new[]
            {
                UsingDirective,
                WriteToDirective,
                MinimumLevelDirective,
                EnrichWithPropertyDirective
            };

            var directives = settings.AllKeys
                .Where(k => supportedDirectives.Any(k.StartsWith))
                .ToDictionary(k => k, k => Environment.ExpandEnvironmentVariables(settings[k]));

            string minimumLevelDirective;
            LogEventLevel minimumLevel;
            if (directives.TryGetValue(MinimumLevelDirective, out minimumLevelDirective) &&
                Enum.TryParse(minimumLevelDirective, out minimumLevel))
            {
                loggerConfiguration.MinimumLevel.Is(minimumLevel);
            }

            foreach (var enrichDirective in directives.Where(dir => 
                dir.Key.StartsWith(EnrichWithPropertyDirectivePrefix) && dir.Key.Length > EnrichWithPropertyDirectivePrefix.Length))
            {
                var name = enrichDirective.Key.Substring(EnrichWithPropertyDirectivePrefix.Length);
                loggerConfiguration.Enrich.WithProperty(name, enrichDirective.Value);
            }

            var splitWriteTo = new Regex(WriteToDirectiveRegex);

            var sinkDirectives = (from wt in directives
                        where splitWriteTo.IsMatch(wt.Key)
                        let match = splitWriteTo.Match(wt.Key)
                        let call = new {
                            GroupKey = match.Groups["name"].Value + match.Groups["method"].Value,
                            Method = match.Groups["method"].Value,
                            Argument = match.Groups["argument"].Value,
                            wt.Value
                        }
                        group call by call.GroupKey).ToList();

            if (sinkDirectives.Any())
            {
                var extensionMethods = FindExtensionMethods(directives);

                var sinkConfigurationMethods = extensionMethods
                    .Where(m => m.GetParameters()[0].ParameterType == typeof(LoggerSinkConfiguration))
                    .ToList();

                foreach (var sinkDirective in sinkDirectives)
                {
                    string method = sinkDirective.First().Method;
                    var target = sinkConfigurationMethods
                        .Where(m => m.Name == method &&
                            m.GetParameters().Skip(1).All(p => p.HasDefaultValue || sinkDirective.Any(s => s.Argument == p.Name)))
                        .OrderByDescending(m => m.GetParameters().Length)
                        .FirstOrDefault();

                    if (target != null)
                    {
                        var config = loggerConfiguration.WriteTo;

                        var call = (from p in target.GetParameters().Skip(1)
                                   let directive = sinkDirective.FirstOrDefault(s => s.Argument == p.Name)
                                   select directive == null ? p.DefaultValue : ConvertToType(directive.Value, p.ParameterType)).ToList();

                        call.Insert(0, config);

                        target.Invoke(null, call.ToArray());
                    }
                }
            }
        }

        internal static object ConvertToType(string value, Type toType)
        {
            if (toType.IsGenericType && toType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (string.IsNullOrEmpty(value))
                    return null;

                // unwrap Nullable<> type since we're not handling null situations
                toType = (new NullableConverter(toType)).UnderlyingType;
            }

            if (toType.IsEnum)
                return Enum.Parse(toType, value);

            var extendedTypeConversions = new Dictionary<Type, Func<string, object>>
            {
                { typeof(Uri), s => new Uri(s) },
                { typeof(TimeSpan), s => TimeSpan.Parse(s) }
            };

            var convertor = extendedTypeConversions
                .Where(t => t.Key.IsAssignableFrom(toType))
                .Select(t => t.Value)
                .FirstOrDefault();

            return convertor == null ? Convert.ChangeType(value, toType) : convertor(value);
        }

        static IList<MethodInfo> FindExtensionMethods(Dictionary<string, string> directives)
        {
            var extensionAssemblies = new List<Assembly> {typeof (ILogger).Assembly, typeof (RollingFileSink).Assembly};
            foreach (var usingDirective in directives.Where(d => d.Key.Equals(UsingDirective) ||
                                                                 d.Key.StartsWith(UsingDirectiveFullFormPrefix)))
            {
                extensionAssemblies.Add(Assembly.Load(usingDirective.Value));
            }

            return extensionAssemblies
                .SelectMany(a => a.ExportedTypes.Where(t => t.IsSealed && t.IsAbstract && !t.IsNested))
                .SelectMany(t => t.GetMethods())
                .Where(m => m.IsStatic && m.IsPublic && m.IsDefined(typeof (ExtensionAttribute), false))
                .ToList();
        }
    }
}
