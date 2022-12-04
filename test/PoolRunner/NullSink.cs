using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolRunner;

internal class NullSink : ILogEventSink
{
    public void Emit(LogEvent logEvent)
    {
    }
}
