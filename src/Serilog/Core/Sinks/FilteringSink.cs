// Copyright 2013-2015 Serilog Contributors
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
    class FilteringSink : ILogEventSink
    {
        readonly ILogEventSink _sink;
        readonly bool _propagateExceptions;
        readonly ILogEventFilter[] _filters;

        public FilteringSink(ILogEventSink sink, IEnumerable<ILogEventFilter> filters, bool propagateExceptions)
        {
            if (sink == null) throw new ArgumentNullException(nameof(sink));
            if (filters == null) throw new ArgumentNullException(nameof(filters));
            _sink = sink;
            _propagateExceptions = propagateExceptions;
            _filters = filters.ToArray();
        }

        public Task Emit(LogEvent logEvent)
        {
            Task innerTask = null;
            try
            {
                foreach (var logEventFilter in _filters)
                {
                    if (!logEventFilter.IsEnabled(logEvent))
                    {
                        return CompletedTask.Instance;
                    }
                }

                innerTask = _sink.Emit(logEvent);

                if (innerTask.Status != TaskStatus.RanToCompletion)
                {
                    var innerTaskOnException = innerTask.ContinueWith(t =>
                    {
                        SelfLog.WriteLine("Caught exception while applying filters: {0}", t.Exception);
                    });

                    return _propagateExceptions ? innerTask : innerTaskOnException;
                }
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine("Caught exception while applying filters: {0}", ex);
                if (_propagateExceptions)
                    throw;
            }

            return innerTask ?? CompletedTask.Instance;
        }
    }
}
