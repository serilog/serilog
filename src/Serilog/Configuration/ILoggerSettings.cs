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

namespace Serilog.Configuration
{
    /// <summary>
    /// Implemented on types that apply settings to a logger configuration.
    /// </summary>
    public interface ILoggerSettings
    {
        /// <summary>
        /// Apply the settings to the logger configuration.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration to apply settings to.</param>
        void Configure(LoggerConfiguration loggerConfiguration);
    }
}