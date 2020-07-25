// Copyright 2019 Serilog Contributors
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

using Serilog.Debugging;
using System;
using System.Collections.Generic;

namespace Serilog.Core.Sinks
{
    class DisposingSafeAggregateSink : SafeAggregateSink, IDisposable
    {
        public DisposingSafeAggregateSink(IEnumerable<ILogEventSink> sinks)
            : base(sinks)
        {
        }

        public void Dispose()
        {
            var sinks = _sinks;
            if (sinks != null)
            {
                foreach (var sink in sinks)
                {
                    if (sink is IDisposable disposable)
                    {
                        try
                        {
                            disposable.Dispose();
                        }
                        catch (Exception ex)
                        {
                            SelfLog.WriteLine("Caught exception while disposing sink {0}: {1}", sink, ex);
                        }
                    }
                }
            }
        }
    }
}
