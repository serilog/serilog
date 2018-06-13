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
using System.Text.RegularExpressions;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Settings.KeyValuePairs
{
    class KeyValuePairSettings : ILoggerSettings
    {
        const string UsingDirective = "using";
        const string LevelSwitchDirective = "level-switch";
        const string AuditToDirective = "audit-to";
        const string WriteToDirective = "write-to";
        const string MinimumLevelDirective = "minimum-level";
        const string MinimumLevelControlledByDirective = "minimum-level:controlled-by";
        const string EnrichWithDirective = "enrich";
        const string EnrichWithPropertyDirective = "enrich:with-property";
        const string FilterDirective = "filter";
        const string DestructureDirective = "destructure";

        const string UsingDirectiveFullFormPrefix = "using:";
        const string EnrichWithPropertyDirectivePrefix = "enrich:with-property:";
        const string MinimumLevelOverrideDirectivePrefix = "minimum-level:override:";

        const string CallableDirectiveRegex = @"^(?<directive>audit-to|write-to|enrich|filter|destructure):(?<method>[A-Za-z0-9]*)(\.(?<argument>[A-Za-z0-9]*)){0,1}$";
        const string LevelSwitchDeclarationDirectiveRegex = @"^level-switch:(?<switchName>.*)$";
        const string LevelSwitchNameRegex = @"^\$[A-Za-z]+[A-Za-z0-9]*$";

        static readonly string[] _supportedDirectives =
        {
            UsingDirective,
            LevelSwitchDirective,
            AuditToDirective,
            WriteToDirective,
            MinimumLevelDirective,
            MinimumLevelControlledByDirective,
            EnrichWithPropertyDirective,
            EnrichWithDirective,
            FilterDirective,
            DestructureDirective
        };

        static readonly Dictionary<string, Type> CallableDirectiveReceiverTypes = new Dictionary<string, Type>
        {
            ["audit-to"] = typeof(LoggerAuditSinkConfiguration),
            ["write-to"] = typeof(LoggerSinkConfiguration),
            ["enrich"] = typeof(LoggerEnrichmentConfiguration),
            ["filter"] = typeof(LoggerFilterConfiguration),
            ["destructure"] = typeof(LoggerDestructuringConfiguration),
        };

        static readonly Dictionary<Type, Func<LoggerConfiguration, object>> CallableDirectiveReceivers = new Dictionary<Type, Func<LoggerConfiguration, object>>
        {
            [typeof(LoggerAuditSinkConfiguration)] = lc => lc.AuditTo,
            [typeof(LoggerSinkConfiguration)] = lc => lc.WriteTo,
            [typeof(LoggerEnrichmentConfiguration)] = lc => lc.Enrich,
            [typeof(LoggerFilterConfiguration)] = lc => lc.Filter,
            [typeof(LoggerDestructuringConfiguration)] = lc => lc.Destructure,
        };

        readonly IReadOnlyDictionary<string, string> _settings;

        public KeyValuePairSettings(IReadOnlyDictionary<string, string> settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Configure(LoggerConfiguration loggerConfiguration)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException(nameof(loggerConfiguration));

            var directives = _settings
                .Where(kvp => _supportedDirectives.Any(kvp.Key.StartsWith))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            var declaredLevelSwitches = ParseNamedLevelSwitchDeclarationDirectives(directives);

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

            string minimumLevelControlledByLevelSwitchName;
            if (directives.TryGetValue(MinimumLevelControlledByDirective, out minimumLevelControlledByLevelSwitchName))
            {
                var globalMinimumLevelSwitch = LookUpSwitchByName(minimumLevelControlledByLevelSwitchName, declaredLevelSwitches);
                loggerConfiguration.MinimumLevel.ControlledBy(globalMinimumLevelSwitch);
            }

            foreach (var minimumLevelOverrideDirective in directives.Where(dir =>
                dir.Key.StartsWith(MinimumLevelOverrideDirectivePrefix) && dir.Key.Length > MinimumLevelOverrideDirectivePrefix.Length))
            {
                var namespacePrefix = minimumLevelOverrideDirective.Key.Substring(MinimumLevelOverrideDirectivePrefix.Length);

                LogEventLevel overriddenLevel;
                if (Enum.TryParse(minimumLevelOverrideDirective.Value, out overriddenLevel))
                {
                    loggerConfiguration.MinimumLevel.Override(namespacePrefix, overriddenLevel);
                }
                else
                {
                    var overrideSwitch = LookUpSwitchByName(minimumLevelOverrideDirective.Value, declaredLevelSwitches);
                    loggerConfiguration.MinimumLevel.Override(namespacePrefix, overrideSwitch);
                }
            }

            var matchCallables = new Regex(CallableDirectiveRegex);

            var callableDirectives = (from wt in directives
                                      where matchCallables.IsMatch(wt.Key)
                                      let match = matchCallables.Match(wt.Key)
                                      select new
                                      {
                                          ReceiverType = CallableDirectiveReceiverTypes[match.Groups["directive"].Value],
                                          Call = new ConfigurationMethodCall
                                          {
                                              MethodName = match.Groups["method"].Value,
                                              ArgumentName = match.Groups["argument"].Value,
                                              Value = wt.Value
                                          }
                                      }).ToList();

            if (callableDirectives.Any())
            {
                var configurationAssemblies = LoadConfigurationAssemblies(directives).ToList();

                foreach (var receiverGroup in callableDirectives.GroupBy(d => d.ReceiverType))
                {
                    var methods = CallableConfigurationMethodFinder.FindConfigurationMethods(configurationAssemblies, receiverGroup.Key);

                    var calls = receiverGroup
                        .Select(d => d.Call)
                        .GroupBy(call => call.MethodName)
                        .ToList();

                    ApplyDirectives(calls, methods, CallableDirectiveReceivers[receiverGroup.Key](loggerConfiguration), declaredLevelSwitches);
                }
            }
        }

        internal static bool IsValidSwitchName(string input)
        {
            return Regex.IsMatch(input, LevelSwitchNameRegex);
        }

        static IReadOnlyDictionary<string, LoggingLevelSwitch> ParseNamedLevelSwitchDeclarationDirectives(IReadOnlyDictionary<string, string> directives)
        {
            var matchLevelSwitchDeclarations = new Regex(LevelSwitchDeclarationDirectiveRegex);

            var switchDeclarationDirectives = (from wt in directives
                                               where matchLevelSwitchDeclarations.IsMatch(wt.Key)
                                               let match = matchLevelSwitchDeclarations.Match(wt.Key)
                                               select new
                                               {
                                                   SwitchName = match.Groups["switchName"].Value,
                                                   InitialSwitchLevel = wt.Value
                                               }).ToList();

            var namedSwitches = new Dictionary<string, LoggingLevelSwitch>();
            foreach (var switchDeclarationDirective in switchDeclarationDirectives)
            {
                var switchName = switchDeclarationDirective.SwitchName;
                var switchInitialLevel = switchDeclarationDirective.InitialSwitchLevel;
                // switchName must be something like $switch to avoid ambiguities
                if (!IsValidSwitchName(switchName))
                {
                    throw new FormatException($"\"{switchName}\" is not a valid name for a Level Switch declaration. Level switch must be declared with a '$' sign, like \"level-switch:$switchName\"");
                }
                LoggingLevelSwitch newSwitch;
                if (string.IsNullOrEmpty(switchInitialLevel))
                {
                    newSwitch = new LoggingLevelSwitch();
                }
                else
                {
                    var initialLevel = (LogEventLevel)SettingValueConversions.ConvertToType(switchInitialLevel, typeof(LogEventLevel));
                    newSwitch = new LoggingLevelSwitch(initialLevel);
                }
                namedSwitches.Add(switchName, newSwitch);
            }
            return namedSwitches;
        }

        static LoggingLevelSwitch LookUpSwitchByName(string switchName, IReadOnlyDictionary<string, LoggingLevelSwitch> declaredLevelSwitches)
        {
            if (declaredLevelSwitches.TryGetValue(switchName, out var levelSwitch))
            {
                return levelSwitch;
            }

            throw new InvalidOperationException($"No LoggingLevelSwitch has been declared with name \"{switchName}\". You might be missing a key \"{LevelSwitchDirective}:{switchName}\"");
        }

        static object ConvertOrLookupByName(string valueOrSwitchName, Type type, IReadOnlyDictionary<string, LoggingLevelSwitch> declaredSwitches)
        {
            if (type == typeof(LoggingLevelSwitch))
            {
                return LookUpSwitchByName(valueOrSwitchName, declaredSwitches);
            }
            return SettingValueConversions.ConvertToType(valueOrSwitchName, type);
        }

        static void ApplyDirectives(List<IGrouping<string, ConfigurationMethodCall>> directives, IList<MethodInfo> configurationMethods, object loggerConfigMethod, IReadOnlyDictionary<string, LoggingLevelSwitch> declaredSwitches)
        {
            foreach (var directiveInfo in directives)
            {
                var target = SelectConfigurationMethod(configurationMethods, directiveInfo.Key, directiveInfo);

                if (target != null)
                {

                    var call = (from p in target.GetParameters().Skip(1)
                                let directive = directiveInfo.FirstOrDefault(s => s.ArgumentName == p.Name)
                                select directive == null ? p.DefaultValue : ConvertOrLookupByName(directive.Value, p.ParameterType, declaredSwitches)).ToList();

                    call.Insert(0, loggerConfigMethod);

                    target.Invoke(null, call.ToArray());
                }
            }
        }

        internal static MethodInfo SelectConfigurationMethod(IEnumerable<MethodInfo> candidateMethods, string name, IEnumerable<ConfigurationMethodCall> suppliedArgumentValues)
        {
            return candidateMethods
                .Where(m => m.Name == name &&
                            m.GetParameters().Skip(1).All(p => p.HasDefaultValue || suppliedArgumentValues.Any(s => s.ArgumentName == p.Name)))
                .OrderByDescending(m => m.GetParameters().Count(p => suppliedArgumentValues.Any(s => s.ArgumentName == p.Name)))
                .FirstOrDefault();
        }

        internal static IEnumerable<Assembly> LoadConfigurationAssemblies(IReadOnlyDictionary<string, string> directives)
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

        internal class ConfigurationMethodCall
        {
            public string MethodName { get; set; }
            public string ArgumentName { get; set; }
            public string Value { get; set; }
        }
    }
}
