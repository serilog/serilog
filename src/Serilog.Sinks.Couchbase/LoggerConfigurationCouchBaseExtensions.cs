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

using System;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.Couchbase;

namespace Serilog
{
    /// <summary>
    /// Adds the WriteTo.Couchbase() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationCouchbaseExtensions
    {
        /// <summary>
        /// Adds a sink that writes log events as documents to a CouchDB database.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="couchbaseUriList">A list of a Couchbase database servers.</param>
        /// <param name="bucketName">The bucket to store batches in.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        public static LoggerConfiguration Couchbase(
            this LoggerSinkConfiguration loggerConfiguration,
            string[] couchbaseUriList, 
            string bucketName, 
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            int batchPostingLimit = CouchbaseSink.DefaultBatchPostingLimit,
            TimeSpan? period = null,
            IFormatProvider formatProvider = null)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
            if (couchbaseUriList == null) throw new ArgumentNullException("couchbaseUriList");
            if (couchbaseUriList.Length == 0) throw new ArgumentException("couchbaseUriList");
            if (couchbaseUriList[0] == null) throw new ArgumentNullException("couchbaseUri");
            if (bucketName == null) throw new ArgumentNullException("bucketName");

            var defaultedPeriod = period ?? CouchbaseSink.DefaultPeriod;
            return loggerConfiguration.Sink(
                new CouchbaseSink(couchbaseUriList, bucketName, batchPostingLimit, defaultedPeriod, formatProvider),
                restrictedToMinimumLevel);
        }
    }
}
