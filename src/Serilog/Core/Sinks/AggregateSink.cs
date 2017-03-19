// Copyright 2016 Serilog Contributors
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
using Serilog.Debugging;
using Serilog.Events;
using System.Threading.Tasks;

namespace Serilog.Core.Sinks
{
    class AggregateSink : ILogEventSink
    {
        readonly ILogEventSink[] _sinks;

        public AggregateSink(IEnumerable<ILogEventSink> sinks)
        {
            if (sinks == null) throw new ArgumentNullException(nameof(sinks));
            _sinks = sinks.ToArray();
        }

        public Task Emit(LogEvent logEvent)
        {
            List<Task> taskList = null;
            foreach (var sink in _sinks)
            {
                try
                {
                    var sinkTask = sink.Emit(logEvent);

                    if (sinkTask.Status != TaskStatus.RanToCompletion)
                    {
                        sinkTask.ContinueWith((t, s) =>
                        {
                            SelfLog.WriteLine("Caught exception while emitting to sink {0}: {1}", s, t.Exception);
                        }, sink, TaskContinuationOptions.OnlyOnFaulted);

                        taskList = taskList ?? new List<Task>(_sinks.Length);
                        taskList.Add(sinkTask);
                    }
                }
                catch (Exception ex)
                {
                    SelfLog.WriteLine("Caught exception while emitting to sink {0}: {1}", sink, ex);

                    var errorTaskSource = new TaskCompletionSource<object>();
                    errorTaskSource.SetException(ex);
                    taskList = taskList ?? new List<Task>(_sinks.Length);
                    taskList.Add(errorTaskSource.Task);
                }
            }

            return taskList != null ? Task.WhenAll(taskList) : CompletedTask.Instance;
        }
    }
}
