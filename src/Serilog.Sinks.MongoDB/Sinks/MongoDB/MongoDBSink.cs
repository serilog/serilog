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
    /// Writes log events as documents to a MongoDB database.
    /// </summary>
    public class MongoDBSink : PeriodicBatchingSink
    {
        readonly string _collectionName;
        readonly IMongoCollectionOptions _collectionCreationOptions;
        readonly IFormatProvider _formatProvider;
        readonly MongoDatabase _mongoDatabase;

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
        /// The default name for the log collection.
        /// </summary>
        public static readonly string DefaultCollectionName = "log";

        /// <summary>
        /// Construct a sink posting to the specified database.
        /// </summary>
        /// <param name="databaseUrl">The URL of a MongoDB database.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="collectionName">Name of the MongoDb collection to use for the log. Default is "log".</param>
        /// <param name="collectionCreationOptions">Collection Creation Options for the log collection creation.</param>
        public MongoDBSink(string databaseUrl, int batchPostingLimit, TimeSpan period, IFormatProvider formatProvider, string collectionName, IMongoCollectionOptions collectionCreationOptions)
            : this (DatabaseFromMongoUrl(databaseUrl), batchPostingLimit, period, formatProvider, collectionName, collectionCreationOptions)
        {
        }

        /// <summary>
        /// Construct a sink posting to a specified database.
        /// </summary>
        /// <param name="database">The MongoDB database.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="collectionName">Name of the MongoDb collection to use for the log. Default is "log".</param>
        /// <param name="collectionCreationOptions">Collection Creation Options for the log collection creation.</param>
        public MongoDBSink(MongoDatabase database, int batchPostingLimit, TimeSpan period, IFormatProvider formatProvider, string collectionName, IMongoCollectionOptions collectionCreationOptions)
            : base(batchPostingLimit, period)
        {
            if (database == null) throw new ArgumentNullException("database");

            _mongoDatabase = database;
            _collectionName = collectionName;
            _collectionCreationOptions = collectionCreationOptions;
            _formatProvider = formatProvider;
        }

        /// <summary>
        /// Get the MongoDatabase for a specified database url
        /// </summary>
        /// <param name="databaseUrl">The URL of a MongoDB database.</param>
        /// <returns>The Mongodatabase</returns>
        private static MongoDatabase DatabaseFromMongoUrl (string databaseUrl)
        {
            if (databaseUrl == null) throw new ArgumentNullException("databaseUrl");

            var mongoUrl = new MongoUrl(databaseUrl);
            var mongoClient = new MongoClient(mongoUrl);
            var server = mongoClient.GetServer();
            return server.GetDatabase(mongoUrl.DatabaseName);
        }


        MongoCollection<BsonDocument> GetLogCollection()
        {
            VerifyCollection();
            return _mongoDatabase.GetCollection(_collectionName);
        }

        /// <summary>
        /// Verifies the the MongoDatabase collection exists or creates it if it doesn't.
        /// </summary>
        protected void VerifyCollection()
        {
            if (!_mongoDatabase.CollectionExists(_collectionName))
            {
                _mongoDatabase.CreateCollection(_collectionName, _collectionCreationOptions);
            }
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

            var formatter = new JsonFormatter(
                omitEnclosingObject: true,
                renderMessage: true,
                formatProvider: _formatProvider);

            var delimStart = "{";
            foreach (var logEvent in events)
            {
                payload.Write(delimStart);
                formatter.Format(logEvent, payload);
                payload.Write(",\"UtcTimestamp\":\"{0:u}\"}}",
                              logEvent.Timestamp.ToUniversalTime().DateTime);
                delimStart = ",{";
            }

            payload.Write("]}");

            var bson = BsonDocument.Parse(payload.ToString());
            var docs = bson["d"].AsBsonArray;
            GetLogCollection().InsertBatch(docs);
        }
    }
}
