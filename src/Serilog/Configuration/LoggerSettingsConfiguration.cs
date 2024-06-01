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

namespace Serilog.Configuration;

/// <summary>
/// Allows additional setting sources to drive the logger configuration.
/// </summary>
public class LoggerSettingsConfiguration
{
    readonly LoggerConfiguration _loggerConfiguration;

    internal LoggerSettingsConfiguration(LoggerConfiguration loggerConfiguration)
    {
        _loggerConfiguration = Guard.AgainstNull(loggerConfiguration);
    }

    /// <summary>
    /// Apply external settings to the logger configuration.
    /// </summary>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="settings"/> is <code>null</code></exception>
    public LoggerConfiguration Settings(ILoggerSettings settings)
    {
        Guard.AgainstNull(settings);

        settings.Configure(_loggerConfiguration);
        return _loggerConfiguration;
    }

    /// <summary>
    /// Apply settings specified in the Serilog key-value setting format to the logger configuration.
    /// </summary>
    /// <param name="settings">A list of key-value pairs describing logger settings.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    /// <remarks>In case of duplicate keys, the last value for the key is kept and the previous ones are ignored.</remarks>
    /// <exception cref="ArgumentNullException">When <paramref name="settings"/> is <code>null</code></exception>
    [RequiresDynamicCode("KeyValuePair scans for configuration assemblies at run time and is not compatible with trimming.")]
    [RequiresUnreferencedCode("KeyValuePair scans for configuration assemblies at run time and is not compatible with trimming.")]
    [RequiresDynamicCode("KeyValuePair may need to create arrays, which requires dynamic code generation and is not compatible with AOT.")]
    public LoggerConfiguration KeyValuePairs(IEnumerable<KeyValuePair<string, string>> settings)
    {
        Guard.AgainstNull(settings);

        var uniqueSettings = new Dictionary<string, string>();
        foreach (var kvp in settings)
        {
            uniqueSettings[kvp.Key] = kvp.Value;
        }
        return KeyValuePairs(uniqueSettings);
    }

    [RequiresDynamicCode("KeyValuePair scans for configuration settings at run time.")]
    [RequiresUnreferencedCode("KeyValuePair scans for configuration settings at run time.")]
    [RequiresDynamicCode("Creates arrays of unknown element type")]
    LoggerConfiguration KeyValuePairs(IReadOnlyDictionary<string, string> settings)
    {
        return Settings(new KeyValuePairSettings(settings));
    }
}
