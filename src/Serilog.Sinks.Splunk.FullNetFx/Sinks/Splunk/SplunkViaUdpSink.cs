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
using System.Threading;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.Splunk
{
    /// <summary>
    /// A sink that logs to Splunk over Udp
    /// </summary>
    public class SplunkViaUdpSink : ILogEventSink, IDisposable
    {
        IFormatProvider _formatProvider;
        Socket _socket;

        /// <summary>
        /// Creates an instance of the Splunk UDP Sink
        /// </summary>
        /// <param name="hostAddress">The Splunk Host</param>
        /// <param name="port">The UDP port configured in Splunk</param>
        /// <param name="formatProvider">Optional format provider</param>
        public SplunkViaUdpSink(IPAddress hostAddress, int port, IFormatProvider formatProvider = null)
        {
            _formatProvider = formatProvider;

            _socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            _socket.Connect(hostAddress, port);
        }

        /// <summary>
        /// Creates an instance of the Splunk UDP Sink
        /// </summary>
        /// <param name="host">The Splunk Host</param>
        /// <param name="port">The UDP port configured in Splunk</param>
        /// <param name="formatProvider">Optional format provider</param>
        public SplunkViaUdpSink(string host, int port, IFormatProvider formatProvider = null)
        {
            _formatProvider = formatProvider;

            _socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            _socket.Connect(host, port);
        }

        public void Emit(LogEvent logEvent)
        {
            var message = _formatProvider != null
                ? logEvent.RenderMessage(_formatProvider)
                : logEvent.RenderMessage();

            //TODO: Enricher to add index, source and sourcetype?

            _socket.Send(Encoding.UTF8.GetBytes(message));
        }

        public void Dispose()
        {
            _socket.Close();
            _socket.Dispose();
        }
    }
}


