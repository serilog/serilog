using System;
using System.Diagnostics;
using Serilog.Events;

namespace Serilog.Extras.Timing
{
    sealed class TimedOperation : IDisposable
    {
        private readonly ILogger _logger;
        private readonly LogEventLevel _level;
        private readonly TimeSpan? _warnIfExceeds;
        private readonly string _identifier;
        private readonly string _description;
        private Stopwatch _sw;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimedOperation" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="identifier">The identifier used for the timing. If non specified, a random guid will be used.</param>
        /// <param name="description">A description for the operation.</param>
        /// <param name="level">The level used to write the timing operation details to the logger. By default this is the information level.</param>
        /// <param name="warnIfExceeds">Specifies a limit, if it takes more than this limit, the level will be set to warning. By default this is not used.</param>
        public TimedOperation(ILogger logger, LogEventLevel level, TimeSpan? warnIfExceeds, string identifier, string description)
        {
            _logger = logger;
            _level = level;
            _warnIfExceeds = warnIfExceeds;
            _identifier = identifier;
            _description = description;

            _logger.Write(_level, "Beginning operation {TimedOperationId}: {TimedOperationDescription}", _identifier, _description);

            _sw = Stopwatch.StartNew();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _sw.Stop();

            if (_warnIfExceeds.HasValue && _sw.Elapsed > _warnIfExceeds.Value)
                _logger.Write(LogEventLevel.Warning, "Operation {TimedOperationId}: {TimedOperationDescription} exceeded the limit of {WarningLimit} by completing in {TimedOperationElapsed}", _identifier, _description, _warnIfExceeds.Value, _sw.Elapsed);
            else
                _logger.Write(_level, "Completed operation {TimedOperationId}: {TimedOperationDescription} in {TimedOperationElapsed}", _identifier, _description, _sw.Elapsed);
        }
    }
}