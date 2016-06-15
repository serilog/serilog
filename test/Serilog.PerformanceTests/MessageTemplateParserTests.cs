
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
    public class MessageTemplateParserTests
    {  
        MessageTemplateParser _parser; 
        const string DefaultConsoleOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}";
        
        [Setup]
        public void Setup()
        { 
            _parser = new MessageTemplateParser();
        }

        [Benchmark(Baseline = true)]
        public void Baseline()
        {
            var template = _parser.Parse("");
        }  

        [Benchmark]
        public void ParseDefaultConsoleOutputTemplate()
        {
            var template = _parser.Parse(DefaultConsoleOutputTemplate);
        }  
    }
}
  