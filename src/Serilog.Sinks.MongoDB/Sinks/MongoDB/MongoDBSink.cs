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

using System;
using System.Collections.Generic;
using System.IO;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.PeriodicBatching;

namespace Serilog.Sinks.MongoDB
{
    /// <summary>
    /// Writes log events as documents to a CouchDB database.
    /// </summary>
    public class MongoDBSink : PeriodicBatchingSink
    {
        readonly MongoUrl _mongoUrl;

        /// <summary>
        /// A reasonable default for the number of events posted in
        /// each batch.
        /// </summary>
        public const int DefaultBatchPostingLimit = 50;

        /// <summary>
        /// A reasonable default time to wait between checking for event batches.
        /// </summary>
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);

        /// <summary>
        /// Construct a sink posting to the specified database.
        /// </summary>
        /// <param name="databaseUrl">The URL of a CouchDB database.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        public MongoDBSink(string databaseUrl, int batchPostingLimit, TimeSpan period)
            : base(batchPostingLimit, period)
        {
            if (databaseUrl == null) throw new ArgumentNullException("databaseUrl");
            _mongoUrl = new MongoUrl(databaseUrl);
        }

        MongoCollection<BsonDocument> GetLogCollection()
        {
            var mongoClient = new MongoClient(_mongoUrl);
            var server = mongoClient.GetServer();
            var logDb = server.GetDatabase(_mongoUrl.DatabaseName);
            return logDb.GetCollection("log");
        }

        /// <summary>
        /// Emit a batch of log events, running to completion synchronously.
        /// </summary>
        /// <param name="events">The events to emit.</param>
        /// <remarks>Override either <see cref="PeriodicBatchingSink.EmitBatch"/> or <see cref="PeriodicBatchingSink.EmitBatchAsync"/>,
        /// not both.</remarks>
        protected override void EmitBatch(IEnumerable<LogEvent> events)
        {
            var payload = new StringWriter();
            payload.Write("{\"d\":[");

            var formatter = new SimpleJsonFormatter(true);
            var delimStart = "{";
            foreach (var logEvent in events)
            {
                payload.Write(delimStart);
                formatter.Format(logEvent, payload);
                var renderedMessage = logEvent.RenderedMessage;
                payload.Write(",\"UtcTimeStamp\":\"{0:u}\",\"RenderedMessage\":\"{1}\"}}",
                              logEvent.TimeStamp.ToUniversalTime().DateTime,
                              SimpleJsonFormatter.Escape(renderedMessage));
                delimStart = ",{";
            }

            payload.Write("]}");

            var bson = BsonDocument.Parse(payload.ToString());
            var docs = bson["d"].AsBsonArray;
            GetLogCollection().InsertBatch(docs);
        }
    }
}
