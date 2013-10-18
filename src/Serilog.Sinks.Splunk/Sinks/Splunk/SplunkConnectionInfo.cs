// Copyright 2013 Serilog Contributors
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

using Splunk;

namespace Serilog.Sinks.Splunk
{
    public class SplunkConnectionInfo : ISplunkConnectionInfo
    {
        public ServiceArgs ServiceArgs { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SplunkSource { get; set; }
        public string SplunkEventType { get; set; }
    }
}