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


namespace Serilog.Core
{
    /// <summary>
    /// An extension interface for providing more open level switching.
    /// </summary>
    public interface IOpenSwitchLogger : ILogger
    {
        /// <summary>
        /// Override the minimum level for all events.
        /// </summary>
        /// <param name="overrider">The object used to set minimum level.</param>
        /// <returns>The same instance to allow call chaining.</returns>
        IOpenSwitchLogger MinimumLevelOverride(ILoggingLevelSwitch overrider);


        /// <summary>
        /// Override the minimum level for events from a specific namespace or type name.
        /// </summary>
        /// <param name="source">The (partial) namespace or type name to set the override for.</param>
        /// <param name="overrider">The object used to set minimum level.</param>
        /// <returns>The same instance to allow call chaining.</returns>
        IOpenSwitchLogger MinimumLevelOverride(string source, ILoggingLevelSwitch overrider);
    }
}
