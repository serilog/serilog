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

           
        }

        private void Tick()
        {

        }

        public void Dispose()
        {
            _cancellationToken.Cancel();

            var value = Interlocked.Read(ref _counter);

            var meanRate = new Rate
            {
                Value = value,
                TimeUnit = Abbreviate(_rateUnit),
                MeterType = _measuring
            };

            _logger.Write(_level, _template, _name, value, meanRate);
        }

        static string Abbreviate(TimeUnit unit)
        {
            switch (unit)
            {
                case TimeUnit.Nanoseconds:
                    return "ns";
                case TimeUnit.Microseconds:
                    return "us";
                case TimeUnit.Milliseconds:
                    return "ms";
                case TimeUnit.Seconds:
                    return "s";
                case TimeUnit.Minutes:
                    return "m";
                case TimeUnit.Hours:
                    return "h";
                case TimeUnit.Days:
                    return "d";
                default:
                    throw new ArgumentOutOfRangeException("unit");
            }
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

        /// <summary>
        /// Returns a formatted representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0} {1}/{2}", Value, MeterType, TimeUnit);
        }

       
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