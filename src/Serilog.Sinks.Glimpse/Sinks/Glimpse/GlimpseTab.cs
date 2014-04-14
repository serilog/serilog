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

using System.Linq;
using System.Reflection;
using Glimpse.Core.Extensibility;
using Glimpse.Core.Extensions;
using Glimpse.Core.Tab.Assist;
using Serilog.Events;

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
            context.PersistMessages<LogEventItem>();
        }

        public override object GetData(ITabContext context)
        {
            var plugin = Plugin.Create("Level", "Timestamp", "Message", "Properties");

            foreach (var item in context.GetMessages<LogEventItem>())
            {
                var properties = item.LogEvent.Properties
                        .Select(pv => new { Name = pv.Key, Value = GlimpsePropertyFormatter.Simplify(pv.Value) })
                        .ToList();

                if (item.LogEvent.Exception != null)
                    properties.Add(new { Name = "Exception", Value = (object)item.LogEvent.Exception });

                properties = properties.OrderBy(p => p.Name).ToList();


                var row = plugin.AddRow();
                row.Column(item.LogEvent.Level.ToString());
                row.Column(item.LogEvent.Timestamp.ToString("HH:mm:ss.fff", item.FormatProvider));
                row.Column(item.LogEvent.RenderMessage(item.FormatProvider)).Strong();
                row.Column(properties);

                ApplyRowLevelStyle(item.LogEvent.Level, row);
            }

            return plugin;
        }

        void ApplyRowLevelStyle(LogEventLevel level, TabSectionRow row)
        {
            switch (level)
            {
                case LogEventLevel.Debug:
                    row.Sub();
                    break;
                case LogEventLevel.Warning:
                    row.Warn();
                    break;
                case LogEventLevel.Error:
                    row.Error();
                    break;
                case LogEventLevel.Fatal:
                    row.Fail();
                    break;
            }
        }
    }
}