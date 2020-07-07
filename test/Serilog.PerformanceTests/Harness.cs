// Copyright 2013-2017 Serilog Contributors
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
    /// <summary>
    /// Wrappers that make it easy to run benchmark suites through the <c>dotnet test</c> runner.
    /// </summary>
    /// <example>
    /// <code>dotnet test -c Release -f netcoreapp3.1 --filter "FullyQualifiedName=Serilog.PerformanceTests.Harness.Allocations"</code>
    /// </example>
    public class Harness
    {
        [Fact]
        public void Allocations()
        {
            BenchmarkRunner.Run<AllocationsBenchmark>();
        }

        [Fact]
        public void AllocationsIgnoringEvents()
        {
            BenchmarkRunner.Run<AllocationsIgnoringEventsBenchmark>();
        }

        [Fact]
        public void AlmostRealWorld()
        {
            BenchmarkRunner.Run<AlmostRealWorldBenchmark>();
        }

        [Fact]
        public void MessageTemplateCache()
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

        [Fact]
        public void OutputTemplateRendering()
        {
            BenchmarkRunner.Run<OutputTemplateRenderingBenchmark>();
        }
        
        [Fact]
        public void MessageTemplateRendering()
        {
            BenchmarkRunner.Run<MessageTemplateRenderingBenchmark>();
        }

        [Fact]
        public void Binding()
        {
            BenchmarkRunner.Run<BindingBenchmark>();
        }
    }
}
