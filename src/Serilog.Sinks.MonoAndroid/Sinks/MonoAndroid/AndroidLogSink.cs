using System;
using System.IO;
using System.Linq;
using Android.Util;
using Serilog.Core;
using Serilog.Enrichers;
using Serilog.Events;
using Serilog.Formatting;
using AndroidLog = Android.Util.Log;

namespace Serilog.Sinks.MonoAndroid
{
	public class AndroidLogSink : ILogEventSink
	{
		private readonly ITextFormatter _textFormatter;

		public AndroidLogSink(ITextFormatter textFormatter)
		{
			if (textFormatter == null) throw new ArgumentNullException("textFormatter");
			_textFormatter = textFormatter;
		}

		public void Emit(LogEvent logEvent)
		{
			if (logEvent == null) throw new ArgumentNullException("logEvent");
			var renderSpace = new StringWriter();
			_textFormatter.Format(logEvent, renderSpace);

			var tag = logEvent.Properties.Where(x => x.Key == AndroidLogTagEnricher.TagPropertyName).Select(x => x.Value.ToString()).FirstOrDefault() ?? "";

			switch (logEvent.Level) {
				case LogEventLevel.Debug:
					AndroidLog.Debug(tag, renderSpace.ToString());
					break;

				case LogEventLevel.Information:
					AndroidLog.Info(tag, renderSpace.ToString());
					break;

				case LogEventLevel.Verbose:
					AndroidLog.Verbose(tag, renderSpace.ToString());
					break;

				case LogEventLevel.Warning:
					AndroidLog.Warn(tag, renderSpace.ToString());
					break;

				case LogEventLevel.Error:
					AndroidLog.Error(tag, renderSpace.ToString());
					break;

				case LogEventLevel.Fatal:
					AndroidLog.Wtf(tag, renderSpace.ToString());
					break;

				default:
					AndroidLog.WriteLine(LogPriority.Assert, tag, renderSpace.ToString());
					break;
			}
		}
	}
}