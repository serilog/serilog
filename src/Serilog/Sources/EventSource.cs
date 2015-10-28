using System;
using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sources
{

	/// <summary>
	///		Provides the ability to create semantic and strongly typed logging sources
	/// </summary>
	public abstract class EventSource : ILogger
	{

		private readonly ILogger _sourceLogger;

		/// <summary>
		///		Creates a new <see cref="EventSource"/>
		/// </summary>
		protected EventSource()
			: this(new LoggerConfiguration())
		{
			
		}

		/// <summary>
		///		Creates a new <see cref="EventSource"/> using an existing <see cref="LoggerConfiguration"/>
		/// </summary>
		protected EventSource(LoggerConfiguration configuration)
		{
			if(configuration == null)
				throw new ArgumentNullException("configuration");
			OnBuildUp(configuration);

			_sourceLogger = configuration.CreateLogger();
		}

		/// <summary>
		/// Create a logger that enriches log events via the provided enrichers.
		/// </summary>
		/// <param name="enrichers">Enrichers that apply in the context.</param>
		/// <returns>A logger that will enrich log events as specified.</returns>
		public ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers)
		{
			return _sourceLogger != null ? _sourceLogger.ForContext(enrichers) : this;
		}

		/// <summary>
		/// Create a logger that enriches log events with the specified property.
		/// </summary>
		/// <returns>A logger that will enrich log events as specified.</returns>
		public ILogger ForContext(string propertyName, object value, bool destructureObjects = false)
		{
			return _sourceLogger != null ? _sourceLogger.ForContext(propertyName, value, destructureObjects) : this;
		}

		/// <summary>
		/// Create a logger that marks log events as being from the specified
		/// source type.
		/// </summary>
		/// <typeparam name="TSource">Type generating log messages in the context.</typeparam>
		/// <returns>A logger that will enrich log events as specified.</returns>
		public ILogger ForContext<TSource>()
		{
			return _sourceLogger != null ? _sourceLogger.ForContext<TSource>() : this;
		}

		/// <summary>
		/// Create a logger that marks log events as being from the specified
		/// source type.
		/// </summary>
		/// <param name="source">Type generating log messages in the context.</param>
		/// <returns>A logger that will enrich log events as specified.</returns>
		public ILogger ForContext(Type source)
		{
			return _sourceLogger != null ? _sourceLogger.ForContext(source) : this;
		}

		/// <summary>
		/// Write an event to the log.
		/// </summary>
		/// <param name="logEvent">The event to write.</param>
		public void Write(LogEvent logEvent)
		{
			if(_sourceLogger == null)
				return;

			_sourceLogger.Write(logEvent);
		}

		/// <summary>
		/// Write a log event with the specified level.
		/// </summary>
		/// <param name="level">The level of the event.</param>
		/// <param name="messageTemplate"></param>
		/// <param name="propertyValues"></param>
		public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
		{
			if (_sourceLogger == null)
				return;

			_sourceLogger.Write(level, messageTemplate, propertyValues);
		}

		/// <summary>
		/// Write a log event with the specified level and associated exception.
		/// </summary>
		/// <param name="level">The level of the event.</param>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		public void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
		{
			if (_sourceLogger == null)
				return;

			_sourceLogger.Write(level, exception, messageTemplate, propertyValues);
		}

		/// <summary>
		/// Determine if events at the specified level will be passed through
		/// to the log sinks.
		/// </summary>
		/// <param name="level">Level to check.</param>
		/// <returns>True if the level is enabled; otherwise, false.</returns>
		public bool IsEnabled(LogEventLevel level)
		{
			return _sourceLogger != null && _sourceLogger.IsEnabled(level);
		}

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example>
		/// Log.Verbose("Staring into space, wondering if we're alone.");
		/// </example>
		public void Verbose(string messageTemplate, params object[] propertyValues)
		{
			if (_sourceLogger == null)
				return;

			_sourceLogger.Verbose(messageTemplate, propertyValues);
		}

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example>
		/// Log.Verbose(ex, "Staring into space, wondering where this comet came from.");
		/// </example>
		public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
		{
			if (_sourceLogger == null)
				return;

			_sourceLogger.Verbose(exception, messageTemplate, propertyValues);
		}

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example>
		/// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
		/// </example>
		public void Debug(string messageTemplate, params object[] propertyValues)
		{
			if (_sourceLogger == null)
				return;

			_sourceLogger.Debug(messageTemplate, propertyValues);
		}

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example>
		/// Log.Debug(ex, "Swallowing a mundane exception.");
		/// </example>
		public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
		{
			if (_sourceLogger == null)
				return;

			_sourceLogger.Debug(exception, messageTemplate, propertyValues);
		}

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example>
		/// Log.Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
		/// </example>
		public void Information(string messageTemplate, params object[] propertyValues)
		{
			if (_sourceLogger == null)
				return;

			_sourceLogger.Information(messageTemplate, propertyValues);
		}

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example>
		/// Log.Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
		/// </example>
		public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
		{
			if (_sourceLogger == null)
				return;

			_sourceLogger.Information(exception, messageTemplate, propertyValues);
		}

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Warning"/> level and associated exception.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example>
		/// Log.Warning("Skipped {SkipCount} records.", skippedRecords.Length);
		/// </example>
		public void Warning(string messageTemplate, params object[] propertyValues)
		{
			if (_sourceLogger == null)
				return;

			_sourceLogger.Warning(messageTemplate, propertyValues);
		}

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Warning"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example>
		/// Log.Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
		/// </example>
		public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
		{
			if (_sourceLogger == null)
				return;

			_sourceLogger.Warning(exception, messageTemplate, propertyValues);
		}

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example>
		/// Log.Error("Failed {ErrorCount} records.", brokenRecords.Length);
		/// </example>
		public void Error(string messageTemplate, params object[] propertyValues)
		{
			if (_sourceLogger == null)
				return;

			_sourceLogger.Error(messageTemplate, propertyValues);
		}

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example>
		/// Log.Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
		/// </example>
		public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
		{
			if (_sourceLogger == null)
				return;

			_sourceLogger.Error(exception, messageTemplate, propertyValues);
		}

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example>
		/// Log.Fatal("Process terminating.");
		/// </example>
		public void Fatal(string messageTemplate, params object[] propertyValues)
		{
			if (_sourceLogger == null)
				return;

			_sourceLogger.Fatal(messageTemplate, propertyValues);
		}

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example>
		/// Log.Fatal(ex, "Process terminating.");
		/// </example>
		public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
		{
			if (_sourceLogger == null)
				return;

			_sourceLogger.Fatal(exception, messageTemplate, propertyValues);
		}

		/// <summary>
		///		This method is called when initializing the <see cref="EventSource"/> and allows
		///		to override the default logger configuration in order to provide custom
		///		filters, sinks and enrichers for the event source
		/// </summary>
		protected virtual void OnBuildUp(LoggerConfiguration configuration)
		{
			
		}

	}
}
