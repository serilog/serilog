using System;
#if ASPNETCORE50
using System.Reflection;
#endif

namespace Serilog.SmokeTest
{
    class Program
    {
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .CreateLogger();

#if !ASPNETCORE50
            var library = typeof(Log).Assembly.GetName();
            Log.Information("Hello {User} from {@Library}!", Environment.UserName, new { library.Name, Version = library.Version.ToString() });
#else
            var library = typeof(Log).GetTypeInfo().Assembly.GetName();
            Log.Information("Hello {User} from {@Library}!", Environment.GetEnvironmentVariable("USERNAME"), new { library.Name, Version = library.Version.ToString() });
#endif
        }
    }
}
