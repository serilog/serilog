
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
using Serilog.Context;
using System;
using Xunit;

namespace Serilog.PerformanceTests
{ 
    public class FromLogContextPushProperty
    {
        private ILogger log;
        
        [Setup]
        public void Setup()
        {
            log = new LoggerConfiguration()
                .WriteTo.Sink(new DelegatingSink(e => { }))
                .Enrich.FromLogContext()
                .CreateLogger();
        }
        
        [Benchmark(Baseline = true)]
        public void Baseline()
        {
            for (var i = 0; i < 1000; ++i)
            {
                log.Information("Event!");
            }
        }  

        [Benchmark]
        public void Push1Property1000()
        { 
            for (var i = 0; i < 1000; ++i)
            {
                using (LogContext.PushProperty("A", 2))
                {
                    log.Information("Event!");                
                }
            }
        }      

        [Benchmark]
        public void Push1Property10000()
        { 
            for (var i = 0; i < 10000; ++i)
            {
                using (LogContext.PushProperty("A", 2))
                {
                    log.Information("Event!");                
                }
            }
        }       
        
        [Benchmark]
        public void Push2Properties1000()
        {
            for (var i = 0; i < 1000; ++i)
            {
                using (LogContext.PushProperty("A", 2))
                using (LogContext.PushProperty("B", 1))                
                {
                    log.Information("Event!");                
                }
            }
        }  

        [Benchmark]
        public void Push2Properties10000()
        {
            for (var i = 0; i < 10000; ++i)
            {
                using (LogContext.PushProperty("A", 2))
                using (LogContext.PushProperty("B", 1))                
                {
                    log.Information("Event!");                
                }
            }
        }  
    }
}
  