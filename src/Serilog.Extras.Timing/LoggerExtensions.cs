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
using Serilog.Events;
using Serilog.Extras.Timing;
using System.Collections.Concurrent;

namespace Serilog
{
    /// <summary>
    /// Extends <see cref="LoggerConfiguration"/> to add Full .NET Framework 
    /// capabilities.
    /// </summary>
    public static class LoggerExtensions
    {

		/// <summary>
		/// The default gauge template.
		/// </summary>
        public const string DefaultGaugeTemplate = "{GaugeName} value = {GaugeValue} {GaugeUnit:l}";
        
		/// <summary>
		/// The default count template.
		/// </summary>
		public const string DefaultCountTemplate = "{CounterName} count = {CounterValue} {CounterUnit:l}";

		/// <summary>
		/// The default meter template.
		/// </summary>
		public const string DefaultMeterTemplate = "{MeterName} count = {CounterValue}, mean rate {MeanRate:l}, 1 minute rate {OneMinuteRate:l}, 5 minute rate {FiveMinuteRate:l}, 15 minute rate {FifteenMinuteRate:l}";

		/// <summary>
		/// The default health template.
		/// </summary>
		public const string DefaultHealthTemplate = "Health check {HealthCheckName} result is {HealthCheckMessage}.";

        /// <summary>
        /// Begins an operation by placing the code to be timed inside a using block. 
        /// When the block is being exited, the time it took is logged.
		/// 
		/// In addition you can specify a warning limit. If it takes more time to execute the code than the specified limit, another message will be logged.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="identifier">The identifier used for the timing. If non specified, a random guid will be used.</param>
        /// <param name="description">A description for this operation.</param>
        /// <param name="level">The level used to write the timing operation details to the log. By default this is the information level.</param>
        /// <param name="warnIfExceeds">Specifies a limit, if it takes more than this limit, the level will be set to warning. By default this is not used.</param>
		/// <param name = "levelExceeds">The level used when the timed operation exceeds the limit set. By default this is Warning.</param>
		/// <param name = "beginningMessage">Template used to indicate the begin of a timed operation. By default it uses the BeginningOperationTemplate.</param>
		/// <param name = "completedMessage">Template used to indicate the completion of a timed operation. By default it uses the CompletedOperationTemplate.</param>
		/// <param name = "exceededOperationMessage">Template used to indicate the exceeding of an operation. By default it uses the OperationExceededTemlate.</param>
		/// <returns>A disposable object. Wrap this inside a using block so the dispose can be called to stop the timing.</returns>
		/// <example>
		/// See the example how to wrap 
		/// <code>
		/// using (logger.BeginTimedOperation("Time a thread sleep for 2 seconds."))
		/// {
		///  	Thread.Sleep(2000);
		/// }
		/// </code>
		/// </example>
		public static IDisposable BeginTimedOperation(
            this ILogger logger,
            string description,
            string identifier = null,
            LogEventLevel level = LogEventLevel.Information,
			TimeSpan? warnIfExceeds = null,			
			LogEventLevel levelExceeds= LogEventLevel.Warning, 
			string beginningMessage = TimedOperation.BeginningOperationTemplate, string completedMessage = TimedOperation.CompletedOperationTemplate, string exceededOperationMessage = TimedOperation.OperationExceededTemplate)
        {
            object operationIdentifier = identifier;

            if (string.IsNullOrEmpty(identifier))
                operationIdentifier = Guid.NewGuid();

			return new TimedOperation(logger, level, warnIfExceeds, operationIdentifier, description, levelExceeds, beginningMessage, completedMessage, exceededOperationMessage);
        }

