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

namespace Serilog
{
    /// <summary>
    /// Extends <see cref="LoggerConfiguration"/> to add Full .NET Framework 
    /// capabilities.
    /// </summary>
    public static class LoggerExtensions
    {

        const string DefaultGaugeTemplate = "{GaugeName} value = {GaugeValue} {GaugeUnit:l}";
        const string DefaultCountTemplate = "{CounterName} count = {CounterValue} {CounterUnit:l}";

        /// <summary>
        /// Begins an operation by placing the code to be timed inside a using block. 
        /// When the block is being exited, the time it took is logged.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="identifier">The identifier used for the timing. If non specified, a random guid will be used.</param>
        /// <param name="description">A description for this operation.</param>
        /// <param name="level">The level used to write the timing operation details to the log. By default this is the information level.</param>
        /// <param name="warnIfExceeds">Specifies a limit, if it takes more than this limit, the level will be set to warning. By default this is not used.</param>
        /// <returns>A disposable object. Wrap this inside a using block so the dispose can be called to stop the timing.</returns>
        public static IDisposable BeginTimedOperation(
            this ILogger logger,
            string description,
            string identifier = null,
            LogEventLevel level = LogEventLevel.Information,
            TimeSpan? warnIfExceeds = null)
        {
            object operationIdentifier = identifier;
            if (string.IsNullOrEmpty(identifier))
                operationIdentifier = Guid.NewGuid();

            return new TimedOperation(logger, level, warnIfExceeds, operationIdentifier, description);
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


    }
}
