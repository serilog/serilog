using System;
using Serilog.Events;

namespace Serilog.Extras.Timing
{
    sealed class GaugedMeasure<T> : IGaugeMeasure
    {
        private readonly ILogger _logger;
        private readonly string _name;
        private readonly Func<T> _operation;
        private readonly LogEventLevel _level;
        private readonly string _template;


        public GaugedMeasure(ILogger logger, string name, Func<T> operation, LogEventLevel level, string template)
        {
            _logger = logger;
            _name = name;
            _operation = operation;
            _level = level;
            _template = template;
        }

        public void Measure()
        {
            var value = _operation.Invoke();
            _logger.Write(_level, _template, _name, value);
        }

    }
}