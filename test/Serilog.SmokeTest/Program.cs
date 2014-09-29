using System;

namespace Serilog.SmokeTest
{
    class Program
    {
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .WriteTo.Seq("http://my-seq/", batchPostingLimit: 2000, period: TimeSpan.FromSeconds(2), apiKey: "tqAXx8MEtnnqURHTHzP")
                .CreateLogger();

            var library = typeof(Log).Assembly.GetName();
            Log.Information("Hello {User} from {@Library}!", Environment.UserName, new { library.Name, Version = library.Version.ToString() });
        }
    }
}
