using System.Threading;
using Serilog.Events;

namespace Serilog.Extras.Timing
{
    sealed class CounterMeasure : ICounterMeasure
    {
        private readonly ILogger _logger;
        private readonly string _name;
        private readonly LogEventLevel _level;
        private readonly string _template;
        private static long _value = 0;

        public CounterMeasure(ILogger logger, string name, LogEventLevel level, string template)
        {
            _logger = logger;
            _name = name;
            _level = level;
            _template = template;
        }


        public void Increment()
        {
            var value = Interlocked.Increment(ref _value);

            _logger.Write(_level, _template, _name, value);
        }

        public void Decrement()
        {
            var value = Interlocked.Decrement(ref _value);

            _logger.Write(_level, _template, _name, value);
        }

        public void Reset()
        {
            var value = Interlocked.Exchange(ref _value, 0);

            _logger.Write(_level, _template, _name, value);
        }
    }
}