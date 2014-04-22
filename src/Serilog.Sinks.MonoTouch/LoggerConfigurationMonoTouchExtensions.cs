using System;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.MonoTouch;

namespace Serilog
{
	public static class LoggerConfigurationMonoTouchExtensions
	{
		private const string DefaultNSLogOutputTemplate = "[{Level}] {Message:l{NewLine:l}{Exception:l}";

		public static LoggerConfiguration NSLog(this LoggerSinkConfiguration sinkConfiguration,
			LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
			string outputTemplate = DefaultNSLogOutputTemplate,
			IFormatProvider formatProvider = null) {

			if (sinkConfiguration == null)
				throw new ArgumentNullException ("sinkConfiguration");

			if (outputTemplate == null)
				throw new ArgumentNullException ("outputTemplate");

			var formatter = new MessageTemplateTextFormatter (outputTemplate, formatProvider);
			return sinkConfiguration.Sink (new NSLogSink (formatter), restrictedToMinimumLevel);
		}
	}
}

