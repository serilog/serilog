
// Copyright 2013-2016 Serilog Contributors
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

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Serilog.Tests.Support;
using Serilog;
using Serilog.Events;
using Serilog.Parsing;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Serilog.PerformanceTests
{
    public class LogEventTests
    { 
        List<LogEventProperty> _properties;
        Exception _exception;
        MessageTemplate _emptyMessageTemplate; 
        
        [Setup]
        public void Setup()
        { 
            _exception = new Exception("An Error");
            _emptyMessageTemplate = new MessageTemplate(Enumerable.Empty<MessageTemplateToken>());
            _properties = new List<LogEventProperty>();

            var items = Enumerable.Range(0,1000);
            foreach (var item in items)
            { 
                var prop = new LogEventProperty(item.ToString(), new ScalarValue(item));
                _properties.Add(prop);
            } 
        }

        [Benchmark(Baseline = true)]
        public void Baseline()
        {
            var le = new LogEvent(DateTimeOffset.Now, 
                LogEventLevel.Information,
                _exception, 
                _emptyMessageTemplate,
                Enumerable.Empty<LogEventProperty>());
        }
        
        [Benchmark()]
        public void LogEvent1000Properties()
        {
            var le = new LogEvent(DateTimeOffset.Now, 
                LogEventLevel.Information,
                _exception, 
                _emptyMessageTemplate,
                _properties);
        }   
    }
}
  