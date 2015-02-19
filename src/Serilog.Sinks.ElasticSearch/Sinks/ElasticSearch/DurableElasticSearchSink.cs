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
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.RollingFile;

namespace Serilog.Sinks.ElasticSearch
{
    class DurableElasticSearchSink : ILogEventSink, IDisposable
    {
        // we rely on the date in the filename later!
        const string FileNameSuffix = "-{Date}.json";

        readonly RollingFileSink _sink;
        readonly ElasticSearchLogShipper _shipper;

        public DurableElasticSearchSink(ElasticsearchSinkOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");

            if (string.IsNullOrWhiteSpace(options.BufferBaseFilename))
            {
                throw new ArgumentException("Cannot create the durable ElasticSearch sink without a buffer base file name!");
            }

            var formatter = options.CustomFormatter ?? new ElasticsearchJsonFormatter(
                formatProvider: options.FormatProvider,
                renderMessage: true,
                closingDelimiter: Environment.NewLine,
                serializer: options.Serializer,
                inlineFields: options.InlineFields
                );

            _sink = new RollingFileSink(
                options.BufferBaseFilename + FileNameSuffix,
                formatter,
                options.BufferFileSizeLimitBytes,
                null);

            _shipper = new ElasticSearchLogShipper(options);
        }

        public void Emit(LogEvent logEvent)
        {
            _sink.Emit(logEvent);
        }

        public void Dispose()
        {
            _sink.Dispose();
            _shipper.Dispose();
        }
    }
}