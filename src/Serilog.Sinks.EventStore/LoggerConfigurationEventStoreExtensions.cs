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

using EventStore.ClientAPI;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.EventStore;
using System;

namespace Serilog
{
    /// <summary>
    /// Adds the WriteTo.EventStore() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationEventStoreExtensions
    {
        public static LoggerConfiguration EventStore(
            this LoggerSinkConfiguration loggerConfiguration,
            IEventStoreConnection connection,
string streamName,
LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            int batchPostingLimit = EventStoreSink.DefaultBatchPostingLimit,
            TimeSpan? period = null,
            IFormatProvider formatProvider = null)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
            if (connection== null) throw new ArgumentNullException("connection");

            var defaultedPeriod = period ?? EventStoreSink.DefaultPeriod;
            return loggerConfiguration.Sink(
                new EventStoreSink(connection, streamName, batchPostingLimit, defaultedPeriod, formatProvider),
                restrictedToMinimumLevel);
        }
    }
}