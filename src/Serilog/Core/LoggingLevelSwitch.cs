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

using Serilog.Events;

namespace Serilog.Core
{
    /// <summary>
    /// Dynamically controls logging level.
    /// </summary>
    public class LoggingLevelSwitch
    {
        volatile LogEventLevel _minimumLevel;

        /// <summary>
        /// Create a <see cref="LoggingLevelSwitch"/> at the initial
        /// minimum level.
        /// </summary>
        /// <param name="initialMinimumLevel">The initial level to which the switch is set.</param>
        public LoggingLevelSwitch(LogEventLevel initialMinimumLevel = LogEventLevel.Information)
        {
            _minimumLevel = initialMinimumLevel;
        }

        /// <summary>
        /// The current minimum level, below which no events
        /// should be generated.
        /// </summary>
        // Reading this property generates a memory barrier,
        // so needs to be used judiciously in the logging pipeline.
        public LogEventLevel MinimumLevel
        {
            get { return _minimumLevel; }
            set { _minimumLevel = value; }
        }
    }
}
