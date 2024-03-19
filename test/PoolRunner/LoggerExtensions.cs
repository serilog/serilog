using Microsoft.Extensions.ObjectPool;
using Serilog.Core;

namespace PoolRunner;

internal static class LoggerExtensions
{
    public static Logger Pooled(this Logger logger, int maximumRetained = 1)
    {
        var provider = new DefaultObjectPoolProvider
        {
            MaximumRetained = maximumRetained
        };

        var pool = provider.Create(new EventLogPolicy());

        logger.BeforeDispatch = (timestamp, level, exception, messageTemplate, properties, traceId, spanId) =>
        {
            var logEvent = pool.Get();
            logEvent.Fill(timestamp, level, exception, messageTemplate, properties, traceId, spanId);
            return logEvent;
        };

        logger.AfterDispatch = pool.Return;

        return logger;
    }
}
