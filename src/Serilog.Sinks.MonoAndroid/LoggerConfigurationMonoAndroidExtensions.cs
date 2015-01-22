using System;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.MonoAndroid;

namespace Serilog
{
	public static class LoggerConfigurationMonoAndroidExtensions
	{
		private const string DefaultAndroidLogOutputTemplate = "[{Level}] {Message:l{NewLine:l}{Exception:l}";

		public static LoggerConfiguration AndroidLog(this LoggerSinkConfiguration sinkConfiguration,
			LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
			string outputTemplate = DefaultAndroidLogOutputTemplate,
			IFormatProvider formatProvider = null)
		{

			if (sinkConfiguration == null)
				throw new ArgumentNullException("sinkConfiguration");

			if (outputTemplate == null)
				throw new ArgumentNullException("outputTemplate");

			var formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);
			return sinkConfiguration.Sink(new AndroidLogSink(formatter), restrictedToMinimumLevel);
		}
	}
}