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
using System.Diagnostics;
using System.Globalization;
using Serilog.Core.Pipeline;
using Serilog.Events;

namespace Serilog.Extras.TraceListener
{
    /// <summary>
    ///     TraceListener implementation that directs all output to Serilog.
    /// </summary>
    public class SerilogTraceListener : System.Diagnostics.TraceListener
    {
        const LogEventLevel FailLevel = LogEventLevel.Fatal;
        const LogEventLevel DefaultLogLevel = LogEventLevel.Debug;
        const string MessagelessTraceEventMessageTemplate = "{TraceSource:l} {TraceEventType}: {TraceEventId}";
        const string MesageMessageTemplate = "{TraceMessage:l}";
        const string MessageWithCategoryMessageTemplate = "{Category:l}: {TraceMessage:l}";
        const string FailMessageTemplate = "Fail: {TraceMessage:l}";
        const string DetailedFailMessageTemplate = "Fail: {TraceMessage:l} {FailDetails:l}";
        static readonly string TraceDataMessageTemplate = "{TraceSource:l} {TraceEventType}: {TraceEventId} :" + Environment.NewLine + "{TraceData:l}";
        static readonly string TraceEventMessageTemplate = "{TraceSource:l} {TraceEventType}: {TraceEventId} :" + Environment.NewLine + "{TraceMessage:l}";
        static readonly string TraceTransferMessageTemplate = "{TraceSource:l} {TraceEventType}: {TraceEventId} :" + Environment.NewLine + "{TraceMessage:l}, relatedActivityId={RelatedActivityId}";
        ILogger logger;

        /// <summary>
        ///     Creates a SerilogTraceListener that uses the logger from `Serilog.Log`
        /// </summary>
        /// <remarks>
        ///     This is needed because TraceListeners are often configured through XML
        ///     where there would be no opportunity for constructor injection
        /// </remarks>
        public SerilogTraceListener() : this(Log.Logger)
        {
        }

        /// <summary>
        ///     Creates a SerilogTraceListener that uses the specified logger
        /// </summary>
        public SerilogTraceListener(ILogger logger)
        {
            this.logger = logger.ForContext<SerilogTraceListener>();
        }

        public override bool IsThreadSafe
        {
            get { return true; }
        }

        public override void Write(string message)
        {
            logger.Write(DefaultLogLevel, MesageMessageTemplate, message);
        }

        public override void Write(string message, string category)
        {
            logger.Write(DefaultLogLevel, MessageWithCategoryMessageTemplate, category, message);
        }

        public override void WriteLine(string message)
        {
            Write(message);
        }

        public override void WriteLine(string message, string category)
        {
            Write(message, category);
        }

        public override void Fail(string message)
        {
            logger.Write(FailLevel, FailMessageTemplate, message);
        }

        public override void Fail(string message, string detailMessage)
        {
            logger.Write(FailLevel, DetailedFailMessageTemplate, message, detailMessage);
        }

        public override void Close()
        {
            logger = new SilentLogger();
            base.Close();
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            WriteEvent(eventCache, eventType, TraceDataMessageTemplate, source, eventType, id, data);
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            WriteEvent(eventCache, eventType, TraceDataMessageTemplate, source, eventType, id, data);
        }

        public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
        {
            WriteEvent(eventCache, TraceEventType.Transfer, TraceTransferMessageTemplate, source, TraceEventType.Transfer, id, message, relatedActivityId);
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
        {
            WriteEvent(eventCache, eventType, MessagelessTraceEventMessageTemplate, source, eventType, id);
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            WriteEvent(eventCache, eventType, TraceEventMessageTemplate, source, eventType, id, message);
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            TraceEvent(eventCache, source, eventType, id, string.Format(CultureInfo.InvariantCulture, format, args));
        }

        void WriteEvent(TraceEventCache eventCache, TraceEventType eventType, string messageTemplate, params object[] propertyValues)
        {
            LogEventLevel level = ToLogEventLevel(eventType);

            logger.Write(level, messageTemplate, propertyValues);
        }

        internal static LogEventLevel ToLogEventLevel(TraceEventType eventType)
        {
            switch (eventType)
            {
                case TraceEventType.Critical:
                {
                    return LogEventLevel.Fatal;
                }
                case TraceEventType.Error:
                {
                    return LogEventLevel.Error;
                }
                case TraceEventType.Information:
                {
                    return LogEventLevel.Information;
                }
                case TraceEventType.Warning:
                {
                    return LogEventLevel.Warning;
                }
                case TraceEventType.Verbose:
                {
                    return LogEventLevel.Verbose;
                }
                default:
                {
                    return LogEventLevel.Debug;
                }
            }
        }
    }
}