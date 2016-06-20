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
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
        const string EnrichWithDirective = "enrich";
        const string EnrichWithPropertyDirective = "enrich:with-property";

        const string UsingDirectiveFullFormPrefix = "using:";
        const string EnrichWithEventEnricherPrefix = "enrich:";
        const string EnrichWithPropertyDirectivePrefix = "enrich:with-property:";

        const string WriteToDirectiveRegex = @"^write-to:(?<method>[A-Za-z0-9]*)(\.(?<argument>[A-Za-z0-9]*)){0,1}$";

        readonly string[] _supportedDirectives =
        {
            UsingDirective,
            WriteToDirective,
            MinimumLevelDirective,
            EnrichWithPropertyDirective,
            EnrichWithDirective
        };

        readonly Dictionary<string, string> _settings;

        public KeyValuePairSettings(IEnumerable<KeyValuePair<string, string>> settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            _settings = settings.ToDictionary(s => s.Key, s => s.Value);
        }

        public void Configure(LoggerConfiguration loggerConfiguration)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException(nameof(loggerConfiguration));

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

            foreach (var enrichProperyDirective in directives.Where(dir =>
                dir.Key.StartsWith(EnrichWithPropertyDirectivePrefix) && dir.Key.Length > EnrichWithPropertyDirectivePrefix.Length))
            {
                var name = enrichProperyDirective.Key.Substring(EnrichWithPropertyDirectivePrefix.Length);
                loggerConfiguration.Enrich.WithProperty(name, enrichProperyDirective.Value);
            }

            var splitWriteTo = new Regex(WriteToDirectiveRegex);

            var sinkDirectives = (from wt in directives
                                  where splitWriteTo.IsMatch(wt.Key)
                                  let match = splitWriteTo.Match(wt.Key)
                                  let call = new MethodArgumentValue
                                  {
                                      Method = match.Groups["method"].Value,
                                      Argument = match.Groups["argument"].Value,
                                      Value = wt.Value
                                  }
                                  group call by call.Method).ToList();

            var eventEnricherDirectives = (from er in directives
                                           where er.Key.StartsWith(EnrichWithEventEnricherPrefix) && !er.Key.StartsWith(EnrichWithPropertyDirectivePrefix) && er.Key.Length > EnrichWithEventEnricherPrefix.Length
                                           let match = er.Key.Substring(EnrichWithEventEnricherPrefix.Length)
                                           let call = new MethodArgumentValue
                                           {
                                               Method = match
                                           }
                                           group call by call.Method).ToList();

            if (sinkDirectives.Any() || eventEnricherDirectives.Any())
            {
                var configurationAssemblies = LoadConfigurationAssemblies(directives);

                if (sinkDirectives.Any())
                {
                    ApplyDirectives(sinkDirectives, FindSinkConfigurationMethods(configurationAssemblies), loggerConfiguration.WriteTo);
                }

                if (eventEnricherDirectives.Any())
                {
                    ApplyDirectives(eventEnricherDirectives, FindEventEnricherConfigurationMethods(configurationAssemblies), loggerConfiguration.Enrich);
                }
            }
        }

        static void ApplyDirectives(List<IGrouping<string, MethodArgumentValue>> directives, IList<MethodInfo> configurationMethods, object loggerConfigMethod)
        {
            foreach (var directiveInfo in directives)
            {
                var target = configurationMethods
                    .Where(m => m.Name == directiveInfo.Key &&
                        m.GetParameters().Skip(1).All(p => p.HasDefaultValue || directiveInfo.Any(s => s.Argument == p.Name)))
                    .OrderByDescending(m => m.GetParameters().Length)
                    .FirstOrDefault();

                if (target != null)
                {

                    var call = (from p in target.GetParameters().Skip(1)
                                let directive = directiveInfo.FirstOrDefault(s => s.Argument == p.Name)
                                select directive == null ? p.DefaultValue : ConvertToType(directive.Value, p.ParameterType)).ToList();

                    call.Insert(0, loggerConfigMethod);

                    target.Invoke(null, call.ToArray());
                }
            }
        }

        internal static IEnumerable<Assembly> LoadConfigurationAssemblies(Dictionary<string, string> directives)
        {
            var configurationAssemblies = new List<Assembly> { typeof(ILogger).GetTypeInfo().Assembly };

            foreach (var usingDirective in directives.Where(d => d.Key.Equals(UsingDirective) ||
                                                                 d.Key.StartsWith(UsingDirectiveFullFormPrefix)))
            {
                if (string.IsNullOrWhiteSpace(usingDirective.Value))
                    throw new InvalidOperationException("A zero-length or whitespace assembly name was supplied to a serilog:using configuration statement.");

                configurationAssemblies.Add(Assembly.Load(new AssemblyName(usingDirective.Value)));
            }

            return configurationAssemblies.Distinct();
        }

        static Dictionary<Type, Func<string, object>> ExtendedTypeConversions = new Dictionary<Type, Func<string, object>>
            {
                { typeof(Uri), s => new Uri(s) },
                { typeof(TimeSpan), s => TimeSpan.Parse(s) }
            };

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

            var convertor = ExtendedTypeConversions
                .Where(t => t.Key.GetTypeInfo().IsAssignableFrom(toTypeInfo))
                .Select(t => t.Value)
                .FirstOrDefault();

            if (convertor != null)
                return convertor(value);

            if (toTypeInfo.IsInterface && !string.IsNullOrWhiteSpace(value))
            {
                var type = Type.GetType(value.Trim(), throwOnError: false).GetTypeInfo();
                if (type != null)
                {
                    var ctor = type.DeclaredConstructors.FirstOrDefault(ci =>
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

        internal static IList<MethodInfo> FindSinkConfigurationMethods(IEnumerable<Assembly> configurationAssemblies)
        {
            return FindConfigurationMethods(configurationAssemblies, typeof(LoggerSinkConfiguration));
        }

        // Unlike the other configuration methods, FromLogContext is an instance method rather than an extension.

        internal static LoggerConfiguration FromLogContext(LoggerEnrichmentConfiguration loggerEnrichmentConfiguration)
        {
            return loggerEnrichmentConfiguration.FromLogContext();
        }

        static readonly MethodInfo SurrogateFromLogContextConfigurationMethod = typeof(KeyValuePairSettings).GetTypeInfo().DeclaredMethods.Single(m => m.Name == "FromLogContext");

        internal static IList<MethodInfo> FindEventEnricherConfigurationMethods(IEnumerable<Assembly> configurationAssemblies)
        {
            var found = FindConfigurationMethods(configurationAssemblies, typeof(LoggerEnrichmentConfiguration));
            if (configurationAssemblies.Contains(typeof(LoggerEnrichmentConfiguration).GetTypeInfo().Assembly))
                found.Add(SurrogateFromLogContextConfigurationMethod);

            return found;
        }

        internal static IList<MethodInfo> FindConfigurationMethods(IEnumerable<Assembly> configurationAssemblies, Type configType)
        {
            return configurationAssemblies
                .SelectMany(a => a.ExportedTypes
                    .Select(t => t.GetTypeInfo())
                    .Where(t => t.IsSealed && t.IsAbstract && !t.IsNested))
                .SelectMany(t => t.DeclaredMethods)
                .Where(m => m.IsStatic && m.IsPublic && m.IsDefined(typeof(ExtensionAttribute), false))
                .Where(m => m.GetParameters()[0].ParameterType == configType)
                .ToList();
        }

        private class MethodArgumentValue
        {
            public string Method { get; set; }
            public string Argument { get; set; }
            public string Value { get; set; }
        }
    }
}
