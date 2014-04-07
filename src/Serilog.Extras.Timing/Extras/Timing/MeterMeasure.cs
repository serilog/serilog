using System;
using System.Threading;
using System.Threading.Tasks;
using Serilog.Events;

namespace Serilog.Extras.Timing
{
    sealed class MeterMeasure : IMeterMeasure, IDisposable
    {
        private readonly ILogger _logger;
        private readonly string _name;
        private readonly TimeUnit _rateUnit;
        private readonly string _measuring;
        private readonly LogEventLevel _level;
        private readonly string _template;
        private static long _counter = 0;

        private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();

        private static readonly TimeSpan Interval = TimeSpan.FromSeconds(5);

        public MeterMeasure(ILogger logger, string name, string measuring, TimeUnit rateUnit, LogEventLevel level, string template)
        {
            _logger = logger;
            _name = name;
            _rateUnit = rateUnit;
            _measuring = measuring ?? "operations";
            _level = level;
            _template = template;

            Task.Factory.StartNew(async () =>
            {
                while (!_cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(Interval, _cancellationToken.Token);
                    Tick();
                }
            }, _cancellationToken.Token);
        }


        public void Mark(long n = 1)
        {
            var value = Interlocked.Add(ref _counter, n);

            var meanRate = new Rate
            {
                Value = value,
                TimeUnit = _rateUnit.ToString(),
                MeterType = _measuring
            };

            _logger.Write(_level, _template, _name, value, meanRate);
        }

        private void Tick()
        {

        }

        public void Dispose()
        {
            _cancellationToken.Cancel();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Rate
    {
        /// <summary>
        /// The actual value
        /// </summary>
        public Double Value { get; set; }

        /// <summary>
        /// What are we measuring
        /// </summary>
        public string MeterType { get; set; }

        /// <summary>
        /// Unit of time
        /// </summary>
        public string TimeUnit { get; set; }
    }

    /// <summary>
    /// Provides support for timing values
    /// <see href="http://download.oracle.com/javase/6/docs/api/java/util/concurrent/TimeUnit.html"/>
    /// </summary>
    public enum TimeUnit
    {
        /// <summary>
        /// 
        /// </summary>
        Nanoseconds = 0,
        /// <summary>
        /// 
        /// </summary>
        Microseconds = 1,
        /// <summary>
        /// 
        /// </summary>
        Milliseconds = 2,
        /// <summary>
        /// 
        /// </summary>
        Seconds = 3,
        /// <summary>
        /// 
        /// </summary>
        Minutes = 4,
        /// <summary>
        /// 
        /// </summary>
        Hours = 5,
        /// <summary>
        /// 
        /// </summary>
        Days = 6
    }
}