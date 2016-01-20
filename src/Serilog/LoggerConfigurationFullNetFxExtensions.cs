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
using System.ComponentModel;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Enrichers;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Formatting.Raw;
using Serilog.Sinks.SystemConsole;

#if PROCESS
using System.Diagnostics;
#endif


#if APPSETTINGS
using Serilog.Settings.AppSettings;
#endif

namespace Serilog
{
    /// <summary>
    /// Extends <see cref="LoggerConfiguration"/> to add Full .NET Framework
    /// capabilities.
    /// </summary>
    public static class LoggerConfigurationFullNetFxExtensions
    {
        //TODO: Need to confirm this is the best location for this default.  Used in File, Trace, RollingFile.  Do we move this to each sink?
        public const string DefaultOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}";
        public const long DefaultFileSizeLimitBytes = 1L * 1024 * 1024 * 1024;
        const string DefaultConsoleOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}";
         
#if LOGCONTEXT
        /// <summary>
        /// Enrich log events with properties from <see cref="Context.LogContext"/>.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static LoggerConfiguration FromLogContext(
            this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<LogContextEnricher>();
        }
#endif

        /// <summary>
        /// Enrich log events with a ThreadId property containing the current <see cref="Thread.ManagedThreadId"/>.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static LoggerConfiguration WithThreadId(
            this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<ThreadIdEnricher>();
        }

#if PROCESS
        /// <summary>
        /// Enrich log events with a ProcessId property containing the current <see cref="Process.Id"/>.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration WithProcessId(
           this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<ProcessIdEnricher>();
        }
#endif

#if !DOTNET5_1
        /// <summary>
        /// Enrich log events with a MachineName property containing the current <see cref="Environment.MachineName"/>.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration WithMachineName(
           this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<MachineNameEnricher>();
        }

        /// <summary>
        /// Enriches log events with an EnvironmentUserName property containing [<see cref="Environment.UserDomainName"/>\]<see cref="Environment.UserName"/>.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration WithEnvironmentUserName(
           this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<EnvironmentUserNameEnricher>();
        }
#endif

#if APPSETTINGS
        /// <summary>
        /// Reads the &lt;appSettings&gt; element of App.config or Web.config, searching for for keys
        /// that look like: <code>serilog:*</code>, which are used to configure
        /// the logger. To add a sink, use a key like <code>serilog:write-to:File.path</code> for
        /// each parameter to the sink's configuration method. To add an additional assembly
        /// containing sinks, use <code>serilog:using</code>. To set the level use
        /// <code>serilog:minimum-level</code>.
        /// </summary>
        /// <param name="settingConfiguration">Logger setting configuration.</param>
        /// <param name="settingPrefix">Prefix to use when reading keys in appSettings. If specified the value
        /// will be prepended to the setting keys and followed by :, for example "myapp" will use "myapp:serilog:minumum-level. If null
        /// no prefix is applied.</param>
        /// <returns>An object allowing configuration to continue.</returns>
        public static LoggerConfiguration AppSettings(
            this LoggerSettingsConfiguration settingConfiguration, string settingPrefix = null)
        {
            if (settingConfiguration == null) throw new ArgumentNullException(nameof(settingConfiguration));
            if (settingPrefix != null)
            {
                if (settingPrefix.Contains(":")) throw new ArgumentException("Custom setting prefixes cannot contain the colon (:) character.");
                if (settingPrefix == "serilog") throw new ArgumentException("The value \"serilog\" is not a permitted setting prefix. To use the default, do not specify a custom prefix at all.");
                if (string.IsNullOrWhiteSpace(settingPrefix)) throw new ArgumentException("To use the default setting prefix, do not supply the settingPrefix parameter, instead pass the default null.");
            }

            return settingConfiguration.Settings(new AppSettingsSettings(settingPrefix));
        }
#endif
    }
}
