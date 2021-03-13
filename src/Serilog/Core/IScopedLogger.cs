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


using Serilog.Events;


namespace Serilog.Core
{
    /// <summary>
    /// An extension interface for providing per-task settings.
    /// </summary>
    public interface IScopedLogger : ILogger
    {
        /// <summary>
        /// Retrieve an object, which can be used to override the minimum logging level.
        /// </summary>
        /// <param name="overrider">The retrieved object</param>
        /// <returns>The same instance to allow call chaining</returns>
        IScopedLogger SetMinimumLevelOverrider(out ILoggingLevelOverrider overrider);


        /// <summary>
        /// Override the minimum level for all events for the current scope.
        /// </summary>
        /// <param name="minimumLevel">The minimum level to set.</param>
        void OverrideMinimumLevel(LogEventLevel minimumLevel);


        /// <summary>
        /// Override the minimum level for events from a specific namespace or type name
        /// for the current scope.
        /// </summary>
        /// <param name="source">The (partial) namespace or type name to set the override for.</param>
        /// <param name="minimumLevel">The minimum level to set.</param>
        void OverrideMinimumLevel(string source, LogEventLevel minimumLevel);
    }
}
