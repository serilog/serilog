using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace TestDummies
{
    public class DummyWithLevelSwitchSink : ILogEventSink
    {
        public DummyWithLevelSwitchSink(LoggingLevelSwitch loggingControlLevelSwitch)
        {
            ControlLevelSwitch = loggingControlLevelSwitch;
        }

        [ThreadStatic]
        public static LoggingLevelSwitch ControlLevelSwitch;

        [ThreadStatic]
        public static List<LogEvent> Emitted = new List<LogEvent>();

        public void Emit(LogEvent logEvent)
        {
            Emitted.Add(logEvent);
        }
    }
}
