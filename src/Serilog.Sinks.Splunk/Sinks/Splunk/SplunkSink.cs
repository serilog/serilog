using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using Splunk;

namespace Serilog.Sinks.Splunk
{
    public class SplunkSink : PeriodicBatchingSink
    {
        /// <summary>
        ///     A reasonable default for the number of events posted in
        ///     each batch.
        /// </summary>
        public const int DefaultBatchPostingLimit = 50;

        /// <summary>
        ///     A reasonable default time to wait between checking for event batches.
        /// </summary>
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);

        private readonly ISplunkConnectionInfo _connectionInfo;

        private readonly ReceiverSubmitArgs _receiveSubmitArgs;

        private readonly Service _service;

        /// <summary>
        /// </summary>
        /// <param name="batchSizeLimit"></param>
        /// <param name="period"></param>
        /// <param name="connectionInfo"></param>
        public SplunkSink(int batchSizeLimit, TimeSpan period, ISplunkConnectionInfo connectionInfo)
            : base(batchSizeLimit, period)
        {
            _connectionInfo = connectionInfo;
            _service = new Service(connectionInfo.ServiceArgs);
            _receiveSubmitArgs = new ReceiverSubmitArgs
            {
                Source = _connectionInfo.SplunkSource,
                SourceType = _connectionInfo.SplunkEventType
            };
        }

        protected override Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            return new Task(() =>
            {
                _service.Login(_connectionInfo.UserName, _connectionInfo.Password);
                
                var receiver = new Receiver(_service);

                foreach (var logEvent in events)
                {
                    receiver.Submit(_receiveSubmitArgs, logEvent.RenderMessage());
                }
            });
        }
    }
}