        /// <summary>
        /// Retrieves a value as defined by the operation. For example the number of items inside a queue.
        /// Call the Write() method to actually read the value and write to log.
        /// </summary>
        /// <example>
        /// Create a gauge to measure the number of items in a queue.
        /// <code>
        ///   var gauge = logger.GaugeOperation("queue", "item(s)", () => queue.Count());
        ///
        ///   gauge.Write();
        ///
        ///   queue.Enqueue(20);
        ///
        ///   gauge.Write();
        /// </code>
        /// </example>
        /// <param name="logger">The logger.</param>
        /// <param name="name">The name of the counter, for example 'Queue size'.</param>
        /// <param name="uom">The unit of measure, for example 'items'.</param>
        /// <param name="operation">The actual function to retrieve the value from.</param>
        /// <param name="level">The level used to write the timing operation details to the log. By default this is the information level.</param>
        /// <param name="template">A message template describing the format used to write to the log.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Returns a IGaugeMeasure on which you can call the Write() function to output to the log.</returns>
        public static IGaugeMeasure GaugeOperation<T>(
            this ILogger logger,
            string name,
             string uom,
            Func<T> operation,
            LogEventLevel level = LogEventLevel.Information,
            string template = DefaultGaugeTemplate)
        {

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            if (string.IsNullOrEmpty(uom))
                uom = "operations(s)";

            if (operation == null)
                throw new ArgumentNullException("operation");

            return new GaugeMeasure<T>(logger, name, uom, operation, level, template);
        }


        /// <summary>
        /// Creates a new counter which can be used to increment or decrement a long value. 
        /// </summary>
        /// <example>
        /// Create a new counter and increment the value.
        /// <code>
        ///    var counter = logger.CountOperation("counter", "operation(s)", true, LogEventLevel.Debug);
        ///    counter.Increment();
        /// </code>
        /// </example>
        /// <param name="logger">The logger.</param>
        /// <param name="name">The name of the counter, for example 'Page visits'.</param>
        /// <param name="uom">The unit of measure, for example 'hits'.</param>
        /// <param name="directWrite">Indicates if a change in the counter needs to be written to the log directly. By default enabled. When disabled, you need to explicitly call the Write() method to output the current value.</param>
        /// <param name="level">The level used to write the timing operation details to the log. By default this is the information level.</param>
        /// <param name="template">A message template describing the format used to write to the log.</param>
        /// <returns></returns>
        public static ICounterMeasure CountOperation(
           this ILogger logger,
           string name,
            string uom = "operation(s)",
            bool directWrite = true,
           LogEventLevel level = LogEventLevel.Information,
           string template = DefaultCountTemplate)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            return new CounterMeasure(logger, name, uom, level, template, directWrite);
        }

		/// <summary>
		/// Creates a new meter operations that measures the rate at which the operation occurs.
		/// </summary>
		/// <param name="logger">The logger</param>
		/// <param name="name">Name of the meter.</param>
		/// <param name="measuring">Specifies what it is measuring, like the number of requests</param>
		/// <param name="rateUnit">The rate unit</param>
		/// <param name="level">The loglevel to use when writing to the log.</param>
		/// <param name="template">The template to use.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static IMeterMeasure MeterOperation(
			this ILogger logger,
			string name,
			string measuring = "operation(s)",
			TimeUnit rateUnit = TimeUnit.Seconds,
			LogEventLevel level = LogEventLevel.Information,
			string template = DefaultMeterTemplate)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException("name");

			return new MeterMeasure(logger, name, measuring, rateUnit, level, template);
		}

		/// <summary>
		/// Creates a new health check. When the Write method is executed, the health function is run and the response is written to the logger. 
		/// </summary>
		/// <returns>The check.</returns>
		/// <param name="logger">Logger.</param>
		/// <param name="name">Name of the health check.</param>
		/// <param name="healthFunction">Health function to execute.</param>
		/// <param name="healthyLevel">Healthy level used when the check was succesful.</param>
		/// <param name="unHealthyLevel">Unhealthy level used when the check was unsuccessful.</param>
		/// <param name="template">Template to use to render the message to the log.</param>
		public static IHealthMeasure HealthCheck(
			this ILogger logger,
			string name,
			Func<HealthCheckResult> healthFunction,
			LogEventLevel healthyLevel = LogEventLevel.Information,
			LogEventLevel unHealthyLevel = LogEventLevel.Warning,
			string template = DefaultHealthTemplate)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException("name");

			return new HealthMeasure (logger, name, healthFunction, healthyLevel, unHealthyLevel, template);
		}

    }


}
