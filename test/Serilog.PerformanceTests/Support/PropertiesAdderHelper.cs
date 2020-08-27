using System;
using System.Collections.Generic;
using System.Linq;
using Serilog.Configuration;
using Serilog.Context;

namespace Serilog.PerformanceTests.Support
{
    static class PropertiesAdderHelper
    {
        public static LoggerConfiguration AddManyProperties(this LoggerEnrichmentConfiguration enrich, int numOfProps)
        {
            LoggerConfiguration config = null;
            for (int i = 0; i < numOfProps; i++)
            {
                config = enrich.WithProperty($"EnrichProp{i}", $"Value{i}");
            }
            return config;
        }
        public static ILogger AddManyProperties(this ILogger log, int numOfProps)
        {
            for (int i = 0; i < numOfProps; i++)
            {
                log = log.ForContext($"EnrichProp{i}", $"Value{i}");
            }
            return log;
        }

        public static IDisposable ManyLogContext(int numOfProps)
        {
            var list = new List<IDisposable>(numOfProps);
            for (int i = 0; i < numOfProps; i++)
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
                if(Disposables == null)
                    return;

                foreach (var disposable in Disposables.Reverse())
                {
                    disposable?.Dispose();
                }
            }
        }
    }
}