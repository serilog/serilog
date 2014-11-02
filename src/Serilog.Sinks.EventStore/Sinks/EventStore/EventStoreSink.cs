using EventStore.ClientAPI;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Serilog.Sinks.EventStore
{
    using System.Runtime.InteropServices;
    using Newtonsoft.Json;

    public class EventStoreSink: PeriodicBatchingSink
    {
        readonly IEventStoreConnection _eventStoreConnection;
        readonly string _streamName;
        readonly IFormatProvider _formatProvider;
        private const string EventClrTypeHeader = "EventClrTypeName";
        private const string AggregateClrTypeHeader = "AggregateClrTypeName";
        private const string CommitIdHeader = "CommitId";

        /// <summary>
        /// A reasonable default for the number of events posted in
        /// each batch.
        /// </summary>
        public const int DefaultBatchPostingLimit = 50;

        /// <summary>
        /// A reasonable default time to wait between checking for event batches.
        /// </summary>
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);
        private static JsonSerializerSettings SerializerSettings;

        static EventStoreSink()
        {
            SerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };
        }
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
            _eventStoreConnection.ConnectAsync();
        }

        protected override async Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            var commitHeaders = new Dictionary<string, object>
            {
                {CommitIdHeader, Guid.NewGuid()},
                {AggregateClrTypeHeader, this.GetType().AssemblyQualifiedName}
            };
            int version = default(int);
            //Read the last event.
            EventReadResult result = await _eventStoreConnection.ReadEventAsync(_streamName, int.MaxValue-1, false);
            if (result.Status == EventReadStatus.NoStream)
            {
                version = ExpectedVersion.NoStream;
            }
            version = result.EventNumber;
            var leq = from e in events select new LogEntryEmittedEvent(e, e.RenderMessage(_formatProvider));
            var eventsToSave = leq.Select(e => ToEventData(Guid.NewGuid(), e, commitHeaders));
            await _eventStoreConnection.AppendToStreamAsync(_streamName, version, eventsToSave);
        }

        private static EventData ToEventData(Guid eventId, LogEntryEmittedEvent evnt, Dictionary<string, object> commitHeaders)
        {
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evnt, SerializerSettings));

            var eventHeaders = new Dictionary<string, object>(commitHeaders)
            {
                {
                    EventClrTypeHeader, evnt.GetType().AssemblyQualifiedName
                }
            };
            var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventHeaders, SerializerSettings));
            var typeName = evnt.GetType().Name;

            return new EventData(eventId, typeName, true, data, metadata);
        }
    }
}