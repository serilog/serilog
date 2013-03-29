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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Serilog.Sinks.MongoDB
{
    /// <summary>
    /// Writes log events as documents to a CouchDB database.
    /// </summary>
    public class MongoDBSink : ILogEventSink, IDisposable
    {
        readonly IMessageTemplateCache _parsedTemplateCache;
        readonly ConcurrentQueue<LogEvent> _queue;
        readonly Timer _timer;
        readonly MongoUrl _mongoUrl;
        const int MaxPost = 50;     // "
        readonly TimeSpan TickInterval = TimeSpan.FromSeconds(2);

        volatile bool _unloading;

        /// <summary>
        /// Construct a sink posting to the specified database.
        /// </summary>
        /// <param name="databaseUrl">The URL of a CouchDB database.</param>
        /// <param name="parsedTemplateCache">Cache for parsed templates.</param>
        public MongoDBSink(string databaseUrl, IMessageTemplateCache parsedTemplateCache)
        {
            if (databaseUrl == null) throw new ArgumentNullException("databaseUrl");
            if (parsedTemplateCache == null) throw new ArgumentNullException("parsedTemplateCache");
            _parsedTemplateCache = parsedTemplateCache;
            _queue = new ConcurrentQueue<LogEvent>();
            _mongoUrl = new MongoUrl(databaseUrl);
            _timer = new Timer(s => OnTick());

            AppDomain.CurrentDomain.DomainUnload += OnAppDomainUnloading;
            AppDomain.CurrentDomain.ProcessExit += OnAppDomainUnloading;
            SetTimer();
        }

        MongoCollection<BsonDocument> LogCollection
        {
            get
            {
                var mongoClient = new MongoClient(_mongoUrl);
                var server = mongoClient.GetServer();
                var logDb = server.GetDatabase(_mongoUrl.DatabaseName);
                return logDb.GetCollection("log");
            }
        }

        void OnAppDomainUnloading(object sender, EventArgs args)
        {
            _unloading = true;
            OnTick();
        }

        void SetTimer()
        {
            _timer.Change(TickInterval, TimeSpan.FromMilliseconds(-1));
        }

        void OnTick()
        {
            var count = 0;
            var events = new Queue<LogEvent>();
            LogEvent next;
            while (count < MaxPost && _queue.TryDequeue(out next))
            {
                count++;
                events.Enqueue(next);
            }

            if (events.Count == 0)
                return;

            var payload = new StringWriter();
            payload.Write("{\"d\":[");

            var formatter = new SimpleJsonFormatter(true);
            var delimStart = "{";
            foreach (var logEvent in events)
            {
                payload.Write(delimStart);
                formatter.Format(logEvent, payload);
                var renderedMessage = _parsedTemplateCache.GetParsedTemplate(logEvent.MessageTemplate).Render(logEvent.Properties);
                payload.Write(",\"UtcTimeStamp\":\"{0:u}\",\"RenderedMessage\":\"{1}\"}}",
                    logEvent.TimeStamp.ToUniversalTime().DateTime,
                    renderedMessage);
                delimStart = ",{";
            }

            payload.Write("]}");

            try
            {
                var bson = BsonDocument.Parse(payload.ToString());
                var docs = bson["d"].AsBsonArray;
                LogCollection.InsertBatch(docs);
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch { }
            // ReSharper restore EmptyGeneralCatchClause
            finally
            {
                if (!_unloading)
                    SetTimer();
            }
        }

        /// <summary>
        /// Emit the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">Log event to emit.</param>
        /// <exception cref="ArgumentNullException">The event is null.</exception>
        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException("logEvent");
            if (!_unloading)
                _queue.Enqueue(logEvent);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            AppDomain.CurrentDomain.DomainUnload -= OnAppDomainUnloading;
            AppDomain.CurrentDomain.ProcessExit -= OnAppDomainUnloading;
        }
    }
}
