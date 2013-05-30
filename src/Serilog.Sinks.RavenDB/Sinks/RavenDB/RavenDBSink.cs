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
using System.Threading.Tasks;
using Raven.Client;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;

namespace Serilog.Sinks.RavenDB
{
    /// <summary>
    /// Writes log events as documents to a CouchDB database.
    /// </summary>
    public class RavenDBSink : PeriodicBatchingSink
    {   
        readonly IFormatProvider _formatProvider;
        readonly IDocumentStore _documentStore;

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
        /// <param name="documentStore">A documentstore for a RavenDB database.</param>
        /// <param name="batchPostingLimit">The maximium number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        public RavenDBSink(IDocumentStore documentStore, int batchPostingLimit, TimeSpan period, IFormatProvider formatProvider)
            : base(batchPostingLimit, period)
        {
            if (documentStore == null) throw new ArgumentNullException("documentStore");
            _formatProvider = formatProvider;
            _documentStore = documentStore;
        }

        /// <summary>
        /// Emit a batch of log events, running asynchronously.
        /// </summary>
        /// <param name="events">The events to emit.</param>
        /// <remarks>Override either <see cref="PeriodicBatchingSink.EmitBatch"/> or <see cref="PeriodicBatchingSink.EmitBatchAsync"/>,
        /// not both.</remarks>
        protected override async Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            using (var session = _documentStore.OpenAsyncSession())
            {
                foreach (var logEvent in events)
                {
                    await session.StoreAsync(new LogEventEntity(logEvent, _formatProvider));
                }
                await session.SaveChangesAsync();
            }
        }
    }
}
