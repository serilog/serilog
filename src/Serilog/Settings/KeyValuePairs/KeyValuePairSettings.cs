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
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Serilog.Configuration;
using Serilog.Events;

namespace Serilog.Settings.KeyValuePairs
{
    class KeyValuePairSettings : ILoggerSettings
    {
        const string UsingDirective = "using";
        const string WriteToDirective = "write-to";
        const string MinimumLevelDirective = "minimum-level";
        const string EnrichWithPropertyDirective = "enrich:with-property";

        const string UsingDirectiveFullFormPrefix = "using:";
        const string EnrichWithPropertyDirectivePrefix = "enrich:with-property:";

        const string WriteToDirectiveRegex = @"^write-to:(?<method>[A-Za-z0-9]*)(\.(?<argument>[A-Za-z0-9]*)){0,1}$";

        readonly string[] _supportedDirectives =
        {
            UsingDirective,
            WriteToDirective,
            MinimumLevelDirective,
            EnrichWithPropertyDirective
        };

        readonly Dictionary<string, string> _settings;

        public KeyValuePairSettings(IEnumerable<KeyValuePair<string, string>> settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            _settings = settings.ToDictionary(s => s.Key, s => s.Value);
        }

        public void Configure(LoggerConfiguration loggerConfiguration)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");

            var directives = _settings.Keys
                .Where(k => _supportedDirectives.Any(k.StartsWith))
                .ToDictionary(k => k, k => _settings[k]);

            string minimumLevelDirective;
            LogEventLevel minimumLevel;
            if (directives.TryGetValue(MinimumLevelDirective, out minimumLevelDirective) &&
                Enum.TryParse(minimumLevelDirective, out minimumLevel))
            {
                loggerConfiguration.MinimumLevel.Is(minimumLevel);
            }

            foreach (var enrichDirective in directives.Where(dir =>
                dir.Key.StartsWith(EnrichWithPropertyDirectivePrefix, StringComparison.CurrentCulture) && dir.Key.Length > EnrichWithPropertyDirectivePrefix.Length))
            {
                var name = enrichDirective.Key.Substring(EnrichWithPropertyDirectivePrefix.Length);
                loggerConfiguration.Enrich.WithProperty(name, enrichDirective.Value);
            }

            var splitWriteTo = new Regex(WriteToDirectiveRegex);

            var sinkDirectives = (from wt in directives
                                  where splitWriteTo.IsMatch(wt.Key)
                                  let match = splitWriteTo.Match(wt.Key)
                                  let call = new
                                  {
                                      Method = match.Groups["method"].Value,
                                      Argument = match.Groups["argument"].Value,
                                      wt.Value
                                  }
                                  group call by call.Method).ToList();

            if (sinkDirectives.Any())
            {
                var configurationAssemblies = LoadConfigurationAssemblies(directives);
                var sinkConfigurationMethods = FindSinkConfigurationMethods(configurationAssemblies);

                foreach (var sinkDirective in sinkDirectives)
                {
                    var target = sinkConfigurationMethods
                        .Where(m => m.Name == sinkDirective.Key &&
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

        internal static IEnumerable<Assembly> LoadConfigurationAssemblies(Dictionary<string, string> directives)
        {
            var configurationAssemblies = new List<Assembly> { typeof(ILogger).GetTypeInfo().Assembly };

            foreach (var usingDirective in directives.Where(d => d.Key.Equals(UsingDirective) ||
                                                                 d.Key.StartsWith(UsingDirectiveFullFormPrefix, StringComparison.CurrentCulture)))
            {
                configurationAssemblies.Add(Assembly.Load(new AssemblyName(usingDirective.Value)));
            }

            return configurationAssemblies.Distinct();
        }

        internal static object ConvertToType(string value, Type toType)
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

            var extendedTypeConversions = new Dictionary<Type, Func<string, object>>
            {
                { typeof(Uri), s => new Uri(s) },
                { typeof(TimeSpan), s => TimeSpan.Parse(s) }
            };

            var convertor = extendedTypeConversions
                .Where(t => t.Key.GetTypeInfo().IsAssignableFrom(toTypeInfo))
                .Select(t => t.Value)
                .FirstOrDefault();

            return convertor == null ? Convert.ChangeType(value, toType) : convertor(value);
        }

        internal static IEnumerable<MethodInfo> FindSinkConfigurationMethods(IEnumerable<Assembly> configurationAssemblies)
        {
            return configurationAssemblies
                .SelectMany(a => a.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsSealed && t.IsAbstract && !t.IsNested))
                .SelectMany(t => t.DeclaredMethods)
                .Where(m => m.IsStatic && m.IsPublic && m.IsDefined(typeof(ExtensionAttribute), false))
                .Where(m => m.GetParameters()[0].ParameterType == typeof(LoggerSinkConfiguration));
        }
    }
}
