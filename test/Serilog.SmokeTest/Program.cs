using System;

namespace Serilog.SmokeTest
{
    class Program
    {
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            var library = typeof(Log).Assembly.GetName();
            Log.Information("Hello {User} from {@Library}!", Environment.UserName, new { library.Name, Version = library.Version.ToString() });
        }
    }
}
