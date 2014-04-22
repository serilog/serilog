using System;
using System.IO;
using System.Linq;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.MonoTouch
{
	class NSLogSink : ILogEventSink
	{
		readonly ITextFormatter _textFormatter;

		public NSLogSink(ITextFormatter textFormatter)
		{
			if (textFormatter == null) throw new ArgumentNullException("textFormatter");
			_textFormatter = textFormatter;
		}

		public void Emit(LogEvent logEvent)
		{
			if (logEvent == null) throw new ArgumentNullException("logEvent");
			var renderSpace = new StringWriter();
			_textFormatter.Format(logEvent, renderSpace);
			Console.WriteLine (renderSpace.ToString ());
		}
	}
}