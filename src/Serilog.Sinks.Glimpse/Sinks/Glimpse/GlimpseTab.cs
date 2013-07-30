using System;
using System.Dynamic;
using System.Linq;
using Glimpse.Core.Extensibility;
using Glimpse.Core.Extensions;
using Microsoft.SqlServer.Server;
using Serilog.Events;

namespace Serilog.Sinks.Glimpse
{
    public class GlimpseTab : TabBase, IDocumentation, ITabSetup
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
                Exception = msg.LogEvent.Exception,
            };
        }
    }
}