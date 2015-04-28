using System;
using Serilog.Core;

namespace Serilog.Sinks.RollingFile
{
    interface IRollingFileSink : ILogEventSink, IDisposable
    {
    }
}