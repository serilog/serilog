using System;
using Raven.Client;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.RavenDB;

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

namespace Serilog
{
    /// <summary>
    /// Adds the WriteTo.RavenDB() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationRavenDBExtensions
    {
        /// <summary>
        /// Adds a sink that writes log events as documents to a RavenDB database.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="documentStore">A documentstore for a RavenDB database.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        public static LoggerConfiguration RavenDB(
            this LoggerSinkConfiguration loggerConfiguration,
            IDocumentStore documentStore,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            int batchPostingLimit = RavenDBSink.DefaultBatchPostingLimit,
            TimeSpan? period = null,
            IFormatProvider formatProvider = null)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
            if (documentStore == null) throw new ArgumentNullException("documentStore");

            var defaultedPeriod = period ?? RavenDBSink.DefaultPeriod;
            return loggerConfiguration.Sink(
                new RavenDBSink(documentStore, batchPostingLimit, defaultedPeriod, formatProvider),
                restrictedToMinimumLevel);
        }
    }
}
