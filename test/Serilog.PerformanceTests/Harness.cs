
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

using BenchmarkDotNet.Running;
using Xunit;

namespace Serilog.PerformanceTests
{
    public class Harness
    {
        [Fact]
        public void MessageTemplateCacheBenchmark()
        {
            BenchmarkRunner.Run<MessageTemplateCacheBenchmark_Cached>();
            BenchmarkRunner.Run<MessageTemplateCacheBenchmark_Leaking>();
        }

        [Fact]
        public void LogContextEnrichment()
        {
            BenchmarkRunner.Run<LogContextEnrichmentBenchmark>();
        }

        [Fact]
        public void MessageTemplateParsing()
        {
            BenchmarkRunner.Run<MessageTemplateParsingBenchmark>();
        }
        
        [Fact]
        public void LevelControl()
        {
            BenchmarkRunner.Run<LevelControlBenchmark>();
        }

        [Fact]
        public void NestedLoggerCreation()
        {
            BenchmarkRunner.Run<NestedLoggerCreationBenchmark>();
        }

        [Fact]
        public void NestedLoggerLatency()
        {
            BenchmarkRunner.Run<NestedLoggerLatencyBenchmark>();
        }

        [Fact]
        public void Pipeline()
        {
            BenchmarkRunner.Run<PipelineBenchmark>();
        }
    }
}