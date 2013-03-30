using System;
using Serilog.Events;
using Serilog.Sinks.MongoDB;

// Copyright 2013 Nicholas Blumhardt
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
    /// Adds the WithCouchDBSink() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationMongoDBExtensions
    {
        /// <summary>
        /// Adds a sink that writes log events as documents to a MongoDb database.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="databaseUrl">The URL of a created CouchDB database that log events will be written to.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        public static LoggerConfiguration WithMongoDBSink(this LoggerConfiguration loggerConfiguration, string databaseUrl, LogEventLevel restrictedToMinimumLevel = LogEventLevel.Minimum)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
            if (databaseUrl == null) throw new ArgumentNullException("databaseUrl");
            return loggerConfiguration.WithSink(new MongoDBSink(databaseUrl, loggerConfiguration.ParsedMessageTemplateCache), restrictedToMinimumLevel);
        }
    }
}
