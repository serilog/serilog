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
    /// Provides filtering of the log event stream.
    /// </summary>
    public interface ILogEventFilter
    {
        /// <summary>
        /// Returns true if the provided event is enabled. Otherwise, false.
        /// </summary>
        /// <param name="logEvent">The event to test.</param>
        /// <returns>True if the event is enabled by this filter. If false
        /// is returned, the event will not be emitted.</returns>
        bool IsEnabled(LogEvent logEvent);
    }
}
