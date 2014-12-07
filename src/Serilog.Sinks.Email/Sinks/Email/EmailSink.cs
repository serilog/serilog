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
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.PeriodicBatching;

namespace Serilog.Sinks.Email
{
    class EmailSink : PeriodicBatchingSink
    {
        readonly EmailConnectionInfo _connectionInfo;

        readonly SmtpClient _smtpClient;

        readonly ITextFormatter _textFormatter;

        /// <summary>
        /// A reasonable default for the number of events posted in
        /// each batch.
        /// </summary>
        public const int DefaultBatchPostingLimit = 100;

        /// <summary>
        /// A reasonable default time to wait between checking for event batches.
        /// </summary>
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(30);

        /// <summary>
        /// Construct a sink emailing with the specified details.
        /// </summary>
        /// <param name="connectionInfo">Connection information used to construct the SMTP client and mail messages.</param>
        /// <param name="batchSizeLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="textFormatter">Supplies culture-specific formatting information, or null.</param>
        public EmailSink(EmailConnectionInfo connectionInfo, int batchSizeLimit, TimeSpan period, ITextFormatter textFormatter)
            : base(batchSizeLimit, period)
        {
            if (connectionInfo == null) throw new ArgumentNullException("connectionInfo");

            _connectionInfo = connectionInfo;
            _textFormatter = textFormatter;

            _smtpClient = new SmtpClient(connectionInfo.MailServer)
            {
                Credentials = _connectionInfo.NetworkCredentials,
                Port = _connectionInfo.Port,
                EnableSsl = _connectionInfo.EnableSsl
            };
            _smtpClient.SendCompleted += SendCompletedCallback;
        }

        /// <summary>
        /// Reports if there is an error in sending an email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void SendCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                SelfLog.WriteLine("Received failed result {0}: {1}", "Cancelled", e.Error);
            }
            if (e.Error != null)
            {
                SelfLog.WriteLine("Received failed result {0}: {1}", "Error", e.Error);
            }
        }

        /// <summary>
        /// Free resources held by the sink.
        /// </summary>
        /// <param name="disposing">If true, called because the object is being disposed; if false,
        /// the object is being disposed from the finalizer.</param>
        protected override void Dispose(bool disposing)
        {
            // First flush the buffer
            base.Dispose(disposing);

            if (disposing)
                _smtpClient.Dispose();
        }

        /// <summary>
        /// Emit a batch of log events, running asynchronously.
        /// </summary>
        /// <param name="events">The events to emit.</param>
        /// <remarks>Override either <see cref="PeriodicBatchingSink.EmitBatch"/> or <see cref="PeriodicBatchingSink.EmitBatchAsync"/>,
        /// not both.</remarks>
        protected override async Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {

            if (events == null)
                throw new ArgumentNullException("events");
            var payload = new StringWriter();

            foreach (var logEvent in events)
            {
                _textFormatter.Format(logEvent, payload);
            }

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_connectionInfo.FromEmail),
                Subject = _connectionInfo.EmailSubject,
                Body = payload.ToString(),
                BodyEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8
            };

            foreach (var recipient in _connectionInfo.ToEmail.Split(",;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                mailMessage.To.Add(recipient);
            }

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
