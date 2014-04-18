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
using System.Linq;
using Serilog.Events;

namespace Serilog.Core.Sinks
{
    class QueueMaxSize<T> : Queue<T>
    {
        public int Size { get; private set; }

        public QueueMaxSize(int size)
        {
            Size = size;
        }

        public new void Enqueue(T obj)
        {
            base.Enqueue(obj);
            lock (this)
            {
                while (base.Count > Size)
                {
                    T outObj = base.Dequeue();
                }
            }
        }
    }

    class BufferedSink : ILogEventSink
    {
        private readonly LogEventLevel _threshHoldLevel;
        private readonly ILogEventSink _copyToSink;

        readonly QueueMaxSize<LogEvent> _queue;

        public BufferedSink(int bufferSize, LogEventLevel threshHoldLevel, ILogEventSink copyToSink)
        {
            if (copyToSink == null) throw new ArgumentNullException("copyToSink");

            _threshHoldLevel = threshHoldLevel;
            _copyToSink = copyToSink;
            _queue = new QueueMaxSize<LogEvent>(bufferSize);
        }

        public void Emit(LogEvent logEvent)
        {
            _queue.Enqueue(logEvent);

            if (logEvent.Level >= _threshHoldLevel)
            {
                var items = _queue.ToList();
                foreach (var item in items)
                {

                    var copy = new LogEvent(
                        item.Timestamp,
                        item.Level,
                        item.Exception,
                        item.MessageTemplate,
                        item.Properties.Select(p => new LogEventProperty(p.Key, p.Value)));

                    _copyToSink.Emit(copy);
                }
              
            }
        }


    }
}