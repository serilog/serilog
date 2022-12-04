using PoolRunner;
using Serilog;

var log = new LoggerConfiguration()
    .WriteTo.Sink<NullSink>()
    .CreateLogger().Pooled();


int i = 0;
while (true)
{
    //if ((++i % 10000) == 0)
    //{
    //    Console.WriteLine("Iteration completed, press key");
    //    Console.ReadKey();
    //}

    log.Information("{Number} Hello, Serilog!", i);
}
