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
using Splunk;

namespace Serilog.Sinks.Splunk
{
    /// <summary>
    /// Connection information for the connection to Splunk.
    /// </summary>
#pragma warning disable 618
    public class SplunkConnectionInfo : ISplunkConnectionInfo
#pragma warning restore 618
    {
        /// <summary>
        /// The service args.
        /// </summary>
        public ServiceArgs ServiceArgs { get; set; }

        /// <summary>
        /// The username.
        /// </summary>
        [Obsolete("Please use the Username (non-camel-case) property.")]
        public string UserName { get; set; }

        /// <summary>
        /// The username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The source.
        /// </summary>
        public string SplunkSource { get; set; }

        /// <summary>
        /// The event type.
        /// </summary>
        public string SplunkEventType { get; set; }
    }
}