// Copyright 2014 Serilog Contriutors
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
// limitations under the License.using System;

using System;
using System.Net.Http;
using Splunk.Client;

namespace Serilog.Sinks.Splunk
{
    /// <summary>
    /// A specialised context relating to splunk that includes the authentication info
    /// </summary>
    public class SplunkContext : Context
    {
        /// <summary>
        /// The user name to authenticate to Splunk via HTTP
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password to authenticate to Splunk via HTTP
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The index to use when logging to splunk
        /// </summary>
        public string Index { get;  set ; }

        /// <summary>
        /// The namespace to log to
        /// </summary>
        public Namespace ResourceNamespace { get;  set; }

        public TransmitterArgs TransmitterArgs { get; set; }

        /// <summary>
        /// Creates an instance of the SplunkViaHttp context
        /// </summary>
        public SplunkContext(Context context, string index, string username, string password, 
            Namespace resourceNamespace = null, 
            TransmitterArgs transmitterArgs = null) 
            :base(context.Scheme, context.Host, context.Port)
        {
            Index = index;
            Username = username;
            Password = password;
            ResourceNamespace = resourceNamespace;
            TransmitterArgs = transmitterArgs;
        }

        /// <summary>
        /// Creates an instance of the SplunkViaHttp context
        /// </summary>
        public SplunkContext(Scheme scheme, string host, int port, string index, string username, string password, TimeSpan timeout, HttpMessageHandler handler, bool disposeHandler = true) : base(scheme, host, port, timeout, handler, disposeHandler)
        {
            Index = index;
            Username = username;
            Password = password;
        }
    }
}