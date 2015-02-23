using System;
using System.Diagnostics;
using System.Linq;
using Serilog.Events;

namespace Serilog.Extras.Timing
{
    /// <summary>
    /// Disposable scope for the timing. Supports explicit completetion messages.
    /// </summary>
    public class TimedScope : ITimedScope
    {
        private const string StartingFormat = "Starting {{TimedScopeId}}: {0}";
        private const string CompletedFormat = "Completed {{TimedScopeId}} in {{TimedScopeElapsed}} ({{TimedScopeElapsedInMs}} ms): {0}";
        private const string CompletedWithWarningFormat = "Completed {{TimedScopeId}} in {{TimedScopeElapsed}} ({{TimedScopeElapsedInMs}} ms), limit {{TimedScopeWarningLimit}} exceeded: {0}";

        private readonly ILogger _logger;
        private readonly object _scopeId;
        private readonly TimeSpan? _warnIfExceeds;
        private LogEventLevel _level;
        private string _messageTemplate;
        private object[] _propertyValues;
        private readonly Stopwatch _sw;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimedScope" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="scopeId">The identifier used for the timing. If non specified, a random guid will be used.</param>
        /// <param name="warnIfExceeds">Specifies a limit, if it takes more than this limit, the level will be set to warning. By default this is not used.</param>
        /// <param name="level">The level used to write the timing operation details to the log. By default this is the information level.</param>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        public TimedScope(ILogger logger, object scopeId, TimeSpan? warnIfExceeds, LogEventLevel level, string messageTemplate, object[] propertyValues)
        {
            _logger = logger;
            _scopeId = scopeId ?? ToShortGuid(Guid.NewGuid());
            _warnIfExceeds = warnIfExceeds;
            _level = level;
            _messageTemplate = messageTemplate;
            _propertyValues = propertyValues;

            propertyValues = new object[_propertyValues.Length + 1];
            _propertyValues.CopyTo(propertyValues, 1);
            propertyValues[0] = _scopeId;

            _logger.Write(level, string.Format(StartingFormat, messageTemplate), propertyValues);

            _sw = Stopwatch.StartNew();
        }

        private static string ToShortGuid(Guid guid, int length = 22)
        {
            var encoded = Convert.ToBase64String(guid.ToByteArray());
            return encoded.Substring(0, length).Replace("/", "").Replace("+", "").Replace("_", "").Replace("-", "");
        }

        /// <summary>
        /// Sets an alternative completetion message.
        /// </summary>
        /// <param name="messageTemplate"></param>
        /// <param name="propertyValues"></param>
        public void SetCompletetionMessage(string messageTemplate, params object[] propertyValues)
        {
            SetCompletetionMessage(_level, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Sets an alternative completetion message and sets the log level to Error.
        /// </summary>
        /// <param name="messageTemplate"></param>
        /// <param name="propertyValues"></param>
        public void SetErrorMessage(string messageTemplate, params object[] propertyValues)
        {
            SetCompletetionMessage(LogEventLevel.Error, messageTemplate, propertyValues);
        }

        /// <summary>
        /// Sets an alternative completetion message and sets the log level to Warning.
        /// </summary>
        /// <param name="messageTemplate"></param>
        /// <param name="propertyValues"></param>
        public void SetWarningMessage(string messageTemplate, params object[] propertyValues)
        {
            SetCompletetionMessage(LogEventLevel.Warning, messageTemplate, propertyValues);
        }

        private void SetCompletetionMessage(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            _level = level;
            _messageTemplate = messageTemplate;
            _propertyValues = propertyValues;
        }

        /// <summary>
        /// Ends the timed scope and triggers the logging of the completetion message.
        /// If no completetion message was specified, the original message is used.
        /// </summary>
        public void Dispose()
        {
            _sw.Stop();

            if (_warnIfExceeds.HasValue && _sw.Elapsed > _warnIfExceeds.Value)
            {
                var propertyValues = new object[_propertyValues.Length + 4];
                _propertyValues.CopyTo(propertyValues, 4);
                propertyValues[0] = _scopeId;
                propertyValues[1] = _sw.Elapsed;
                propertyValues[2] = _sw.ElapsedMilliseconds;
                propertyValues[3] = _warnIfExceeds.Value;

                _logger.Write(_level <= LogEventLevel.Warning ? LogEventLevel.Warning : _level, string.Format(CompletedWithWarningFormat, _messageTemplate), propertyValues);
            }
            else
            {
                var propertyValues = new object[_propertyValues.Length + 3];
                _propertyValues.CopyTo(propertyValues, 3);
                propertyValues[0] = _scopeId;
                propertyValues[1] = _sw.Elapsed;
                propertyValues[2] = _sw.ElapsedMilliseconds;

                _logger.Write(_level, string.Format(CompletedFormat, _messageTemplate), propertyValues);
            }
        }
    }
}
