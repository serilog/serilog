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
using System.Threading.Tasks;
using Serilog.Core;
using Serilog.Events;
using SplunkClient = Splunk.Client;


namespace Serilog.Sinks.Splunk
{
    //TODO: Possibly look at enricher for index
    /// <summary>
    /// A log event sink that writes to SplunkViaHttp.
    /// </summary>
    public class SplunkViaHttpSink : ILogEventSink, IDisposable
    {
        readonly SplunkClient.Context _context;
        readonly string _userName;
        readonly string _password;
        readonly SplunkClient.TransmitterArgs _transmitterArgs;
        readonly IFormatProvider _formatProvider;
        readonly SplunkClient.Service _service;
        string _index;

        /// <summary>
        /// Creates a new instance of the splunk sink
        /// </summary>
        /// <param name="context"></param>
        /// <param name="formatProvider"></param>
        public SplunkViaHttpSink(
            SplunkContext context,
            IFormatProvider formatProvider = null)
            : this(context, context.Index, context.Username, context.Password, context.ResourceNamespace, context.TransmitterArgs, formatProvider)
        {
        }

        /// <summary>
        /// Create an instance of the SplunkViaHttp sink.
        /// </summary>
        /// <param name="context">Connection info.</param>
        /// <param name="index"></param>
        /// <param name="userName">The username to authenticate with</param>
        /// <param name="password">The password to authenticate with</param>
        /// <param name="resourceNamespace"></param>
        /// <param name="transmitterArgs"></param>
        /// <param name="formatProvider"></param>
        public SplunkViaHttpSink(
            SplunkClient.Context context,
            string index,
            string userName,
            string password,
            SplunkClient.Namespace resourceNamespace = null,
            SplunkClient.TransmitterArgs transmitterArgs = null,
            IFormatProvider formatProvider = null
            )
        {
            _context = context;
            _index = index;
            _userName = userName;
            _password = password;
            _transmitterArgs = transmitterArgs;
            _formatProvider = formatProvider;

            _service = resourceNamespace == null
                ? new SplunkClient.Service(_context, new SplunkClient.Namespace("nobody", "search"))
                : new SplunkClient.Service(_context, resourceNamespace);
        }

        /// <inheritdoc/>
        public void Emit(LogEvent logEvent)
        {
            //TODO: Change to continuation
            Task.Factory.StartNew(async () =>
            {
                //Login
                await _service.LogOnAsync(_userName, _password);

                //Ensure that the index has been created
                var index = await _service.Indexes.GetOrNullAsync(_index)
                            ?? await _service.Indexes.CreateAsync(_index);

                var transmitter = _service.Transmitter;

                var message = _formatProvider != null
                    ? logEvent.RenderMessage(_formatProvider)
                    : logEvent.RenderMessage();

                if (_transmitterArgs == null)
                {
                    await transmitter.SendAsync(message, index.Name);
                }
                else
                {
                    await transmitter.SendAsync(message, index.Name, _transmitterArgs);
                }
            });
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _service.Dispose();
        }
    }
}