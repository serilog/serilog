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

using System;
using Glimpse.Core.Extensibility;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.Glimpse
{
    class GlimpseSink : ILogEventSink
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