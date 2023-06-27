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

namespace Serilog.Core;

/// <summary>
/// Event arguments for <see cref="LoggingLevelSwitch.MinimumLevelChanged"/> event.
/// </summary>
public class LoggingLevelSwitchChangedEventArgs : EventArgs
{
    /// <summary>
    /// Creates an instance of <see cref="LoggingLevelSwitchChangedEventArgs"/> specifying old and new levels.
    /// </summary>
    /// <param name="oldLevel">Old level.</param>
    /// <param name="newLevel">New level.</param>
    public LoggingLevelSwitchChangedEventArgs(LogEventLevel oldLevel, LogEventLevel newLevel)
    {
        OldLevel = oldLevel;
        NewLevel = newLevel;
    }

    /// <summary>
    /// Old level.
    /// </summary>
    public LogEventLevel OldLevel { get; }

    /// <summary>
    /// New level.
    /// </summary>
    public LogEventLevel NewLevel { get; }
}
