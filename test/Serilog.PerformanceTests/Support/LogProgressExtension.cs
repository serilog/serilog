using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Serilog.PerformanceTests.Support
{
    public static class LogProgressExtension
    {
        public static IEnumerable<TSource> LogProgress<TSource>(this ICollection<TSource> source, ILogger logger, int logAfterXItems = 500, string messageTemplate = "Job Progress...")
        {
            return LogProgress(source, logger, source.Count, logAfterXItems, messageTemplate);
        }

        public static IEnumerable<TSource> LogProgress<TSource>(this IEnumerable<TSource> source, ILogger logger, long totalNumberOfItems, int logAfterXItems = 500, string messageTemplate = "Job Progress...")
        {
            return source.Select((item, idx) =>
            {
                var progress = idx + 1;
                LogProgress(logger, progress, totalNumberOfItems, logAfterXItems, messageTemplate);

                return item;
            });
        }

        public static void LogProgress(this ILogger logger, long currentProgress, long totalNumberOfItems, int logAfterXItems = 500, string messageTemplate = "Job Progress...")
        {
            var progress = currentProgress;
            if (progress == 1 || progress % logAfterXItems == 0 || progress == totalNumberOfItems)
            {
                logger.ForContext("ProgressEntry", true).Debug($"{messageTemplate} {{Current}} of {{Total}} - {{Percentage:P1}}", progress, totalNumberOfItems, (progress / (double)totalNumberOfItems));
            }
        }
    }
}
