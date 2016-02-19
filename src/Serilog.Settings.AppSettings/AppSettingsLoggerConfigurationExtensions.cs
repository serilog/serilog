// Copyright 2013-2016 Serilog Contributors
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
using Serilog.Configuration;
using Serilog.Settings.AppSettings;

namespace Serilog
{
    /// <summary>
    /// Extends <see cref="LoggerConfiguration"/> with support for System.Configuration appSettings elements.
    /// </summary>
    public static class AppSettingsLoggerConfigurationExtensions
    {
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
    }
}
