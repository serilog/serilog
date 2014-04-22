using System;
using System.Linq;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Enrichers
{
	public class AndroidLogTagEnricher : ILogEventEnricher
	{
		private LogEventProperty _cachedProperty;

		/// <summary>
		/// The property name added to enriched log events.
		/// </summary>
		public const string TagPropertyName = "Tag";

		public readonly string TagPropertyValue;

		public AndroidLogTagEnricher(string tag)
		{
			TagPropertyValue = tag ?? "";
		}

		/// <summary>
		/// Enrich the log event.
		/// </summary>
		/// <param name="logEvent">The log event to enrich.</param>
		/// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
		public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
		{
			_cachedProperty = _cachedProperty ?? propertyFactory.CreateProperty(TagPropertyName, TagPropertyValue);
			logEvent.AddPropertyIfAbsent(_cachedProperty);
		}
	}
}