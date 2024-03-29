using Serilog.Configuration;

namespace Serilog.PerformanceTests.Support;

static class PropertiesAdderHelper
{
    public static LoggerConfiguration AddManyProperties(this LoggerEnrichmentConfiguration enrich, int numOfProps)
    {
        LoggerConfiguration config = null!;
        for (var i = 0; i < numOfProps; i++)
        {
            config = enrich.WithProperty($"ConfigEnrichProp{i}", $"Value{i}");
        }
        return config;
    }
    public static ILogger AddManyProperties(this ILogger log, int numOfProps)
    {
        for (var i = 0; i < numOfProps; i++)
        {
            log = log.ForContext($"LoggerEnrichProp{i}", $"Value{i}");
        }
        return log;
    }

    public static IDisposable ManyLogContext(int numOfProps)
    {
        var list = new List<IDisposable>(numOfProps);
        for (var i = 0; i < numOfProps; i++)
        {
            list.Add(LogContext.PushProperty($"LogContextProp{i}", $"Value{i}"));
        }
        return new ManyDisposable(list);
    }

    class ManyDisposable : IDisposable
    {
        IList<IDisposable> Disposables { get; }

        public ManyDisposable(IList<IDisposable> disposables)
        {
            Disposables = disposables;
        }

        public void Dispose()
        {
            if (Disposables == null)
                return;

            foreach (var disposable in Disposables.Reverse())
            {
                disposable?.Dispose();
            }
        }
    }
}
