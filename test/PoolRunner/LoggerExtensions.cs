using Microsoft.Extensions.ObjectPool;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolRunner;

internal static class LoggerExtensions
{
    public static Logger Pooled(this Logger logger, int maximumRetained = 1)
    {
        var provider = new DefaultObjectPoolProvider()
        {
            MaximumRetained = maximumRetained
        };

        var pool = provider.Create(new EventLogPolicy());

        logger.CreateFunc = (timestamp, level, exception, messageTemplate, properties) =>
        {
            var logEvent = pool.Get();
            logEvent.Fill(timestamp, level, exception, messageTemplate, properties);
            return logEvent;
        };

        logger.ReturnFunc = pool.Return;

        return logger;
    }
}
