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
using System.Net;
using System.Net.Sockets;
using System.Text;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.Splunk
{
    /// <summary>
    /// A sink that logs to Splunk over TCP
    /// </summary>
    public class SplunkViaTcpSink : ILogEventSink, IDisposable
    {
        readonly IPAddress _hostAddress;
        readonly int _port;
        readonly IFormatProvider _formatProvider;
        TcpClient _client;

        /// <summary>
        /// Creates an instance of the Splunk TCP Sink
        /// </summary>
        /// <param name="hostAddress">The Splunk Host</param>
        /// <param name="port">The UDP port configured in Splunk</param>
        /// <param name="formatProvider">Optional format provider</param>
        public SplunkViaTcpSink(IPAddress hostAddress, int port, IFormatProvider formatProvider = null)
        {
            _hostAddress = hostAddress;
            _port = port;
            _formatProvider = formatProvider;

            _client = new TcpClient();
            _client.Connect(hostAddress, port);
        }

        /// <summary>
        /// Creates an instance of the Splunk TCP Sink
        /// </summary>
        /// <param name="host">The Splunk Host</param>
        /// <param name="port">The UDP port configured in Splunk</param>
        /// <param name="formatProvider">Optional format provider</param>
        public SplunkViaTcpSink(string host, int port, IFormatProvider formatProvider = null)
        {
            _port = port;
            _formatProvider = formatProvider;
            _hostAddress = IPAddress.Parse(host);
            _client = new TcpClient();
            _client.Connect(host, port);
        }

        /// <inheritdoc/>
        public void Emit(LogEvent logEvent)
        {
            var message = _formatProvider != null
                ? logEvent.RenderMessage(_formatProvider)
                : logEvent.RenderMessage();

            if (!_client.Connected)
            {
                _client = new TcpClient();
                _client.Connect(_hostAddress, _port);
            }

            //TODO: Quick hack to get TCP working.  Needs a rethink
            using (var networkStream = _client.GetStream())
            {
                var data = Encoding.UTF8.GetBytes(message);
                networkStream.Write(data, 0, data.Length);
                networkStream.Flush();
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _client.Close();
        }
    }
}


