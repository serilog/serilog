// Copyright 2013-2021 Serilog Contributors
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

using System.Threading;
using Serilog.Events;


namespace Serilog.Core
{
    /// <summary>
    /// This mimics <see cref="LoggingLevelSwitch"/>, but is an interface implementation.
    /// </summary>
    internal class ScopedLoggingLevelSwitch : ILoggingLevelSwitch
    {
        private readonly AsyncLocal<LogEventLevel> mLevel = new();


        /// <summary>
        /// The minimum level, below which the events are not generated.
        /// </summary>
        LogEventLevel ILoggingLevelSwitch.MinimumLevel
        {
            get => mLevel.Value;
            set => mLevel.Value = value;
        }
    }
}
