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
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Json;
using Splunk.Logging;

namespace Serilog.Sinks.Splunk
{
    /// <summary>
    /// A sink that logs to Splunk over TCP
    /// </summary>
    public class SplunkViaTcpSink : ILogEventSink, IDisposable
    {
        readonly JsonFormatter _jsonFormatter;
        private TcpSocketWriter _writer;

        /// <summary>
        /// Creates an instance of the Splunk TCP Sink
        /// </summary>
        /// <param name="hostAddress">The Splunk Host</param>
        /// <param name="port">The UDP port configured in Splunk</param>
        /// <param name="formatProvider">Optional format provider</param>
        public SplunkViaTcpSink(
            IPAddress hostAddress,
            int port,
            IFormatProvider formatProvider = null)
        {
            var reconnectionPolicy = new ExponentialBackoffTcpReconnectionPolicy();

            _writer = new TcpSocketWriter(hostAddress, port, reconnectionPolicy, 10000);

            _jsonFormatter = new JsonFormatter(renderMessage: true, formatProvider: formatProvider);
        }

        /// <summary>
        /// Creates an instance of the Splunk TCP Sink
        /// </summary>
        /// <param name="host">The Splunk Host</param>
        /// <param name="port">The UDP port configured in Splunk</param>
        /// <param name="formatProvider">Optional format provider</param>
        public SplunkViaTcpSink(
            string host,
            int port,
            IFormatProvider formatProvider = null)
        {
            var reconnectionPolicy = new ExponentialBackoffTcpReconnectionPolicy();
            var ipAddress = IPAddress.Parse(host);

            _writer = new TcpSocketWriter(ipAddress, port, reconnectionPolicy, 10000);

            _jsonFormatter = new JsonFormatter(renderMessage: true, formatProvider: formatProvider);
        }

        /// <inheritdoc/>
        public void Emit(LogEvent logEvent)
        {
            var sw = new StringWriter();

            _jsonFormatter.Format(logEvent, sw);

            var message = sw.ToString();

            _writer.Enqueue(message);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}

