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

using Serilog.Configuration;
using Serilog.Extras.Attributed;

namespace Serilog
{
    /// <summary>
    /// Adds the Destructure.UsingAttributes() extension to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationAppSettingsExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="configuration">The logger configuration to apply configuration to.</param>
        /// <returns>An object allowing configuration to continue.</returns>
        public static LoggerConfiguration UsingAttributes(this LoggerDestructuringConfiguration configuration)
        {
            return configuration.With<AttributedDestructuringPolicy>();
        }
    }
}

