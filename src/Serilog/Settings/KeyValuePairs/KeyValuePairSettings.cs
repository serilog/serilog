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

#if NET6_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif

namespace Serilog.Settings.KeyValuePairs;

#if !NET5_0
[RequiresUnreferencedCode("Scans assemblies at runtime")] // RequiresUnreferencedCode couldn't be applied to types in .NET 5.
#endif
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

    static readonly Dictionary<string, Type> CallableDirectiveReceiverTypes = new()
    {
        ["audit-to"] = typeof(LoggerAuditSinkConfiguration),
        ["write-to"] = typeof(LoggerSinkConfiguration),
        ["enrich"] = typeof(LoggerEnrichmentConfiguration),
        ["filter"] = typeof(LoggerFilterConfiguration),
        ["destructure"] = typeof(LoggerDestructuringConfiguration),
    };

    static readonly Dictionary<Type, Func<LoggerConfiguration, object>> CallableDirectiveReceivers = new()
    {
        [typeof(LoggerAuditSinkConfiguration)] = lc => lc.AuditTo,
        [typeof(LoggerSinkConfiguration)] = lc => lc.WriteTo,
        [typeof(LoggerEnrichmentConfiguration)] = lc => lc.Enrich,
        [typeof(LoggerFilterConfiguration)] = lc => lc.Filter,
        [typeof(LoggerDestructuringConfiguration)] = lc => lc.Destructure,
    };

    readonly IReadOnlyDictionary<string, string> _settings;

    // This is only needed on net5 because RequiresUnreferencedCode couldn't be applied to types
    // in .NET 5. In .NET 6+, RUC can be applied to types (as it is above) and it will warn on the
    // constructor and hide warnings in members.
    [RequiresUnreferencedCode("Finds accessors by name")]
    public KeyValuePairSettings(IReadOnlyDictionary<string, string> settings)
    {
        _settings = Guard.AgainstNull(settings);
    }

#if NET5_0
    // Suppressing warnings here in .NET 5 because RUC is placed on the constructor. See constructor
    // for more info on why this is not necessary in .NET 6.
    [UnconditionalSuppressMessage("ILTrim", "IL2026")]
