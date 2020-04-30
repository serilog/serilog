using System;
using Serilog.Core;
using Serilog.Events;
using TestDummies.Console.Themes;

namespace TestDummies.Console
{
    public class DummyConsoleSink : ILogEventSink
    {
        public DummyConsoleSink(ConsoleTheme? theme = null)
        {
            Theme = theme ?? ConsoleTheme.None;
        }

        [ThreadStatic]
        public static ConsoleTheme? Theme;

        public void Emit(LogEvent logEvent)
        {
        }
    }
}
