
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
using System;
using Xunit;

namespace Serilog.PerformanceTests
{
    // TODO:

    // For Context

    // Perf logger off
    // Perf level off - Debug -
   
    // LogContext Push
    // ctor logevent -
    // message template parser perf
    // property binding perf (Bind message template)

    public class Runner
    {
        [Fact]
        public void ForContext()
        {
            var context = BenchmarkRunner.Run<ForContext>();
        }

        [Fact]
        public void MinimumLevel()
        {
            var context = BenchmarkRunner.Run<MinimumLevel>();
        }
        
        [Fact]
        public void FromLogContextPushProperty()
        {
            var context = BenchmarkRunner.Run<FromLogContextPushProperty>();
        }
    }
}