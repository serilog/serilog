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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using Splunk;

namespace Serilog.Sinks.Splunk
{
    /// <summary>
    /// A log event sink that writes to Splunk.
    /// </summary>
    public class SplunkSink : PeriodicBatchingSink
    {
        readonly SplunkConnectionInfo _connectionInfo;

        readonly ReceiverSubmitArgs _receiveSubmitArgs;
        readonly Service _service;

        /// <summary>
        ///     A reasonable default for the number of events posted in
        ///     each batch.
        /// </summary>
        public const int DefaultBatchPostingLimit = 50;

        /// <summary>
        ///     A reasonable default time to wait between checking for event batches.
        /// </summary>
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);

        /// <summary>
        /// Create an instance of the Splunk sink.
        /// </summary>
        /// <param name="batchSizeLimit">The maximum number of log events to send in a single batch.</param>
        /// <param name="period">The time allowed to elapse before a non-empty buffer of events will be flushed.</param>
        /// <param name="connectionInfo">Connection info.</param>
        public SplunkSink(int batchSizeLimit, TimeSpan period, SplunkConnectionInfo connectionInfo)
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

        /// <summary>
        /// Create an instance of the Splunk sink.
        /// </summary>
        /// <param name="batchSizeLimit">The maximum number of log events to send in a single batch.</param>
        /// <param name="period">The time allowed to elapse before a non-empty buffer of events will be flushed.</param>
        /// <param name="connectionInfo">Connection info.</param>
#pragma warning disable 618
        [Obsolete("Please use the concrete SplunkConnectionInfo class instead of ISplunkConnectionInfo.")]
        public SplunkSink(int batchSizeLimit, TimeSpan period, ISplunkConnectionInfo connectionInfo)
            : this(batchSizeLimit, period, new SplunkConnectionInfo {
                ServiceArgs = connectionInfo.ServiceArgs,
                Username = connectionInfo.UserName,
                Password = connectionInfo.Password,
                SplunkEventType = connectionInfo.SplunkEventType,
                SplunkSource = connectionInfo.SplunkSource
            })
#pragma warning restore 618
        {
        }

        /// <summary>
        /// Emit a batch of log events, running to completion synchronously.
        /// </summary>
        /// <param name="events">The events to emit.</param>
        protected override void EmitBatch(IEnumerable<LogEvent> events)
        {
            _service.Login(_connectionInfo.Username, _connectionInfo.Password);

            var receiver = new Receiver(_service);
            receiver.Attach(_receiveSubmitArgs);

            foreach (var logEvent in events)
            {
                var data = new { logEvent.Timestamp, logEvent.Level, Data = logEvent.RenderMessage()};
                var json = JsonConvert.SerializeObject(data, new StringEnumConverter());
                receiver.Log(json);
            }
        }
    }
}