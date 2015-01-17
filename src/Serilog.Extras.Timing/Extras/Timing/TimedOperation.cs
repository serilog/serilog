// Copyright 2014 Serilog Contributors
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
using System.Linq;
using System.Diagnostics;
using Serilog.Events;

namespace Serilog.Extras.Timing
{
	/// <summary>
	/// Timed operation.
	/// </summary>
	public class TimedOperation : IDisposable
	{
		readonly ILogger _logger;
		readonly LogEventLevel _level;
		readonly LogEventLevel _levelExceeds;
		private object[] _propertyValues;

		readonly TimeSpan? _warnIfExceeds;
		readonly object _identifier;
		readonly string _description;
		readonly Stopwatch _sw;

		/// <summary>
		/// The beginning operation template.
		/// </summary>
		public const string BeginningOperationTemplate = "Beginning operation {TimedOperationId}: {TimedOperationDescription}";

		/// <summary>
		/// The completed operation template.
		/// </summary>
		public const string CompletedOperationTemplate = "Completed operation {TimedOperationId}: {TimedOperationDescription} in {TimedOperationElapsed} ({TimedOperationElapsedInMs} ms)";

		/// <summary>
		/// The operation exceeded template.
		/// </summary>
		public const string OperationExceededTemplate = "Operation {TimedOperationId}: {TimedOperationDescription} exceeded the limit of {WarningLimit} by completing in {TimedOperationElapsed}  ({TimedOperationElapsedInMs} ms)";

		readonly string _beginningOperationMessage;
		readonly string _completedOperationMessage;
		readonly string _exceededOperationMessage;

		/// <summary>
		/// Initializes a new instance of the <see cref="TimedOperation" /> class.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="identifier">The identifier used for the timing. If non specified, a random guid will be used.</param>
		/// <param name="description">A description for the operation.</param>
		/// <param name="level">The level used to write the timing operation details to the logger. By default this is the information level.</param>
		/// <param name="warnIfExceeds">Specifies a limit, if it takes more than this limit, the level will be set to warning. By default this is not used.</param>
		/// <param name = "levelExceeds">The level used when the timed operation exceeds the limit set. By default this is Warning.</param>
		/// <param name = "beginningMessage">Template used to indicate the begin of a timed operation. By default it uses the BeginningOperationTemplate.</param>
		/// <param name = "completedMessage">Template used to indicate the completion of a timed operation. By default it uses the CompletedOperationTemplate.</param>
		/// <param name = "exceededOperationMessage">Template used to indicate the exceeding of an operation. By default it uses the OperationExceededTemlate.</param>
		/// <param name = "propertyValues">Additional values to be logged along side the timing data.</param>
		public TimedOperation (ILogger logger, LogEventLevel level, TimeSpan? warnIfExceeds, object identifier, string description, 
		                      LogEventLevel levelExceeds = LogEventLevel.Warning,
		                      string beginningMessage = BeginningOperationTemplate, string completedMessage = CompletedOperationTemplate, string exceededOperationMessage = OperationExceededTemplate,
		                      params object[] propertyValues)
		{
			_logger = logger;
			_level = level;
			_levelExceeds = levelExceeds;
			_warnIfExceeds = warnIfExceeds;
			_identifier = identifier;
			_description = description;
			_propertyValues = propertyValues;

			// Messages
			_beginningOperationMessage = beginningMessage ?? BeginningOperationTemplate;
			_completedOperationMessage = completedMessage ?? CompletedOperationTemplate;
			_exceededOperationMessage = exceededOperationMessage ?? OperationExceededTemplate;

			// Write first message to indicate start
			_logger.Write (_level, _beginningOperationMessage, GeneratePropertyBag (_identifier, _description));

			_sw = Stopwatch.StartNew ();
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public virtual void Dispose ()
		{
			_sw.Stop ();

			if (_warnIfExceeds.HasValue && _sw.Elapsed > _warnIfExceeds.Value)
				_logger.Write (_levelExceeds, _exceededOperationMessage, GeneratePropertyBag (_identifier, _description, _warnIfExceeds.Value, _sw.Elapsed, _sw.ElapsedMilliseconds));
			else
				_logger.Write (_level, _completedOperationMessage, GeneratePropertyBag (_identifier, _description, _sw.Elapsed, _sw.ElapsedMilliseconds));
		}

		/// <summary>
		/// Generates the property bag by combining parameter values with the timed operation values.
		/// </summary>
		/// <returns>The property bag with the combined values.</returns>
		/// <param name="values">Values.</param>
		protected virtual object[] GeneratePropertyBag (params object[] values)
		{
			if (_propertyValues != null)
				return values.Concat (_propertyValues).ToArray ();
			else
				return values;
		
		}
	}
}