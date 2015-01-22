// Copyright 2015 Serilog Contributors
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.IO;
using System.Linq;
using Android.Util;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using AndroidLog = Android.Util.Log;

namespace Serilog.Sinks.MonoAndroid
{
	/// <summary>
	/// Writes events to <see cref="AndroidLog"/>.
	/// </summary>
	public class AndroidLogSink : ILogEventSink
	{
		private readonly ITextFormatter _textFormatter;

		/// <summary>
		/// Create an instance with the provided <see cref="ITextFormatter"/>.
		/// </summary>
		/// <param name="textFormatter">Formatter for log events</param>
		/// <exception cref="ArgumentNullException">The text formatter must be provided</exception>
		public AndroidLogSink(ITextFormatter textFormatter)
		{
			if (textFormatter == null) throw new ArgumentNullException("textFormatter");
			_textFormatter = textFormatter;
		}

        /// <summary>
        /// Emit the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">The log event to write.</param>
        public void Emit(LogEvent logEvent)
		{
			if (logEvent == null) throw new ArgumentNullException("logEvent");
			var renderSpace = new StringWriter();
			_textFormatter.Format(logEvent, renderSpace);

			var tag = logEvent.Properties.Where(x => x.Key == Constants.SourceContextPropertyName).Select(x => x.Value.ToString("l", null)).FirstOrDefault() ?? "";

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