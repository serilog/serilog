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

using Serilog.Extras.AppSettings;

namespace Serilog
{
    /// <summary>
    /// Adds the ReadAppSettings() extension to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationAppSettingsExtensions
    {
        /// <summary>
        /// Reads the &lt;appSettings&gt; element of App.config or Web.config, searching for for keys
        /// that look like: <code>serilog:*</code>, which are used to configure
        /// the logger. To add a sink, use a key like <code>serilog:write-to:File.path</code> for
        /// each parameter to the sink's configuration method. To add an additional assembly
        /// containing sinks, use <code>serilog:using</code>. To set the level use 
        /// <code>serilog:minimum-level</code>.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration to apply configuration to.</param>
        /// <returns>An object allowing configuration to continue.</returns>
        public static LoggerConfiguration ReadAppSettings(this LoggerConfiguration loggerConfiguration)
        {
            PrefixedAppSettingsReader.ConfigureLogger(loggerConfiguration);
            return loggerConfiguration;
        }
    }
}

