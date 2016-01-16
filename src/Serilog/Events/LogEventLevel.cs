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

namespace Serilog.Events
{
    /// <summary>
    /// Specifies the meaning and relative importance of a log event.
    /// </summary>
    public enum LogEventLevel
    {
        /// <summary>
        /// Anything and everything you might want to know about
        /// a running block of code.
        /// </summary>
        Verbose,

        /// <summary>
        /// Internal system events that aren't necessarily
        /// observable from the outside.
        /// </summary>
        Debug,

        /// <summary>
        /// The lifeblood of operational intelligence - things
        /// happen.
        /// </summary>
        Information,

        /// <summary>
        /// Service is degraded or endangered.
        /// </summary>
        Warning,

        /// <summary>
        /// Functionality is unavailable, invariants are broken
        /// or data is lost.
        /// </summary>
        Error,

        /// <summary>
        /// If you have a pager, it goes off when one of these
        /// occurs.
        /// </summary>
        Fatal
    }
}