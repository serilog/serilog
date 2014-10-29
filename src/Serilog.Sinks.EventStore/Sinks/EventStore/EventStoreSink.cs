using EventStore.ClientAPI;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serilog.Sinks.EventStore
{
    public class EventStoreSink: PeriodicBatchingSink
    {
        readonly IEventStoreConnection _eventStoreConnection;
        readonly string _streamName;
        readonly IFormatProvider _formatProvider;
        
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
        /// Construct a sink posting to the specified stream.
        /// </summary>
        /// <param name="eventStoreConnection">A connection for the EventStore.</param>
        /// <param name="streamName">The stream to post to.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        public EventStoreSink(IEventStoreConnection eventStoreConnection, string streamName, int batchSizeLimit, TimeSpan period, IFormatProvider formatProvider=null)
            : base(batchSizeLimit, period)
        {
            if (eventStoreConnection == null)
            {
                throw new ArgumentNullException("eventStoreConnection");
            }
            _eventStoreConnection = eventStoreConnection;
            if (streamName == null)
            {
                throw new ArgumentNullException("streamName");
            }
            else if (!streamName.Any())
            {
                throw new ArgumentException("streamName");
            }
            _streamName = streamName;
            _formatProvider = formatProvider;
        }

        protected override async Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            
        }
    }
}
