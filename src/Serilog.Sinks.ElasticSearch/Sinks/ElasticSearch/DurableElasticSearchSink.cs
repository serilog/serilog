using System;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.RollingFile;

namespace Serilog.Sinks.ElasticSearch
{
    class DurableElasticSearchSink : ILogEventSink, IDisposable
    {
        // we rely on the date in the filename later!
        public const string FileNameSuffix = "-{Date}.json";

        readonly RollingFileSink _sink;
        readonly ElasticSearchLogShipper _shipper;

        public DurableElasticSearchSink(ElasticsearchSinkOptions options)
        {
            if (options == null) throw new ArgumentNullException("options");

            if (string.IsNullOrWhiteSpace(options.BufferBaseFilename))
            {
                throw new ArgumentException("Cannot create the durable ElasticSearch sink without a buffer base file name!");
            }

            var formatter = new ElasticsearchJsonFormatter(formatProvider: options.FormatProvider,
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