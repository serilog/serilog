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
using System.Threading.Tasks;
using Newtonsoft.Json;
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

        protected override void EmitBatch(IEnumerable<LogEvent> events)
        {
            //TODO: Create formatter possibly format with following
            //TODO: Investigate AsynEmit

            _service.Login(_connectionInfo.UserName, _connectionInfo.Password);

            var receiver = new Receiver(_service);
            receiver.Attach(_receiveSubmitArgs);

            foreach (var logEvent in events)
            {
                dynamic data = new {Timestamp = logEvent.Timestamp, Level = logEvent.Level, Data = logEvent.RenderMessage()};
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(data, new Newtonsoft.Json.Converters.StringEnumConverter());
                receiver.Log(json);
            }
        }
    }
}