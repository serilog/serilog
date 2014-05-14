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
using Loggr;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.Loggr
{
    /// <summary>
    /// Writes log events to the loggr.com service.
    /// </summary>
    public class LoggrSink : ILogEventSink
    {
        readonly IFormatProvider _formatProvider;
        private readonly string _userNameProperty;
        private readonly LogClient _client;

        /// <summary>
        /// Construct a sink that saves logs to the specified storage account. Properties are being send as data and the level is used as tag.
        /// </summary>
        ///  <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="logKey">The log key as found on the loggr website.</param>
        /// <param name="apiKey">The api key as found on the loggr website.</param>
        /// <param name="useSecure">Use a SSL connection. By default this is false.</param>
        /// <param name="userNameProperty">Specifies the property name to read the username from. By default it is UserName.</param>
        public LoggrSink(IFormatProvider formatProvider, string logKey, string apiKey, bool useSecure = false, string userNameProperty = "UserName")
        {
            _formatProvider = formatProvider;
            _userNameProperty = userNameProperty;

            _client = new LogClient(logKey, apiKey, useSecure);
        }

        /// <summary>
        /// Emit the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">The log event to write.</param>
        public void Emit(LogEvent logEvent)
        {
            // Create a new FluentEvent for Loggr based on the exception or the properties.
            var ev = logEvent.Exception != null
                ? global::Loggr.Events.CreateFromException(logEvent.Exception)
                : global::Loggr.Events.CreateFromVariable(logEvent.Properties);

            ev.Text(logEvent.RenderMessage(_formatProvider));
            ev.AddTags(logEvent.Level.ToString());

            if (!String.IsNullOrWhiteSpace(_userNameProperty) && logEvent.Properties.ContainsKey(_userNameProperty) && logEvent.Properties[_userNameProperty] != null)
            {
                ev.User(logEvent.Properties[_userNameProperty].ToString());
            }

            ev.UseLogClient(_client);
            ev.Post(true);

        }
    }
}
