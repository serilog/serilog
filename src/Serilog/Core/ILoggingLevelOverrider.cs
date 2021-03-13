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

using Serilog.Core;
using Serilog.Events;


namespace Serilog
{
    /// <summary>
    /// An object returned by <see cref="IScopedLogger.SetMinimumLevelOverrider"/>
    /// and used by the client to ovverride the minimum logging level
    /// per scope.
    /// </summary>
    public interface ILoggingLevelOverrider
    {
        /// <summary>
        /// Override the minimum level for all events.
        /// </summary>
        /// <param name="minimumLevel">The minimum level to set.</param>
        void OverrideMinimumLevel(LogEventLevel minimumLevel);


        /// <summary>
        /// Override the minimum level for events from a specific namespace or type name.
        /// </summary>
        /// <param name="source">The (partial) namespace or type name to set the override for.</param>
        /// <param name="minimumLevel">The minimum level to set.</param>
        void OverrideMinimumLevel(string source, LogEventLevel minimumLevel);
    }
}
