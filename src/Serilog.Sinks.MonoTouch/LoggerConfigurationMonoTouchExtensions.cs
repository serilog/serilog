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
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Sinks.MonoTouch;

namespace Serilog
{
	/// <summary>
	/// Adds WriteTo.NSLog() to the logger configuration.
	/// </summary>
	public static class LoggerConfigurationMonoTouchExtensions
	{
		const string DefaultNSLogOutputTemplate = "[{Level}] {Message:l{NewLine:l}{Exception:l}";

	    /// <summary>
	    /// Adds a sink that writes log events to a Azure DocumentDB table in the provided endpoint.
	    /// </summary>
	    /// <param name="sinkConfiguration">The configuration being modified.</param>
	    /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
	    /// <param name="outputTemplate">Template for the output events</param>
	    /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
	    /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
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

