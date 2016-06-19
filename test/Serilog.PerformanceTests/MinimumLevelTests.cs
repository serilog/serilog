
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
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Tests.Support;
using System;
using Xunit;

namespace Serilog.PerformanceTests
{
    public class MinimumLevelTests
    {
        ILogger log;
        LoggingLevelSwitch levelSwitch;

        [Setup]
        public void Setup()
        {
            levelSwitch = new LoggingLevelSwitch();
            levelSwitch.MinimumLevel = LogEventLevel.Verbose; 

            log = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.Sink(new DelegatingSink(e => { }))
                .CreateLogger();
        }
        
        [Benchmark(Baseline = true)]
        public void Baseline()
        {
            //By default this is info, set to verbose for baseline
            for (var i = 0; i < 1000; ++i)
            {
                log.Verbose("A verbose event!");
                log.Debug("A debug event!");
                log.Information("An info event!");
                log.Warning("A warm event!");
                log.Error("An error event!");
                log.Fatal("A fatal event!");
            }
        }  
        
        [Benchmark]
        public void MinimumVerbose()
        {
            levelSwitch.MinimumLevel = LogEventLevel.Verbose;

            for (var i = 0; i < 1000; ++i)
            {
                log.Verbose("A verbose event!");
                log.Debug("A debug event!");
                log.Information("An info event!");
                log.Warning("A warm event!");
                log.Error("An error event!");
                log.Fatal("A fatal event!");
            }
        } 
                
        [Benchmark]
        public void MinimumDebug()
        {
            levelSwitch.MinimumLevel = LogEventLevel.Debug;

            for (var i = 0; i < 1000; ++i)
            {
                log.Verbose("A verbose event!");
                log.Debug("A debug event!");
                log.Information("An info event!");
                log.Warning("A warm event!");
                log.Error("An error event!");
                log.Fatal("A fatal event!");
            }
        } 

        [Benchmark]
        public void MinimumInfo()
        {
            levelSwitch.MinimumLevel = LogEventLevel.Information;
            for (var i = 0; i < 1000; ++i)
            {
                log.Verbose("A verbose event!");
                log.Debug("A debug event!");
                log.Information("An info event!");
                log.Warning("A warm event!");
                log.Error("An error event!");
                log.Fatal("A fatal event!");
            }
        } 

        [Benchmark]
        public void MinimumWarn()
        {
            levelSwitch.MinimumLevel = LogEventLevel.Warning;
            for (var i = 0; i < 1000; ++i)
            {
                log.Verbose("A verbose event!");
                log.Debug("A debug event!");
                log.Information("An info event!");
                log.Warning("A warm event!");
                log.Error("An error event!");
                log.Fatal("A fatal event!");
            }
        } 

        [Benchmark]
        public void MinimumError()
        {
            levelSwitch.MinimumLevel = LogEventLevel.Error;
            for (var i = 0; i < 1000; ++i)
            {
                log.Verbose("A verbose event!");
                log.Debug("A debug event!");
                log.Information("An info event!");
                log.Warning("A warm event!");
                log.Error("An error event!");
                log.Fatal("A fatal event!");
            }
        }        
        
        [Benchmark]
        public void MinimumFatal()
        {
            levelSwitch.MinimumLevel = LogEventLevel.Fatal;
            for (var i = 0; i < 1000; ++i)
            {
                log.Verbose("A verbose event!");
                log.Debug("A debug event!");
                log.Information("An info event!");
                log.Warning("A warm event!");
                log.Error("An error event!");
                log.Fatal("A fatal event!");
            }
        }
    }
}
  