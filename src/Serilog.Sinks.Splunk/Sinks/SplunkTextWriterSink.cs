// Copyright 2013 Serilog Contributors
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
using System.Text;
using System.Threading.Tasks;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using Splunk;

namespace Serilog.Sinks.Splunk.Sinks
{
    public class SplunkTextWriterSink : ILogEventSink
    {
        public void Emit(LogEvent logEvent)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface ISplunkConnectionInfo
    {
        ServiceArgs ServiceArgs { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string SplunkSource { get; set; }
        string SplunkEventType { get; set; }
    }

    public class SplunkConnectionInfoInfo : ISplunkConnectionInfo
    {
        public ServiceArgs ServiceArgs { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SplunkSource { get; set; }
        public string SplunkEventType { get; set; }

    }

    public class SplunkSink : PeriodicBatchingSink
    {
        private readonly ISplunkConnectionInfo _connectionInfo;

        /// <summary>
        /// A reasonable default for the number of events posted in
        /// each batch.
        /// </summary>
        public const int DefaultBatchPostingLimit = 50;

        /// <summary>
        /// A reasonable default time to wait between checking for event batches.
        /// </summary>
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);

        private readonly Service _service;
        private readonly ReceiverSubmitArgs _receiveSubmitArgs;

        public SplunkSink(int batchSizeLimit, TimeSpan period, ISplunkConnectionInfo connectionInfo)
            : base(batchSizeLimit, period)
        {
            _connectionInfo = connectionInfo;
            _service = new Service(connectionInfo.ServiceArgs);
            _receiveSubmitArgs = new ReceiverSubmitArgs { Source = _connectionInfo.SplunkSource, SourceType = _connectionInfo.SplunkEventType };
        }

        protected override Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            _service.Login(_connectionInfo.UserName, _connectionInfo.Password);
            var receiver = new Receiver(_service);

            //TODO: make awaitable
            //TODO: create formatters
            foreach (var logEvent in events)
            {
                receiver.Submit(_receiveSubmitArgs, logEvent.ToString());
            }
            return new Task(() => { });
        }
    }

}