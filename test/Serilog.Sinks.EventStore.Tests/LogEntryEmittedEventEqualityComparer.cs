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
using System.Collections.Generic;

namespace Serilog.Sinks.EventStore.Tests
{
    /// <summary>
    /// Class to compare two <see cref="LogEntryEmittedEvent"/> instances.
    /// </summary>
    internal class LogEntryEmittedEventEqualityComparer: IEqualityComparer<LogEntryEmittedEvent>
    {
        public bool Equals(LogEntryEmittedEvent x, LogEntryEmittedEvent y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return CheckPropertyDictionariesAreEqual(x.Properties, y.Properties) && x.Timestamp.Equals(y.Timestamp) && x.Level.Equals(y.Level) && x.MessageTemplate.Equals(y.MessageTemplate) && x.RenderedMessage.Equals(y.RenderedMessage) && x.Exception.ToString().Equals(y.Exception.ToString());
        }

        public int GetHashCode(LogEntryEmittedEvent obj)
        {
            throw new NotImplementedException();
        }

        private bool CheckExceptionsAreEqual(Exception event1Exception, Exception event2Exception)
        {
            if (event1Exception == event2Exception)
            {
                return true;
            }

            return event1Exception.Data == event2Exception.Data && event1Exception.GetBaseException() == event2Exception.GetBaseException() && event1Exception.GetType().Equals(event1Exception.GetType()) && event1Exception.HResult.Equals(event2Exception.HResult) && event1Exception.HelpLink.Equals(event2Exception.HelpLink) && event1Exception.InnerException.Equals(event2Exception.InnerException) && event1Exception.Message.Equals(event2Exception.Message) && event1Exception.Source.Equals(event2Exception.Source) && event1Exception.StackTrace.Equals(event2Exception.StackTrace) && event1Exception.TargetSite.Equals(event2Exception.TargetSite);
        }
        private bool CheckPropertyDictionariesAreEqual(IDictionary<string, object> firstEventProperties, IDictionary<string, object> secondEventProperties)
        {
            if (firstEventProperties == secondEventProperties)
            {
                return true;
            }

            if ((firstEventProperties == null) || (secondEventProperties == null))
            {
                return false;
            }

            if (firstEventProperties.Count != secondEventProperties.Count)
            {
                return false;
            }

            var comparer = EqualityComparer<Object>.Default;

            foreach (KeyValuePair<string, object> kvp in firstEventProperties)
            {
                object secondValue;
                if (!secondEventProperties.TryGetValue(kvp.Key, out secondValue))
                {
                    return false;
                }
                if (!comparer.Equals(kvp.Value, secondValue))
                {
                    return false;
                }
            }
            return true;
        }
    }
}