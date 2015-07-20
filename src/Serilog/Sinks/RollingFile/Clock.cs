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

namespace Serilog.Sinks.RollingFile
{
    static class Clock
    {
        static Func<DateTime> _dateTimeNow = () => DateTime.Now;

        [ThreadStatic]
        static DateTime _testDateTimeNow;

        public static DateTime DateTimeNow
        {
            get { return _dateTimeNow(); }
        }
        
        // Time is set per thread to support parallel 
        // If any thread uses the clock in test mode, all threads
        // must use it in test mode; once set to test mode only
        // terminating the application returns it to normal use.
        public static void SetTestDateTimeNow(DateTime now)
        {
            _testDateTimeNow = now;
            _dateTimeNow = () => _testDateTimeNow;
        }
    }
}
