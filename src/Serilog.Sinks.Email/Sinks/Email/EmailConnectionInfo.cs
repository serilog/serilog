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

using System.ComponentModel;
using System.Net;

namespace Serilog.Sinks.Email
{
    /// <summary>
    /// Connection information for use by the Email sink.
    /// </summary>
    public class EmailConnectionInfo
    {
        /// <summary>
        /// The default port used by for SMTP transfer.
        /// </summary>
        private const int DefaultPort = 25;

        /// <summary>
        /// The default subject used for email messages.
        /// </summary>
        private const string DefaultSubject = "Log Email";

        /// <summary>
        /// Constructs the <see cref="EmailConnectionInfo"/> with the default port and default email subject set.
        /// </summary>
        public EmailConnectionInfo()
        {
            Port = DefaultPort;
            EmailSubject = DefaultSubject;
        }

        /// <summary>
        /// Gets or sets the credentials used for authentication.
        /// </summary>
        public ICredentialsByHost NetworkCredentails { get; set; }

        /// <summary>
        /// Gets or sets the port used for the connection.
        /// Default value is 25.
        /// </summary>
        [DefaultValue(DefaultPort)]
        public int Port { get; set; }

        /// <summary>
        /// The email address emails will be sent from.
        /// </summary>
        public string FromEmail { get; set; }

        /// <summary>
        /// The email address emails will be sent to.
        /// </summary>
        public string ToEmail { get; set; }

        /// <summary>
        /// The subject to use for the email.
        /// </summary>
        [DefaultValue(DefaultSubject)]
        public string EmailSubject { get; set; }

        /// <summary>
        /// Flag as true to use SSL in the SMTP client.
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// The SMTP email server to use.
        /// </summary>
        public string MailServer { get; set; }
    }
}
