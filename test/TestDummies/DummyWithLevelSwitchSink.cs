using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace TestDummies
{
    public class DummyWithLevelSwitchSink : ILogEventSink
    {
        public DummyWithLevelSwitchSink(ILoggingLevelSwitch loggingControlLevelSwitch)
        {
            ControlLevelSwitch = loggingControlLevelSwitch;
        }

        [ThreadStatic]
        public static ILoggingLevelSwitch ControlLevelSwitch;

        [ThreadStatic]
        // ReSharper disable ThreadStaticFieldHasInitializer
        public static List<LogEvent> Emitted = new List<LogEvent>();
        // ReSharper restore ThreadStaticFieldHasInitializer

        public void Emit(LogEvent logEvent)
        {
            Emitted.Add(logEvent);
        }
    }
}
