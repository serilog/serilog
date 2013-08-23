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

using System.Collections.Generic;
using System.Linq;
using Glimpse.Core.Extensibility;
using Glimpse.Core.Extensions;

namespace Serilog.Sinks.Glimpse
{
    class GlimpseTab : TabBase, IDocumentation, ITabSetup
    {
        internal static IMessageBroker MessageBroker { get; set; }

        public override string Name
        {
            get { return "Serilog"; }
        }

        public string DocumentationUri
        {
            get
            {
                return "http://serilog.net";
            }
        }

        public void Setup(ITabSetupContext context)
        {
            // Glimpse uses a single-instance message broker. This looks like the best way to get a reference to it. 
            MessageBroker = context.MessageBroker;

            // Will persist any messages raised by the broker to a per-thread store (HttpContext)
            // Messages on non-ASP.NET threads get dumped
            context.PersistMessages<LogEventMessage>();
        }

        public override object GetData(ITabContext context)
        {
            // Retrieve from the per-thread store
            return context.GetMessages<LogEventMessage>().Select(Format).ToArray();

        }

        private object Format(LogEventMessage msg)
        {
            return new
            {
                Level = msg.LogEvent.Level.ToString(),
                Message = msg.LogEvent.RenderMessage(msg.FormatProvider),
                Properties = msg.LogEvent.Properties
                    .OrderBy(p => p.Key)
                    .Select(pv => new { Name = pv.Key, Value = GlimpsePropertyFormatter.Simplify(pv.Value) })
                    .ToList(),
                msg.LogEvent.Exception,
            };
        }
    }
}