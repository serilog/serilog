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
using Serilog.Sinks.PeriodicBatching;
using Serilog.Sinks.XSockets.Sinks.XSockets.Data;
using XSockets.Core.XSocket.Helpers;

namespace Serilog.Sinks.XSockets.Sinks.XSockets
{
    /// <summary>
    /// Sends log events as messages to filtered clients.
    /// </summary>
    public class XSocketsSink : PeriodicBatchingSink
    {
        readonly IFormatProvider _formatProvider;
        readonly LogController _controller;
        /// <summary>
        /// A reasonable default for the number of events posted in
        /// each batch.
        /// </summary>
        public const int DefaultBatchPostingLimit = 5;

        /// <summary>
        /// A reasonable default time to wait between checking for event batches.
        /// </summary>
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);

        /// <summary>
        /// Construct a sink posting to the specified database.
        /// </summary>        
        /// <param name="batchPostingLimit">The maximium number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        public XSocketsSink(int batchPostingLimit, TimeSpan period, IFormatProvider formatProvider)
            : base(batchPostingLimit, period)
        {
            _formatProvider = formatProvider;
            _controller = new LogController();
        }

        /// <summary>
        /// Will send the message to the clients that has the LogEventLevel equal to or lower than the level for each LogEvent
        /// 
        /// Example: 
        /// If the client has set the level to "Information" it will only receive messages for LogEvents where level is "Information" or higher.
        /// </summary>
        /// <param name="events"></param>
        protected override void EmitBatch(IEnumerable<Events.LogEvent> events)
        {
            foreach (var logEvent in events)
            {
                _controller.InvokeTo(p => (int)logEvent.Level >= (int)p.LogEventLevel ,new LogEventWrapper(logEvent, logEvent.RenderMessage(_formatProvider)), "logEvent");
            }        
        }
    }
}
