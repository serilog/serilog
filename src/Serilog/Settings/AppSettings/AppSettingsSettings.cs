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

#if !PROFILE259 && !DNXCORE50

using System;
using System.Configuration;
using System.Linq;
using Serilog.Configuration;
using Serilog.Settings.KeyValuePairs;

namespace Serilog.Settings.AppSettings
{
    class AppSettingsSettings : ILoggerSettings
    {
        readonly string _settingPrefix;

        public AppSettingsSettings(string settingPrefix = null)
        {
            _settingPrefix = settingPrefix == null ? "serilog:" : $"{settingPrefix}:serilog:";
            System.Console.WriteLine(_settingPrefix);
        }

        public void Configure(LoggerConfiguration loggerConfiguration)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException(nameof(loggerConfiguration));

            var settings = ConfigurationManager.AppSettings;
            
            var pairs = settings.AllKeys
                .Where(k => k.StartsWith(_settingPrefix))
                .ToDictionary(k => k.Substring(_settingPrefix.Length), k => Environment.ExpandEnvironmentVariables(settings[k]));

            var keyValuePairSettings = new KeyValuePairSettings(pairs);
            keyValuePairSettings.Configure(loggerConfiguration);
        }
    }
}
#endif
