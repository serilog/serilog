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

using Serilog.Events;

namespace Serilog.Extras.Timing
{
    sealed class CounterMeasure : ICounterMeasure
    {
        readonly ILogger _logger;
        readonly string _name;
        readonly string _counts;
        readonly LogEventLevel _level;
        readonly string _template;
        readonly bool _directWrite;
        readonly AtomicLong _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="CounterMeasure"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="name">The name.</param>
        /// <param name="counts">The counts.</param>
        /// <param name="level">The level.</param>
        /// <param name="template">The template.</param>
        /// <param name="directWrite">if set to <c>true</c> then directly write to the log.</param>
        public CounterMeasure(ILogger logger, string name, string counts, LogEventLevel level, string template, bool directWrite = false)
        {
            _logger = logger;
            _name = name;
            _counts = counts;
            _level = level;
            _template = template;
            _directWrite = directWrite;
            _value = new AtomicLong();
        }


        public void Increment()
        {
            _value.Increment();

            if (_directWrite)
                Write();
        }

        public void Decrement()
        {
            _value.Decrement();

            if (_directWrite)
                Write();

        }

        public void Reset()
        {
            _value.Set(0);

            if (_directWrite)
                Write();
        }

        public void Write()
        {
            var value = _value.Get();
            _logger.Write(_level, _template, _name, value, _counts);
        }

		public long Value(){

			return _value.Get ();
		}
    }
}