#endif
    public void Configure(LoggerConfiguration loggerConfiguration)
    {
        Guard.AgainstNull(loggerConfiguration);

        var directives = _settings
            .Where(kvp => _supportedDirectives.Any(kvp.Key.StartsWith))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        var declaredLevelSwitches = ParseNamedLevelSwitchDeclarationDirectives(directives);

        if (directives.TryGetValue(MinimumLevelDirective, out var minimumLevelDirective) &&
            Enum.TryParse(minimumLevelDirective, out LogEventLevel minimumLevel))
        {
            loggerConfiguration.MinimumLevel.Is(minimumLevel);
        }

        foreach (var enrichPropertyDirective in directives.Where(dir =>
                     dir.Key.StartsWith(EnrichWithPropertyDirectivePrefix) && dir.Key.Length > EnrichWithPropertyDirectivePrefix.Length))
        {
            var name = enrichPropertyDirective.Key.Substring(EnrichWithPropertyDirectivePrefix.Length);
            loggerConfiguration.Enrich.WithProperty(name, enrichPropertyDirective.Value);
        }

        if (directives.TryGetValue(MinimumLevelControlledByDirective, out var minimumLevelControlledByLevelSwitchName))
        {
            var globalMinimumLevelSwitch = LookUpSwitchByName(minimumLevelControlledByLevelSwitchName, declaredLevelSwitches);
            loggerConfiguration.MinimumLevel.ControlledBy(globalMinimumLevelSwitch);
        }

        foreach (var minimumLevelOverrideDirective in directives.Where(dir =>
                     dir.Key.StartsWith(MinimumLevelOverrideDirectivePrefix) && dir.Key.Length > MinimumLevelOverrideDirectivePrefix.Length))
        {
            var namespacePrefix = minimumLevelOverrideDirective.Key.Substring(MinimumLevelOverrideDirectivePrefix.Length);

            if (Enum.TryParse(minimumLevelOverrideDirective.Value, out LogEventLevel overriddenLevel))
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
                Call = new ConfigurationMethodCall(
                    match.Groups["method"].Value,
                    match.Groups["argument"].Value,
                    wt.Value)
            }).ToList();

        if (!callableDirectives.Any()) return;

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

    internal static bool IsValidSwitchName(string input)
    {
        return Regex.IsMatch(input, LevelSwitchNameRegex);
    }

    [RequiresUnreferencedCode("Reflects against accessors using dynamic string")]
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
            if (switchInitialLevel == string.Empty)
            {
                newSwitch = new();
            }
            else
            {
                var initialLevel = (LogEventLevel) Enum.Parse(typeof(LogEventLevel), switchInitialLevel);
                newSwitch = new(initialLevel);
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

    [RequiresUnreferencedCode("Finds accessors by name")]
    static object ConvertOrLookupByName(string valueOrSwitchName, Type type, IReadOnlyDictionary<string, LoggingLevelSwitch> declaredSwitches)
    {
        if (type == typeof(LoggingLevelSwitch))
        {
            return LookUpSwitchByName(valueOrSwitchName, declaredSwitches);
        }
        return SettingValueConversions.ConvertToType(valueOrSwitchName, type)!;
    }

    [RequiresUnreferencedCode("Finds accessors by name")]
    static void ApplyDirectives(List<IGrouping<string, ConfigurationMethodCall>> directives, IList<MethodInfo> configurationMethods, object loggerConfigMethod, IReadOnlyDictionary<string, LoggingLevelSwitch> declaredSwitches)
    {
        foreach (var directiveInfo in directives)
        {
            var target = SelectConfigurationMethod(configurationMethods, directiveInfo.Key, directiveInfo);

            if (target == null)
            {
                SelfLog.WriteLine("Setting \"{0}\" could not be matched to an implementation in any of the loaded assemblies. " +
                                  "To use settings from additional assemblies, specify them with the \"serilog:using\" key.", directiveInfo.Key);
            }
            else
            {
                var call = (from p in target.GetParameters().Skip(1)
                    let directive = directiveInfo.FirstOrDefault(s => s.ArgumentName == p.Name)
                    select SuppressConvertCall(directive, p)).ToList();

// Work around inability to annotate lambdas in query expressions. The parent *must* have RUC for safety.
                    [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2026")]
                    object? SuppressConvertCall(ConfigurationMethodCall? directive, ParameterInfo p)
                        => directive == null ? p.DefaultValue : ConvertOrLookupByName(directive.Value, p.ParameterType, declaredSwitches);

                call.Insert(0, loggerConfigMethod);

                target.Invoke(null, call.ToArray());
            }
        }
    }

    internal static MethodInfo? SelectConfigurationMethod(IEnumerable<MethodInfo> candidateMethods, string name, IEnumerable<ConfigurationMethodCall> suppliedArgumentValues)
    {
        return candidateMethods
            .Where(m => m.Name == name &&
                        m.GetParameters().Skip(1)
                            .All(p => p.HasDefaultValue ||
                                      suppliedArgumentValues.Any(s => s.ArgumentName == p.Name)))
            .OrderByDescending(m => m.GetParameters().Count(p => suppliedArgumentValues.Any(s => s.ArgumentName == p.Name)))
            .FirstOrDefault();
    }

    internal static IEnumerable<Assembly> LoadConfigurationAssemblies(IReadOnlyDictionary<string, string> directives)
    {
        var configurationAssemblies = new List<Assembly> { typeof(ILogger).Assembly };

        foreach (var usingDirective in directives.Where(d => d.Key.Equals(UsingDirective) ||
                                                             d.Key.StartsWith(UsingDirectiveFullFormPrefix)))
        {
            if (string.IsNullOrWhiteSpace(usingDirective.Value))
                throw new InvalidOperationException("A zero-length or whitespace assembly name was supplied to a serilog:using configuration statement.");

            var assemblyName = new AssemblyName(usingDirective.Value);
            configurationAssemblies.Add(Assembly.Load(assemblyName));
        }

        return configurationAssemblies.Distinct();
    }

    internal class ConfigurationMethodCall
    {
        public ConfigurationMethodCall(string methodName, string argumentName, string value)
        {
            MethodName = methodName;
            ArgumentName = argumentName;
            Value = value;
        }

        public string MethodName { get; }

        public string ArgumentName { get; }

        public string Value { get; }
    }
}
