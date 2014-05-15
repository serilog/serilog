using System;
using Serilog;

namespace ElmahIOSample
{


    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Verbose()
             .WriteTo.ColoredConsole()
             .WriteTo.ElmahIO(new Guid(""))  // Enter your own elmah.io log id.
             .CreateLogger();

            Log.Verbose("This app, {ExeName}, demonstrates the basics of using Serilog", "Demo.exe");

            try
            {
                DoBad();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "We did some bad work here.");
            }

            var result = 0;
            var divideBy = 0;
            try
            {
                result = 10 / divideBy;
            }
            catch (Exception e)
            {
                Log.Error(e, "Unable to divide by {divideBy}", divideBy);
            }

            Log.Fatal("That's all folks - and all done using {WorkingSet} bytes of RAM", Environment.WorkingSet);
            Console.ReadKey(true);
        }

        static void DoBad()
        {
            throw new InvalidOperationException("Everything's broken!");
        }


    }
}
