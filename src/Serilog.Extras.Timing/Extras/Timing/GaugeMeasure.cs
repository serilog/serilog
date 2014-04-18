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

namespace Serilog.Extras.Timing
{
    sealed class GaugeMeasure<T> : IGaugeMeasure
    {
        private readonly ILogger _logger;
        private readonly string _name;
        private readonly string _gauges;
        private readonly Func<T> _operation;
        private readonly LogEventLevel _level;
        private readonly string _template;


        public GaugeMeasure(ILogger logger, string name, string gauges, Func<T> operation, LogEventLevel level, string template)
        {
            _logger = logger;
            _name = name;
            _gauges = gauges;
            _operation = operation;
            _level = level;
            _template = template;
        }

        public void Write()
        {
            var value = _operation.Invoke();

            _logger.Write(_level, _template, _name, value, _gauges);
        }

    }
}