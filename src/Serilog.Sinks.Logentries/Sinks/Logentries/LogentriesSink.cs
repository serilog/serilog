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
using System.IO;
using System.Linq;
using Serilog.Debugging;
using System.Text;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using Serilog.Sinks.PeriodicBatching;

namespace Serilog.Sinks.Logentries
{
    /// <summary>
    /// Writes log events to the Logentries.com service.
    /// </summary>
    public class LogentriesSink : PeriodicBatchingSink
    {
        readonly string _token;
        bool _useSsl;
        LeClient _client;
        readonly ITextFormatter _textFormatter;

        /// <summary>
        /// UTF-8 output character set.
        /// </summary>
        protected static readonly UTF8Encoding UTF8 = new UTF8Encoding();

        /// <summary>
        /// A reasonable default for the number of events posted in
        /// each batch.
        /// </summary>
        public const int DefaultBatchPostingLimit = 50;

        /// <summary>
        /// A reasonable default time to wait between checking for event batches.
        /// </summary>
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);

        /// <summary>
        /// Construct a sink that saves logs to the specified storage account. Properties are being send as data and the level is used as tag.
        /// </summary>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="token">The input key as found on the Logentries website.</param>
        /// <param name="useSsl">Indicates if you want to use SSL or not.</param>
        public LogentriesSink(string outputTemplate, IFormatProvider formatProvider, string token, bool useSsl, int batchPostingLimit, TimeSpan period)
            : base(batchPostingLimit, period)
        {
            if (outputTemplate == null) throw new ArgumentNullException("outputTemplate");
            _textFormatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);

            _token = token;
            _useSsl = useSsl;
        }

        /// <summary>
        /// Emit a batch of log events, running to completion synchronously.
        /// </summary>
        /// <param name="events">The events to emit.</param>
        /// <remarks>
        /// Override either <see cref="M:Serilog.Sinks.PeriodicBatching.PeriodicBatchingSink.EmitBatch(System.Collections.Generic.IEnumerable{Serilog.Events.LogEvent})" /> or <see cref="M:Serilog.Sinks.PeriodicBatching.PeriodicBatchingSink.EmitBatchAsync(System.Collections.Generic.IEnumerable{Serilog.Events.LogEvent})" />,
        /// not both.
        /// </remarks>
        protected override void EmitBatch(IEnumerable<LogEvent> events)
        {
            if (events.Any() == false)
                return;

            try
            {
                if (_client == null)
                    _client = new LeClient(false, _useSsl);

                _client.Connect();
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine("Unable to connect to Logentries API.", ex);
                return;
            }

            foreach (var logEvent in events)
            {
                var renderSpace = new StringWriter();
                _textFormatter.Format(logEvent, renderSpace);

                var renderedString = renderSpace.ToString();

                // LogEntries uses a NewLine character to determine the end of a log message
                // this causes problems with stack traces.
                if (!string.IsNullOrEmpty(renderedString))
                    renderedString = renderedString.Replace("\n", "");

                var finalLine = _token + renderedString + '\n';

                var data = UTF8.GetBytes(finalLine);

                _client.Write(data, 0, data.Length);
            }

            _client.Flush();
            _client.Close();
        }

        /// <summary>
        /// Dispose the connection.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (_client != null)
            {
                _client.Flush();
                _client.Close();
            }
            base.Dispose(disposing);
        }

    }
}
