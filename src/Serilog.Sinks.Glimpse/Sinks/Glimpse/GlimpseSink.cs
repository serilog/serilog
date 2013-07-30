using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Glimpse.Core.Extensibility;
using Glimpse.Core.Framework;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.Glimpse
{
    public class GlimpseSink : ILogEventSink
    {
        private readonly Func<IMessageBroker> _messageBrokerFactory;
        private readonly IFormatProvider _formatProvider;

        public GlimpseSink(Func<IMessageBroker> messageBrokerFactory, IFormatProvider formatProvider = null)
        {
            _messageBrokerFactory = messageBrokerFactory;
            _formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
            var messageBroker = _messageBrokerFactory();
            if (messageBroker == null)
                return;

            messageBroker.Publish(new LogEventMessage(logEvent, _formatProvider));
        }

    }
}